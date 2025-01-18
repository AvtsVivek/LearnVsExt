using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;


namespace SnapshotSpanIntersectionTrial
{
    /// <summary>
    /// Interaction logic for IntersectionTrialControl.
    /// </summary>
    public partial class IntersectionTrialControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        SnapshotSpan _snapShotSpanOne = default;

        SnapshotSpan _snapShotSpanTwo = default;

        SnapshotSpan _snapShotSpanThree = default;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntersectionTrialControl"/> class.
        /// </summary>
        public IntersectionTrialControl()
        {
            InitializeComponent();
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();
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
                "IntersectionTrial");
        }

        private void buttonClearCanvas_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool TryCreateSnapshotSpanThree()
        {
            string snapshotTwoText = txtSnapshotTextTwo.Text;

            if (string.IsNullOrEmpty(snapshotTwoText))
            {
                MessageBox.Show("No text entered for snapshot text Two. Enter some valid text", "No text", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            var fullTextTwoBuffer = _textBufferFactoryService.CreateTextBuffer(snapshotTwoText, 
                contentType: _textBufferFactoryService.PlaintextContentType);

            Span spanThree;

            if (!EnsureTextBlockThreeValuesAreValid(out spanThree))
                return false;

            ITextSnapshot currentTextTwoSnapshot = fullTextTwoBuffer.CurrentSnapshot;

            if (spanThree.Start + spanThree.Length > currentTextTwoSnapshot.Length)
            {
                MessageBox.Show($"The range represented by the span three is out of range of the " +
                    $"current spanshot represented by the text you entred. " +
                    $"The lenght of the text is {currentTextTwoSnapshot.Length}, " +
                    $"but 'spanOne.Start + spanOne.Length' = {spanThree.Start + spanThree.Length} " +
                    $"is greater." + Environment.NewLine + "Cannot Continue", "Span ONE out of Range",

                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            _snapShotSpanThree = new SnapshotSpan(currentTextTwoSnapshot, spanThree);

            return true;
        }

        private bool TryCreateSnapshotSpansOneTwo()
        {
            string fullSnapshotText = txtFullSnapshotText.Text;

            if (string.IsNullOrEmpty(fullSnapshotText))
            {
                MessageBox.Show("No text entered for snapshot text. Enter some valid text", "No text", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            var fullTextBuffer = _textBufferFactoryService.CreateTextBuffer(fullSnapshotText, contentType: _textBufferFactoryService.PlaintextContentType);

            Span spanOne, spanTwo;

            if (!EnsureTextBlockOneTwoValuesAreValid(out spanOne, out spanTwo))
                return false;

            ITextSnapshot currentFullTextSnapshot = fullTextBuffer.CurrentSnapshot;

            if (spanOne.Start + spanOne.Length > currentFullTextSnapshot.Length)
            {
                MessageBox.Show($"The range represented by the span is out of range of the " +
                    $"current spanshot represented by the text you entred. " +
                    $"The lenght of the text is {currentFullTextSnapshot.Length}, " +
                    $"but 'spanOne.Start + spanOne.Length' = {spanOne.Start + spanOne.Length} " +
                    $"is greater." + Environment.NewLine + "Cannot Continue", "Span ONE out of Range",

                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            if (spanTwo.Start + spanTwo.Length > currentFullTextSnapshot.Length)
            {
                MessageBox.Show($"The range represented by the span is out of range of the " +
                    $"current spanshot represented by the text you entred. " +
                    $"The lenght of the text is {currentFullTextSnapshot.Length}, " +
                    $"but 'spanTwo.Start + spanTwo.Length' = {spanTwo.Start + spanTwo.Length} " +
                    $"is greater." + Environment.NewLine + "Cannot Continue", "Span TWO out of Range",

                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return false;
            }

            _snapShotSpanOne = new SnapshotSpan(currentFullTextSnapshot, spanOne);

            _snapShotSpanTwo = new SnapshotSpan(currentFullTextSnapshot, spanTwo);

            return true;
        }

        private void buttonDoTheyIntersect_Click(object sender, RoutedEventArgs e)
        {
            if (!TryCreateSnapshotSpansOneTwo())
            {
                return;
            }

            if (_snapShotSpanOne == default)
            {
                MessageBox.Show(" SnapshotSpan One is not initialized ", "Snapshot one not ready", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_snapShotSpanTwo == default)
            {
                MessageBox.Show(" SnapshotSpan Two is not initialized ", "Snapshot two not ready", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool doTheyIntersect = _snapShotSpanOne.IntersectsWith(_snapShotSpanTwo);

            if (doTheyIntersect)
            {
                MessageBox.Show("Yes, they do intersect.", "Yes");
            }
            else 
            {
                MessageBox.Show("No, they do not intersect.", "No");
            }
        }

        private bool EnsureTextBlockOneTwoValuesAreValid(out Span spanOne, out Span spanTwo)
        {
            spanOne = spanTwo = default;

            if (string.IsNullOrWhiteSpace(txtFullSnapshotText.Text))
            {
                MessageBox.Show("Please put some snapshot text.", "No snap shot text", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(endTextBoxOne.Text, out int endValueOne))
            {
                MessageBox.Show("End Span Value of one is not an integer. Please enter an int value!", "End not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(startTextBoxOne.Text, out int startValueOne))
            {
                MessageBox.Show("Start Span one Value is not an integer. Please enter an int value!", "Start not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueOne < 0 || startValueOne < 0)
            {
                MessageBox.Show("Start or End Span one Value cannot be -ve. Please ensure a +ve value for both", "Start or End not a +ve. value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueOne <= startValueOne)
            {
                MessageBox.Show("Start one value cannot be greater than end one value. Please End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(endTextBoxTwo.Text, out int endValueTwo))
            {
                MessageBox.Show("End Span two Value is not an integer. Please enter an int value!", "End not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(startTextBoxTwo.Text, out int startValueTwo))
            {
                MessageBox.Show("Start Span two Value is not an integer. Please enter an int value!", "Start not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueTwo < 0 || startValueTwo < 0)
            {
                MessageBox.Show("Start or End Span two Value cannot be -ve. Please ensure a +ve value for both", "Start or End not a +ve. value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueTwo <= startValueTwo)
            {
                MessageBox.Show("Start value two cannot be greater than end value. Please End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            spanOne = new Span(start: startValueOne, length: endValueOne - startValueOne);

            spanTwo = new Span(start: startValueTwo, length: endValueTwo - startValueTwo);

            return true;
        }

        private bool EnsureTextBlockThreeValuesAreValid(out Span spanThree)
        {
            spanThree = default;

            if (string.IsNullOrWhiteSpace(txtSnapshotTextTwo.Text))
            {
                MessageBox.Show("Please put some snapshot text in Two.", "No snap shot text", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(endTextBoxThree.Text, out int endValueThree))
            {
                MessageBox.Show("End Span Value of three is not an integer. Please enter an int value!", "End not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(startTextBoxThree.Text, out int startValueThree))
            {
                MessageBox.Show("Start Span Three Value is not an integer. Please enter an int value!", "Start not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueThree < 0 || startValueThree < 0)
            {
                MessageBox.Show("Start or End Span Three Value cannot be -ve. Please ensure a +ve value for both", "Start or End not a +ve. value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValueThree <= startValueThree)
            {
                MessageBox.Show("Start Three value cannot be greater than end three value. Please End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            spanThree = new Span(start: startValueThree, length: endValueThree - startValueThree);

            return true;
        }

        private void buttonDoTheyIntersectTwo_Click(object sender, RoutedEventArgs e)
        {

            if(!TryCreateSnapshotSpanThree())
                return;

            if (_snapShotSpanThree == default)
            {
                MessageBox.Show(" SnapshotSpan Three is not initialized ", "Snapshot three not ready", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool doTheyIntersect;

            try
            {
                doTheyIntersect = _snapShotSpanThree.IntersectsWith(_snapShotSpanOne);
            }
            catch (ArgumentException argException)
            {
                var message = "The specified SnapshotPoint or SnapshotSpan  is on a different ITextSnapshot than this SnapshotSpan.";
                if (argException.Message == message)
                {
                    MessageBox.Show("The root text bufferes for the snapshots is diffrent. " 
                        + Environment.NewLine + message);
                }
                return;
            }

            if (doTheyIntersect)
            {
                MessageBox.Show("Yes, they do intersect.", "Yes");
            }
            else
            {
                MessageBox.Show("No, they do not intersect.", "No");
            }
        }
    }
}