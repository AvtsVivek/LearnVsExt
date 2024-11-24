using CaretPositionOnToolWindow.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Windows;
using System.Windows.Threading;
using WpfMb = System.Windows.MessageBox;
using Microsoft.Xaml.Behaviors;
using System.Windows.Media;

namespace CaretPositionOnToolWindow.ToolWindows
{
    public partial class CaretPositionToolWindowViewModel : ObservableObject
    {
        private readonly IGreeterService _greeterService;

        public CaretPositionToolWindowViewModel(IGreeterService greeterService)
        {

            Assumes.Present(greeterService);
            _greeterService = greeterService;

            OpenedFilePath = string.Empty;
            SomeFileIsOpen = false;
            Message = _greeterService.GetGreetingsMessage();


            InitializeTextFileVariables();
        }

        private Visibility _userControlVisibility;

        public Visibility UserControlVisibility
        {
            get { return _userControlVisibility; }
            set { _userControlVisibility = value; }
        }

        [RelayCommand]
        private void UserControlLostFocus(object obj)
        {
            // WpfMb.Show("Lost Focus");
            // var asdf = System.Windows.Media.Visual
            // The following does not work.
            // var presentationSource = PresentationSource.FromVisual(MessageBox.Show("Lost Focus"));

            // This Visual is not connected to a PresentationSource.
            //try
            //{
            //    WpfMb.Show("Lost Focus");
            //}
            //catch (InvalidOperationException exception)
            //{
            //    if (exception.Message == "This Visual is not connected to a PresentationSource.")
            //    {
            //        // As of now, just swallow it.
            //    }
            //}
        }

        [RelayCommand]
        private void UserControlGotFocus(object obj)
        {
            WpfMb.Show("Got Focus");
        }

        [RelayCommand]
        private void UserControlLoaded(object obj)
        {
            WpfMb.Show("Loaded ..");
        }

        [RelayCommand]
        private void UserControlUnLoaded(object obj)
        {
            WpfMb.Show("Un Loaded ..");
        }

        [RelayCommand]
        public void ButtonOneClick()
        {
            WpfMb.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "CaretPositionToolWindow");
        }

        private void InitializeTextFileVariables()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            if (componentModel == null)
                return;

            // The following is working. Its returning a non null settings manager.
            var textStructureNavigatorSelectorService = componentModel.GetService<ITextStructureNavigatorSelectorService>();

            var textSearchService = componentModel.GetService<ITextSearchService>();

            // Need ensure the following is not null.
            // var vsTextManagerFromComponentModel = componentModel.GetService<IVsTextManager>();

            // Is the above and (vsTextManagerFromComponentModel) and below (vsTextManager) same?
            // Need to check
            var vsTextManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            if (vsTextView == null)
            {
                SomeFileIsOpen = false;
                MessageNotes = "No text view is currently open. Probably no text file is open. Open any text file and try again.";
                return;
            }
            else
            {
                SomeFileIsOpen = true;
            }

            vsTextView.GetBuffer(out IVsTextLines currentDocTextLines);

            var vsTextBuffer = currentDocTextLines as IVsTextBuffer;

            var persistFileFormat = vsTextBuffer as IPersistFileFormat;

            persistFileFormat.GetCurFile(out string filePath, out uint pnFormatIndex);

            vsTextBuffer.GetLineCount(out var lineCount);

            MessageNotes = $"The number of lines: {lineCount}" + Environment.NewLine +
                $"The file path is as follows:" + Environment.NewLine + filePath;

            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    $"The number of lines: {lineCount}" + Environment.NewLine +
            //    $"The file path is as follows:" + Environment.NewLine +
            //    filePath
            //    ,
            //    "File Details",
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            for (int i = 0; i < lineCount; i++)
            {
                currentDocTextLines.GetLengthOfLine(i, out int lineSize);

                //VsShellUtilities.ShowMessageBox(
                //    this.package,
                //    $"Length of line {lineSize}",
                //    $"Data of line {i}",
                //    OLEMSGICON.OLEMSGICON_INFO,
                //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            // I am not sure what this language service id is
            vsTextBuffer.GetLanguageServiceID(out var languageServiceID);

            // Need to understand iLineCount and index
            vsTextBuffer.GetLastLineIndex(out int iLineCount, out int iLineIndex);

            // What is this size?
            vsTextBuffer.GetSize(out var size);

            // What is this totalLength? How is this different from size above?
            currentDocTextLines.GetSize(out var totalLength);

            /////////////////////////////////////////////////////////////////////////////

            // What the hell is this Guid?
            var viewHostGuid = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;

            // What is this userData?
            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivsuserdata
            var vsUserData = vsTextView as IVsUserData;

            vsUserData.GetData(ref viewHostGuid, out object holder);

            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.editor.iwpftextviewhost
            var wpfTextViewHost = (IWpfTextViewHost)holder;

            var wpfTextView = wpfTextViewHost.TextView;

            var textView = wpfTextView as ITextView;

            var caretPosition = textView.Caret.Position;
            textView.Caret.PositionChanged += Caret_PositionChanged;
        }

        private void Caret_PositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
            
        }

        private string _openedFilePath;

        public string OpenedFilePath
        {
            get { return _openedFilePath; }
            set { _openedFilePath = value; }
        }

        private bool _someFileOpen;

        public bool SomeFileIsOpen
        {
            get { return _someFileOpen; }
            set { _someFileOpen = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _messageNotes;

        public string MessageNotes
        {
            get { return _messageNotes; }
            set { _messageNotes = value; }
        }

        private static T GetGlobalService<T>(Type serviceType)
        {
            return (T)Package.GetGlobalService(serviceType);
        }
    }
}