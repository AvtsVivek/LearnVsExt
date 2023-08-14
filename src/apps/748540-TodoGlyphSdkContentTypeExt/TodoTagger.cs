using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;

namespace ToDoGlyphSdkContentTypeExt
{
    /// <summary>
    /// This class implements ITagger for ToDoTag.  It is responsible for creating
    /// ToDoTag TagSpans, which our GlyphFactory will then create glyphs for.
    /// </summary>
    internal class ToDoTagger : ITagger<ToDoTag>
    {
        private IClassifier m_classifier;

        private const string _searchText = "todo";

        internal ToDoTagger(IClassifier classifier)
        {
            m_classifier = classifier;
        }

        public IEnumerable<ITagSpan<ToDoTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan span in spans)
            {

                //if the word "todo" is in the comment,
                //create a new TodoTag TagSpan
                int locationIndex = span.GetText().ToLower().IndexOf(_searchText);
                if (locationIndex != -1)
                {
                    var todoSpan1 = new SnapshotSpan(span.Snapshot, new Span(span.Start + locationIndex, _searchText.Length));
                    var todoSpan2 = new SnapshotSpan(span.Start + locationIndex, _searchText.Length);
                    yield return new TagSpan<ToDoTag>(todoSpan2, new ToDoTag());
                }

                ////look at each classification span \
                //foreach (ClassificationSpan classification in m_classifier.GetClassificationSpans(span))
                //{
                //    //if the classification is a comment
                //    if (classification.ClassificationType.Classification.ToLower().Contains("comment"))
                //    {
                //        //if the word "todo" is in the comment,
                //        //create a new TodoTag TagSpan
                //        int index = classification.Span.GetText().ToLower().IndexOf(m_searchText);
                //        if (index != -1)
                //        {
                //            yield return new TagSpan<TodoTag>(new SnapshotSpan(classification.Span.Start + index, m_searchText.Length), new TodoTag());
                //        }
                //    }
                //}
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

    }

}
