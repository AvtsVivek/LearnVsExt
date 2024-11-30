using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.TextManager.Interop;

namespace CaretPositionOnToolWindow.Services
{
    public interface IDocumentService
    {
        IVsTextView GetActiveVsTextViewWithFocus();

        IVsTextView GetActiveVsTextViewWithoutFocus();

        IWpfTextView GetWpfTextView();

        ITextView GetTextView();

        DocumentView GetDocumentView();

        ITextDocument GetDocument();

        bool IsAnyFileOpen();

        IWpfTextView GetWpfTextViewOldWay();
    }
    public class DocumentService : IDocumentService
    {
        private IVsTextManager _textManager;
        public DocumentService() { }
        public bool IsAnyFileOpen()
        {
            var textView = GetActiveVsTextViewWithFocus();

            if (textView != null)
            {
                return true;
            }
            
            return false;
        }

        public IVsTextView GetActiveVsTextViewWithoutFocus()
        {
            var vsTextManager = GetTextManager();

            int mustHaveFocus = 0;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            return vsTextView;
        }

        public IVsTextView GetActiveVsTextViewWithFocus()
        {
            var vsTextManager = GetTextManager();

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            return vsTextView;
        }

        private IVsTextManager GetTextManager()
        {
            var vsTextManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));

            return vsTextManager;
        }

        public IWpfTextView GetWpfTextView()
        {
            IVsTextView vsTextView = GetActiveVsTextViewWithFocus();

            if (vsTextView == null)
                return null;

            IWpfTextView wpfTextView = vsTextView.ToIWpfTextView();

            return wpfTextView;
        }

        public ITextView GetTextView()
        {
            IVsTextView vsTextView = GetActiveVsTextViewWithFocus();

            if (vsTextView == null)
                return null;

            IWpfTextView wpfTextView = vsTextView.ToIWpfTextView();

            return wpfTextView;
        }

        public ITextView2 GetTextView2()
        {
            IVsTextView vsTextView = GetActiveVsTextViewWithFocus();

            if (vsTextView == null)
                return null;

            IWpfTextView wpfTextView = vsTextView.ToIWpfTextView();

            if (wpfTextView == null)
                return null;

            ITextView2 textView2 = wpfTextView as ITextView2;

            return textView2;
        }

        public DocumentView GetDocumentView()
        {
            IVsTextView vsTextView = GetActiveVsTextViewWithFocus();

            if (vsTextView == null)
                return null;

            IWpfTextView wpfTextView = vsTextView.ToIWpfTextView();

            if (wpfTextView == null)
                return null;

            var documentView = vsTextView.ToDocumentView();

            return documentView;            
        }

        public ITextDocument GetDocument()
        {
            DocumentView documentView = GetDocumentView();

            if (documentView == null)
                return null;

            ITextDocument textDocument = documentView.Document;

            return textDocument;
        }

        public IWpfTextView GetWpfTextViewOldWay()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            if (componentModel == null)
                return null;

            // The following is working. Its returning a non null settings manager. 
            // But this is not used in this example. 
            //var textStructureNavigatorSelectorService = componentModel.GetService<ITextStructureNavigatorSelectorService>();

            //var textSearchService = componentModel.GetService<ITextSearchService>();

            // Need ensure the following is not null.
            // var vsTextManagerFromComponentModel = componentModel.GetService<IVsTextManager>();

            // Is the above and (vsTextManagerFromComponentModel) and below (vsTextManager) same?
            // Need to check
            var vsTextManager = (IVsTextManager)Package.GetGlobalService(typeof(SVsTextManager));

            int mustHaveFocus = 1;

            vsTextManager.GetActiveView(mustHaveFocus, null, out IVsTextView vsTextView);

            

            if (vsTextView == null)
            {
                return null;
            }
            else
            {
                // SomeFileIsOpen = true;
            }

            vsTextView.GetBuffer(out IVsTextLines currentDocTextLines);

            var wpfTextViewDirectly = vsTextView.ToIWpfTextView();

            var docView = vsTextView.ToDocumentView();

            var vsTextBuffer = currentDocTextLines as IVsTextBuffer;

            var persistFileFormat = vsTextBuffer as IPersistFileFormat;

            persistFileFormat.GetCurFile(out string filePath, out uint pnFormatIndex);

            vsTextBuffer.GetLineCount(out var lineCount);

            //MessageNotes = $"The number of lines: {lineCount}" + Environment.NewLine +
            //    $"The file path is as follows:" + Environment.NewLine + filePath;

            //VsShellUtilities.ShowMessageBox(
            //    this.package,
            //    $"The number of lines: {lineCount}" + Environment.NewLine +
            //    $"The file path is as follows:" + Environment.NewLine +
            //    filePath
            //    ,
            //    "File Details",
            //    OLEMSGICON.OLEMSGICON_INFO,
            //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            for (int i = 0; i < lineCount; i++)
            {
                currentDocTextLines.GetLengthOfLine(i, out int lineSize);

                //VsShellUtilities.ShowMessageBox(
                //    this.package,
                //    $"Length of line {lineSize}",
                //    $"Data of line {i}",
                //    OLEMSGICON.OLEMSGICON_INFO,
                //    OLEMSGBUTTON.OLEMSGBUTTON_OK,
                //    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            }

            // I am not sure what this language service id is
            vsTextBuffer.GetLanguageServiceID(out var languageServiceID);

            // Need to understand iLineCount and index
            vsTextBuffer.GetLastLineIndex(out int iLineCount, out int iLineIndex);

            // What is this size?
            vsTextBuffer.GetSize(out var size);

            // What is this totalLength? How is this different from size above?
            currentDocTextLines.GetSize(out var totalLength);

            /////////////////////////////////////////////////////////////////////////////

            // What the hell is this Guid?
            var viewHostGuid = Microsoft.VisualStudio.Editor.DefGuidList.guidIWpfTextViewHost;

            // What is this userData?
            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.textmanager.interop.ivsuserdata
            var vsUserData = vsTextView as IVsUserData;

            if (vsUserData == null)
                return null;

            vsUserData.GetData(ref viewHostGuid, out object holder);

            if (holder == null)
                return null;

            // https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.editor.iwpftextviewhost
            var wpfTextViewHost = (IWpfTextViewHost)holder;

            var wpfTextView = wpfTextViewHost.TextView;

            return wpfTextView;
        }
    }
}
