using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProjectionBufferTut
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("cf61840b-fee0-4d3f-a6be-64c9e857e7ec")]
    public class ProjBufferToolWindow : ToolWindowPane, IOleCommandTarget
    {
        //CHANGE ME TO A FILE THAT YOU HAVE OPENED IN VISUAL STUDIO WHEN LAUNCHING THE TOOL WINDOW
        //CHANGE ME TO A FILE THAT YOU HAVE OPENED IN VISUAL STUDIO WHEN LAUNCHING THE TOOL WINDOW 

        private string filePath = @"C:\Users\koppviv\source\repos\LargeCsFile.cs";
        // private string filePath = @"C:\Users\UserName\source\repos\LargeCsFile.cs";
        // private string filePath = @"C:\Trials\Ex\LearnVsExt\src\apps\900960-ProjectionBufferTut\ProjectionBufferTut\ProjBufferToolWindow.cs";

        //CHANGE ME TO A FILE THAT YOU HAVE OPENED IN VISUAL STUDIO WHEN LAUNCHING THE TOOL WINDOW
        //CHANGE ME TO A FILE THAT YOU HAVE OPENED IN VISUAL STUDIO WHEN LAUNCHING THE TOOL WINDOW

        IComponentModel _componentModel;
        IVsInvisibleEditorManager _invisibleEditorManager;
        //This adapter allows us to convert between Visual Studio 2010 editor components and
        //the legacy components from Visual Studio 2008 and earlier.
        IVsEditorAdaptersFactoryService _editorAdapter;
        ITextEditorFactoryService _editorFactoryService;
        IVsTextView _currentlyFocusedTextView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjBufferToolWindow"/> class.
        /// </summary>
        public ProjBufferToolWindow() : base(null)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.Caption = "ProjBufferToolWindow";

            // This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
            // we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
            // the object returned by the Content property.


            this.BitmapResourceID = 301;
            this.BitmapIndex = 1;

            _componentModel = (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));
            
            _invisibleEditorManager = (IVsInvisibleEditorManager)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsInvisibleEditorManager));
            
            // var invisibleEditorManager2 = _componentModel.GetService<IVsInvisibleEditorManager>();
            _editorAdapter = _componentModel.GetService<IVsEditorAdaptersFactoryService>();
            _editorFactoryService = _componentModel.GetService<ITextEditorFactoryService>();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show(
                text: "File path is invalid. First set this to a large file(with lines more than say 200). Cannot continue",
                caption: "Invalid File path");
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                text: $"File path {filePath} is invalid. First set this to any large file. Cannot continue",
                caption: "Invalid File path");
            }
        }

        /// <summary>
        /// Creates an invisible editor for a given filePath. 
        /// If you're frequently creating projection buffers, it may be worth caching
        /// these editors as they're somewhat expensive to create.
        /// </summary>
        private IVsInvisibleEditor GetInvisibleEditor(string filePath)
        {
            IVsInvisibleEditor invisibleEditor;
            ErrorHandler.ThrowOnFailure(this._invisibleEditorManager.RegisterInvisibleEditor(
                filePath
                , pProject: null
                , dwFlags: (uint)_EDITORREGFLAGS.RIEF_ENABLECACHING
                , pFactory: null
                , ppEditor: out invisibleEditor));
            RegisterDocument(filePath);
            return invisibleEditor;
        }

        uint RegisterDocument(string targetFile)
        {
            //Then when creating the IVsInvisibleEditor, find and lock the document 
            uint itemID;
            IntPtr docData;
            uint docCookie;
            IVsHierarchy hierarchy;

            var runningDocTable = (IVsRunningDocumentTable)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsRunningDocumentTable));

            try
            {
                ErrorHandler.ThrowOnFailure(runningDocTable.FindAndLockDocument(
                dwRDTLockType: (uint)_VSRDTFLAGS.RDT_EditLock,
                pszMkDocument: targetFile,
                ppHier: out hierarchy,
                pitemid: out itemID,
                ppunkDocData: out docData,
                pdwCookie: out docCookie));
                return docCookie;
            }
            catch (COMException comException)
            {
                if (comException.Message.Contains("Catastrophic failure (Exception from HRESULT: 0x8000FFFF (E_UNEXPECTED))"))
                {
                    MessageBox.Show(
                    text: "First Open a file, then View -> Other Windows -> ProjBufferToolWindow",
                    caption: "No File Open");
                    return 0;
                }
                throw comException;
            }
        }

        private IWpfTextViewHost CreateEditor(string filePath, int start = 0, int end = 0, bool createProjectedEditor = false)
        {
            //IVsInvisibleEditors are in-memory represenations of typical Visual Studio editors.
            //Language services, highlighting and error squiggles are hooked up to these editors
            //for us once we convert them to WpfTextViews. 
            var invisibleEditor = GetInvisibleEditor(filePath);

            var docDataPointer = IntPtr.Zero;
            Guid guidIVsTextLines = typeof(IVsTextLines).GUID;

            ErrorHandler.ThrowOnFailure(invisibleEditor.GetDocData(
                fEnsureWritable: 1
                , riid: ref guidIVsTextLines
                , ppDocData: out docDataPointer));

            IVsTextLines docData = (IVsTextLines)Marshal.GetObjectForIUnknown(docDataPointer);

            // This will actually be defined as _codewindowbehaviorflags2.CWB_DISABLEDIFF once the latest version of
            // Microsoft.VisualStudio.TextManager.Interop.16.0.DesignTime is published. Setting the flag will have no effect
            // on releases prior to d16.0.
            const _codewindowbehaviorflags CWB_DISABLEDIFF = (_codewindowbehaviorflags)0x04;

            //Create a code window adapter
            var codeWindow = _editorAdapter.CreateVsCodeWindowAdapter(VisualStudioServices.OLEServiceProvider);

            // You need to disable the dropdown, splitter and -- for d16.0 -- diff since you are extracting the code window's TextViewHost and using it.
            ((IVsCodeWindowEx)codeWindow).Initialize((uint)_codewindowbehaviorflags.CWB_DISABLESPLITTER | (uint)_codewindowbehaviorflags.CWB_DISABLEDROPDOWNBAR | (uint)CWB_DISABLEDIFF,
                                                     VSUSERCONTEXTATTRIBUTEUSAGE.VSUC_Usage_Filter,
                                                     string.Empty,
                                                     string.Empty,
                                                     0,
                                                     new INITVIEW[1]);

            ErrorHandler.ThrowOnFailure(codeWindow.SetBuffer(docData));

            //Get a text view for our editor which we will then use to get the WPF control for that editor.
            IVsTextView textView;
            ErrorHandler.ThrowOnFailure(codeWindow.GetPrimaryView(out textView));

            if (createProjectedEditor)
            {
                //We add our own role to this text view. Later this will allow us to selectively modify
                //this editor without getting in the way of Visual Studio's normal editors.
                var roles = _editorFactoryService.DefaultRoles.Concat(new string[] { "CustomProjectionRole" });

                var vsTextBuffer = docData as IVsTextBuffer;
                var textBuffer = _editorAdapter.GetDataBuffer(vsTextBuffer);

                textBuffer.Properties.AddProperty("StartPosition", start);
                textBuffer.Properties.AddProperty("EndPosition", end);
                var guid = VSConstants.VsTextBufferUserDataGuid.VsTextViewRoles_guid;
                ((IVsUserData)codeWindow).SetData(ref guid, _editorFactoryService.CreateTextViewRoleSet(roles).ToString());
            }

            _currentlyFocusedTextView = textView;
            var textViewHost = _editorAdapter.GetWpfTextViewHost(textView);
            return textViewHost;
        }

        private IWpfTextViewHost _completeTextViewHost;
        public IWpfTextViewHost CompleteTextViewHost
        {
            get
            {
                if (_completeTextViewHost == null)
                {
                    _completeTextViewHost = CreateEditor(filePath);
                }
                return _completeTextViewHost;
            }
        }

        private IWpfTextViewHost _projectedTextViewHost;
        public IWpfTextViewHost ProjectedTextViewHost
        {
            get
            {
                if (_projectedTextViewHost == null)
                {
                    _projectedTextViewHost = CreateEditor(filePath, start: 0, end: 10, createProjectedEditor: true);
                }
                return _projectedTextViewHost;
            }
        }

        private ProjBufferToolWindowControl _myControl;
        public override object Content
        {
            get
            {
                if (_myControl == null)
                {
                    _myControl = new ProjBufferToolWindowControl();
                    _myControl.fullFile.Content = CompleteTextViewHost.HostControl; 
                    _myControl.partialFile.Content = ProjectedTextViewHost.HostControl;
                }
                return _myControl;
            }
        }

        public override void OnToolWindowCreated()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            //We need to set up the tool window to respond to key bindings
            //They're passed to the tool window and its buffers via Query() and Exec()
            var windowFrame = (IVsWindowFrame)Frame;
            var cmdUi = Microsoft.VisualStudio.VSConstants.GUID_TextEditorFactory;
            windowFrame.SetGuidProperty((int)__VSFPROPID.VSFPROPID_InheritKeyBindings, ref cmdUi);
            base.OnToolWindowCreated();
        }

        protected override bool PreProcessMessage(ref Message m)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (CompleteTextViewHost != null)
            {
                // copy the Message into a MSG[] array, so we can pass
                // it along to the active core editor's IVsWindowPane.TranslateAccelerator
                var pMsg = new MSG[1];
                pMsg[0].hwnd = m.HWnd;
                pMsg[0].message = (uint)m.Msg;
                pMsg[0].wParam = m.WParam;
                pMsg[0].lParam = m.LParam;

                var vsWindowPane = (IVsWindowPane)_currentlyFocusedTextView;
                return vsWindowPane.TranslateAccelerator(pMsg) == 0;
            }
            return base.PreProcessMessage(ref m);
        }

        int IOleCommandTarget.Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var hr =
              (int)Microsoft.VisualStudio.OLE.Interop.Constants.OLECMDERR_E_NOTSUPPORTED;

            if (_currentlyFocusedTextView != null)
            {
                var cmdTarget = (IOleCommandTarget)_currentlyFocusedTextView;
                hr = cmdTarget.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
            }
            return hr;
        }

        int IOleCommandTarget.QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var hr =
              (int)Microsoft.VisualStudio.OLE.Interop.Constants.OLECMDERR_E_NOTSUPPORTED;

            if (_currentlyFocusedTextView != null)
            {
                var cmdTarget = (IOleCommandTarget)_currentlyFocusedTextView;
                hr = cmdTarget.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
            }
            return hr;
        }

    }
}
