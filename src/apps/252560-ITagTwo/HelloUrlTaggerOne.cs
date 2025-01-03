using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Diagnostics;
using System.Reflection;

namespace ITagTwo
{
    public class HelloUrlTaggerOne : ITagger<IUrlTag>
    {
        private readonly ITextSearchService2 _textSearchService;
        private static int _helloUrlTaggerOneCtorCallCount = 0;
        private static int _helloUrlTaggerOneGetTagsCallCount = 0;

        public HelloUrlTaggerOne(ITextSearchService2 textSearchService)
        {
            _helloUrlTaggerOneCtorCallCount++;
            Debug.WriteLine(GetType().FullName + " Constructor is called. Call count: " + _helloUrlTaggerOneCtorCallCount);
            _textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            _helloUrlTaggerOneGetTagsCallCount++;
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " GetTags is called. Count is: " + _helloUrlTaggerOneGetTagsCallCount);
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                    new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello"))));
        }
    }
}
