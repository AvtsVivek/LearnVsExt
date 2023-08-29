using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace OokLanguage.Intellisence
{
    //[Export(typeof(IWpfTextViewCreationListener))]
    //[ContentType("ook!")]
    //[TextViewRole(PredefinedTextViewRoles.Interactive)]
    //internal sealed class WpfTextViewCreationListener : IWpfTextViewCreationListener
    //{
    //    [Import]
    //    IVsEditorAdaptersFactoryService AdaptersFactory = null;

    //    [Import]
    //    ICompletionBroker CompletionBroker = null;

    //    public void TextViewCreated(IWpfTextView textView)
    //    {
    //        Debug.Assert(textView != null);

    //        IVsTextView vsTextView = AdaptersFactory.GetViewAdapter(textView);

    //        CommandFilter filter = new CommandFilter(textView, CompletionBroker);

    //        IOleCommandTarget next;
    //        vsTextView.AddCommandFilter(filter, out next);
    //        filter.Next = next;
    //    }
    //}
}
