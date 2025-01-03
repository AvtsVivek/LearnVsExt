using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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

        private List<int> SubstringCount(string fullText, string search_str)
        {
            var subStringIndexList = new List<int>();
            // Loop through the characters of the original string
            for (int i = 0; i < fullText.Length - search_str.Length + 1; i++)
            {
                // Check if the substring from the current position matches the search string
                if (fullText.Substring(startIndex: i, length: search_str.Length) == search_str)
                {
                    subStringIndexList.Add(i);
                    i = i + search_str.Length;
                }
            }

            return subStringIndexList;
        }

        public IEnumerable<ITagSpan<TodoTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            Debug.WriteLine("           GetTags is called ..... ");
            var spanList = spans.ToList();
            var spanCount = spanList.Count; // Just for understanding. 

            foreach (SnapshotSpan span in spans)
            {
                var spanText = span.GetText().ToLower();
                var locationIndex = spanText.IndexOf(m_searchText);

                var subStringIndexList = SubstringCount(spanText, m_searchText);
                foreach (var subStringIndex in subStringIndexList)
                {
                    var todoSnapshotSpan = new SnapshotSpan(snapshot: span.Snapshot,
                        span: new Span(start: span.Start + subStringIndex, length: m_searchText.Length));
                    var todoSpanText = todoSnapshotSpan.GetText().ToLower(); // Just for testing.
                    yield return new TagSpan<TodoTag>(span: todoSnapshotSpan, tag: new TodoTag());
                }
            }

            // As of now, classifier does not seem to work. 
            // I am not sure, what classifier is, need to learn more about it. 
            // So the following(which does not work) is commented out.
            // The following is returning zero because the m_classifier.GetClassificationSpans(span) is returning 0
            /*
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
            */
        }
    }
}
