using Microsoft.VisualStudio.LanguageServer.Client;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace FileExtToContentTypeDefIntro
{
    public class FooAbcdContentDefinition
    {
        public const string ContentTypeName = "FooAbcd";

        // Comment out the following eight lines, to test content types registration.

        [Export]
        [Name(ContentTypeName)]
        [BaseDefinition(CodeRemoteContentDefinition.CodeRemoteContentTypeName)]
        internal static ContentTypeDefinition FooContentTypeDefinition;

        [Export]
        [FileExtension(".FooAbcd")]
        [ContentType(ContentTypeName)]
        internal static FileExtensionToContentTypeDefinition FooFileExtensionDefinition;
    }
}
