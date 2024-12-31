using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;
using TagVarieties.Taggers;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TagVarieties.TaggerProviders
{
    [Export(typeof(IViewTaggerProvider))]
    [TagType(typeof(InterLineAdornmentTag))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeInterLineAdornmentTag)]
    public class InterLineAdornmentViewTaggerProvider : IViewTaggerProvider
    {
        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            Debug.WriteLine(GetType().FullName + " is called");
            return (ITagger<T>)new InterLineAdornmentTagger(TextSearchService);
        }
    }

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(InterLineAdornmentTag))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeInterLineAdornmentTag)]
    public class InterLineAdornmentTaggerProvider : ITaggerProvider
    {
        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            Debug.WriteLine(GetType().FullName + " is called");
            return (ITagger<T>)new InterLineAdornmentTagger(TextSearchService);
        }
    }
}
