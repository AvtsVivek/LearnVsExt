using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace FilePathToContentTypeProviderIntro
{
    internal static class CustomContentTypeDefinition
    {
        [Export]
        [Name(CustomContentTypeConstants.ContentTypeName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition FooAbcdContentType { get; set; }

        //[Export]
        //[ContentType(CustomContentTypeConstants.ContentTypeName)]
        //[FileExtension(CustomContentTypeConstants.FileExtension)]
        //internal static FileExtensionToContentTypeDefinition FooAbcdFileExtensionDefinition { get; set; }
    }
}
