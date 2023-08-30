using MyDataObject;
using MyFirstVisualizer;

namespace MyTestConsole
{
    internal class TestConsole
    {
        static void Main(string[] args)
        {
            var customDataObject = new CustomDataObject();

            DebuggerSide.TestShowVisualizer(customDataObject);
        }
    }
}
