using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleIntraTextAdornment
{
    internal sealed class ColorAdornment : Button
    {
        private Rectangle rect;

        internal ColorAdornment(ColorTag colorTag)
        {
            rect = new Rectangle()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Width = 20,
                Height = 10
            };

            Update(colorTag);

            Content = rect;
        }

        private Brush MakeBrush(Color color)
        {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }

        internal void Update(ColorTag colorTag)
        {
            rect.Fill = MakeBrush(colorTag.Color);
        }
    }
}
