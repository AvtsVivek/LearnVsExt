using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace SmartTagExOne
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("text")]
    [Order(Before = "default")]
    [TagType(typeof(ErrorTag))]
    internal class TestSmartTaggerProvider : IViewTaggerProvider
    {
        [Import(typeof(ITextStructureNavigatorSelectorService))]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (buffer == null || textView == null)
            {
                return null;
            }

            //make sure we are tagging only the top buffer
            if (buffer == textView.TextBuffer)
            {
                return new TestSmartTagger(buffer, textView, NavigatorService) as ITagger<T>;
            }
            else return null;
        }
    }
}
