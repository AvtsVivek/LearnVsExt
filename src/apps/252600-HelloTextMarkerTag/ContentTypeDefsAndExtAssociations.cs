using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTextMarkerTag
{
    internal class ContentTypeDefsAndExtAssociations
    {
        //// ITextMarkerTag /////////////////////////////////////////////////

        public const string ContentTypeITextMarkerTag = "ITextMarkerTag";

        [Export]
        [Name(ContentTypeITextMarkerTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ITextMarkerTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeITextMarkerTag)]
        [ContentType(ContentTypeITextMarkerTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeITextMarkerTagExtensionDefinition;
    }
}
