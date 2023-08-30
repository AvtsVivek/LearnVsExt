using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;


[assembly: System.Diagnostics.DebuggerVisualizer(typeof(MyFirstVisualizer.DebuggerSide), typeof(VisualizerObjectSource),
    Target = typeof(MyDataObject.CustomDataObject), Description = "My First Visualizer")]

namespace MyFirstVisualizer
{
    public class DebuggerSide : DialogDebuggerVisualizer
    {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            // MessageBox.Show(objectProvider.GetObject().ToString());
            var data = objectProvider.GetObject() as MyDataObject.CustomDataObject;

            // You can replace displayForm with your own custom Form or Control.  
            Form displayForm = new Form();
            displayForm.Text = data.MyData;
            windowService.ShowDialog(displayForm);
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSide));
            visualizerHost.ShowVisualizer();
        }
    }
}
