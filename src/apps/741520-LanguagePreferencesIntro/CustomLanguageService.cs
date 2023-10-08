using EnvDTE;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design.Serialization;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.ComponentModelHost;

namespace LanguagePreferencesIntro
{

    [ComVisible(true)]
    internal class CustomLanguageService : LanguageService, IVsEditorFactory
    {
        private readonly Microsoft.VisualStudio.Shell.Package _package;
        private readonly Guid _languageServiceId;

        public CustomLanguageService(Microsoft.VisualStudio.Shell.Package package)
        {
            _package = package;
            _languageServiceId = GetType().GUID;
        }

        public override string Name => "Aabbcc";

        public override string GetFormatFilterList()
        {
            throw new System.NotImplementedException();
        }

        public override LanguagePreferences GetLanguagePreferences()
        {
            var languagePreferences = new LanguagePreferences(base.Site, _languageServiceId, Name);
            languagePreferences.WordWrap = true;
            languagePreferences.Init();
            return languagePreferences;
        }

        public override IScanner GetScanner(IVsTextLines buffer)
        {
            throw new System.NotImplementedException();
        }

        public override AuthoringScope ParseSource(ParseRequest req)
        {
            throw new System.NotImplementedException();
        }
        protected virtual bool PromptEncodingOnLoad => false;

        public virtual int CreateEditorInstance(uint grfCreateDoc, string pszMkDocument, string pszPhysicalView, IVsHierarchy pvHier, uint itemid, IntPtr punkDocDataExisting, out IntPtr ppunkDocView,         out IntPtr ppunkDocData,         out string pbstrEditorCaption,      out Guid pguidCmdUI, out int pgrfCDW)    
        {
            ThreadHelper.ThrowIfNotOnUIThread("CreateEditorInstance");
            ppunkDocView = IntPtr.Zero;
            ppunkDocData = IntPtr.Zero;
            pguidCmdUI = Guid.Empty;
            pgrfCDW = 0;
            pbstrEditorCaption = null;
            if ((grfCreateDoc & 6) == 0)
            {
                return -2147024809;
            }

            if (punkDocDataExisting != IntPtr.Zero && PromptEncodingOnLoad)
            {
                return -2147213334;
            }

            IVsTextLines textBuffer = GetTextBuffer(punkDocDataExisting, pszMkDocument);
            if (punkDocDataExisting != IntPtr.Zero)
            {
                ppunkDocData = punkDocDataExisting;
                Marshal.AddRef(ppunkDocData);
            }
            else
            {
                ppunkDocData = Marshal.GetIUnknownForObject(textBuffer);
            }

            try
            {
                object o = CreateDocumentView(pszMkDocument, pszPhysicalView, pvHier, itemid, textBuffer, punkDocDataExisting == IntPtr.Zero, out pbstrEditorCaption, out pguidCmdUI);
                ppunkDocView = Marshal.GetIUnknownForObject(o);
            }
            finally
            {
                if (ppunkDocView == IntPtr.Zero && punkDocDataExisting != ppunkDocData && ppunkDocData != IntPtr.Zero)
                {
                    Marshal.Release(ppunkDocData);
                    ppunkDocData = IntPtr.Zero;
                }
            }

            return 0;
        }

        private object CreateDocumentView(string documentMoniker, string physicalView, IVsHierarchy hierarchy, uint itemid, IVsTextLines textLines, bool createdDocData, out string editorCaption, out Guid cmdUI)
        {
            ThreadHelper.ThrowIfNotOnUIThread("CreateDocumentView");
            editorCaption = string.Empty;
            cmdUI = Guid.Empty;
            if (string.IsNullOrEmpty(physicalView))
            {
                return CreateCodeView(documentMoniker, textLines, createdDocData, ref editorCaption, ref cmdUI);
            }

            throw Marshal.GetExceptionForHR(-2147213333);
        }
        

        private IVsCodeWindow CreateCodeView(string documentMoniker, IVsTextLines textLines, bool createdDocData, ref string editorCaption, ref Guid cmdUI)
        {
            ThreadHelper.ThrowIfNotOnUIThread("CreateCodeView");
            if (_serviceProvider == null)
            {
                throw new Exception("ServiceProvider can't be null");
            }

            var componentModel = (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var vsEditorAdaptersFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            IVsCodeWindow vsCodeWindow = vsEditorAdaptersFactoryService.CreateVsCodeWindowAdapter((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)_serviceProvider.GetService(typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider)));

            // IVsCodeWindow vsCodeWindow = VS.GetMefService<IVsEditorAdaptersFactoryService>().CreateVsCodeWindowAdapter((Microsoft.VisualStudio.OLE.Interop.IServiceProvider)_serviceProvider!.GetService(typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider)));
            ErrorHandler.ThrowOnFailure(vsCodeWindow.SetBuffer(textLines));
            ErrorHandler.ThrowOnFailure(vsCodeWindow.SetBaseEditorCaption(null));
            ErrorHandler.ThrowOnFailure(vsCodeWindow.GetEditorCaption(READONLYSTATUS.ROSTATUS_Unknown, out editorCaption));
            if (textLines is IVsUserData vsUserData && PromptEncodingOnLoad)
            {
                Guid riidKey = VSConstants.VsTextBufferUserDataGuid.VsBufferEncodingPromptOnLoad_guid;
                vsUserData.SetData(ref riidKey, 1u);
            }

            cmdUI = VSConstants.GUID_TextEditorFactory;
            if (!createdDocData && textLines != null)
            {
                new TextBufferEventListener(textLines, _languageServiceId).OnLoadCompleted(0);
            }

            return vsCodeWindow;
        }

        private ServiceProvider _serviceProvider;

        protected virtual IVsTextLines GetTextBuffer(IntPtr docDataExisting, string filename)
        {
            ThreadHelper.ThrowIfNotOnUIThread("GetTextBuffer");
            IVsTextLines ppTextBuffer;
            if (docDataExisting == IntPtr.Zero)
            {
                Type typeFromHandle = typeof(IVsTextLines);
                Guid iid = typeFromHandle.GUID;
                Guid clsid = typeof(VsTextBufferClass).GUID;
                ppTextBuffer = _package.CreateInstance(ref clsid, ref iid, typeFromHandle) as IVsTextLines;
                ((IObjectWithSite)ppTextBuffer).SetSite(_serviceProvider?.GetService(typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider)));
            }
            else
            {
                object objectForIUnknown = Marshal.GetObjectForIUnknown(docDataExisting);
                ppTextBuffer = objectForIUnknown as IVsTextLines;
                if (ppTextBuffer == null && objectForIUnknown is IVsTextBufferProvider vsTextBufferProvider)
                {
                    vsTextBufferProvider.GetTextBuffer(out ppTextBuffer);
                }

                if (ppTextBuffer == null)
                {
                    throw Marshal.GetExceptionForHR(-2147213334);
                }
            }

            return ppTextBuffer;
        }

        public int SetSite(Microsoft.VisualStudio.OLE.Interop.IServiceProvider psp)
        {
            _serviceProvider = new ServiceProvider(psp);
            return 0;
        }

        public int Close()
        {
            return 0;
        }

        public int MapLogicalView(ref Guid rguidLogicalView, out string pbstrPhysicalView)
        {
            pbstrPhysicalView = null;
            bool flag = false;
            if (VSConstants.LOGVIEWID_Primary == rguidLogicalView || VSConstants.LOGVIEWID_Debugging == rguidLogicalView || VSConstants.LOGVIEWID_Code == rguidLogicalView || VSConstants.LOGVIEWID_UserChooseView == rguidLogicalView || VSConstants.LOGVIEWID_TextView == rguidLogicalView)
            {
                flag = true;
            }
            else if (VSConstants.LOGVIEWID_Designer == rguidLogicalView)
            {
                pbstrPhysicalView = "Design";
                flag = true;
            }

            if (flag)
            {
                return 0;
            }

            return -2147467263;
        }
    }


}