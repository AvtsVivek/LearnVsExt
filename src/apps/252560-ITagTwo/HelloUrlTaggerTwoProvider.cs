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
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeTwoName)]
    public class HelloUrlTaggerTwoProvider : ITaggerProvider
    {
        private static int _helloUrlTaggerTwoProviderCreateTaggerCallCount = 0;
        private static int _helloUrlTaggerTwoProviderCtorCallCount = 0;
        public HelloUrlTaggerTwoProvider()
        {
            _helloUrlTaggerTwoProviderCtorCallCount++;
            Debug.WriteLine(GetType().FullName + " Constructor is called. Count: " + _helloUrlTaggerTwoProviderCtorCallCount);
        }
        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            _helloUrlTaggerTwoProviderCreateTaggerCallCount++;
            Debug.WriteLine(GetType().FullName + " CreateTagger is called. Count is: " + _helloUrlTaggerTwoProviderCreateTaggerCallCount);
            return (ITagger<T>)new HelloUrlTaggerTwo(TextSearchService);
        }
    }
}
