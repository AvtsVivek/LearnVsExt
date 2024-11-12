using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;

namespace TodoGlyphTest
{
    /// <summary>
    /// Export a <see cref="ITaggerProvider"/>
    /// </summary>
    [Export(typeof(ITaggerProvider))]
    [ContentType("code")]
    [TagType(typeof(TodoTag))]
    public class TodoTaggerProvider : ITaggerProvider
    {
        public TodoTaggerProvider()
        {

        }
        /// <summary>
        /// Creates an instance of our custom TodoTagger for a given buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">The buffer we are creating the tagger for.</param>
        /// <returns>An instance of our custom TodoTagger.</returns>
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
