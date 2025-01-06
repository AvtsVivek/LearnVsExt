using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TagVarieties.Taggers
{
    public class OutliningRegionTagger : ITagger<IOutliningRegionTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public OutliningRegionTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans)
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

            var text = fullSnapshotSpan.Snapshot.GetText();

            var outlineRegionTag = new OutliningRegionTag(circle, text);

            var outlineRegionTagSpan = new TagSpan<IOutliningRegionTag>(fullSnapshotSpan,  outlineRegionTag);

            return new List<TagSpan<IOutliningRegionTag>> { outlineRegionTagSpan };
        }
    }
}
