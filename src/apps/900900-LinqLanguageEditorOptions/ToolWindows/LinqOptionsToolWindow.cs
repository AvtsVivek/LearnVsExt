using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LinqLanguageEditorOptions
{
    public class LinqOptionsToolWindow : BaseToolWindow<LinqOptionsToolWindow>
    {
        public override string GetTitle(int toolWindowId) => Constants.LinqEditorToolWindowTitle;

        public override Type PaneType => typeof(Pane);

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new LinqOptionsToolWindowControl());
        }

        [Guid("27550092-a995-4ea8-8934-ac9d1536ae99")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}