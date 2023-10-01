using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditor
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditor", "Button clicked");
        }
    }
}