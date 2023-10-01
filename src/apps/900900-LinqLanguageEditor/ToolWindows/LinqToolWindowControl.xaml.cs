using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditor
{
    public partial class LinqToolWindowControl : UserControl
    {
        public LinqToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditor", "Button clicked");
        }
    }
}