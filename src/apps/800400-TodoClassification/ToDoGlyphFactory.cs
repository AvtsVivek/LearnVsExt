using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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
            if (tag == null || !(tag is ToDoTag))
                return null;

            return new TodoGlyph();

            // You can use the following instead of the above 
            // Ensure we can draw a glyph for this marker.

            /*
            
            double m_glyphSize = 16.0;

            var ellipse = new Ellipse()
            {
                Fill = Brushes.Yellow,
                StrokeThickness = 2,
                Stroke = Brushes.Red,
                Height = m_glyphSize,
                Width = m_glyphSize,
            };

            return ellipse;
            */
        }
    }
}
