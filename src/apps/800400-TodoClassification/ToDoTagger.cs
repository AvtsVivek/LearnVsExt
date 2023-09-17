using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;

namespace TodoClassification
{
    /// <summary>
    /// This class implements ITagger for ToDoTag.  It is responsible for creating
    /// ToDoTag TagSpans, which our GlyphFactory will then create glyphs for.
    /// </summary>
    internal class ToDoTagger : ITagger<ToDoTag>
    {

        private const string _searchText = "todo";

        /// <summary>
        /// This method creates ToDoTag TagSpans over a set of SnapshotSpans.
        /// </summary>
        /// <param name="spanCollection">A set of spans we want to get tags for.</param>
        /// <returns>The list of ToDoTag TagSpans.</returns>
        public IEnumerable<ITagSpan<ToDoTag>> GetTags(NormalizedSnapshotSpanCollection spanCollection)
        {
            //todo: implement tagging
            foreach (SnapshotSpan span in spanCollection)
            {
                int locationIndex = span.GetText().ToLower().IndexOf(_searchText);
                if (locationIndex > -1)
                {
                    SnapshotSpan todoSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start + locationIndex, _searchText.Length));
                    yield return new TagSpan<ToDoTag>(todoSpan, new ToDoTag());
                }
            }

        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
