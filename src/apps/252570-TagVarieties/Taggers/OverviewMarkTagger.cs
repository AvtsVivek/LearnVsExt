using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace TagVarieties.Taggers
{
    [Export(typeof(EditorFormatDefinition))]
    [Name("OverviewMarkTagFormatName")]
    [UserVisible(true)]
    class OverviewMarkTaggerFormatDefinition : EditorFormatDefinition
    {
        protected OverviewMarkTaggerFormatDefinition()
        {
            BackgroundColor = Colors.Bisque;
            ForegroundColor = Colors.Black;

            DisplayName = "Overview Mark Tag Format";
        }
    }
    public class OverviewMarkTagger : ITagger<IOverviewMarkTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public OverviewMarkTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IOverviewMarkTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            var snapshot = spans[0].Snapshot;
            var fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            var helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<IOverviewMarkTag>(s,
                    new OverviewMarkTag("OverviewMarkTagFormatName")));
        }
    }
}
