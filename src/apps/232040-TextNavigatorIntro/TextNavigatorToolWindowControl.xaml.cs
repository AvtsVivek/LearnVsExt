using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft;
using System.Linq;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

namespace TextNavigatorIntro
{
    /// <summary>
    /// Interaction logic for TextNavigatorToolWindowControl.
    /// </summary>
    public partial class TextNavigatorToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextNavigatorToolWindowControl"/> class.
        /// </summary>
        public TextNavigatorToolWindowControl()
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
            IWpfTextView wpfTextView = GetCurentWpfTextView();

            if (wpfTextView == null)
            {
                ResetTextBlocks();
                MessageBox.Show(
                messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture, "No Text View is found. No file is probably open"),
                caption: "No Text View");
                return;
            }

            ITextBuffer textBuffer = wpfTextView.TextBuffer;

            if (textBuffer == null)
            {
                ResetTextBlocks();
                MessageBox.Show(
                messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture, "No Text Buffer is found. No file is probably open"),
                caption: "No Text Buffer");
                return;
            }

            var textStructureNavigatorSelectorService = GetTextNavigatorService();

            if (textStructureNavigatorSelectorService == null)
            {
                ResetTextBlocks();
                MessageBox.Show(
                messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture,
                "ITextStructureNavigatorSelectorService is null. Cannot Continue. Returning."),
                caption: "Error");
                return;
            }

            ITextStructureNavigator textNavigator = textStructureNavigatorSelectorService
                .GetTextStructureNavigator(textBuffer);

            if (textNavigator == null)
            {
                ResetTextBlocks();
                MessageBox.Show(messageBoxText: string.Format(System.Globalization.CultureInfo.CurrentUICulture,
                "ITextStructureNavigator is null. Cannot Continue. Returning."),
                caption: "Error");
                return;
            }


            ITextCaret caret = wpfTextView.Caret;

            SnapshotPoint point;

            if (caret.Position.BufferPosition > 0)
                point = caret.Position.BufferPosition - 1;
            else
            {
                Debug.WriteLine("buffer position is 0. Cannot Continue.");
                return;
            }

            TextExtent extent = textNavigator.GetExtentOfWord(point);

            //don't display the tag if the extent has whitespace
            if (extent.IsSignificant)
            {
                // Debug.WriteLine($"Returning smart tag for {extent.Span.GetText()}");
                wordTextBlock.Text = extent.Span.GetText();
                //yield return new TagSpan<TestSmartTag>(extent.Span,
                //    //new TestSmartTag(GetSmartTagActions(extent.Span)));
                //    new TestSmartTag(PredefinedErrorTypeNames.CompilerError, $"{PredefinedErrorTypeNames.CompilerError} - {extent.Span.GetText()} tool tip"));
            }
            else
            {
                Debug.WriteLine("extent is not significant. So breaking yield break 3 ");
                return;
            }

            ITextSnapshot textSnapshot = textBuffer.CurrentSnapshot;

            List<ITextSnapshotLine> lines = textSnapshot.Lines.ToList();

            lineCountInOpenedFileTextBlock.Text = lines.Count.ToString();

            CaretPosition caretPosition = caret.Position;

            List<IWpfTextViewLine> wpfTextViewLines = wpfTextView.TextViewLines.WpfTextViewLines.ToList();

            SnapshotPoint caretPositionSnapshotPoint = caretPosition.BufferPosition;

            ITextSnapshot textSnapShot = caretPositionSnapshotPoint.Snapshot;

            caretTextBlock.Text = textSnapShot.GetText();

            int caretPositionInt = caretPositionSnapshotPoint.Position;

            caretPositionAbsoluteTextBlock.Text = caretPositionInt.ToString();

            ITextSnapshotLine caretLine = caretPositionSnapshotPoint.GetContainingLine();

            SnapshotPoint startOfCaretLine = caretLine.Start;

            SnapshotPoint endOfCaretLine = caretLine.End;

            int startOfCaretLinePosition = startOfCaretLine.Position;

            caretPositionFromStartTextBlock.Text = (caretPositionInt - startOfCaretLinePosition).ToString();

            int caretLineNumber = caretLine.LineNumber;

            int caretLineNumberTwo = caretPositionSnapshotPoint.GetContainingLineNumber();

            SnapshotSpan extentOfLineOfCaret = caretLine.Extent;

            caretSpanTextBlock.Text = extentOfLineOfCaret.Span.ToString();

            caretLengthTextBlock.Text = extentOfLineOfCaret.Length.ToString();

            caretLineNumberTextBlock.Text = caretLineNumber.ToString();

            caretLineTextBlock.Text = caretLine.GetText();
        }

        private void ResetTextBlocks()
        {
            lineCountInOpenedFileTextBlock.Text = string.Empty;
            caretLineNumberTextBlock.Text = string.Empty;
            caretLineTextBlock.Text = string.Empty;
        }

        private ITextStructureNavigatorSelectorService GetTextNavigatorService()
        {
            var componentModel = GetComponentModel();

            if (componentModel == null)
                return null;

            var navigatorService = componentModel.GetService<ITextStructureNavigatorSelectorService>();

            return navigatorService;
        }

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
    }
}