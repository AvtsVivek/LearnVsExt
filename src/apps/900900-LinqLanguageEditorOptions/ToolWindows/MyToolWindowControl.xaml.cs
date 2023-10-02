using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditorOptions
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditorOptions", "Button clicked");
        }
    }
}