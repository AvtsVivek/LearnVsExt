using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Windows.Controls;
using Task = System.Threading.Tasks.Task;

namespace WpfTextViewHostControl
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
        public static readonly Guid CommandSet = new Guid("3c645f98-f4d9-4010-bf96-9fa97911af4c");

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

            // Guid used to get an IWpfTextViewHost from an IWpfTextView
            var viewHostGuid = DefGuidList.guidIWpfTextViewHost;

            var vsTextManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            if (vsTextView == null)
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    "No text view is currently open. Probably no text file is open. Open any text file and try again.",
                    "No text view!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                return;
            }

            // What is this userData?
            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivsuserdata
            var vsUserData = vsTextView as IVsUserData;

            vsUserData.GetData(ref viewHostGuid, out object holder);

            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.editor.iwpftextviewhost
            IWpfTextViewHost wpfTextViewHost = (IWpfTextViewHost)holder;

            var wpfTextViewHostType = wpfTextViewHost.GetType();

            var hostControlType = wpfTextViewHost.HostControl.GetType();

            Control hostControl = wpfTextViewHost.HostControl;

            VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: $"{hostControl.GetType().FullName}",
                title: $"{nameof(hostControl)} Type",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: $"{wpfTextViewHost.GetType().FullName}",
                title: $"{nameof(wpfTextViewHost)} Type",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
