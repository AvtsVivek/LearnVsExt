using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TodoGlyphSdkContentTypeExt
{
    public static class FileAndContentTypeDefinitions
    {
        [Export]
        [Name("hid")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition hidingContentTypeDefinition;

        [Export]
        [FileExtension(".hid")]
        [ContentType("hid")]
        internal static FileExtensionToContentTypeDefinition hiddenFileExtensionDefinition;
    }

}
