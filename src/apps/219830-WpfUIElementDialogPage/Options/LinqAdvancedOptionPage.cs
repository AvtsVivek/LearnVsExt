using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.Windows;
using WpfUIElementDialogPage.Options;

namespace WpfUIElementDialogPage.Options
{
    [ComVisible(true)]
    [Guid(Constants.LinqAdvancedOptionPageGuid)]
    public class LinqAdvancedOptionPage : UIElementDialogPage
    {
        private string optionValue = "alpha123567";

        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }
        protected override UIElement Child
        {
            get
            {
                AdvancedOptions page = new AdvancedOptions
                {
                    AdvancedOptionsPage = this
                };
                page.Initialize();
                return page;
            }
        }
    }
}
