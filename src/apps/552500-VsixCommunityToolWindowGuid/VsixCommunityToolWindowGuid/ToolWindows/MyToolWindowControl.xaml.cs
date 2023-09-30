using System.Windows;
using System.Windows.Controls;

namespace VsixCommunityToolWindowGuid
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("VsixCommunityToolWindowGuid", "Button clicked");

            var dialog = new MyToolWindowDialog();
            dialog.ShowDialog();
        }
    }
}