using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace ITagTwo
{
    public class HelloUrlTaggerTwo : ITagger<IUrlTag>
    {
        private readonly ITextSearchService2 _textSearchService;
        private ITextSnapshot _lastTaggedSnapshot = null;
        private IReadOnlyCollection<ITagSpan<IUrlTag>> _tagSpans = null;

        private static int _helloUrlTaggerTwoCtorCallCount = 0;
        private static int _helloUrlTaggerTwoGetTagsCallCount = 0;


        public HelloUrlTaggerTwo(ITextSearchService2 textSearchService)
        {
            _helloUrlTaggerTwoCtorCallCount++;
            Debug.WriteLine(GetType().FullName + " Constructor is called. Call count: " + _helloUrlTaggerTwoCtorCallCount);
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            _helloUrlTaggerTwoGetTagsCallCount++;
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name + " GetTags is called. Count is: " + _helloUrlTaggerTwoGetTagsCallCount);

            if (spans.Count == 0)
                return Enumerable.Empty<ITagSpan<IUrlTag>>();

            var currentSnapshot = spans[0].Snapshot;
            if (currentSnapshot != _lastTaggedSnapshot)
            {
                UpdateTags(currentSnapshot);
            }

            return _tagSpans.Where(s => spans.IntersectsWith(s.Span));
        }

        private void UpdateTags(ITextSnapshot currentSnapshot)
        {
            var fullSnapshotSpan = new SnapshotSpan(currentSnapshot,
                     new Span(0, currentSnapshot.Length));
            var helloWords = _textSearchService
                   .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            _tagSpans = new ReadOnlyCollection<TagSpan<IUrlTag>>(
                helloWords.Select(s =>
                    new TagSpan<IUrlTag>(s,
                        new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello"))))
                .ToList());

            _lastTaggedSnapshot = currentSnapshot;
        }
    }
}
