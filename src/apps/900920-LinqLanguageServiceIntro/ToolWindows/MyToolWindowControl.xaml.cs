using System.Windows;
using System.Windows.Controls;

namespace LinqLanguageServiceIntro
{
    public partial class LinqToolWindowControl : UserControl
    {
        public LinqToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("LinqLanguageServiceIntro", "Button clicked");
        }
    }
}