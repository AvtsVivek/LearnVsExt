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

            IEnumerable<SnapshotSpan> helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IUrlTag>(s,
                    new UrlTag(new Uri("https://en.wikipedia.org/wiki/Hello"))));
        }
    }
}
