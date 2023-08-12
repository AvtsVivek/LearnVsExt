using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text.Editor;

namespace TodoClassification
{
    internal class ToDoTag : IGlyphTag { }

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
        /// <param name="spans">A set of spans we want to get tags for.</param>
        /// <returns>The list of ToDoTag TagSpans.</returns>
        IEnumerable<ITagSpan<ToDoTag>> ITagger<ToDoTag>.GetTags(NormalizedSnapshotSpanCollection spans)
        {
            //todo: implement tagging
            foreach (SnapshotSpan curSpan in spans)
            {
                int loc = curSpan.GetText().ToLower().IndexOf(_searchText);
                if (loc > -1)
                {
                    SnapshotSpan todoSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curSpan.Start + loc, _searchText.Length));
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
