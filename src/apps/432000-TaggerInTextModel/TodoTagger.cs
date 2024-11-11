using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;

namespace TaggerInTextModel
{
    internal class TodoTag : ITag { }
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
            // The following foreach loop is returning only one tag span.
            //foreach (SnapshotSpan span in spans)
            //{
            //    int locationIndex = span.GetText().ToLower().IndexOf(m_searchText);
            //    if (locationIndex > -1)
            //    {
            //        SnapshotSpan todoSpan = new SnapshotSpan(span.Snapshot, new Span(span.Start + locationIndex, m_searchText.Length));
            //        yield return new TagSpan<TodoTag>(todoSpan, new TodoTag());
            //    }
            //}

            // And in the following, its returning zero because the m_classifier.GetClassificationSpans(span) is returning 0
            foreach (SnapshotSpan span in spans)
            {
                // Microsoft.VisualStudio.Text.Classification.Implimentation.ClassifierAggregator
                //look at each classification span \
                foreach (ClassificationSpan classificationSpan in m_classifier.GetClassificationSpans(span))
                {
                    //if the classification is a comment
                    if (classificationSpan.ClassificationType.Classification.ToLower().Contains("comment"))
                    {
                        //if the word "todo" is in the comment,
                        //create a new TodoTag TagSpan
                        int index = classificationSpan.Span.GetText().ToLower().IndexOf(m_searchText);
                        if (index != -1)
                        {
                            yield return new TagSpan<TodoTag>(
                                new SnapshotSpan(classificationSpan.Span.Start + index, m_searchText.Length), new TodoTag());
                        }
                    }
                }
            }
        }
    }
}
