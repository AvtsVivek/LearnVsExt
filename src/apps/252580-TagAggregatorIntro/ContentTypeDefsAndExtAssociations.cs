using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TagAggregatorIntro
{
    public class ContentTypeDefsAndExtAssociations
    {
        // One
        public const string ContentTypeTagOneName = "TagOne";

        [Export]
        [Name(ContentTypeTagOneName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition TagOneContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeTagOneName)]
        [ContentType(ContentTypeTagOneName)]
        internal static FileExtensionToContentTypeDefinition TagOneFileExtensionDefinition;
    }
}
