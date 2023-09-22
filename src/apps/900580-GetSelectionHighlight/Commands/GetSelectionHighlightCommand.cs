using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace GetSelectionHighlight.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class GetSelectionHighlightCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("4bd1cefd-c173-4c28-b509-4f83e0d36759");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSelectionHighlightCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private GetSelectionHighlightCommand(AsyncPackage package, OleMenuCommandService commandService)
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
        public static GetSelectionHighlightCommand Instance
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
            // Switch to the main thread - the call to AddCommand in GetSelectionHighlightCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new GetSelectionHighlightCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private async void Execute(object sender, EventArgs e)
        {
            TextViewSelection? selection = await GetSelectionAsync();

            if (selection == null)
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "Probably no file is open. Open any file and try again",
                    "Selection is null",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                return;
            }

            var activeDocumentPath = await GetActiveDocumentFilePathAsync();

            if (string.IsNullOrWhiteSpace(activeDocumentPath))
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "Probably no file is open. Open any file and try again",
                    "Active Document path is null",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            ShowAddDocumentationWindow(activeDocumentPath, selection);

        }
        private void ShowAddDocumentationWindow(string activeDocumentPath, TextViewSelection? selection)
        {
            var documentationControl = new AddDocumentationWindow(activeDocumentPath, selection);
            documentationControl.ShowDialog();
        }

        private async Task<TextViewSelection?> GetSelectionAsync()
        {
            var vsTextManager = await ServiceProvider.GetServiceAsync(typeof(SVsTextManager));
            var textManager = vsTextManager as IVsTextManager2;

            int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out IVsTextView vsTextView);

            if (vsTextView == null)
                return null;

            vsTextView.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn);//end could be before beginning
            var startTextViewPosition = new TextViewPosition(startLine, startColumn);
            var endTextViewPosition = new TextViewPosition(endLine, endColumn);

            vsTextView.GetSelectedText(out string selectedText);

            var textViewSelection = new TextViewSelection(startTextViewPosition, endTextViewPosition, selectedText);
            return textViewSelection;
        }

        private async Task<string> GetActiveDocumentFilePathAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            EnvDTE80.DTE2 applicationObject = await ServiceProvider.GetServiceAsync(typeof(DTE)) as EnvDTE80.DTE2;

            if (applicationObject == null) return null;

            if (applicationObject.ActiveDocument == null) return null;

            return applicationObject.ActiveDocument.FullName;
        }
    }
}
