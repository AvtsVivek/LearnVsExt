using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace ContentTypeRegistryServiceIntro
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class DisplayContentTypesCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("032ec808-c390-4348-857f-8cc2491a8071");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        // The following is giving null, not sure why.
        [Import]
        internal IContentTypeRegistryService ContentTypeRegistryService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayContentTypesCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private DisplayContentTypesCommand(AsyncPackage package, OleMenuCommandService commandService)
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
        public static DisplayContentTypesCommand Instance
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
            // Switch to the main thread - the call to AddCommand in DisplayContentTypesCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DisplayContentTypesCommand(package, commandService);
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

            var message = $"A total of {contentTypeList.Count} content Types are found." + Environment.NewLine;
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

            var title = $"Display Content Types: Total Count - {contentTypeList.Count}";

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
