using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TagVarieties
{
    internal class ContentTypeDefsAndExtAssociations
    {
        // CompilerErrorTag ///////////////////////////////////////////////// Not Done

        public const string ContentTypeCompilerErrorTag = "CompilerErrorTag";

        [Export]
        [Name(ContentTypeCompilerErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition CompilerErrorContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeCompilerErrorTag)]
        [ContentType(ContentTypeCompilerErrorTag)]
        internal static FileExtensionToContentTypeDefinition CompilerErrorFileExtensionDefinition;

        // HintedSuggestion ///////////////////////////////////////////////// Not Done

        public const string ContentTypeHintedSuggestionTag = "HintedSuggestionTag";

        [Export]
        [Name(ContentTypeHintedSuggestionTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition HintedSuggestionContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeHintedSuggestionTag)]
        [ContentType(ContentTypeHintedSuggestionTag)]
        internal static FileExtensionToContentTypeDefinition HintedSuggestionFileExtensionDefinition;


        // InformationTag ///////////////////////////////////////////////// Not Done

        public const string ContentTypeInformationTag = "InformationTag";

        [Export]
        [Name(ContentTypeInformationTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition InformationContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeInformationTag)]
        [ContentType(ContentTypeInformationTag)]
        internal static FileExtensionToContentTypeDefinition InformationFileExtensionDefinition;

        // OtherErrorTag ///////////////////////////////////////////////// Not Done

        public const string ContentTypeOtherErrorTag = "OtherErrorTag";

        [Export]
        [Name(ContentTypeOtherErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OtherErrorContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeOtherErrorTag)]
        [ContentType(ContentTypeOtherErrorTag)]
        internal static FileExtensionToContentTypeDefinition OtherErrorFileExtensionDefinition;

        // SuggestionTag ///////////////////////////////////////////////// Not Done

        public const string ContentTypeSuggestionTag = "SuggestionTag";

        [Export]
        [Name(ContentTypeSuggestionTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition SuggestionContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeSuggestionTag)]
        [ContentType(ContentTypeSuggestionTag)]
        internal static FileExtensionToContentTypeDefinition SuggestionFileExtensionDefinition;


        // SyntaxErrorTag ///////////////////////////////////////////////// Not Done

        public const string ContentTypeSyntaxErrorTag = "SyntaxErrorTag";

        [Export]
        [Name(ContentTypeSyntaxErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition SyntaxErrorContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeSyntaxErrorTag)]
        [ContentType(ContentTypeSyntaxErrorTag)]
        internal static FileExtensionToContentTypeDefinition SyntaxErrorFileExtensionDefinition;

        // WarningErrorTag /////////////////////////////////////////////////

        public const string ContentTypeWarningErrorTag = "WarningErrorTag";

        [Export]
        [Name(ContentTypeWarningErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition WarningErrorContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeWarningErrorTag)]
        [ContentType(ContentTypeWarningErrorTag)]
        internal static FileExtensionToContentTypeDefinition WarningErrorFileExtensionDefinition;

        // IVsVisibleTextMarkerTag /////////////////////////////////////////////////

        public const string ContentTypeIVsVisibleTextMarkerTag = "IVsVisibleTextMarkerTag";

        [Export]
        [Name(ContentTypeIVsVisibleTextMarkerTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IVsVisibleTextMarkerContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIVsVisibleTextMarkerTag)]
        [ContentType(ContentTypeIVsVisibleTextMarkerTag)]
        internal static FileExtensionToContentTypeDefinition IVsVisibleTextMarkerFileExtensionDefinition;

        //// TextMarkerTag /////////////////////////////////////////////////

        public const string ContentTypeTextMarkerTag = "TextMarkerTag";

        [Export]
        [Name(ContentTypeTextMarkerTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition TextMarkerTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeTextMarkerTag)]
        [ContentType(ContentTypeTextMarkerTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeTextMarkerTagExtensionDefinition;

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


        // IDiagnosticTag /////////////////////////////////////////////////

        public const string ContentTypeIDiagnosticTag = "IDiagnosticTag";

        [Export]
        [Name(ContentTypeIDiagnosticTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IDiagnosticTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIDiagnosticTag)]
        [ContentType(ContentTypeIDiagnosticTag)]
        internal static FileExtensionToContentTypeDefinition IDiagnosticTagExtensionDefinition;

        // ICodeLensTag /////////////////////////////////////////////////

        public const string ContentTypeICodeLensTag = "ICodeLensTag";

        [Export]
        [Name(ContentTypeICodeLensTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ICodeLensTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeICodeLensTag)]
        [ContentType(ContentTypeICodeLensTag)]
        internal static FileExtensionToContentTypeDefinition ICodeLensTagExtensionDefinition;

        // ICodeLensTag2 /////////////////////////////////////////////////

        public const string ContentTypeICodeLensTag2 = "ICodeLensTag2";

        [Export]
        [Name(ContentTypeICodeLensTag2)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ICodeLensTag2ContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeICodeLensTag2)]
        [ContentType(ContentTypeICodeLensTag2)]
        internal static FileExtensionToContentTypeDefinition ICodeLensTag2ExtensionDefinition;


        // ICodeLensTag3 /////////////////////////////////////////////////

        public const string ContentTypeICodeLensTag3 = "ICodeLensTag3";

        [Export]
        [Name(ContentTypeICodeLensTag3)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ICodeLensTag3ContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeICodeLensTag3)]
        [ContentType(ContentTypeICodeLensTag3)]
        internal static FileExtensionToContentTypeDefinition ICodeLensTag3ExtensionDefinition;

        // ChangeTag /////////////////////////////////////////////////

        public const string ContentTypeChangeTag = "ChangeTag";

        [Export]
        [Name(ContentTypeChangeTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ChangeTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeChangeTag)]
        [ContentType(ContentTypeChangeTag)]
        internal static FileExtensionToContentTypeDefinition ChangeTagExtensionDefinition;

        // IGlyphTag ///////////////////////////////////////////////// Not Done. Look at the example TodoGlyphTest

        public const string ContentTypeIGlyphTag = "IGlyphTag";

        [Export]
        [Name(ContentTypeIGlyphTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IGlyphTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIGlyphTag)]
        [ContentType(ContentTypeIGlyphTag)]
        internal static FileExtensionToContentTypeDefinition IGlyphTagExtensionDefinition;

        // InterLineAdornmentTag /////////////////////////////////////////////////

        public const string ContentTypeInterLineAdornmentTag = "InterLineAdornmentTag";

        [Export]
        [Name(ContentTypeInterLineAdornmentTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition InterLineAdornmentTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeInterLineAdornmentTag)]
        [ContentType(ContentTypeInterLineAdornmentTag)]
        internal static FileExtensionToContentTypeDefinition InterLineAdornmentTagExtensionDefinition;

        // IntraTextAdornmentTag /////////////////////////////////////////////////

        public const string ContentTypeIntraTextAdornmentTag = "IntraTextAdornmentTag";

        [Export]
        [Name(ContentTypeIntraTextAdornmentTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IntraTextAdornmentTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIntraTextAdornmentTag)]
        [ContentType(ContentTypeIntraTextAdornmentTag)]
        internal static FileExtensionToContentTypeDefinition IntraTextAdornmentTagExtensionDefinition;

        // IClassificationTag ///////////////////////////////////////////////// Not Done.
        // We will look into this in another example. A lot needs to be understood such as IClassifierAggregatorService etc.

        public const string ContentTypeIClassificationTag = "IClassificationTag";

        [Export]
        [Name(ContentTypeIClassificationTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IClassificationTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIClassificationTag)]
        [ContentType(ContentTypeIClassificationTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeIClassificationTagExtensionDefinition;

        // ClassificationTag /////////////////////////////////////////////////

        public const string ContentTypeClassificationTag = "ClassificationTag";

        [Export]
        [Name(ContentTypeClassificationTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ClassificationTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeClassificationTag)]
        [ContentType(ContentTypeClassificationTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeClassificationTagExtensionDefinition;

        // IEndOfLineAdornmentTag /////////////////////////////////////////////////

        public const string ContentTypeIEndOfLineAdornmentTag = "IEndOfLineAdornmentTag";

        [Export]
        [Name(ContentTypeIEndOfLineAdornmentTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IEndOfLineAdornmentTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIEndOfLineAdornmentTag)]
        [ContentType(ContentTypeIEndOfLineAdornmentTag)]
        internal static FileExtensionToContentTypeDefinition IEndOfLineAdornmentTagExtensionDefinition;

        // IErrorTag /////////////////////////////////////////////////

        public const string ContentTypeIErrorTag = "IErrorTag";

        [Export]
        [Name(ContentTypeIErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IErrorTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIErrorTag)]
        [ContentType(ContentTypeIErrorTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeIErrorTagExtensionDefinition;

        // ErrorTag ///////////////////////////////////////////////// Note done because we did IErrorTag

        public const string ContentTypeErrorTag = "ErrorTag";

        [Export]
        [Name(ContentTypeErrorTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ErrorTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeErrorTag)]
        [ContentType(ContentTypeErrorTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeErrorTagExtensionDefinition;

        // IOutliningRegionTag /////////////////////////////////////////////////

        public const string ContentTypeIOutliningRegionTag = "IOutliningRegionTag";

        [Export]
        [Name(ContentTypeIOutliningRegionTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IOutliningRegionTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIOutliningRegionTag)]
        [ContentType(ContentTypeIOutliningRegionTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeIOutliningRegionTagExtensionDefinition;


        // OutliningRegionTag /////////////////////////////////////////////////

        public const string ContentTypeOutliningRegionTag = "OutliningRegionTag";

        [Export]
        [Name(ContentTypeOutliningRegionTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OutliningRegionTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeOutliningRegionTag)]
        [ContentType(ContentTypeOutliningRegionTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeOutliningRegionTagExtensionDefinition;

        // IOverviewMarkTag ///////////////////////////////////////////////// - Not done, because OverviewMarkTag 

        public const string ContentTypeIOverviewMarkTag = "IOverviewMarkTag";

        [Export]
        [Name(ContentTypeIOverviewMarkTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IOverviewMarkTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIOverviewMarkTag)]
        [ContentType(ContentTypeIOverviewMarkTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeIOverviewMarkTagExtensionDefinition;

        // OverviewMarkTag /////////////////////////////////////////////////

        public const string ContentTypeOverviewMarkTag = "OverviewMarkTag";

        [Export]
        [Name(ContentTypeOverviewMarkTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition OverviewMarkTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeOverviewMarkTag)]
        [ContentType(ContentTypeOverviewMarkTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeOverviewMarkTagExtensionDefinition;

        // StructureTag /////////////////////////////////////////////////

        public const string ContentTypeStructureTag = "StructureTag";

        [Export]
        [Name(ContentTypeStructureTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition StructureTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeStructureTag)]
        [ContentType(ContentTypeStructureTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeStructureTagExtensionDefinition;

        // IUrlTag /////////////////////////////////////////////////

        public const string ContentTypeIUrlTag = "IUrlTag";

        [Export]
        [Name(ContentTypeIUrlTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition IUrlTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeIUrlTag)]
        [ContentType(ContentTypeIUrlTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeIUrlTagExtensionDefinition;

        // UrlTag /////////////////////////////////////////////////

        public const string ContentTypeUrlTag = "UrlTag";

        [Export]
        [Name(ContentTypeUrlTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition UrlTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeUrlTag)]
        [ContentType(ContentTypeUrlTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeUrlTagExtensionDefinition;

        // SpaceNegotiatingAdornmentTag /////////////////////////////////////////////////

        public const string ContentTypeSpaceNegotiatingAdornmentTag = "SpaceNegotiatingAdornmentTag";

        [Export]
        [Name(ContentTypeSpaceNegotiatingAdornmentTag)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition SpaceNegotiatingAdornmentTagContentTypeDefinition;

        [Export]
        [FileExtension("." + ContentTypeSpaceNegotiatingAdornmentTag)]
        [ContentType(ContentTypeSpaceNegotiatingAdornmentTag)]
        internal static FileExtensionToContentTypeDefinition ContentTypeSpaceNegotiatingAdornmentTagExtensionDefinition;
    }
}
