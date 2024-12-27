using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text.Operations;
using System.Collections.ObjectModel;

namespace ITagOne
{
    public class HelloUrlTaggerThree : ITagger<IUrlTag>
    {
        private readonly ITextBuffer _buffer;
        private readonly ITextSearchService2 _textSearchService;
        private ITextSnapshot _lastTaggedSnapshot = null;
        private IReadOnlyCollection<TrackingTagSpan<IUrlTag>> _tagSpans = null;

        public HelloUrlTaggerThree(ITextBuffer buffer, ITextSearchService2 textSearchService)
        {
            this._buffer = buffer;
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                return Enumerable.Empty<ITagSpan<IUrlTag>>();

            var currentSnapshot = _buffer.CurrentSnapshot;
            if (currentSnapshot != _lastTaggedSnapshot)
            {
                UpdateTags(currentSnapshot);
            }

            var requestedSnapshot = spans[0].Snapshot;
            return _tagSpans
                .Select(s => new TagSpan<IUrlTag>(
                             s.Span.GetSpan(requestedSnapshot), s.Tag))
                .Where(s => spans.IntersectsWith(s.Span));
        }

        private void UpdateTags(ITextSnapshot currentSnapshot)
        {
            var fullSnapshotSpan =
                   new SnapshotSpan(currentSnapshot, new Span(0, currentSnapshot.Length));
            var helloWords =
                   _textSearchService.FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            _tagSpans = new ReadOnlyCollection<TrackingTagSpan<IUrlTag>>(
                helloWords
                    .Select(s => currentSnapshot.
                                CreateTrackingSpan(s, SpanTrackingMode.EdgeExclusive))
                    .Select(s => new TrackingTagSpan<IUrlTag>(
                                 s, new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello"))))
                    .ToList());

            _lastTaggedSnapshot = currentSnapshot;
        }
    }
}
