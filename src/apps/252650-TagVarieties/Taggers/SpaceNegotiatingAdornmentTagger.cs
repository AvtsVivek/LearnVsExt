using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TagVarieties.Taggers
{
    public class SpaceNegotiatingAdornmentTagger : ITagger<ITag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public SpaceNegotiatingAdornmentTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<ITag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var SpaceNegotiatingAdornmentTagWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "SpaceNegotiatingAdornmentTag", FindOptions.WholeWord);

            return SpaceNegotiatingAdornmentTagWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<ITag>(s,
                    new SpaceNegotiatingAdornmentTag(20, 0, 0, 0, 0, PositionAffinity.Predecessor, null, null)));
        }
    }


}
