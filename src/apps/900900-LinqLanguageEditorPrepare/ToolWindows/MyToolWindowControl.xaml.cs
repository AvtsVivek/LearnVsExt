using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageEditorPrepare
{
    public partial class LinqToolWindowControl : UserControl
    {
        public LinqToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageEditorPrepare", "Button clicked");
        }
    }
}