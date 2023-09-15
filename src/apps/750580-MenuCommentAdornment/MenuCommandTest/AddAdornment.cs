using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Editor;
using CommentAdornmentTest;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Text;
using System.Windows.Forms;
//using Microsoft.VisualStudio.ComponentModelHost;
// https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-using-a-shell-command-with-an-editor-extension

namespace MenuCommandTest
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AddAdornment
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("8ced5348-3b98-408d-b45c-610bf0b5a474");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddAdornment"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private AddAdornment(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.AddAdornmentHandler, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AddAdornment Instance
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
            // Switch to the main thread - the call to AddCommand in AddAdornment's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AddAdornment(package, commandService);
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
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "AddAdornment";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private async void AddAdornmentHandler(object sender, EventArgs e)
        {
            IVsTextManager vsTextManager = (IVsTextManager)await ServiceProvider.GetServiceAsync(typeof(SVsTextManager));
            IVsTextView vsTextView = null;
            int mustHaveFocus = 1;
            vsTextManager.GetActiveView(mustHaveFocus, null, out vsTextView);
            IVsUserData vsUserData = vsTextView as IVsUserData;
            if (vsUserData == null)
            {
                Console.WriteLine("No text view is currently open");
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "No text view is currently open. Probably no text file is open. Open any text file and try again.",
                    "No text view!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            
            // IVsTextBufferAdapter curDocTextLiness = null;
            IVsTextLines curDocTextLines = null;
            // VsTextBufferAdapter
            vsTextView.GetBuffer(out curDocTextLines); //Getting Current Text Lines 
            var vsTextBuffer = curDocTextLines as IVsTextBuffer;

            var componentModel = (IComponentModel)await this.ServiceProvider.GetServiceAsync(typeof(SComponentModel));
            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();
            ITextBuffer currentDocTextBuffer = vsEditorAdaptersFactoryService.GetDocumentBuffer(curDocTextLines);
            
            // Microsoft.VisualStudio.Text.Editor.ITextView2
            // var filePath = ((Microsoft.VisualStudio.Text.Implementation.TextDocument)((Microsoft.VisualStudio.Editor.Implementation.TextDocData)vsTextBuffer).TextDocument).FilePath;

            IWpfTextViewHost wpfTextViewHost;
            object holder;
            Guid guidViewHost = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;
            vsUserData.GetData(ref guidViewHost, out holder);
            wpfTextViewHost = (IWpfTextViewHost)holder;
            Connector.Execute(wpfTextViewHost);
        }
    }
}
