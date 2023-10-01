namespace LinqLanguageEditor
{
    [Command(PackageIds.LinqToolWindowCommand)]
    internal sealed class LinqToolWindowCommand : BaseCommand<LinqToolWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return LinqToolWindow.ShowAsync();
        }
    }
}
