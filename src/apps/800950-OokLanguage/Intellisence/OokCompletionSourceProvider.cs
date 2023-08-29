using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace OokLanguage.Intellisence
{
    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("ook!")]
    [Name("ookCompletion")]
    class OokCompletionSourceProvider : ICompletionSourceProvider
    {
        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new OokCompletionSource(textBuffer);
        }
    }

}
