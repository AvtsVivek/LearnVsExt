using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace QuickInfoSourceNoAttribute
{
    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [Name("Line Async Quick Info Provider")]
    [ContentType("any")] // This line is needed. If you completely remove this attribute, the following method is not called at all.
    [Order]
    internal sealed class LineAsyncQuickInfoSourceProvider 
        : IAsyncQuickInfoSourceProvider
    {
        public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            if (textBuffer.CurrentSnapshot.ContentType.TypeName == CustomContentTypeConstants.ContentTypeName)
                // Basically what we are doing here is, for a given textBuffer(which has a given content type) a LineAsyncQuickInfoSource object is assigned.
                // I think(not sure) a text buffer tis created every time a new file is opened in Visual Studio.
                // And this method is called each time and a new text file is opened, and the cursor is hovered over on any of its text.
                // Finally, calling GetOrCreateSingletonProperty, ensures only one instance per textbuffer is created
                return textBuffer.Properties.GetOrCreateSingletonProperty(() => new LineAsyncQuickInfoSource(textBuffer));

            return null;
        }
    }
}
