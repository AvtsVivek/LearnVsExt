using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace SnapshotSpanIntro
{
    /// <summary>
    /// Interaction logic for SnapshotTrialsWindowControl.
    /// </summary>
    public partial class SnapshotTrialsWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotTrialsWindowControl"/> class.
        /// </summary>
        public SnapshotTrialsWindowControl()
        {
            this.InitializeComponent();
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            textLengthOfGivenString.Text = "Length of the above given string: " + txtFullSnapshotText.Text.Length.ToString();
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
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "SnapshotTrialsWindow");
        }

        private int _startSpanValue, _endSpanValue = 0;

        private void CreateSnapshotSpanAndShowText()
        {
            var span = new Span(_startSpanValue, _endSpanValue - _startSpanValue);

            var fullTextBuffer = _textBufferFactoryService.CreateTextBuffer(txtFullSnapshotText.Text,
                contentType: _textBufferFactoryService.PlaintextContentType);

            ITextSnapshot currentTextSnapshot = fullTextBuffer.CurrentSnapshot;

            var snapShotSpan = new SnapshotSpan(currentTextSnapshot, span);

            textLengthOfGivenString.Text = "Length of the above given string: " + txtFullSnapshotText.Text.Length.ToString();

            textSnapshotSpan.Text = snapShotSpan.GetText();
            
            textEndMinusStart.Text = $"End - Start is {(_endSpanValue - _startSpanValue)}";
        }

        private void buttonStartDecrement_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            _startSpanValue--;

            startSpanTextBox.Text = _startSpanValue.ToString();

            if (!ValidateInputFields())
            {
                _startSpanValue++;

                startSpanTextBox.Text = _startSpanValue.ToString();

                return;
            }           

            CreateSnapshotSpanAndShowText();
        }

        private void buttonStartIncrement_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            _startSpanValue++;

            startSpanTextBox.Text = _startSpanValue.ToString();

            if (!ValidateInputFields())
            {
                _startSpanValue--;

                startSpanTextBox.Text = _startSpanValue.ToString();

                return;
            }

            CreateSnapshotSpanAndShowText();
        }

        private void buttonEndDecrement_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            _endSpanValue--;

            endSpanTextBox.Text = _endSpanValue.ToString();

            if (!ValidateInputFields())
            {
                _endSpanValue++;

                endSpanTextBox.Text = _endSpanValue.ToString();

                return;
            }

            CreateSnapshotSpanAndShowText();
        }

        private void buttonEndIncrement_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputFields())
                return;

            _endSpanValue++;

            endSpanTextBox.Text = _endSpanValue.ToString();

            if (!ValidateInputFields())
            {
                _endSpanValue--;

                endSpanTextBox.Text = _endSpanValue.ToString();

                return;
            }

            CreateSnapshotSpanAndShowText();
        }

        private bool ValidateInputFields()
        {
            var errorMessage = string.Empty;

            bool returnValue = true;

            if (string.IsNullOrWhiteSpace(txtFullSnapshotText.Text))
            {
                errorMessage = "Please put some snapshot text.";
                returnValue = false;
            }

            if (!int.TryParse(startSpanTextBox.Text, out int startSpanValue))
            {
                errorMessage = "End Span Value is not an integer. Please enter an int value!";
                returnValue = false;
            }

            if (!int.TryParse(endSpanTextBox.Text, out int endSpanValue))
            {
                errorMessage = "Start Span Value is not an integer. Please enter an int value!";
                returnValue = false;
            }

            if (endSpanValue < 0 || startSpanValue < 0)
            {
                errorMessage = "Start or End Span Value cannot be -ve. Please ensure a +ve value for both";
                returnValue = false;
            }

            if (endSpanValue < startSpanValue)
            {
                errorMessage = "Start value cannot be greater than end value. Please ensure End value is always greater than or atlest equal to start value";
                returnValue = false;
            }

            if (endSpanValue > txtFullSnapshotText.Text.Length)
            {
                errorMessage = "End value is longer than the lenght of the given string.";
                returnValue = false;
            }

            textErrorMessage.Text = errorMessage;
            _startSpanValue = startSpanValue;
            _endSpanValue = endSpanValue;

            return returnValue;
        }
    }
}