using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows;

namespace TodoClassification
{
    /// <summary>
    /// This class implements IGlyphFactory, which provides the visual
    /// element that will appear in the glyph margin.
    /// </summary>
    internal class ToDoGlyphFactory : IGlyphFactory
    {
        UIElement IGlyphFactory.GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            return new TodoGlyph();
        }
    }
}
