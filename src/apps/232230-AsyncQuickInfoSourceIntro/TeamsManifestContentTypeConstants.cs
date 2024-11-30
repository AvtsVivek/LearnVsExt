using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace AsyncQuickInfoSourceIntro
{
    public static class TeamsManifestContentTypeConstants
    {
        public const string ContentTypeName = "TeamsManifest";
        public const string FileExtension = ".fooabcd";
    }
    internal static class TeamsManifestContentTypeDefinition
    {
        [Export]
        [Name(TeamsManifestContentTypeConstants.ContentTypeName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition FooAbcdContentType { get; set; }

        [Export]
        [ContentType(TeamsManifestContentTypeConstants.ContentTypeName)]
        [FileExtension(TeamsManifestContentTypeConstants.FileExtension)]
        internal static FileExtensionToContentTypeDefinition FooAbcdFileExtensionDefinition { get; set; }
    }
}