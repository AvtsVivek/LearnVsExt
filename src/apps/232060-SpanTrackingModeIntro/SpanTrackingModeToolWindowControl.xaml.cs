using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using EnvDTE;

namespace SpanTrackingModeIntro
{
    /// <summary>
    /// Interaction logic for SpanTrackingModeToolWindowControl.
    /// </summary>
    public partial class SpanTrackingModeToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpanTrackingModeToolWindowControl"/> class.
        /// </summary>
        public SpanTrackingModeToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "SpanTrackingModeToolWindow");
        }

        private bool ProcessText()
        {
            IWpfTextView wpfTextView = GetCurentWpfTextView();

            if (wpfTextView == null)
            {
                MessageBox.Show(
                messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture, "No Text View is found. No file is probably open"),
                caption: "No Text View");
                return false;
            }

            _textBuffer = wpfTextView.TextBuffer;

            if (_textBuffer == null)
            {
                MessageBox.Show(
                messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture, "No Text Buffer is found. No file is probably open"),
                caption: "No Text Buffer");
                return false;
            }

            ITextSnapshot textSnapshotOne = _textBuffer.CurrentSnapshot;

            List<ITextSnapshotLine> lines = textSnapshotOne.Lines.ToList();

            ITextCaret caret = wpfTextView.Caret;

            CaretPosition caretPosition = caret.Position;

            List<IWpfTextViewLine> wpfTextViewLines = wpfTextView.TextViewLines.WpfTextViewLines.ToList();

            SnapshotPoint caretPositionSnapshotPoint = caretPosition.BufferPosition;

            _textSnapshotTwo = caretPositionSnapshotPoint.Snapshot;

            int caretPositionInt = caretPositionSnapshotPoint.Position;

            ITextSnapshotLine caretLine = caretPositionSnapshotPoint.GetContainingLine();

            int caretLineNumber = caretLine.LineNumber;

            _extentOfLineOfCaret = caretLine.Extent;

            caretSpanTextBlock.Text = _extentOfLineOfCaret.Span.ToString();

            caretLengthTextBlock.Text = "Length: " + _extentOfLineOfCaret.Length.ToString();

            caretLineNumberTextBlock.Text = "Line No.: " + caretLineNumber.ToString();

            caretLineTextBlock.Text = caretLine.GetText();

            return true;
        }

        private SnapshotSpan _extentOfLineOfCaret;
        private ITextBuffer _textBuffer;
        private ITextSnapshot _textSnapshotTwo;

        private IWpfTextView GetCurentWpfTextView()
        {
            var componentModel = GetComponentModel();

            if (componentModel == null)
                return null;

            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            var vsTextView = GetCurrentNativeTextView();

            if (vsTextView == null)
                return null;

            return vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);
        }

        private IVsTextView GetCurrentNativeTextView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var textManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));
            Assumes.Present(textManager);

            var tempInt = textManager.GetActiveView(1, null, out IVsTextView activeView);


            if (activeView == null)
                return null;

            return activeView;
        }

        private IComponentModel GetComponentModel()
        {
            return (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
        }

        private void buttonProcessTextEdgeExclusive_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessText())
                return;

            ITrackingSpan trackingSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(_extentOfLineOfCaret, SpanTrackingMode.EdgeExclusive);

            trackingSpanTextTextBlock.Text = trackingSpan.GetText(_textSnapshotTwo);

            SnapshotSpan trackingSpanSnapshotSpan = trackingSpan.GetSpan(_textSnapshotTwo);

            trackingSpanStartTextBlock.Text = "Start: " + trackingSpanSnapshotSpan.Start.Position.ToString();

            trackingSpanEndTextBlock.Text = "End: " + trackingSpanSnapshotSpan.End.Position.ToString();
        }

        private void buttonProcessTextEdgeInclusive_Click(object sender, RoutedEventArgs e)
        {
            if(!ProcessText())
                return;

            ITrackingSpan trackingSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(_extentOfLineOfCaret, SpanTrackingMode.EdgeInclusive);

            trackingSpanTextTextBlock.Text = trackingSpan.GetText(_textSnapshotTwo);

            SnapshotSpan trackingSpanSnapshotSpan = trackingSpan.GetSpan(_textSnapshotTwo);

            trackingSpanStartTextBlock.Text = "Start: " + trackingSpanSnapshotSpan.Start.Position.ToString();

            trackingSpanEndTextBlock.Text = "End: " + trackingSpanSnapshotSpan.End.Position.ToString();
        }

        private void buttonProcessTextEdgePositive_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessText())
                return;

            ITrackingSpan trackingSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(_extentOfLineOfCaret, SpanTrackingMode.EdgePositive);

            trackingSpanTextTextBlock.Text = trackingSpan.GetText(_textSnapshotTwo);

            SnapshotSpan trackingSpanSnapshotSpan = trackingSpan.GetSpan(_textSnapshotTwo);

            trackingSpanStartTextBlock.Text = "Start: " + trackingSpanSnapshotSpan.Start.Position.ToString();

            trackingSpanEndTextBlock.Text = "End: " + trackingSpanSnapshotSpan.End.Position.ToString();
        }

        private void buttonProcessTextEdgeNegative_Click(object sender, RoutedEventArgs e)
        {
            if (!ProcessText())
                return;

            ITrackingSpan trackingSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(_extentOfLineOfCaret, SpanTrackingMode.EdgeNegative);

            trackingSpanTextTextBlock.Text = trackingSpan.GetText(_textSnapshotTwo);

            SnapshotSpan trackingSpanSnapshotSpan = trackingSpan.GetSpan(_textSnapshotTwo);

            trackingSpanStartTextBlock.Text = "Start: " + trackingSpanSnapshotSpan.Start.Position.ToString();

            trackingSpanEndTextBlock.Text = "End: " + trackingSpanSnapshotSpan.End.Position.ToString();
        }
    }
}