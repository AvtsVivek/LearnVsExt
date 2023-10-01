using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LinqLanguageEditor
{
    public class LinqToolWindow : BaseToolWindow<LinqToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "LINQ Query Tool Window";

        public override Type PaneType => typeof(Pane);

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new LinqToolWindowControl());
        }

        [Guid("60312794-c844-4330-bd50-d5b10215f8e9")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}