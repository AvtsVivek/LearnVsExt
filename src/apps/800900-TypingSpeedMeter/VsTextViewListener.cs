using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace TypingSpeedMeter
{
    [Export(typeof(IVsTextViewCreationListener))]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    [ContentType("text")]
    internal sealed class VsTextViewListener : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService = null;

        public VsTextViewListener()
        {
            Debugger.Break();
        }

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            Debugger.Break();
            ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            if (textView == null)
                return;

            var adornment = textView.Properties.GetProperty<TypingSpeedMeter>(typeof(TypingSpeedMeter));

            textView.Properties.GetOrCreateSingletonProperty(
                () => new TypeCharFilter(textViewAdapter, textView, adornment));
        }
    }
}
