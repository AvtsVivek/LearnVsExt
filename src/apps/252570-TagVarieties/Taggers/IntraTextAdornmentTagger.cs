using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TagVarieties.Taggers
{
    public class IntraTextAdornmentTagger : ITagger<IntraTextAdornmentTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public IntraTextAdornmentTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IntraTextAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            var circle = new Ellipse()
            {
                Fill = new SolidColorBrush(Colors.Red),
                Height = 14,
                Width = 14,
                Stretch = Stretch.Fill
            };

            var tags = helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IntraTextAdornmentTag>(s,
                    new IntraTextAdornmentTag(circle, null))).ToList();
        
            return tags;
        }
    }
}
