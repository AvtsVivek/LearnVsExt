using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace VsTextManagerIntro.Commands
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
        public static readonly Guid CommandSet = new Guid("0186637c-c9de-4545-af89-a9dcd12a3c97");

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
            // var vsTextManager = (IVsTextManager)await ServiceProvider.GetServiceAsync(typeof(SVsTextManager));
            
            ThreadHelper.ThrowIfNotOnUIThread();

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

            vsTextView.GetBuffer(out IVsTextLines currentDocTextLines);

            var vsTextBuffer = currentDocTextLines as IVsTextBuffer;

            var persistFileFormat = vsTextBuffer as IPersistFileFormat;

            persistFileFormat.GetCurFile(out string filePath, out uint pnFormatIndex);

            vsTextBuffer.GetLineCount(out var lineCount);

            VsShellUtilities.ShowMessageBox(
                this.package,
                $"The number of lines: {lineCount}" + Environment.NewLine + 
                $"The file path is as follows:" + Environment.NewLine + 
                filePath
                ,
                "File Details",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            // I am not sure what this language service id is
            vsTextBuffer.GetLanguageServiceID(out var languageServiceID);

            vsTextBuffer.GetLastLineIndex(out int iLineCount, out int iLineIndex);

            vsTextBuffer.GetSize(out var size);

            currentDocTextLines.GetSize(out var totalLength);

            for (int i = 0; i < lineCount; i++)
            {
                currentDocTextLines.GetLengthOfLine(i, out int lineSize);

                VsShellUtilities.ShowMessageBox(
                    this.package,
                    $"Length of line {lineSize}",
                    $"Data of line {i}",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            var viewHostGuid = DefGuidList.guidIWpfTextViewHost;

            var vsUserData = vsTextView as IVsUserData;

            vsUserData.GetData(ref viewHostGuid, out object holder);
            
            var wpfTextViewHost = (IWpfTextViewHost)holder;

            var propertiesList = wpfTextViewHost.TextView.TextBuffer.Properties.PropertyList;

            VsShellUtilities.ShowMessageBox(
                this.package,
                $"There are {propertiesList.Count} properties on ITextBuffer object.",
                $"Property count",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            foreach ( var property in propertiesList)
            {
                VsShellUtilities.ShowMessageBox(
                    this.package,
                    $"Property Key {property.Key}" + Environment.NewLine
                    //+ $"Property Key type: {property.Key.GetType()}" + Environment.NewLine
                    //+ $"Property Value type: {property.Value.GetType()}" + Environment.NewLine
                    //+ $"Property Value type: {property.Value}"
                    ,
                    $"Property {property.Key}",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            var asdfasdf = currentDocTextLines.GetType().GetInterfaces();
            var vsTextLinesInterfaceList = typeof(IVsTextLines).GetInterfaces();
            var vsTextBufferInterfaceList = typeof(IVsTextBuffer).GetInterfaces();

            // wpfTextViewHost.TextView.TextBuffer.Properties.TryGetProperty(typeof(IVsTextBuffer), out IVsTextBuffer bufferAdapter);

            var IPersistFileFormatInterfaceList = typeof(IPersistFileFormat).GetInterfaces();

            //var openedTextDocument = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).TextDocument;

            //var openedDataTextBuffer = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).DataTextBuffer;

            //var textSnapshot = (Microsoft.VisualStudio.Text.ITextSnapshot)openedDataTextBuffer;

            // var openedTextDocument = ((Microsoft.VisualStudio.TextManager.Introp.IVsTextBuffer)vsTextBuffer).TextDocument;

            // var openedDataTextBuffer = ((Microsoft.VisualStudio.Editor.Implementation.VsTextBufferAdapter)vsTextBuffer).DataTextBuffer;

            // var textSnapshot = (Microsoft.VisualStudio.Text.ITextSnapshot)openedDataTextBuffer;
                    
        
        }
    }
}
