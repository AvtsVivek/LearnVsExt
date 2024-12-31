using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using System.Diagnostics;

namespace ITagTwo
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IUrlTag))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeThreeName)]
    public class HelloUrlTaggerThreeProvider : ITaggerProvider
    {
        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            Debug.WriteLine(GetType().FullName + " is called");
            return (ITagger<T>)new HelloUrlTaggerThree(buffer, TextSearchService);
        }
    }

}
