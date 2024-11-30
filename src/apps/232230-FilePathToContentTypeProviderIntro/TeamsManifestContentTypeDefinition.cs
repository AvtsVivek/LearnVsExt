using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace FilePathToContentTypeProviderIntro
{
    internal static class TeamsManifestContentTypeDefinition
    {
        //[Export]
        //[Name(TeamsManifestContentTypeConstants.ContentTypeName)]
        //[BaseDefinition("JSON")]
        //internal static ContentTypeDefinition ManifestContentType { get; set; }

        //[Export]
        //[ContentType(TeamsManifestContentTypeConstants.ContentTypeName)]
        //[FileExtension(TeamsManifestContentTypeConstants.FileExtension)]
        //internal static FileExtensionToContentTypeDefinition ManifestFileExtensionDefinition { get; set; }

        //[Export]
        //[ContentType(TeamsManifestContentTypeConstants.ContentTypeName)]
        //[FileName(TeamsManifestContentTypeConstants.FileName)]
        //internal static FileExtensionToContentTypeDefinition ManifestFileNameDefinition { get; set; }
    }
}
