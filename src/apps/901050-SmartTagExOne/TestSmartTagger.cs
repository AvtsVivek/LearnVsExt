using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.Text.Adornments;
using System.Diagnostics;

namespace SmartTagExOne
{
    internal class TestSmartTagger : ITagger<TestSmartTag>, IDisposable
    {
        private ITextBuffer m_buffer;
        private ITextView m_view;
        private ITextStructureNavigatorSelectorService m_navigatorSelectorService;
        private bool m_disposed;

        public TestSmartTagger(ITextBuffer buffer, ITextView view, ITextStructureNavigatorSelectorService navigatorSelectorService)
        {
            m_buffer = buffer;
            m_view = view;
            m_navigatorSelectorService = navigatorSelectorService;
            // m_view.LayoutChanged += OnLayoutChanged;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
        private static int _callCount = 0;
        public IEnumerable<ITagSpan<TestSmartTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            _callCount++;
            Debug.WriteLine($"                   Get Tags is called. {_callCount}");
            ITextSnapshot snapshot = m_buffer.CurrentSnapshot;

            if (snapshot.Length == 0)
            {
                Debug.WriteLine("Snapshot lenght is zero, so breaking yield break 1 ");
                yield break; //don't do anything if the buffer is empty
            }
            //set up the navigator
            ITextStructureNavigator navigator = m_navigatorSelectorService.GetTextStructureNavigator(m_buffer);

            foreach (var span in spans)
            {
                ITextCaret caret = m_view.Caret;
                SnapshotPoint point;

                if (caret.Position.BufferPosition > 0)
                    point = caret.Position.BufferPosition - 1;
                else
                {
                    Debug.WriteLine("buffer position is 0 so breaking yield break 2 ");
                    yield break;
                }
                TextExtent extent = navigator.GetExtentOfWord(point);

                //don't display the tag if the extent has whitespace
                if (extent.IsSignificant)
                {
                    Debug.WriteLine($"Returning smart tag for {extent.Span.GetText()}");
                    yield return new TagSpan<TestSmartTag>(extent.Span,
                        //new TestSmartTag(GetSmartTagActions(extent.Span)));
                        new TestSmartTag(PredefinedErrorTypeNames.CompilerError, $"{PredefinedErrorTypeNames.CompilerError} - {extent.Span.GetText()} tool tip"));
                }
                else
                {
                    Debug.WriteLine("extent is not significant. So breaking yield break 3 ");
                    yield break;
                }
            }
        }

        private void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            //ITextSnapshot snapshot = e.NewSnapshot;
            ////don't do anything if this is just a change in case
            //if (!snapshot.GetText().ToLower().Equals(e.OldSnapshot.GetText().ToLower()))
            //{
            //    SnapshotSpan span = new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
            //    EventHandler<SnapshotSpanEventArgs> handler = this.TagsChanged;
            //    if (handler != null)
            //    {
            //        handler(this, new SnapshotSpanEventArgs(span));
            //    }
            //}
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.m_disposed)
            {
                if (disposing)
                {
                    // m_view.LayoutChanged -= OnLayoutChanged;
                    m_view = null;
                }

                m_disposed = true;
            }
        }

        //private ReadOnlyCollection<SmartTagActionSet> GetSmartTagActions(SnapshotSpan span)
        //{
        //    List<SmartTagActionSet> actionSetList = new List<SmartTagActionSet>();
        //    List<ISmartTagAction> actionList = new List<ISmartTagAction>();

        //    ITrackingSpan trackingSpan = span.Snapshot.CreateTrackingSpan(span, SpanTrackingMode.EdgeInclusive);
        //    actionList.Add(new UpperCaseSmartTagAction(trackingSpan));
        //    actionList.Add(new LowerCaseSmartTagAction(trackingSpan));
        //    SmartTagActionSet actionSet = new SmartTagActionSet(actionList.AsReadOnly());
        //    actionSetList.Add(actionSet);
        //    return actionSetList.AsReadOnly();
        //}
    }
}
