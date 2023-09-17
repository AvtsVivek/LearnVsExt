using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;

using Task = System.Threading.Tasks.Task;

namespace ComponentModelIntro.Commands
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
        public static readonly Guid CommandSet = new Guid("ed6127ca-3299-4709-8de2-e58fa3955502");

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
        private IAsyncServiceProvider ServiceProvider
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

            var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            if (componentModel == null)
                return;

            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            // VsTextBufferAdapter vsTextBufferOne = vsEditorAdaptersFactoryService.CreateVsTextBufferAdapter(this.package);
            IVsTextBuffer vsTextBufferOne = vsEditorAdaptersFactoryService.CreateVsTextBufferAdapter(this.package);

            ITextBuffer documentTextBufferOne = vsEditorAdaptersFactoryService.GetDocumentBuffer(vsTextBufferOne);

            var stringMessage = $"The {nameof(documentTextBufferOne)} retrieved from a 'Created' Text buffer is null. " + Environment.NewLine +
                "There is no data buffered in this yet." + Environment.NewLine + 
                "Note we used vsEditorAdaptersFactoryService.CreateVsTextBufferAdapter(this.package) " + Environment.NewLine +
                $"to create {nameof(vsTextBufferOne)}.";

            if (documentTextBufferOne == null)
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    stringMessage,
                    $"{nameof(documentTextBufferOne)} is null!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            VsTextBuffer vsTextBufferTwo = vsTextBufferOne as VsTextBuffer;

            ITextBuffer documentTextBufferTwo = vsEditorAdaptersFactoryService.GetDocumentBuffer(vsTextBufferTwo);

            stringMessage = $"The {nameof(documentTextBufferTwo)} retrieved from a 'Created' Text buffer is null. " + Environment.NewLine +
                "There is no data buffered in this yet." + Environment.NewLine +
                "Note we used vsEditorAdaptersFactoryService.CreateVsTextBufferAdapter(this.package) " + Environment.NewLine +
                $"to create {nameof(vsTextBufferTwo)}.";

            if (documentTextBufferTwo == null)
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    stringMessage,
                    $"{nameof(documentTextBufferTwo)} is null!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

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

            // VsTextBufferAdapter
            vsTextView.GetBuffer(out IVsTextLines currentDocTextLines); //Getting Current Text Lines 
            
            var vsTextBufferThree = currentDocTextLines as IVsTextBuffer;

            ITextBuffer documentTextBufferThree = vsEditorAdaptersFactoryService.GetDocumentBuffer(vsTextBufferThree);

            if (documentTextBufferThree != null)
            {
                stringMessage = $"The {nameof(documentTextBufferThree)} retrieved from a text buffer got from 'vsTextView.GetBuffer' is NOT null. " + Environment.NewLine +

                "Note we used the current text view vsTextView.GetBuffer() " + Environment.NewLine +
                $"to create {nameof(vsTextBufferThree)}.";

                VsShellUtilities.ShowMessageBox(
                    this.package,
                    stringMessage,
                    $"{nameof(documentTextBufferThree)} is NOT null!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }


            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "TestCommand";

            // Now we can use the adapter service to get the Wpf Text View. vsEditorAdaptersFactoryService
            var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            var wpfTextViewHost = vsEditorAdaptersFactoryService.GetWpfTextViewHost(vsTextView);

            if( wpfTextView != null )
                message = message + Environment.NewLine + "IWpfTextView is created using IVsEditorAdaptersFactoryService.";

            if (wpfTextViewHost != null)
                message = message + Environment.NewLine + "IWpfTextViewHost is created using IVsEditorAdaptersFactoryService.";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);


        }

        private static T GetGlobalService<T>(Type serviceType)
        {
            return (T)Package.GetGlobalService(serviceType);
        }
    }
}
