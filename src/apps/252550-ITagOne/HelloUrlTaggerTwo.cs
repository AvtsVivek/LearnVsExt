using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text.Operations;
using System.Collections.ObjectModel;

namespace ITagOne
{
    public class HelloUrlTaggerTwo : ITagger<IUrlTag>
    {
        private readonly ITextSearchService2 _textSearchService;
        private ITextSnapshot _lastTaggedSnapshot = null;
        private IReadOnlyCollection<ITagSpan<IUrlTag>> _tagSpans = null;

        public HelloUrlTaggerTwo(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
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
