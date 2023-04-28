using Microsoft.VisualStudio.Text;

namespace VsixCommandCommunity
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();
            
            DocumentView documentView = await VS.Documents.GetActiveDocumentViewAsync();
            
            if (documentView?.TextView == null) 
                return;
            
            SnapshotPoint position = documentView.TextView.Caret.Position.BufferPosition;
            
            documentView.TextBuffer?.Insert(position, Guid.NewGuid().ToString());
        }
    }
}
