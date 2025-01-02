using Microsoft.VisualStudio.Text.Tagging;
using System.Windows.Media;

namespace SimpleIntraTextAdornment
{
    internal class ColorTag : ITag
    {
        internal ColorTag(Color color)
        {
            Color = color;
        }

        internal readonly Color Color;
    }
}
