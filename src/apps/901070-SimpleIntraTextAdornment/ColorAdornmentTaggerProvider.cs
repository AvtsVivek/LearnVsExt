using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using EnvDTE;

namespace SimpleIntraTextAdornment
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("text")]
    // [ContentType("projection")]
    [TagType(typeof(IntraTextAdornmentTag))]
    internal sealed class ColorAdornmentTaggerProvider : IViewTaggerProvider
    {
#pragma warning disable 649 // "field never assigned to" -- field is set by MEF.
        [Import]
        internal IBufferTagAggregatorFactoryService BufferTagAggregatorFactoryService;
#pragma warning restore 649

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (textView == null)
                throw new ArgumentNullException("textView");

            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (buffer != textView.TextBuffer)
                return null;

            //return GetTagger(
            //    (IWpfTextView)textView,
            //    new Lazy<ITagAggregator<ColorTag>>(
            //        () => BufferTagAggregatorFactoryService.CreateTagAggregator<ColorTag>(textView.TextBuffer)))
            //    as ITagger<T>;

            // var lazyColorTagAggregator = new Lazy<ITagAggregator<ColorTag>>(() => BufferTagAggregatorFactoryService.CreateTagAggregator<ColorTag>(textView.TextBuffer));
            
            var view = (IWpfTextView)textView;

            // var colorAdornmentTagger = view.Properties.GetOrCreateSingletonProperty(() => new ColorAdornmentTagger(view, BufferTagAggregatorFactoryService.CreateTagAggregator<ColorTag>(textView.TextBuffer)));
            
            var colorAdornmentTagger = new SimpleColorAdornmentTagger(view, BufferTagAggregatorFactoryService.CreateTagAggregator<ColorTag>(textView.TextBuffer));
            
            return colorAdornmentTagger as ITagger<T>;
        }

        //internal static ITagger<IntraTextAdornmentTag> GetTagger(IWpfTextView view, Lazy<ITagAggregator<ColorTag>> colorTagger)
        //{
        //    return view.Properties.GetOrCreateSingletonProperty(() => new ColorAdornmentTagger(view, colorTagger.Value));
        //}
    }
}
