using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutofacComnityToolKitNotWorking
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public override string GetTitle(int toolWindowId) => "My Tool Window";

        public override Type PaneType => typeof(Pane);

        private readonly MyToolWindowControl _myToolWindowControl;

        public MyToolWindow()
        {

        }

        // The following ctor with a parameter does not work.
        // Thats because DI does not work.
        // Only a parameterless ctor(above) works.
        public MyToolWindow(MyToolWindowControl myToolWindowControl)
        {
            _myToolWindowControl = myToolWindowControl;
        }

        public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<FrameworkElement>(new MyToolWindowControl());

            // The following does not work, because, _myToolWindowControl is null.
            // Thats because, _myToolWindowControl is not injected, DI simply does not work.
            // return Task.FromResult<FrameworkElement>(_myToolWindowControl);
        }

        [Guid("4055b93d-3474-45c3-a1c1-ccd3b882c917")]
        internal class Pane : ToolkitToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }
    }
}