using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ITagTwo
{
    public class ContentTypeDefsAndExtAssociations
    {
        // One

        public const string ContentTypeOneName = "TagOne";

        [Export]
        [Name(ContentTypeOneName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition TagOneContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeOneName)]
        [ContentType(ContentTypeOneName)]
        internal static FileExtensionToContentTypeDefinition TagOneFileExtensionDefinition;

        // Two

        public const string ContentTypeTwoName = "TagTwo";

        [Export]
        [Name(ContentTypeTwoName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition TagTwoContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeTwoName)]
        [ContentType(ContentTypeTwoName)]
        internal static FileExtensionToContentTypeDefinition TagTwoFileExtensionDefinition;

        // Three

        public const string ContentTypeThreeName = "TagThree";

        [Export]
        [Name(ContentTypeThreeName)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition TagThreeContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeThreeName)]
        [ContentType(ContentTypeThreeName)]
        internal static FileExtensionToContentTypeDefinition TagThreeFileExtensionDefinition;
    }
}
