using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfReadOptionsValues
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

        internal AdvancedOptionPage AdvancedOptionsPage;

        public void Initialize()
        {
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
