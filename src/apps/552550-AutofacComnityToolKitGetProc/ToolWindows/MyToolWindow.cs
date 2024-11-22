using Microsoft.VisualStudio.Imaging;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutofacComnityToolKitGetProc
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "My Tool Window";

        public override Type PaneType => typeof(Pane);

        public MyToolWindow()
        {
            
        }

        [Import]
        private MyToolWindowViewModel _myToolWindowViewModel { get; set; }

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            if (_myToolWindowViewModel == null)
            {
                throw new InvalidOperationException("View Model is null!!!");
            }

            return Task.FromResult<FrameworkElement>(new MyToolWindowControl(_myToolWindowViewModel));
        }

        [Guid("f9da0977-0ac7-4c13-a39d-1dc8666e9c8a")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}