using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using WpfMb = System.Windows.MessageBox;


namespace CaretPositionOnToolWindow.ToolWindows
{
    /// <summary>
    /// Interaction logic for CaretPositionToolWindowControl.
    /// </summary>
    public partial class CaretPositionToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaretPositionToolWindowControl"/> class.
        /// </summary>
        public CaretPositionToolWindowControl(CaretPositionToolWindowViewModel caretPositionToolWindowControlViewModel )
        {
            this.InitializeComponent();
            // The following line is needed, without it I am getting the following exception.
            // FileNotFoundException: Could not load file or assembly 'Microsoft.Xaml.Behaviors, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies.
            // The system cannot find the file specified.
            // https://github.com/microsoft/XamlBehaviorsWpf/issues/86
            // var _ = new Microsoft.Xaml.Behaviors.DefaultTriggerAttribute(typeof(Trigger), typeof(Microsoft.Xaml.Behaviors.TriggerBase), null);
            // Or else you can simply name the command in the xaml as shown in the comment
            // in the same bug report. Click the below link to go there.
            // https://github.com/microsoft/XamlBehaviorsWpf/issues/86#issuecomment-772745380
            this.DataContext = caretPositionToolWindowControlViewModel;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        //[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        //[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    System.Windows.MessageBox.Show(
        //        string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
        //        "CaretPositionToolWindow");
        //}

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var messageBoxText = string.Empty;

            if (this.IsVisible)
                messageBoxText = "Tool Window is opened";
            else
                messageBoxText = "Tool Window is closed";

            //await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            //await Dispatcher.BeginInvoke(new Action(() => WpfMb.Show(messageBoxText)));

            ThreadHelper.JoinableTaskFactory.Run(async delegate {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                /*UI code here*/
                WpfMb.Show(messageBoxText);
            });

        }
    }
}