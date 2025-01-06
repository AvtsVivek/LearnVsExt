using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TagAggregatorIntro
{
    [Export(typeof(ITaggerProvider))]
    // [TagType(typeof(IUrlTag))]
    [TagType(typeof(HelloTagOne))]
    [ContentType(ContentTypeDefsAndExtAssociations.ContentTypeTagOneName)]
    public class HelloTagOneTaggerProvider : ITaggerProvider
    {
        private static int _helloTagOneTaggerProviderCreateTaggerCallCount = 0;

        private static int _helloTagOneTaggerProviderCtorCallCount = 0;

        public HelloTagOneTaggerProvider()
        {
            _helloTagOneTaggerProviderCtorCallCount++;
            Debug.WriteLine(GetType().FullName + " Constructor is called. Count: " + _helloTagOneTaggerProviderCtorCallCount);
        }

        [Import]
        public ITextSearchService2 TextSearchService { get; set; }

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            _helloTagOneTaggerProviderCreateTaggerCallCount++;
            Debug.WriteLine(GetType().FullName + " CreateTagger is called. Count is: " + _helloTagOneTaggerProviderCreateTaggerCallCount);
            return buffer.Properties.GetOrCreateSingletonProperty(
                () => new HelloTagOneTagger()) as ITagger<T>;
        }
    }
}
