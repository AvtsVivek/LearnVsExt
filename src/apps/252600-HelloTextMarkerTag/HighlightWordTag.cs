using Microsoft.VisualStudio.Text.Tagging;

namespace HelloTextMarkerTag
{
    /// <summary>
    /// Derive from TextMarkerTag, in case anyone wants to consume
    /// just the HighlightWordTags by themselves.
    /// </summary>
    public class HighlightWordTag : TextMarkerTag
    {
        public HighlightWordTag() : base("MarkerFormatDefinition/HighlightWordFormatDefinition") { }
    }
}
