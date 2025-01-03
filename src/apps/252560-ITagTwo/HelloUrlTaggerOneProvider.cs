using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace ITagTwo
{
    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IUrlTag))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeOneName)]
    public class HelloUrlTaggerOneProvider : ITaggerProvider
    {
        private static int _helloUrlTaggerOneProviderCreateTaggerCallCount = 0;
        private static int _helloUrlTaggerOneProviderCtorCallCount = 0;

        public HelloUrlTaggerOneProvider()
        {
            _helloUrlTaggerOneProviderCtorCallCount++;
            Debug.WriteLine(GetType().FullName + " Constructor is called. Count: " + _helloUrlTaggerOneProviderCtorCallCount);
        }

        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            _helloUrlTaggerOneProviderCreateTaggerCallCount++;
            Debug.WriteLine(GetType().FullName + " CreateTagger is called. Count is: " + _helloUrlTaggerOneProviderCreateTaggerCallCount);
            // return (ITagger<T>)new HelloUrlTaggerOne(TextSearchService);
            return buffer.Properties.GetOrCreateSingletonProperty(
                () => new HelloUrlTaggerOne(TextSearchService)) 
                as ITagger<T>;
        }
    }
}
