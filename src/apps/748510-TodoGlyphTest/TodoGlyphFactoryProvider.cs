using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace TodoGlyphTest
{
    /// <summary>
    /// Export a <see cref="IGlyphFactoryProvider"/>
    /// </summary>
    [Export(typeof(IGlyphFactoryProvider))]
    [Name("TodoGlyph")]
    [Order(After = "VsTextMarker")]
    [ContentType("code")] // This attribute is needed. Else you will get the following exception.
    // System.InvalidOperationException: TodoTag factory is not initialized.
    [TagType(typeof(TodoTag))]
    internal sealed class TodoGlyphFactoryProvider : IGlyphFactoryProvider
    {
        public TodoGlyphFactoryProvider()
        {

        }
        /// <summary>
        /// This method creates an instance of our custom glyph factory for a given text view.
        /// </summary>
        /// <param name="view">The text view we are creating a glyph factory for, we don't use this.</param>
        /// <param name="margin">The glyph margin for the text view, we don't use this.</param>
        /// <returns>An instance of our custom glyph factory.</returns>
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new TodoGlyphFactory();
        }
    }

}
