namespace LinqLanguageServiceIntro
{
    [Command(PackageIds.LinqToolWindowCommandId)]
    internal sealed class LinqOptionsToolWindowCommand : BaseCommand<LinqOptionsToolWindowCommand>
    {
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            return LinqToolWindow.ShowAsync();
        }
    }
}
