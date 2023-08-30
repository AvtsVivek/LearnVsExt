using MyFirstVisualizer;

namespace MyTestConsole
{
    internal class TestConsole
    {
        static void Main(string[] args)
        {
            var myString = "Hello, World";
            DebuggerSide.TestShowVisualizer(myString);
        }
    }
}
