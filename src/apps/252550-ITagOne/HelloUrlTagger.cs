using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Text.Operations;

namespace ITagOne
{
    public class HelloUrlTagger : ITagger<IUrlTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public HelloUrlTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IUrlTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            ITextSnapshot snapshot = spans[0].Snapshot;

            SnapshotSpan fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            IEnumerable<SnapshotSpan> helloWordSnapshotSpans = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            // In the below, we are using 'spans' in the where clause. spans is of type NormalizedSnapshotSpanCollection
            IEnumerable<ITagSpan<IUrlTag>> tagSpanListOne = helloWordSnapshotSpans
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                    new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello")))).ToList();

            // In the below, we are using 'fullSnapshotSpan' in the where clause. fullSnapshotSpan is of type SnapshotSpan
            IEnumerable<ITagSpan<IUrlTag>> tagSpanListTwo = helloWordSnapshotSpans
                .Where(s => fullSnapshotSpan.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                    new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello")))).ToList();

            IEnumerable<ITagSpan<IUrlTag>> tagSpanListThreeWithoutWhereClause = helloWordSnapshotSpans
                // .Where(s => fullSnapshotSpan.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                    new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello")))).ToList();

            // In here we are using List instead of IEnumerable.
            List<TagSpan<IUrlTag>> tagSpanListFour = helloWordSnapshotSpans
                .Where(s => fullSnapshotSpan.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello")))).ToList();

            // return tagSpanListOne;
            // You can use the above or below.
            return tagSpanListThreeWithoutWhereClause;
        }
    }
}
