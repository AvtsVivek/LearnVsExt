using Microsoft;
using Microsoft.VisualStudio.ComponentModelHost;
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

namespace TextSnapshotIntro
{
    /// <summary>
    /// Interaction logic for ToolWindowForTextSnapshotControl.
    /// </summary>
    public partial class ToolWindowForTextSnapshotControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowForTextSnapshotControl"/> class.
        /// </summary>
        public ToolWindowForTextSnapshotControl()
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

            ITextSnapshot textSnapshot = textBuffer.CurrentSnapshot;

            List<ITextSnapshotLine> lines = textSnapshot.Lines.ToList();

            lineCountInOpenedFileTextBlock.Text = lines.Count.ToString();

            ITextCaret caret = wpfTextView.Caret;

            CaretPosition caretPosition = caret.Position;

            List<IWpfTextViewLine> wpfTextViewLines = wpfTextView.TextViewLines.WpfTextViewLines.ToList();

            SnapshotPoint caretPositionSnapshotPoint = caretPosition.BufferPosition;

            ITextSnapshotLine caretLine = caretPositionSnapshotPoint.GetContainingLine();

            int caretLineNumber = caretLine.LineNumber;
            
            caretLineNumberTextBlock.Text = caretLineNumber.ToString();

            caretLineTextBlock.Text = caretLine.GetText();
        }

        private void ResetTextBlocks()
        {
            lineCountInOpenedFileTextBlock.Text = string.Empty;
            caretLineNumberTextBlock.Text = string.Empty;
            caretLineTextBlock.Text = string.Empty;
        }

        private static IWpfTextView GetCurentWpfTextView()
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

        private static IVsTextView GetCurrentNativeTextView()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var textManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));
            Assumes.Present(textManager);

            var tempInt = textManager.GetActiveView(1, null, out IVsTextView activeView);


            if (activeView == null)
                return null;

            return activeView;
        }

        private static IComponentModel GetComponentModel()
        {
            return (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
        }
    }
}