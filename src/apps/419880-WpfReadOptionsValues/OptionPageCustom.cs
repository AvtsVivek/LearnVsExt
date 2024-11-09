using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WpfReadOptionsValues
{
    [Guid("00000000-0000-0000-0000-000000000000")]
    public class OptionPageCustom : UIElementDialogPage
    {
        private string optionValue = "alpha";

        [Category(category: "My string Category")]
        [DisplayName(displayName: "My String Option")]
        [Description(description: "An option of the type string")]
        public string OptionString
        {
            get { return optionValue; }
            set { optionValue = value; }
        }

        protected override System.Windows.UIElement Child
        {
            get
            {
                var wpfUserControl = new MyWpfUserControl();
                wpfUserControl.optionsPage = this;
                wpfUserControl.Initialize();
                return wpfUserControl;
            }
        }
    }
}
