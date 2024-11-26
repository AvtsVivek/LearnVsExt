
using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfUIElementDialogPage.Options
{
    /// <summary>
    /// Interaction logic for AdvancedOptions.xaml
    /// </summary>
    public partial class AdvancedOptions : UserControl
    {
        public AdvancedOptions()
        {
            InitializeComponent();
        }
        internal LinqAdvancedOptionPage AdvancedOptionsPage;

        public void Initialize()
        {
            // textBlockStringTextInput.Text = AdvancedOptionsPage.OptionString;

            var taskTemp = ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                textBlockStringTextInput.Text = AdvancedOptionsPage.OptionString;
            });
        }

        private void textBlockStringTextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            AdvancedOptionsPage.OptionString = textBlockStringTextInput.Text;
        }
    }
}
