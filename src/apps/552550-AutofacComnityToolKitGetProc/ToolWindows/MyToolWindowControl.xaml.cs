using System.Windows;
using System.Windows.Controls;

namespace AutofacComnityToolKitGetProc
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl(MyToolWindowViewModel myToolWindowViewModel)
        {
            InitializeComponent();
            DataContext = myToolWindowViewModel;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("AutofacComnityToolKitGetProc", "Button clicked");
        }
    }
}