using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Web;
using Task = System.Threading.Tasks.Task;

namespace WebSearchMultipleOptions.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class WebSearchCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("efb8b346-9d9f-446f-aac4-f8b3962f82e5");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

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
        /// Initializes a new instance of the <see cref="WebSearchCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private WebSearchCommand(AsyncPackage package, OleMenuCommandService commandService)
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
        public static WebSearchCommand Instance
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
            // Switch to the main thread - the call to AddCommand in WebSearchCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OutputWindow = await package.GetServiceAsync(typeof(SVsGeneralOutputWindowPane)) as IVsOutputWindowPane;
            Assumes.Present(OutputWindow);
            DteInstance = await package.GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(DteInstance);
            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new WebSearchCommand(package, commandService);
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
            var options = this.package.GetDialogPage(typeof(ExternalSearchOptionPage)) as ExternalSearchOptionPage;
            TextSelection textSelection = DteInstance?.ActiveDocument?.Selection as TextSelection;
            if (textSelection == null)
            {
                DteInstance.StatusBar.Text = "The selection is null or empty";
                return;
            }

            string textToBeSearched = textSelection?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(textToBeSearched))
            {
                string encodedText = HttpUtility.UrlEncode(textToBeSearched);
                DteInstance.StatusBar.Text = $"Searching {textToBeSearched}";
                OutputWindow.OutputStringThreadSafe($"Searching {textToBeSearched}");
                string url = string.Format(options.Url, encodedText);
                if (options.UseVSBrowser)
                {
                    DteInstance.ItemOperations.Navigate(url, vsNavigateOptions.vsNavigateOptionsDefault);
                }
                else
                {
                    System.Diagnostics.Process.Start(url);
                }
            }
            else
            {
                DteInstance.StatusBar.Text = "The selection is null or empty";
            }
        }
    }
}
