using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;

namespace DisplayMatchingBraces
{
    /// <summary>
    /// Derive from TextMarkerTag, in case anyone wants to consume
    /// just the HighlightWordTags by themselves.
    /// </summary>
    public class HighlightMatchingBraceTag : TextMarkerTag
    {
        public HighlightMatchingBraceTag() : base("MarkerFormatDefinition/HighlightMatchingBraceFormatDefinition") { }
    }

    [Export(typeof(EditorFormatDefinition))]
    [Name("MarkerFormatDefinition/HighlightMatchingBraceFormatDefinition")]
    [UserVisible(true)]
    internal class HighlightMatchingBraceFormatDefinition : MarkerFormatDefinition
    {
        public HighlightMatchingBraceFormatDefinition()
        {
            this.BackgroundColor = Colors.LightGreen;
            this.ForegroundColor = Colors.DarkGreen;
            this.DisplayName = "Highlight Matching Brace";
            this.ZOrder = 5;

            //    this.BackgroundColor = Colors.LightBlue;
            //    this.ForegroundColor = Colors.DarkBlue;
            //    this.DisplayName = "Highlight Matching Brace";
            //    this.ZOrder = 5;

            //    this.BackgroundColor = Colors.LightPink;
            //    this.ForegroundColor = Colors.DarkRed;
            //    this.DisplayName = "Highlight Matching Brace";
            //    this.ZOrder = 5;
        }

    }
}
