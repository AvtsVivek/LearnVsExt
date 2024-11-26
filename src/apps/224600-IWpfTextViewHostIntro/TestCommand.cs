using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace IWpfTextViewHostIntro
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
        public static readonly Guid CommandSet = new Guid("540aa6b4-1981-4e6a-bb0b-8f18f7d95ced");

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

            ITextBuffer textBuffer = wpfTextViewHost.TextView.TextBuffer;

            if (textBuffer == null)
            {
                VsShellUtilities.ShowMessageBox(
                    serviceProvider: this.package,
                    message: "The text buffer is empty.",
                    title: "No Text buffer!!",
                    icon: OLEMSGICON.OLEMSGICON_INFO,
                    msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
                return;
            }

            IWpfTextView wpfTextView = wpfTextViewHost.TextView;

            var propertiesList = wpfTextView.TextBuffer.Properties.PropertyList;

            VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: $"There are {propertiesList.Count} properties on ITextBuffer object.",
                title: $"Property count",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            var propCount = 1;
            var allProText = string.Empty;

            foreach (var property in propertiesList)
            {
                var message = $"Property {propCount} of {propertiesList.Count}" + Environment.NewLine
                    + $"Property Value type : {property.Value.GetType()}" + Environment.NewLine
                    + $"Property Value value: {property.Value}";
                
                VsShellUtilities.ShowMessageBox(
                    serviceProvider: this.package,
                    message: message,
                    title: $"Property Key: {property.Key}",
                    icon: OLEMSGICON.OLEMSGICON_INFO,
                    msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                propCount++;
                allProText += message + Environment.NewLine;
            }

            Clipboard.SetText(allProText);

            VsShellUtilities.ShowMessageBox(
                serviceProvider: this.package,
                message: "All props are Copied to clip board",
                title: "Copyed to clip board",
                icon: OLEMSGICON.OLEMSGICON_INFO,
                msgButton: OLEMSGBUTTON.OLEMSGBUTTON_OK,
                defaultButton: OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            // var asdfasdf = currentDocTextLines.GetType().GetInterfaces();

            // var vsTextLinesInterfaceList = typeof(IVsTextLines).GetInterfaces();

            // var vsTextBufferInterfaceList = typeof(IVsTextBuffer).GetInterfaces();

            // wpfTextViewHost.TextView.TextBuffer.Properties.TryGetProperty(typeof(IVsTextBuffer), out IVsTextBuffer bufferAdapter);

            // var IPersistFileFormatInterfaceList = typeof(IPersistFileFormat).GetInterfaces();

            ////////////////////////////////// end

            //var openedTextDocument = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).TextDocument;

            //var openedDataTextBuffer = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).DataTextBuffer;

            //var textSnapshot = (Microsoft.VisualStudio.Text.ITextSnapshot)openedDataTextBuffer;

            // var openedTextDocument = ((Microsoft.VisualStudio.TextManager.Introp.IVsTextBuffer)vsTextBuffer).TextDocument;

            // var openedDataTextBuffer = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).DataTextBuffer;

            // var textSnapshot = (Microsoft.VisualStudio.Text.ITextSnapshot)openedDataTextBuffer;


        }
    }
}
