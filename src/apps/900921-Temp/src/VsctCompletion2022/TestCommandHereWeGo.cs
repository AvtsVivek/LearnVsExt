using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Task = System.Threading.Tasks.Task;

namespace VsctCompletion
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class TestCommandHereWeGo
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("a984ead0-fa2a-4a28-b39d-265a810ad768");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommandHereWeGo"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private TestCommandHereWeGo(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static TestCommandHereWeGo Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in TestCommandHereWeGo's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new TestCommandHereWeGo(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            //string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            //string title = "TestCommandHereWeGo 123456..";

            //// Show a message box to prove we were here
            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    message,
            //    title,
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            string message = string.Empty;
            string title = string.Empty;

            var componentModel = (IComponentModel)Package.GetGlobalService(serviceType: typeof(SComponentModel));

            if (componentModel == null)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                "Component Model is null",
                "Component Model is null",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            var contentTypeRegistryServiceLocal = componentModel.GetService<IContentTypeRegistryService>();

            var contentTypeList = contentTypeRegistryServiceLocal.ContentTypes.
                OrderBy(contentType => contentType.TypeName).ToList();

            message = $"A total of {contentTypeList.Count} content Types are found." + Environment.NewLine;
            message += $"Here they follow" + Environment.NewLine;
            message += "-------------------------------------------" + Environment.NewLine;
            message += "";

            var i = 0;

            foreach (var contentType in contentTypeList)
            {
                i++;
                if (i == 1)
                {
                    message += contentType.TypeName;
                    continue;
                }

                if (i % 4 == 0)
                {
                    message += ", " + Environment.NewLine + contentType.TypeName;
                }
                else
                {
                    message += ", " + contentType.TypeName;
                }
            }

            title = $"Display Content Types: Total Count - {contentTypeList.Count}";

            // Just copy to clip board, in case you want to paste it to notepad and study
            Clipboard.SetText(message);

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

        }
    }
}
