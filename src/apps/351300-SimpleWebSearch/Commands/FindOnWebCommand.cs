using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Task = System.Threading.Tasks.Task;

namespace SimpleWebSearch.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class FindOnWebCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("da587d54-32dc-4e4f-b762-2cd5abe48811");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindOnWebCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private FindOnWebCommand(AsyncPackage package, OleMenuCommandService commandService)
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
        public static FindOnWebCommand Instance
        {
            get;
            private set;
        }

        public static IVsOutputWindowPane OutputWindow
        {
            get;
            private set;
        }

        public static DTE2 DteInstance
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
            // Switch to the main thread - the call to AddCommand in FindOnWebCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OutputWindow = await package.GetServiceAsync(typeof(SVsGeneralOutputWindowPane)) as IVsOutputWindowPane;
                
            Assumes.Present(OutputWindow);

            DteInstance = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(DteInstance);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new FindOnWebCommand(package, commandService);
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

            var activeDocument = DteInstance?.ActiveDocument;

            var textSelection = activeDocument?.Selection as TextSelection;

            if (textSelection == null)
            {
                DteInstance.StatusBar.Text = "No Text selected";
                return;
            }

            var textToBeSearched = textSelection?.Text.Trim();

            if (string.IsNullOrWhiteSpace(textToBeSearched))
            {
                DteInstance.StatusBar.Text = "No Text selected";
                return;
            }

            DteInstance.StatusBar.Text = string.Empty;

            var searchTextMessage = $"Searching text: {textToBeSearched}";

            DteInstance.StatusBar.Text = searchTextMessage;

            OutputWindow.OutputStringThreadSafe(searchTextMessage);

            var url = $"https://www.bing.com/search?q={textToBeSearched}";

            var encodedText = HttpUtility.UrlEncode(textToBeSearched);

            var encodedUrl = string.Format(url, encodedText);

            System.Diagnostics.Process.Start(encodedUrl);
        }
    }
}
