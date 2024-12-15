using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Windows;
using System.Windows.Controls;

namespace IReadOnlyRegionEditIntro
{
    /// <summary>
    /// Interaction logic for ReadOnlyEditToolWindowControl.
    /// </summary>
    public partial class ReadOnlyEditToolWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        private ITextBuffer _textBuffer = null;

        private ITextEdit _textEdit = null;

        private string _text = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyEditToolWindowControl"/> class.
        /// </summary>
        public ReadOnlyEditToolWindowControl()
        {
            this.InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");
        }

        private void ITextEditCreateReadOnlyExtentButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextBufferNull())
            {
                return;
            }

            if (!TryGetSpanDetails(out int spanStart, out int spanLength))
            {
                return;
            }

            if (_textBuffer.EditInProgress)
            {
                MessageBox.Show(
                messageBoxText: "Edit is in progress. Cannot create read only region. Click Start or reset to start all over again.",
                caption: "Edit in progress");
                return;
            }

            var span = new Span(spanStart, spanLength);

            IReadOnlyRegionEdit readOnlyRegionEdit = _textBuffer.CreateReadOnlyRegionEdit();

            try
            {
                readOnlyRegionEdit.CreateReadOnlyRegion(span);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                if (exception.Message.Contains("Specified argument was out of the range of valid values."))
                {
                    MessageBox.Show(
                    messageBoxText: "Specified argument was out of the range of valid values. Try a differnt range.",
                    caption: "span out of range");
                    return;
                }
                throw exception;
            }

            readOnlyRegionEdit.Apply();

            UpdateReadOnlyExtentCount();

            AddItemToListView(spanStart, spanLength, span.ToString(), "CreateReadOnlyRegion");      
        }

        private bool TryGetSpanDetails(out int spanStart, out int spanLength)
        {
            spanStart = spanLength = 0;

            if (string.IsNullOrWhiteSpace(ITextEditInputSpanStartTextBox.Text))
            {
                MessageBox.Show(
                messageBoxText: "Span start value cannot be empty. Please input some +ve number",
                caption: "No Span Start value");
                return false;
            }

            if (!int.TryParse(ITextEditInputSpanStartTextBox.Text, out spanStart))
            {
                MessageBox.Show(
                messageBoxText: "Span start value must be a +ve int.",
                caption: "Invalid Span start value.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ITextEditInputSpanLengthTextBox.Text))
            {
                MessageBox.Show(
                messageBoxText: "Span length value cannot be empty. Please input some +ve number",
                caption: "No Span Length value");
                return false;
            }

            if (!int.TryParse(ITextEditInputSpanLengthTextBox.Text, out spanLength))
            {
                MessageBox.Show(
                messageBoxText: "Span length value must be a +ve int.",
                caption: "Invalid Span length value.");
                return false;
            }

            return true;
        }

        private bool IsTextBufferNull()
        {
            if (_textBuffer == null)
            {
                MessageBox.Show(
                messageBoxText: "Text buffer is null, click start button",
                caption: "Click start");
                return true;
            }
            return false;
        }

        private void getReadOnlyExtentsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateReadOnlyExtentCount();
        }

        private void UpdateReadOnlyExtentCount()
        {
            if (IsTextBufferNull())
            {
                return;
            }

            var sn1 = _textBuffer.CurrentSnapshot;
            var ro1 = _textBuffer.GetReadOnlyExtents(new Span(0, sn1.Length));
            readonlyExtentsTextBlock.Text = ro1.Count.ToString();

        }

        #region ITextEditStartAppyReset

        private void ITextEditStartButton_Click(object sender, RoutedEventArgs e)
        {
            var inputText = ITextEditInputTextBox.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show(messageBoxText: "Source text of ITextEdit Manipulation is empty. Please input some text",
                    caption: "No input");
                return;
            }

            _textBuffer = _textBufferFactoryService
            .CreateTextBuffer(inputText, _textBufferFactoryService.PlaintextContentType);

            ITextInputListView.Items.Clear();
        }

        private void ITextEditApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var textSnapshot = _textEdit.Apply();
            ITextEditApply();
        }

        private void ITextEditResetButton_Click(object sender, RoutedEventArgs e)
        {
            _textBuffer = null;
            _textEdit = null;

            ITextEditInputTextBox.Text = string.Empty;
            ITextEditInputSpanLengthTextBox.Text = string.Empty;
            ITextEditInputSpanStartTextBox.Text = string.Empty;
            ITextEditInputPositionReplaceTxtBox.Text = string.Empty;
            ITextEditInputLengthReplaceTxtBox.Text = string.Empty;
            ITextEditInputTextReplaceTxtBox.Text = string.Empty;

            ITextEditOutputTextBlock.Text = string.Empty;
            readonlyExtentsTextBlock.Text = string.Empty;


            ITextInputListView.Items.Clear();
        }

        #endregion

        private void ITextEditApply()
        {
            var sn2 = _textBuffer.CurrentSnapshot;
            
            ITextEditOutputTextBlock.Text = sn2.GetText();
        }

        private void ITextEditReplaceButton_Click(object sender, RoutedEventArgs e)
        {

            if (IsTextBufferNull())
            {
                return;
            }

            var inputText = ITextEditInputTextBox.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show(
                    messageBoxText: "Source text of ITextEdit Manipulationi for Insert is empty. Please input some text",
                    caption: "No input");
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputPositionReplaceTxtBox, textBoxValue: out int position))
            {
                MessageBox.Show(
                messageBoxText: "Position int is null. Cannot continue",
                caption: "No input");
                return;
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputLengthReplaceTxtBox, textBoxValue: out int length))
            {
                MessageBox.Show(
                messageBoxText: "Length int is null. Cannot continue",
                caption: "No input");

                return;
            }

            var replaceString = string.Empty;
            if (!string.IsNullOrEmpty(value: ITextEditInputTextReplaceTxtBox.Text))
            {
                replaceString = ITextEditInputTextReplaceTxtBox.Text;
            }

            if (!_textBuffer.EditInProgress)
            {
                _textEdit = _textBuffer.CreateEdit();
            }

            _textEdit.Replace(startPosition: position, charsToReplace: length, replaceWith: replaceString);

            ITextEditApply();

            AddItemToListView(position, length, ITextEditInputTextReplaceTxtBox.Text, "Replace");

        }

        private void AddItemToListView(int startPosition, int length, string operationText, string operation)
        {
            var itemString = string.Empty;
            if (operation == "Replace")
            {
                itemString = $"Opration: {operation}, start: {startPosition}, length: {length}. Replace text: {operationText}";
            }
            else if (operation == "CreateReadOnlyRegion")
            {
                itemString = $"span: {operationText} - {operation}";
            }
            else
            {
                MessageBox.Show($"Unknown operation: {operation}", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Diagnostics.Debugger.Break();
            }

            ITextInputListView.Items.Add(newItem: itemString);
        }


        private bool TryParseTextBoxToInt(TextBox textBox, out int textBoxValue)
        {
            textBoxValue = 0;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                MessageBox.Show(
                    messageBoxText: $"{textBox.Tag} is empty. Please input some text",
                    caption: "No input");
                return false;
            }

            return int.TryParse(textBox.Text, out textBoxValue);
        }

    }
}