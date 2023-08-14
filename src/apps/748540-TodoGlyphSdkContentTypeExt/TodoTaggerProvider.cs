using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;

namespace ToDoGlyphSdkContentTypeExt
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("hid")]
    [TagType(typeof(ToDoTag))]
    public class ToDoTaggerProvider : ITaggerProvider
    {
        public ToDoTaggerProvider()
        {

        }

        [Import]
        internal IClassifierAggregatorService AggregatorService;
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            var classifier = AggregatorService.GetClassifier(buffer);

            return new ToDoTagger(classifier) as ITagger<T>;
        }
    }
}
