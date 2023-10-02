using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditorOptions
{
    public partial class LinqOptionsToolWindowControl : UserControl
    {
        public LinqOptionsToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditorOptions", "Button clicked");
        }
    }
}