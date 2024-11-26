using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
// using System.Windows;

namespace CommunityToolKitGetProcess
{
    public class MyToolWindow : BaseToolWindow<MyToolWindow>
    {
        public bool IsSolutionWithProjectsOpenedInVs { get; set; } = false;
        public static DTE2 DteTwoInstance
        {
            get;
            private set;
        }

        public MyToolWindow()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            // VS.MessageBox.Show("Tool Window activated");

        }
        public override string GetTitle(int toolWindowId) => "My Tool Window";

        public override Type PaneType => typeof(Pane);

        public override Task<System.Windows.FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
        {
            return Task.FromResult<System.Windows.FrameworkElement>(new MyToolWindowView());
        }

        [Guid("26d9d120-54cd-48e3-aaa9-5872af860726")]
        internal class Pane : ToolWindowPane
        {
            public Pane()
            {
                BitmapImageMoniker = KnownMonikers.ToolWindow;
            }
        }



    }
}