using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TodoClassification
{
    /// <summary>
    /// Export a <see cref="IGlyphFactoryProvider"/>
    /// </summary>
    [Export(typeof(IGlyphFactoryProvider))]
    [Name("ToDoGlyph")]
    [Order(Before = "VsTextMarker")]
    [ContentType("code")]
    [TagType(typeof(ToDoTag))]
    internal sealed class ToDoGlyphFactoryProvider : IGlyphFactoryProvider
    {
        /// <summary>
        /// This method creates an instance of our custom glyph factory for a given text view.
        /// </summary>
        /// <param name="view">The text view we are creating a glyph factory for, we don't use this.</param>
        /// <param name="margin">The glyph margin for the text view, we don't use this.</param>
        /// <returns>An instance of our custom glyph factory.</returns>
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new ToDoGlyphFactory();
        }
    }
}
