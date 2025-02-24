using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.ComponentModelHost;
using System;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO.Packaging;
using System.Linq;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Operations;

namespace ITagOne
{
    /// <summary>
    /// Interaction logic for TaggersToolWindowControl.
    /// </summary>
    public partial class TaggersToolWindowControl : UserControl
    {
        private ToolWindowPane _toolWindowPane;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggersToolWindowControl"/> class.
        /// </summary>
        public TaggersToolWindowControl(ToolWindowPane toolWindowPane)
        {
            // I am not sure, this is a good idea to have ToolWindowPane as a dependency of this control class.
            // This is a circular dependency, so I am not sure if this right
            _toolWindowPane = toolWindowPane;
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
                "TaggersToolWindow");
        }

        private void buttonVersion1_Click(object sender, RoutedEventArgs e)
        {
            //var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

            //int mustHaveFocus = 1;

            //vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            //if (vsTextView == null)
            //{
            //    VsShellUtilities.ShowMessageBox(
            //        (AsyncPackage)_toolWindowPane.Package,
            //        "No text view is currently open. Probably no text file is open. Open any text file and try again.",
            //        "No text view!!",
            //        OLEMSGICON.OLEMSGICON_INFO,
            //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            //    return;
            //}

            //var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            //var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            //var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            //var textBuffer = wpfTextView.TextBuffer;

            //var currentTextSnapShot = textBuffer.CurrentSnapshot;

            //var currentTextSnapShotSpan = new SnapshotSpan(currentTextSnapShot, 0, currentTextSnapShot.Length);

            //var normalizedSnapshotSpanCollection = new NormalizedSnapshotSpanCollection(currentTextSnapShotSpan);

            //var textSearchService2 = componentModel.GetService<ITextSearchService2>();

            Tuple<ITextBuffer, ITextSearchService2, NormalizedSnapshotSpanCollection> textBufferSearchSnapshotCollectionTuple = default;

            if (!TryGetTextBufferSearchServiceSnapshotCollection(out textBufferSearchSnapshotCollectionTuple))
            {
                return;
            }

            var helloUrlTagger = new HelloUrlTagger(textBufferSearchSnapshotCollectionTuple.Item2);
            
            var tagList = helloUrlTagger.GetTags(textBufferSearchSnapshotCollectionTuple.Item3).ToList();

            tagCountTextBlock.Text = tagList.Count.ToString();
        }

        private void buttonVersion2_Click(object sender, RoutedEventArgs e)
        {
            //var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

            //int mustHaveFocus = 1;

            //vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            //if (vsTextView == null)
            //{
            //    VsShellUtilities.ShowMessageBox(
            //        (AsyncPackage)_toolWindowPane.Package,
            //        "No text view is currently open. Probably no text file is open. Open any text file and try again.",
            //        "No text view!!",
            //        OLEMSGICON.OLEMSGICON_INFO,
            //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            //    return;
            //}

            //var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            //var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            //var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            //var textBuffer = wpfTextView.TextBuffer;

            //var currentTextSnapShot = textBuffer.CurrentSnapshot;

            //var currentTextSnapShotSpan = new SnapshotSpan(currentTextSnapShot, 0, currentTextSnapShot.Length);

            //var normalizedSnapshotSpanCollection = new NormalizedSnapshotSpanCollection(currentTextSnapShotSpan);

            //var textSearchService2 = componentModel.GetService<ITextSearchService2>();

            Tuple<ITextBuffer, ITextSearchService2, NormalizedSnapshotSpanCollection> textBufferSearchSnapshotCollectionTuple = default;

            if (!TryGetTextBufferSearchServiceSnapshotCollection(out textBufferSearchSnapshotCollectionTuple))
            {
                return;
            }

            var helloUrlTagger = new HelloUrlTaggerTwo(textBufferSearchSnapshotCollectionTuple.Item2);

            var tagList = helloUrlTagger.GetTags(textBufferSearchSnapshotCollectionTuple.Item3).ToList();

            tagCountTextBlock.Text = tagList.Count.ToString();
        }

        private void buttonVersion3_Click(object sender, RoutedEventArgs e)
        {
            //var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

            //int mustHaveFocus = 1;

            //vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            //if (vsTextView == null)
            //{
            //    VsShellUtilities.ShowMessageBox(
            //        (AsyncPackage)_toolWindowPane.Package,
            //        "No text view is currently open. Probably no text file is open. Open any text file and try again.",
            //        "No text view!!",
            //        OLEMSGICON.OLEMSGICON_INFO,
            //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            //    return;
            //}

            //var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            //var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            //var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            //var textBuffer = wpfTextView.TextBuffer;

            //var currentTextSnapShot = textBuffer.CurrentSnapshot;

            //var currentTextSnapShotSpan = new SnapshotSpan(currentTextSnapShot, 0, currentTextSnapShot.Length);

            //var normalizedSnapshotSpanCollection = new NormalizedSnapshotSpanCollection(currentTextSnapShotSpan);

            //var textSearchService2 = componentModel.GetService<ITextSearchService2>();

            Tuple<ITextBuffer, ITextSearchService2, NormalizedSnapshotSpanCollection> textBufferSearchSnapshotCollectionTuple = default;

            if (!TryGetTextBufferSearchServiceSnapshotCollection(out textBufferSearchSnapshotCollectionTuple))
            {
                return;
            }

            var helloUrlTagger = new HelloUrlTaggerThree(textBufferSearchSnapshotCollectionTuple.Item1, textBufferSearchSnapshotCollectionTuple.Item2);

            var tagList = helloUrlTagger.GetTags(textBufferSearchSnapshotCollectionTuple.Item3).ToList();

            tagCountTextBlock.Text = tagList.Count.ToString();
        }

        private bool TryGetTextBufferSearchServiceSnapshotCollection(out 
            Tuple<ITextBuffer, ITextSearchService2, NormalizedSnapshotSpanCollection> textBufferSearchSnapshotCollectionTuple)
        {
            textBufferSearchSnapshotCollectionTuple = default;

            var vsTextManager = GetGlobalService<IVsTextManager>(typeof(SVsTextManager));

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            if (vsTextView == null)
            {
                VsShellUtilities.ShowMessageBox(
                    (AsyncPackage)_toolWindowPane.Package,
                    "No text view is currently open. Probably no text file is open. Open any text file and try again.",
                    "No text view!!",
                    OLEMSGICON.OLEMSGICON_INFO,
                    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

                return false;
            }

            var componentModel = GetGlobalService<IComponentModel>(typeof(SComponentModel));

            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            var wpfTextView = vsEditorAdaptersFactoryService.GetWpfTextView(vsTextView);

            var textBuffer = wpfTextView.TextBuffer;

            var currentTextSnapShot = textBuffer.CurrentSnapshot;

            var currentTextSnapShotSpan = new SnapshotSpan(currentTextSnapShot, 0, currentTextSnapShot.Length);

            var normalizedSnapshotSpanCollection = new NormalizedSnapshotSpanCollection(currentTextSnapShotSpan);

            var textSearchService2 = componentModel.GetService<ITextSearchService2>();

            textBufferSearchSnapshotCollectionTuple = new Tuple<ITextBuffer, ITextSearchService2, NormalizedSnapshotSpanCollection>(textBuffer, textSearchService2, normalizedSnapshotSpanCollection);

            return true;
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            tagCountTextBlock.Text = string.Empty;
        }

        private static T GetGlobalService<T>(Type serviceType)
        {
            return (T)Microsoft.VisualStudio.Shell.Package.GetGlobalService(serviceType);
        }
    }    
}