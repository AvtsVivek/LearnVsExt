using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITextEditIntroWpfNotWorking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;
        public MainWindow()
        {
            InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");

        }

        private void ITextEditInsertButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stringInsertButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}