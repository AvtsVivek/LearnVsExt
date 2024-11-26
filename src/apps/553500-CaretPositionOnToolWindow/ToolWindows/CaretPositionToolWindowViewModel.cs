using CaretPositionOnToolWindow.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft;
using Microsoft.VisualStudio.Text.Editor;
using System.Linq;
using System.Windows;

namespace CaretPositionOnToolWindow.ToolWindows
{
    public partial class CaretPositionToolWindowViewModel : ObservableObject
    {
        private readonly IDocumentService _documentService;

        public CaretPositionToolWindowViewModel(IDocumentService documentService)
        {
            Assumes.Present(documentService);
            _documentService = documentService;

            OpenedFilePath = string.Empty;

        }

        private string _cursorPosition;

        public string CursorPosition
        {
            get { return _cursorPosition; }
            set { _cursorPosition = value; OnPropertyChanged(); }
        }

        private int _windowFrameCount;

        public int WindowFrameCount
        {
            get { return _windowFrameCount; }
            set { _windowFrameCount = value; OnPropertyChanged(); }
        }

        private int _documentCount;

        public int DocumentCount
        {
            get { return _documentCount; }
            set { _documentCount = value; OnPropertyChanged(); }
        }

        private string _openedFilePath;

        public string OpenedFilePath
        {
            get { return _openedFilePath; }
            set { _openedFilePath = value; OnPropertyChanged(); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        private string _messageNotes;

        public string MessageNotes
        {
            get { return _messageNotes; }
            set { _messageNotes = value; OnPropertyChanged(); }
        }

        private string _filesOpenedWithoutFocus;

        public string FilesOpenedWithoutFocus
        {
            get { return _filesOpenedWithoutFocus; }
            set { _filesOpenedWithoutFocus = value; OnPropertyChanged(); }
        }

        private string _filesOpenedWithFocus;

        public string FilesOpenedWithFocus
        {
            get { return _filesOpenedWithFocus; }
            set { _filesOpenedWithFocus = value; OnPropertyChanged(); }
        }

        [RelayCommand]
        private void UserControlLostFocus(object obj)
        {
            // WpfMb.Show("Lost Focus");
            MessageNotes += Environment.NewLine + " - " + $"UserControlLostFocus: {obj}";
        }

        [RelayCommand]
        private void UserControlGotFocus(object obj)
        {
            //WpfMb.Show("Got Focus");
            var rea = (RoutedEventArgs)obj;
            var source = rea.Source;
            var originalSource = rea.OriginalSource;
            var sourceDpiContect = source.GetDpiContext();
            var originalSourceDpiContect = originalSource.GetDpiContext();
            MessageNotes += Environment.NewLine + " - " + $"UserControlGotFocus: {obj}";
            SetStatues();
        }

        [RelayCommand]
        private void UserControlLoaded(object obj)
        {
            //WpfMb.Show("Loaded ..");
            MessageNotes += Environment.NewLine + " - " + $"UserControlLoaded: {obj}";
            WireupEventHandlers();
            SetStatues();
        }

        [RelayCommand]
        private void UserControlUnLoaded(object obj)
        {
            // WpfMb.Show("UserControlUnLoaded..");
            MessageNotes += Environment.NewLine + " - " + $"UserControl-UN-Loaded : {obj}";
            UnWireupEventHandlers();
        }

        [RelayCommand]
        public void ButtonOneClick()
        {
            //WpfMb.Show(
            //    string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
            //    "CaretPositionToolWindow");

            MessageNotes = " - Cleared - ";
        }

        private async void SetStatues()
        {
            var activeVsTextViewWithoutFocus = _documentService.GetActiveVsTextViewWithoutFocus();

            if (activeVsTextViewWithoutFocus == null)
            {
                FilesOpenedWithoutFocus = false.ToString();
            }
            else
            {
                FilesOpenedWithoutFocus = true.ToString();
            }

            var activeVsTextViewWithFocus = _documentService.GetActiveVsTextViewWithFocus();

            if (activeVsTextViewWithFocus == null)
            {
                FilesOpenedWithFocus = false.ToString();
            }
            else
            {
                FilesOpenedWithFocus = true.ToString();
            }
            
            if (activeVsTextViewWithFocus == null)
            {
                MessageNotes = "No text view is currently open. Probably no text file is open. Open any text file and try again.";
            }

            var windowFrameArray = await VS.Windows.GetAllDocumentWindowsAsync();

            var windowFrameList = windowFrameArray.ToList();

            WindowFrameCount = windowFrameList.Count;

            var documentCount = 0;

            foreach (var documentFrame in windowFrameList)
            {
                var doucumentView = await documentFrame.GetDocumentViewAsync();
                if (doucumentView != null) 
                    documentCount++;
            }

            DocumentCount = documentCount;
        }

        private void UnWireupEventHandlers()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            UnSubscribeToDocumentEvents();

            UnSubscribeToTextViewEvents();

            UnSubscribeToShellEvents();

            UnSubscribeToWindowEvents();

            UnSubscribeToSelectionEvents();
        }

        private void WireupEventHandlers()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            SubscribeToDocumentEvents();

            SubscribeToTextViewEvents();

            SubscribeToShellEvents();

            SubscribeToWindowEvents();

            SubscribeToSelectionEvents();
        }

        private void SubscribeToTextViewEvents()
        {
            var wpfTextView = _documentService.GetWpfTextView();

            if (wpfTextView == null)
            {
                MessageNotes += Environment.NewLine + " - " + $"SubscribeToTextViewEvents: wpf Text view is null";
                // System.Diagnostics.Debugger.Break();
                return;
            }

            var textView = wpfTextView as ITextView;

            var caretPosition = textView.Caret.Position;

            // https://stackoverflow.com/a/7065771/1977871
            textView.Caret.PositionChanged -= Caret_PositionChanged;
            textView.Caret.PositionChanged += Caret_PositionChanged;
            textView.ViewportLeftChanged += TextView_ViewportLeftChanged;
            textView.Closed += TextView_Closed;
            textView.GotAggregateFocus += TextView_GotAggregateFocus;
            textView.LayoutChanged += TextView_LayoutChanged;
            textView.LostAggregateFocus += TextView_LostAggregateFocus;
            textView.MouseHover += TextView_MouseHover;
            textView.ViewportHeightChanged += TextView_ViewportHeightChanged;
            textView.ViewportWidthChanged += TextView_ViewportWidthChanged;
            wpfTextView.BackgroundBrushChanged += WpfTextView_BackgroundBrushChanged;
            wpfTextView.ZoomLevelChanged += WpfTextView_ZoomLevelChanged;
        }

        private void UnSubscribeToTextViewEvents()
        {
            var wpfTextView = _documentService.GetWpfTextView();

            if (wpfTextView == null)
            {
                MessageNotes += Environment.NewLine + " - " + $"Un-Sub ToTextViewEvents: wpf Text view is null";
                System.Diagnostics.Debugger.Break();
                return;
            }

            var textView = wpfTextView as ITextView;

            var caretPosition = textView.Caret.Position;

            textView.Caret.PositionChanged -= Caret_PositionChanged;
            textView.ViewportLeftChanged -= TextView_ViewportLeftChanged;
            textView.Closed -= TextView_Closed;
            textView.GotAggregateFocus -= TextView_GotAggregateFocus;
            textView.LayoutChanged -= TextView_LayoutChanged;
            textView.LostAggregateFocus -= TextView_LostAggregateFocus;
            textView.MouseHover -= TextView_MouseHover;
            textView.ViewportHeightChanged -= TextView_ViewportHeightChanged;
            textView.ViewportWidthChanged -= TextView_ViewportWidthChanged;

            wpfTextView.BackgroundBrushChanged -= WpfTextView_BackgroundBrushChanged;
            wpfTextView.ZoomLevelChanged -= WpfTextView_ZoomLevelChanged;
        }

        private void SubscribeToShellEvents()
        {
            VS.Events.ShellEvents.ShellAvailable += ShellEvents_ShellAvailable;
            VS.Events.ShellEvents.EnvironmentColorChanged += ShellEvents_EnvironmentColorChanged;
            VS.Events.ShellEvents.ShutdownStarted += ShellEvents_ShutdownStarted;
            VS.Events.ShellEvents.MainWindowVisibilityChanged += ShellEvents_MainWindowVisibilityChanged;
        }

        private void UnSubscribeToShellEvents()
        {
            VS.Events.ShellEvents.ShellAvailable -= ShellEvents_ShellAvailable;
            VS.Events.ShellEvents.EnvironmentColorChanged -= ShellEvents_EnvironmentColorChanged;
            VS.Events.ShellEvents.ShutdownStarted -= ShellEvents_ShutdownStarted;
            VS.Events.ShellEvents.MainWindowVisibilityChanged -= ShellEvents_MainWindowVisibilityChanged;
        }

        private void SubscribeToWindowEvents()
        {
            VS.Events.WindowEvents.ActiveFrameChanged += WindowEvents_ActiveFrameChanged;
            VS.Events.WindowEvents.Destroyed += WindowEvents_Destroyed;
            VS.Events.WindowEvents.Created += WindowEvents_Created;
            VS.Events.WindowEvents.FrameIsOnScreenChanged += WindowEvents_FrameIsOnScreenChanged;
            VS.Events.WindowEvents.FrameIsVisibleChanged += WindowEvents_FrameIsVisibleChanged;            
        }

        private void UnSubscribeToWindowEvents()
        {
            VS.Events.WindowEvents.ActiveFrameChanged -= WindowEvents_ActiveFrameChanged;
            VS.Events.WindowEvents.Destroyed -= WindowEvents_Destroyed;
            VS.Events.WindowEvents.Created -= WindowEvents_Created;
            VS.Events.WindowEvents.FrameIsOnScreenChanged -= WindowEvents_FrameIsOnScreenChanged;
            VS.Events.WindowEvents.FrameIsVisibleChanged -= WindowEvents_FrameIsVisibleChanged;
        }

        private void SubscribeToSelectionEvents()
        {
            VS.Events.SelectionEvents.SelectionChanged += SelectionEvents_SelectionChanged;
            VS.Events.SelectionEvents.UIContextChanged += SelectionEvents_UIContextChanged;
        }

        private void UnSubscribeToSelectionEvents()
        {
            VS.Events.SelectionEvents.SelectionChanged -= SelectionEvents_SelectionChanged;
            VS.Events.SelectionEvents.UIContextChanged -= SelectionEvents_UIContextChanged;
        }

        private void SubscribeToDocumentEvents()
        {
            VS.Events.DocumentEvents.Opened += DocumentEvents_Opened;
            VS.Events.DocumentEvents.Closed += DocumentEvents_Closed;
            VS.Events.DocumentEvents.BeforeDocumentWindowShow += DocumentEvents_BeforeDocumentWindowShow;
            VS.Events.DocumentEvents.AfterDocumentWindowHide += DocumentEvents_AfterDocumentWindowHide;
        }

        private void UnSubscribeToDocumentEvents()
        {
            VS.Events.DocumentEvents.Opened -= DocumentEvents_Opened;
            VS.Events.DocumentEvents.Closed -= DocumentEvents_Closed;
            VS.Events.DocumentEvents.BeforeDocumentWindowShow -= DocumentEvents_BeforeDocumentWindowShow;
            VS.Events.DocumentEvents.AfterDocumentWindowHide -= DocumentEvents_AfterDocumentWindowHide;
        }

        private void SelectionEvents_UIContextChanged(object sender, UIContextChangedEventArgs e)
        {
            var dpiAwarenessContext = e.GetDpiContext();
            // MessageNotes += Environment.NewLine + " - " + $"SelectionEvents_UIContextChanged: {dpiAwarenessContext}";
            // WpfMb.Show($"SelectionEvents_UIContextChanged: {sender}");
        }

        private void SelectionEvents_SelectionChanged(object sender, Community.VisualStudio.Toolkit.SelectionChangedEventArgs e)
        {
            MessageNotes += Environment.NewLine + " - " + $"SelectionEvents_SelectionChanged: {sender}";
            // WpfMb.Show($"SelectionEvents_SelectionChanged: {sender}");
        }

        private async void WindowEvents_FrameIsVisibleChanged(FrameVisibilityEventArgs obj)
        {
            MessageNotes += Environment.NewLine + " - " + $"WindowEvents_FrameIsVisibleChanged: {obj}";
            // WpfMb.Show($"WindowEvents_FrameIsVisibleChanged: {obj}");
        }

        private async void WindowEvents_FrameIsOnScreenChanged(FrameOnScreenEventArgs obj)
        {
            var documentView = await obj.Frame.GetDocumentViewAsync();
            var text = "doc view is null";
            if (documentView != null)
            {
                var caretPosition = documentView.TextView.Caret.Position;
                var caretPoint = caretPosition.Point;
                var bufferPosition = caretPosition.BufferPosition;
                var position = bufferPosition.Position;
                var textSnapshot = bufferPosition.Snapshot;
                text = textSnapshot.GetText();
            }

            MessageNotes += Environment.NewLine + " - " + $"WindowEvents_FrameIsOnScreenChanged: {text}";
            // WpfMb.Show($"WindowEvents_FrameIsOnScreenChanged: {obj}");
        }

        private async void WindowEvents_ActiveFrameChanged(ActiveFrameChangeEventArgs obj)
        {
            var newFrameDocView = await obj.NewFrame.GetDocumentViewAsync();
            var oldFrameDocView = await obj.OldFrame.GetDocumentViewAsync();

            var newDocFilePath = newFrameDocView?.FilePath ?? " is null";
            var oldDocFilePath = oldFrameDocView?.FilePath ?? " is null";

            MessageNotes += Environment.NewLine + " - " + $"WindowEvents_ActiveFrameChanged:";
            MessageNotes += Environment.NewLine + " - - new doc file path: " + newDocFilePath;
            MessageNotes += Environment.NewLine + " - - old doc file path: " + oldDocFilePath;

            if (newFrameDocView != null)
            {
                newFrameDocView.TextView.Caret.PositionChanged -= Caret_PositionChanged;
                newFrameDocView.TextView.Caret.PositionChanged += Caret_PositionChanged;
                var caretPosition = newFrameDocView.TextView.Caret.Position;
                CursorPosition = caretPosition.VirtualBufferPosition.Position.Position.ToString();
            }
            else
                CursorPosition = "Not a txt file";
            // WpfMb.Show($"WindowEvents_ActiveFrameChanged: {obj}");
        }

        private void ShellEvents_MainWindowVisibilityChanged(bool obj)
        {
            MessageNotes += Environment.NewLine + " - " + $"ShellEvents_MainWindowVisibilityChanged: {obj}";
            // WpfMb.Show($"ShellEvents_MainWindowVisibilityChanged: {obj}");
        }

        private void ShellEvents_ShutdownStarted()
        {
            MessageNotes += Environment.NewLine + " - " + $"ShellEvents_ShutdownStarted";
            // WpfMb.Show($"ShellEvents_ShutdownStarted");
        }

        private void ShellEvents_EnvironmentColorChanged()
        {
            MessageNotes += Environment.NewLine + " - " + $"ShellEvents_EnvironmentColorChanged";
            // WpfMb.Show($"ShellEvents_EnvironmentColorChanged");
        }

        private void ShellEvents_ShellAvailable()
        {
            MessageNotes += Environment.NewLine + " - " + $"ShellEvents_ShellAvailable";
            // WpfMb.Show($"ShellEvents_ShellAvailable");
        }

        private void DocumentEvents_AfterDocumentWindowHide(DocumentView documentView)
        {
            SetStatues();
            if (documentView.TextView != null)
            {
                documentView.TextView.Caret.PositionChanged -= Caret_PositionChanged;
                MessageNotes += Environment.NewLine + " - " + $"DocumentEvents_AfterDocumentWindowHide: {documentView.FilePath}";
            }
            // WpfMb.Show($"DocumentEvents_AfterDocumentWindowHide: {documentView.Document.FilePath}");
        }

        private void DocumentEvents_BeforeDocumentWindowShow(DocumentView documentView)
        {
            SetStatues();
            // https://stackoverflow.com/a/7065771/1977871

            if (documentView.TextView != null)
            {
                documentView.TextView.Caret.PositionChanged -= Caret_PositionChanged;
                documentView.TextView.Caret.PositionChanged += Caret_PositionChanged;
            }
            MessageNotes += Environment.NewLine + " - " + $"DocumentEvents_BeforeDocumentWindowShow: {documentView.FilePath}";
            // WpfMb.Show($"DocumentEvents_BeforeDocumentWindowShow: {documentView.Document.FilePath}");
        }

        private void Caret_PositionChanged(object sender, CaretPositionChangedEventArgs e)
        {
            CursorPosition = e.NewPosition.VirtualBufferPosition.Position.Position.ToString();
            MessageNotes += Environment.NewLine + " - " + $"Caret_PositionChanged: {sender.GetType()}";
        }

        private void DocumentEvents_Closed(string obj)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"DocumentEvents_Closed: {obj}";
            // WpfMb.Show($"DocumentEvents_Closed: {obj}");
        }

        private async void WindowEvents_Destroyed(WindowFrame windowFrame)
        {
            SetStatues();
            var documentView = await windowFrame.GetDocumentViewAsync();
            if (documentView != null)
            {
                MessageNotes += Environment.NewLine + " - " + $"WindowEvents_Created: {documentView.FilePath}";
            }
            else
                MessageNotes += Environment.NewLine + " - " + $"WindowEvents_Created: No doc is currently open";
            // WpfMb.Show($"WindowEvents_Destroyed: {obj.Caption}");
        }

        private async void WindowEvents_Created(WindowFrame windowFrame)
        {
            SetStatues();
            var documentView = await windowFrame.GetDocumentViewAsync();
            if (documentView != null)
            {
                MessageNotes += Environment.NewLine + " - " + $"WindowEvents_Created: {documentView.FilePath}";
            }
            else
                MessageNotes += Environment.NewLine + " - " + $"WindowEvents_Created: No doc is currently open";
            // WpfMb.Show($"WindowEvents_Created: {obj.Caption}");
        }

        private void DocumentEvents_Opened(string obj)
        {
            SetStatues();
            // CursorPosition
            MessageNotes += Environment.NewLine + " - " + $"DocumentEvents_Opened: {obj}";
            // WpfMb.Show($"DocumentEvents_Opened:: {obj}");
        }

        private void WpfTextView_ZoomLevelChanged(object sender, ZoomLevelChangedEventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"WpfTextView_ZoomLevelChanged: {sender}";
            // WpfMb.Show($"WpfTextView_ZoomLevelChanged: {sender}");
        }

        private void WpfTextView_BackgroundBrushChanged(object sender, BackgroundBrushChangedEventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"WpfTextView_BackgroundBrushChanged: {sender}";
            // WpfMb.Show($"WpfTextView_BackgroundBrushChanged: {sender}");
        }

        private void TextView_ViewportWidthChanged(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_ViewportWidthChanged: {sender}";
            // WpfMb.Show($"TextView_ViewportWidthChanged: {sender}");
        }

        private void TextView_ViewportHeightChanged(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_ViewportHeightChanged: {sender}";
            // WpfMb.Show($"TextView_ViewportHeightChanged: {sender}");
        }

        private void TextView_MouseHover(object sender, MouseHoverEventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_MouseHover: {sender}";
            // WpfMb.Show($"TextView_MouseHover: {sender}");
        }

        private void TextView_LostAggregateFocus(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_LostAggregateFocus: {sender}";
            // WpfMb.Show($"TextView_LostAggregateFocus: {sender}");
        }

        private void TextView_LayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_LayoutChanged: {sender}";
            // WpfMb.Show($"TextView_LayoutChanged: {sender}");
        }

        private void TextView_GotAggregateFocus(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_GotAggregateFocus: {sender}";
            // WpfMb.Show($"TextView_GotAggregateFocus: {sender}");
        }

        private void TextView_Closed(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_Closed: {sender}";
            // WpfMb.Show($"TextView_Closed: {sender}");
        }

        private void TextView_ViewportLeftChanged(object sender, EventArgs e)
        {
            SetStatues();
            MessageNotes += Environment.NewLine + " - " + $"TextView_ViewportLeftChanged: {sender}";
            // WpfMb.Show($"TextView_ViewportLeftChanged: {sender}");
        }

        private static T GetGlobalService<T>(Type serviceType)
        {
            return (T)Package.GetGlobalService(serviceType);
        }
    }
}