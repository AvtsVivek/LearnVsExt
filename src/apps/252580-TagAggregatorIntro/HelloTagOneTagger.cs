using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TagAggregatorIntro
{
    internal class HelloTagOne : ITag { }

    internal class HelloTagOneTagger : ITagger<HelloTagOne>
    {
        private const string m_searchText = "hello";

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        private static int _helloTagOneTaggerGetTagsCallCount = 0;

        private static int _helloTagOneTaggerProviderCtorCallCount = 0;

        internal HelloTagOneTagger() 
        {
            _helloTagOneTaggerProviderCtorCallCount++;
            Debug.WriteLine("HelloTagOneTagger Constructor is called. Count: " + _helloTagOneTaggerProviderCtorCallCount);
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

        public IEnumerable<ITagSpan<HelloTagOne>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            _helloTagOneTaggerGetTagsCallCount++;
            Debug.WriteLine("GetTags is called. Count: " + _helloTagOneTaggerGetTagsCallCount);
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
                    yield return new TagSpan<HelloTagOne>(span: todoSnapshotSpan, tag: new HelloTagOne());
                }
            }
        }
    }
}
