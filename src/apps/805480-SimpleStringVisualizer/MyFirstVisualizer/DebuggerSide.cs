using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;


[assembly: System.Diagnostics.DebuggerVisualizer(typeof(MyFirstVisualizer.DebuggerSide), typeof(VisualizerObjectSource),
    Target = typeof(string), Description = "My First Visualizer")]

namespace MyFirstVisualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            MessageBox.Show(objectProvider.GetObject().ToString());
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSide));
            visualizerHost.ShowVisualizer();
        }
    }
}
