using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Classification;
using System.Linq;

namespace TodoGlyphTest
{
    internal class TodoTagger : ITagger<TodoTag>
    {
        private IClassifier m_classifier;

        private const string m_searchText = "todo";

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        internal TodoTagger(IClassifier classifier)
        {
            m_classifier = classifier;
        }

        public IEnumerable<ITagSpan<TodoTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            //todo: implement tagging. I am not sure what this comment line means is, I just got this from the code examples. 
            var spanList = spans.ToList(); // Just for debugging.
            var spanCount = spanList.Count;
            foreach (SnapshotSpan span in spans)
            {
                int locationIndex = span.GetText().ToLower().IndexOf(m_searchText);
                if (locationIndex > -1)
                {
                    SnapshotSpan todoSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start + locationIndex, m_searchText.Length));
                    yield return new TagSpan<TodoTag>(todoSpan, new TodoTag());
                }
            }

            // As of now, classifier does not seem to work. 
            // I am not sure, what classifier is, need to learn more about it. 
            // So the following(which does not work) is commented out.
            //foreach (SnapshotSpan span in spans)
            //{
            //    //look at each classification span \
            //    foreach (ClassificationSpan classification in m_classifier.GetClassificationSpans(span))
            //    {
            //        //if the classification is a comment
            //        if (classification.ClassificationType.Classification.ToLower().Contains("comment"))
            //        {
            //            //if the word "todo" is in the comment,
            //            //create a new TodoTag TagSpan
            //            int index = classification.Span.GetText().ToLower().IndexOf(m_searchText);
            //            if (index != -1)
            //            {
            //                yield return new TagSpan<TodoTag>(new SnapshotSpan(classification.Span.Start + index, m_searchText.Length), new TodoTag());
            //            }
            //        }
            //    }
            //}
        }
    }
}
