using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace TaggerInTextModel
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("code")]
    [TagType(typeof(TodoTag))]
    public class TodoTaggerProvider : ITaggerProvider
    {
        public TodoTaggerProvider()
        {

        }

        [Import]
        internal IClassifierAggregatorService AggregatorService;
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            var classifier = AggregatorService.GetClassifier(buffer);

            return new TodoTagger(classifier) as ITagger<T>;
        }
    }
}
