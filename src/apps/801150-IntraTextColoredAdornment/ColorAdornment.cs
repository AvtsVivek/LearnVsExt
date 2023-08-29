using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IntraTextColoredAdornment
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

    [Export(typeof(ITaggerProvider))]
    [ContentType("text")]
    [TagType(typeof(ColorTag))]
    internal sealed class ColorTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            return buffer.Properties.GetOrCreateSingletonProperty(() => new ColorTagger(buffer)) as ITagger<T>;
        }
    }

}
