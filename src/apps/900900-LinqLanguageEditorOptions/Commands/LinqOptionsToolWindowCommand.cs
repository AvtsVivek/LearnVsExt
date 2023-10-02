namespace LinqLanguageEditorOptions
{
    [Command(PackageIds.LinqOptionsToolWindowCommandId)]
    internal sealed class LinqOptionsToolWindowCommand : BaseCommand<LinqOptionsToolWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return LinqOptionsToolWindow.ShowAsync();
        }
    }
}
