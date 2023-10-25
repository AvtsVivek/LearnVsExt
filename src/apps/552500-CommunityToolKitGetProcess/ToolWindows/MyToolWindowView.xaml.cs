using CommunityToolKitGetProcess;
using System.Windows;
using System.Windows.Controls;

namespace CommunityToolKitGetProcess
{
    public partial class MyToolWindowView : UserControl
    {
        public MyToolWindowView()
        {
            // DataContext = new MyToolWindowViewModel();
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("CommunityToolKitGetProcess", "Button clicked");
        }
    }
}