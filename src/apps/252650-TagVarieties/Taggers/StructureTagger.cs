using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;

namespace TagVarieties.Taggers
{
    public class StructureTagger : ITagger<IStructureTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public StructureTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IStructureTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            ITextSnapshot snapshot = spans[0].Snapshot;

            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            // We need to check this. Is this the right way to do it, for each hello? 
            // Should a structure tag must be present for every hello? How should that be?
            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IStructureTag>(s,
                    new StructureTag(snapshot)));
        }
    }
}
