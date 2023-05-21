using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace OleMenuCommandIntro.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class EnableDisableCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("804ac15b-135e-4eac-bfa4-a1ff18558d2c");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableDisableCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private EnableDisableCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            menuItem.BeforeQueryStatus += OnBeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private async void OnBeforeQueryStatus(object sender, EventArgs e)
        {
            // await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var myCommand = sender as OleMenuCommand;
            if (null != myCommand)
            {
                myCommand.Text = "New Text";
            }

            var dte = (DTE2)ServiceProvider.GetServiceAsync(typeof(DTE)).Result;
            // var dte = await ServiceProvider.GetServiceAsync(typeof(DTE)) as DTE2;
            Assumes.Present(dte);

            var activeDocument = dte.ActiveDocument;

            if (activeDocument != null)
            {
                string fileExtension = Path.GetExtension(activeDocument.Name).ToLowerInvariant();
                string[] supportedFiles = new[] { ".cs" }; // You can have something like this as well. { ".cs", ".vb" };

                // Enable it only if it is a cs file.
                myCommand.Visible = true;
                myCommand.Enabled = supportedFiles.Contains(fileExtension);
                if (supportedFiles.Contains(fileExtension))
                    myCommand.Text = $"Add new class to the file {activeDocument.Name}";
                else
                    myCommand.Text = "Not Relevant";
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static EnableDisableCommand Instance
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
            // Switch to the main thread - the call to AddCommand in EnableDisableCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new EnableDisableCommand(package, commandService);
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
            string title = "EnableDisableCommand";

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
