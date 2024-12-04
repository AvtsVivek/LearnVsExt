using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace QuickInfoSourceNoAttribute
{
    internal static class CustomContentTypeDefinition
    {
        [Export]
        [Name(CustomContentTypeConstants.ContentTypeName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition FooAbcdContentType { get; set; }

        // In this example, we dont need this field.
        // This field is used to connect or associate a Content type with a file extension. 
        // But for this example, this association is done in the IFilePathToContentTypeProvider implimentation
        // 
        //[Export]
        //[ContentType(CustomContentTypeConstants.ContentTypeName)]
        //[FileExtension(CustomContentTypeConstants.FileExtension)]
        //internal static FileExtensionToContentTypeDefinition FooAbcdFileExtensionDefinition { get; set; }
    }
}
