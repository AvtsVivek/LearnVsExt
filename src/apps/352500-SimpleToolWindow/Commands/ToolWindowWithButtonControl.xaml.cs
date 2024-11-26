using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace SimpleToolWindow.Commands
{
    /// <summary>
    /// Interaction logic for ToolWindowWithButtonControl.
    /// </summary>
    public partial class ToolWindowWithButtonControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowWithButtonControl"/> class.
        /// </summary>
        public ToolWindowWithButtonControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Debugger.Break();
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "ToolWindowWithButton");
        }
    }
}