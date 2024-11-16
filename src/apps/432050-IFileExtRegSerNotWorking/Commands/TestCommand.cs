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
using Task = System.Threading.Tasks.Task;

namespace IFileExtRegSerNotWorking.Commands
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
        public static readonly Guid CommandSet = new Guid("3ee3701b-1837-4fcd-bf9c-228c7a9f641c");

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
            var message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            message += " Here we go.. ";
            var title = "Test Command ";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

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

            var extensionString = "cs";

            var contentTypeForGivenExtension = fileExtensionRegistryService.GetContentTypeForExtension(extension: extensionString);

            if (contentTypeForGivenExtension != null)
            {
                VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: $"contentType for extension '{extensionString}' is {contentTypeForGivenExtension.DisplayName}",
                title: "contentType for extension",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            extensionString = "txt";

            contentTypeForGivenExtension = fileExtensionRegistryService.GetContentTypeForExtension(extension: extensionString);

            if (contentTypeForGivenExtension != null)
            {
                VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: $"contentType for extension '{extensionString}' is {contentTypeForGivenExtension.DisplayName}",
                title: "contentType for extension",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            var contentTypeRegistryServiceLocal = componentModel.GetService<IContentTypeRegistryService>();

            // I get a total of 95 content types here.
            var contentTypeList = contentTypeRegistryServiceLocal.ContentTypes.
                OrderBy(keySelector: contentType => contentType.TypeName).ToList();

            // I get the text Content Type correctly here. 
            var textContentType = contentTypeList.
                Where(predicate: contentType => contentType.DisplayName.Equals("text")).First();

            // I get the extensionList count as 0 here for text content type. Not sure why
            var extensionListForTextContentType = fileExtensionRegistryService.
                GetExtensionsForContentType(contentType: textContentType).ToList();

            // I get the CSharp Content Type correctly here. 
            var cSharpContentType = contentTypeList.
                Where(predicate: contentType => contentType.DisplayName.Equals("CSharp")).First();

            // I get the extensionList count as 0 here for CSharp content type. Not sure why
            var extensionListForCSharpContentType = fileExtensionRegistryService.
                GetExtensionsForContentType(contentType: cSharpContentType).ToList();

        }
    }
}
