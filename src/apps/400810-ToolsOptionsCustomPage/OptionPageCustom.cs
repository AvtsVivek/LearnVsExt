using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ToolsOptionsCustomPage
{
    [Guid("00000000-0000-0000-0000-000000000000")]
    public class OptionPageCustom : DialogPage
    {
        private string optionValue = "alpha";

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        protected override IWin32Window Window
        {
            get
            {
                var userControl = new MyUserControl();
                userControl.optionsPage = this;
                userControl.Initialize();
                return userControl;
            }
        }
    }
}
