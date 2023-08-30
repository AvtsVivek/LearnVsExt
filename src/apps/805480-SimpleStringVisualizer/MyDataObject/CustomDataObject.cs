using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataObject
{
    [Serializable]
    public class CustomDataObject
    {
        public CustomDataObject()
        {
            this.MyData = "MyTestData";
        }
        public string MyData { get; set; }
    }
}
