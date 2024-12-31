using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.Text.Document;

namespace TagVarieties.Taggers
{
    public class ChangeTagger : ITagger<ChangeTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public ChangeTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<ChangeTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<ChangeTag>(s,
                    new ChangeTag(ChangeTypes.None)));
        }
    }
}
