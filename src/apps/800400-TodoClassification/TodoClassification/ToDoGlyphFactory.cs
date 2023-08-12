using Microsoft.VisualStudio.Text.Editor;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Text.Formatting;

namespace TodoClassification
{
    /// <summary>
    /// This class implements IGlyphFactory, which provides the visual
    /// element that will appear in the glyph margin.
    /// </summary>
    internal class ToDoGlyphFactory : IGlyphFactory
    {
        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            return new TodoGlyph();
        }
    }
}
