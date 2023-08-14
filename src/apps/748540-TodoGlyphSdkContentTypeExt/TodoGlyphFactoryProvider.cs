using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace ToDoGlyphSdkContentTypeExt
{
    [Export(typeof(IGlyphFactoryProvider))]
    [Name("TodoGlyph")]
    [Order(After = "VsTextMarker")]
    [ContentType("hid")] // This attribute is needed. Else you will get the following exception.
    // System.InvalidOperationException: TodoTag factory is not initialized.
    [TagType(typeof(ToDoTag))]
    internal sealed class ToDoGlyphFactoryProvider : IGlyphFactoryProvider
    {
        public ToDoGlyphFactoryProvider()
        {

        }
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new ToDoGlyphFactory();
        }
    }

}
