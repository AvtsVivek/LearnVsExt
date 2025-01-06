using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;
using TagVarieties.Taggers;
using Microsoft.VisualStudio.Text.Editor;

namespace TagVarieties.TaggerProviders
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IntraTextAdornmentTag))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeIntraTextAdornmentTag)]
    public class IntraTextAdornmentTaggerProvider : ITaggerProvider
    {
        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            Debug.WriteLine(GetType().FullName + " is called");
            return (ITagger<T>)new IntraTextAdornmentTagger(TextSearchService);
        }
    }
}
