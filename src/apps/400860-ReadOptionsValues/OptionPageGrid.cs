using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace ReadOptionsValues
{
    public class OptionPageGrid : DialogPage
    {
        private int optionInt = 256;

        //[Category("My Category")]
        //[DisplayName("My Integer Option")]
        //[Description("My integer option")]
        public int OptionInteger
        {
            get { return optionInt; }
            set { optionInt = value; }
        }
    }
}
