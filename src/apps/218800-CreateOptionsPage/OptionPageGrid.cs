using Microsoft.VisualStudio.Shell;
using System.ComponentModel;

namespace CreateOptionsPage
{
    public class OptionPageGrid : DialogPage
    {
        private int optionInt = 256;

        [Category("My int Category")]
        [DisplayName("My Integer Option")]
        [Description("An option of the type integer")]
        public int OptionInteger
        {
            get { return optionInt; }
            set { optionInt = value; }
        }
    }
}
