using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToDoGlyphSdkContentTypeExt
{
    internal class ToDoGlyphFactory : IGlyphFactory
    {
        const double m_glyphSize = 16.0;

        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            // Ensure we can draw a glyph for this marker.
            if (tag == null )
                return null;

            if (!(tag is ToDoTag))
                return null;

            var ellipse = new Ellipse()
            {
                Fill = Brushes.Yellow,
                StrokeThickness = 2,
                Stroke = Brushes.Red,
                Height = m_glyphSize,
                Width = m_glyphSize,
            };

            return ellipse;
        }
    }

}
