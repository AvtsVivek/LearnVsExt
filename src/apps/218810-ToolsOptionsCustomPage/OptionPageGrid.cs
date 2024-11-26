using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace ToolsOptionsCustomPage
{
    public class OptionPageGrid : DialogPage
    {
        private int optionInt = 300; //256;

        // [Category("My Category")]
        // [DisplayName("My Integer Option")]
        // [Description("My integer option")]
        public int OptionInteger
        {
            get { return optionInt; }
            set { optionInt = value; }
        }
    }
}
