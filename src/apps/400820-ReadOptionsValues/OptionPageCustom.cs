using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ReadOptionsValues
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
                var page = new MyUserControl();
                page.optionsPage = this;
                page.Initialize();
                return page;
            }
        }
    }
}
