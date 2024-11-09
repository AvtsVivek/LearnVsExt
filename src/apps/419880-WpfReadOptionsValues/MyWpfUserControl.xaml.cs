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
    /// Interaction logic for MyWpfUserControl.xaml
    /// </summary>
    public partial class MyWpfUserControl : UserControl
    {
        public MyWpfUserControl()
        {
            InitializeComponent();
        }

        internal OptionPageCustom optionsPage;

        public void Initialize()
        {
            textBoxStringTextInput.Text = optionsPage.OptionString;
        }

        private void textBoxStringTextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            optionsPage.OptionString = textBoxStringTextInput.Text;
        }
    }
}
