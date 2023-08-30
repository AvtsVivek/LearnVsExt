using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataObject;
using MyFirstVisualizer;

namespace MyTestConsole
{
    internal class TestConsole
    {
        static void Main(string[] args)
        {
            // var myString = "Hello, World";
            // DebuggerSide.TestShowVisualizer(myString);

            var customDataObject = new CustomDataObject();

            DebuggerSide.TestShowVisualizer(customDataObject);
        }
    }
}
