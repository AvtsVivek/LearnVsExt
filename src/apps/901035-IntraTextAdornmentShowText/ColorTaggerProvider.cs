using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace IntraTextAdornmentShowText
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("text")]
    [TagType(typeof(ColorTag))]
    internal sealed class ColorTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            return buffer.Properties.GetOrCreateSingletonProperty(() => new ColorTagger(buffer)) as ITagger<T>;
            // Instead of the above, the following also works. Not sure which is correct. 
            // Asked an so question here. https://stackoverflow.com/q/79322867/1977871.
            // Need to wait for the answer. 
            // return new ColorTagger(buffer) as ITagger<T>;
        }
    }
}
