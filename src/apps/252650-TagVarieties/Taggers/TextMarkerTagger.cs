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
    [Name("TextMarkTagFormatName")]
    [UserVisible(true)]
    public class TextMarkTaggerFormatDefinition : EditorFormatDefinition
    {
        protected TextMarkTaggerFormatDefinition()
        {
            this.BackgroundColor = Colors.Bisque; // Try with Colors.Bisque as well
            this.ForegroundColor = Colors.Black;

            DisplayName = "Text Mark Tag Format";

            // Try with other colors as well like the following.
            this.BackgroundColor = Colors.LightPink;
            this.ForegroundColor = Colors.DarkRed;
            this.DisplayName = "Highlight Word";
            // this.ZOrder = 5;
        }
    }

    public class TextMarkerTagger : ITagger<ITextMarkerTag>
    {
        private readonly ITextSearchService2 _textSearchService;

        public TextMarkerTagger(ITextSearchService2 textSearchService)
        {
            this._textSearchService = textSearchService;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<ITextMarkerTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            ITextSnapshot snapshot = spans[0].Snapshot;

            SnapshotSpan fullSnapshotSpan = new SnapshotSpan(snapshot,
                    new Span(0, snapshot.Length));

            IEnumerable<SnapshotSpan> helloWords = _textSearchService
                    .FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            return helloWords
                .Where(s => spans.IntersectsWith(s))
                .Select(s => new TagSpan<ITextMarkerTag>(s,
                    new TextMarkerTag("TextMarkTagFormatName")));
        }
    }
}
