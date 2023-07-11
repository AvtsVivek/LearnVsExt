using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

namespace TodoGlyphTest
{
    internal class TodoGlyphFactory : IGlyphFactory
    {
        const double m_glyphSize = 16.0;

        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            // Ensure we can draw a glyph for this marker.
            if (tag == null || !(tag is TodoTag))
            {
                return null;
            }

            System.Windows.Shapes.Ellipse ellipse = new Ellipse();
            ellipse.Fill = Brushes.Yellow;
            ellipse.StrokeThickness = 2;
            ellipse.Stroke = Brushes.Red;
            ellipse.Height = m_glyphSize;
            ellipse.Width = m_glyphSize;

            return ellipse;
        }
    }

}
