using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditorPrepare
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditorPrepare", "Button clicked");
        }
    }
}