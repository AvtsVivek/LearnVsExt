using Microsoft.VisualStudio.Text;

namespace VsixCommunityInsertGuid
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await Package.JoinableTaskFactory.SwitchToMainThreadAsync();
            
            var activeDocumentView = await VS.Documents.GetActiveDocumentViewAsync();

            if (activeDocumentView?.TextView == null)
            {
                var messageBox = new MessageBox();
                await messageBox.ShowErrorAsync("Active Doc null!!", "Active Document is null." + Environment.NewLine + "Likely no file is opened in the editor.");
                return;
            }
            SnapshotPoint position = activeDocumentView.TextView.Caret.Position.BufferPosition;
            
            activeDocumentView.TextBuffer?.Insert(position, Guid.NewGuid().ToString());
        }
    }
}
