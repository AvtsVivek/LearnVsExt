using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.Text.Editor;

namespace TagVarieties.Taggers
{
    public class InterLineAdornmentTagger : ITagger<InterLineAdornmentTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public InterLineAdornmentTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<InterLineAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            InterLineAdornmentTag interLineAdornmentTag = null; // interLineAdornmentTag

            if (interLineAdornmentTag == null)
            {
                var message = $"{nameof(InterLineAdornmentTag)} is null. Cannot continue.";
                throw new Exception(message);
            }

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<InterLineAdornmentTag>(s, interLineAdornmentTag));
        }
    }
}
