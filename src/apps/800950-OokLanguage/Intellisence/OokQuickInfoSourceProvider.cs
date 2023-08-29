using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Tagging;

namespace OokLanguage.Intellisence
{
    [Export(typeof(IQuickInfoSourceProvider))]
    [ContentType("ook!")]
    [Name("ookQuickInfo")]
    class OokQuickInfoSourceProvider : IQuickInfoSourceProvider
    {

        [Import]
        IBufferTagAggregatorFactoryService aggService = null;

        public IQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
        {
            return new OokQuickInfoSource(textBuffer, aggService.CreateTagAggregator<OokTokenTag>(textBuffer));
        }
    }

}
