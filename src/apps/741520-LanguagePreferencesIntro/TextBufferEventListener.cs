using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using System;

namespace LanguagePreferencesIntro
{
    public sealed class TextBufferEventListener : IVsTextBufferDataEvents
    {
        private readonly IVsTextLines _textLines;

        private readonly IConnectionPoint _connectionPoint;

        private readonly uint _cookie;

        private Guid _languageServiceId;

        public TextBufferEventListener(IVsTextLines textLines, Guid languageServiceId)
        {
            ThreadHelper.ThrowIfNotOnUIThread(".ctor");
            _textLines = textLines;
            _languageServiceId = languageServiceId;
            IConnectionPointContainer obj = textLines as IConnectionPointContainer;
            Guid riid = typeof(IVsTextBufferDataEvents).GUID;
            obj?.FindConnectionPoint(ref riid, out _connectionPoint);
            _connectionPoint.Advise(this, out _cookie);
        }

        public void OnFileChanged(uint grfChange, uint dwFileAttrs)
        {
        }

        public int OnLoadCompleted(int fReload)
        {
            ThreadHelper.ThrowIfNotOnUIThread("OnLoadCompleted");
            _connectionPoint.Unadvise(_cookie);
            _textLines.SetLanguageServiceID(ref _languageServiceId);
            return 0;
        }
    }


}