using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace HelloTextMarkerTag
{
    /// <summary>
    /// This tagger will provide tags for every word in the buffer that
    /// matches the word currently under the cursor.
    /// </summary>
    public class HighlightWordTagger : ITagger<HighlightWordTag>
    {
        private ITextView View { get; set; }
        private ITextBuffer SourceBuffer { get; set; }
        private ITextSearchService2 TextSearchService2 { get; set; }

        private object updateLock = new object();

        private NormalizedSnapshotSpanCollection WordSpans { get; set; }

        public HighlightWordTagger(ITextView view, ITextBuffer sourceBuffer, ITextSearchService2 textSearchService)
        {
            View = view;
            SourceBuffer = sourceBuffer;
            TextSearchService2 = textSearchService;
            WordSpans = new NormalizedSnapshotSpanCollection();
            View.LayoutChanged += ViewLayoutChanged;
        }

        #region Event Handlers

        /// <summary>
        /// Force an update if the view layout changes
        /// </summary>
        private void ViewLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            // If a new snapshot wasn't generated, then skip this layout
            if (e.NewViewState.EditSnapshot != e.OldViewState.EditSnapshot)
            {
                ThreadPool.QueueUserWorkItem(UpdateWordAdornments);
            }
        }

        /// <summary>
        /// The currently highlighted word has changed. Update the adornments to reflect this change
        /// </summary>
        private void UpdateWordAdornments(object threadContext)
        {
            string wordToLookFor = "hello";

            FindData findData = new FindData(wordToLookFor, SourceBuffer.CurrentSnapshot);
            findData.FindOptions = FindOptions.WholeWord | FindOptions.MatchCase;
            IEnumerable<SnapshotSpan> wordSnapShotSpans = TextSearchService2.FindAll(findData);

            if (!wordSnapShotSpans.Any())
            { 
                return;
            }

            SynchronousUpdate(new NormalizedSnapshotSpanCollection(wordSnapShotSpans));
        }

        /// <summary>
        /// Perform a synchronous update, in case multiple background threads are running
        /// </summary>
        private void SynchronousUpdate(NormalizedSnapshotSpanCollection newSpans)// , SnapshotSpan? newCurrentWord)
        {
            if (newSpans == null)
            {
                return;
            }

            if (newSpans.Count == 0)
            {
                return;
            }

            lock (updateLock)
            {
                WordSpans = newSpans;

                var tempEvent = TagsChanged;
                if (tempEvent != null)
                    tempEvent(this, new SnapshotSpanEventArgs(new SnapshotSpan(SourceBuffer.CurrentSnapshot, 0, SourceBuffer.CurrentSnapshot.Length)));
            }
        }

        #endregion

        #region ITagger<HighlightWordTag> Members

        private int gitTagsCallCount = 0;

        /// <summary>
        /// Find every instance of CurrentWord in the given span
        /// </summary>
        /// <param name="spans">A read-only span of text to be searched for instances of CurrentWord</param>
        public IEnumerable<ITagSpan<HighlightWordTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            Debug.WriteLine("       GetTags is called.....");

            NormalizedSnapshotSpanCollection wordSpans = WordSpans;

            if (spans.Count == 0 || WordSpans.Count == 0)
            {
                ThreadPool.QueueUserWorkItem(UpdateWordAdornments);
                yield break;
            }

            // If the requested snapshot isn't the same as the one our words are on, translate our spans
            // to the expected snapshot
            if (spans[0].Snapshot != wordSpans[0].Snapshot)
            {
                // Need to understand what this is.
                wordSpans = new NormalizedSnapshotSpanCollection(
                    wordSpans.Select(span => span.TranslateTo(spans[0].Snapshot, SpanTrackingMode.EdgeExclusive)));
            }

            foreach (SnapshotSpan span in NormalizedSnapshotSpanCollection.Overlap(spans, wordSpans))
            {
                yield return new TagSpan<HighlightWordTag>(span, new HighlightWordTag());
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        #endregion
    }
}
