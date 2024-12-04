using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace FileExtToContentTypeDefIntro
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class TestCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("06b534bb-70a4-4f0c-89d0-89f0e81f06bb");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private TestCommand(AsyncPackage package, OleMenuCommandService commandService)
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
        public static TestCommand Instance
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
            // Switch to the main thread - the call to AddCommand in TestCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new TestCommand(package, commandService);
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

            var contentTypeRegistryService = componentModel.GetService<IContentTypeRegistryService>();

            var contentTypeList = contentTypeRegistryService.ContentTypes.
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

            var fooAbcdContentType = contentTypeList.Where(contentType => contentType.DisplayName == FooAbcdContentDefinition.ContentTypeName).FirstOrDefault();

            if (fooAbcdContentType != null)
            {
                message = $"The {FooAbcdContentDefinition.ContentTypeName} is registered." + Environment.NewLine;
                message += $"The total count now is {contentTypeList.Count}";
            }
            else
            {
                message = $"The {FooAbcdContentDefinition.ContentTypeName} is NOT registered." + Environment.NewLine;
                message += $"The total count now is {contentTypeList.Count}";
            }

            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            var fileExtensionRegistryService = componentModel.GetService<IFileExtensionRegistryService>();

            if (fileExtensionRegistryService == null)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                "IFileExtensionRegistryService Model is null",
                "IFileExtensionRegistryService is null",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            var extensionListForFooAbcdContentType = fileExtensionRegistryService.
                GetExtensionsForContentType(contentType: fooAbcdContentType).ToList();

            if (extensionListForFooAbcdContentType.Count == 0)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                $"No extensions found for {fooAbcdContentType.TypeName}",
                "extensionListForFooAbcdContentType count is zero",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
            else if (extensionListForFooAbcdContentType.Count == 1)
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                $"Extensions found for {fooAbcdContentType.TypeName}, the ext is {extensionListForFooAbcdContentType[0]}",
                "extensionListForFooAbcdContentType count is one",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
            else
            {
                VsShellUtilities.ShowMessageBox(
                this.package,
                $"More than one Extensions found for {fooAbcdContentType.TypeName}",
                "extensionListForFooAbcdContentType count is more than one",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }
        }
    }
}
