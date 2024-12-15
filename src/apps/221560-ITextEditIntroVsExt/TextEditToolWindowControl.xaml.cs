using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace ITextEditIntroVsExt
{
    /// <summary>
    /// Interaction logic for TextEditToolWindowControl.
    /// </summary>
    public partial class TextEditToolWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        private ITextBuffer _textBuffer = null;

        private ITextEdit _textEdit = null;

        private string _text = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextEditToolWindowControl"/> class.
        /// </summary>
        public TextEditToolWindowControl()
        {
            this.InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");
        }

        #region Insert

        private void stringInsertButton_Click(object sender, RoutedEventArgs e)
        {
            var positionText = stringInputPosionInsertTxtBox.Text;
            var lengthText = stringInputLengthInsertTxtBox.Text;

            if (!TryParseTextBoxToInt(stringInputPosionInsertTxtBox, out int position))
            {
                return;
            }

            _text = _text.Insert(position, stringInputTextInsertTxtBox.Text);

            StringApply();
        }

        private void ITextEditInsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsITextEditIsNull())
            {
                MessageBox.Show(
                messageBoxText: "Text edit or buffer is null. Cannot continue",
                caption: "Click start");
                return;
            }

            if (!TryParseTextBoxToInt(ITextEditInputPositionInsertTxtBox, out int position))
            {
                return;
            }

            if (string.IsNullOrEmpty(ITextEditInputTextInsertTxtBox.Text))
            {
                MessageBox.Show(
                messageBoxText: "Insert text of ITextEdit for Insert is empty. Please input some text",
                caption: "No input");
                return;
            }

            try
            {
                _textEdit.Insert(position, ITextEditInputTextInsertTxtBox.Text);
            }
            catch (Exception exception)
            {
                if (exception.Message == "Attempted to reuse an already applied edit.")
                {
                    MessageBox.Show(messageBoxText: $"{exception.Message} So restart by clicking the Reset button, then start and then the opration insert, delete or reset. " +
                        $"{Environment.NewLine}Click the Apply button at the end.",
                    caption: "Start again");
                    return;
                }
            }

            ITextEditApply();
            AddItemToListView(startPosition: position, 0, operationText: ITextEditInputTextInsertTxtBox.Text, operation: "Insert");
        }

        #endregion

        #region Delete
        private void stringDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var positionText = stringInputPosionInsertTxtBox.Text;
            var lengthText = stringInputLengthInsertTxtBox.Text;

            if (!TryParseTextBoxToInt(stringInputPositionDeleteTxtBox, out int position))
            {
                return;
            }

            if (!TryParseTextBoxToInt(stringInputLengthDeleteTxtBox, out int length))
            {
                return;
            }
            _text = _text.Remove(position, length);

            StringApply();
        }

        private void ITextEditDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsITextEditIsNull())
            {
                MessageBox.Show(
                messageBoxText: "Text edit or buffer is null. Cannot continue",
                caption: "Click start");
                return;
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputPositionDeleteTxtBox, textBoxValue: out int position))
            {
                return;
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputLengthDeleteTxtBox, textBoxValue: out int length))
            {
                return;
            }

            _textEdit.Delete(startPosition: position, charsToDelete: length);
            ITextEditApply();
            AddItemToListView(startPosition: position, length, operationText: string.Empty, operation: "Delete");
        }

        #endregion

        #region Replace
        private void ITextEditReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsITextEditIsNull())
            {
                MessageBox.Show(
                messageBoxText: "Text edit or buffer is null. Cannot continue",
                caption: "Click start");
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

                //MessageBox.Show(
                //messageBoxText: "Text to replace is empty. Cannot continue",
                //caption: "No input");
                //return;

                replaceString = ITextEditInputTextReplaceTxtBox.Text;
            }


            _textEdit.Replace(startPosition: position, charsToReplace: length, replaceWith: replaceString);
            ITextEditApply();
            AddItemToListView(position, length, ITextEditInputTextReplaceTxtBox.Text, "Replace");

        }
        #endregion

        #region ITextEditStartAppyReset

        private void ITextEditStartButton_Click(object sender, RoutedEventArgs e)
        {
            var inputText = ITextEditInputTextBox.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show(
                    messageBoxText: "Source text of ITextEdit Manipulationi for Insert is empty. Please input some text",
                    caption: "No input");
                return;
            }

            _textBuffer = _textBufferFactoryService
            .CreateTextBuffer(inputText, _textBufferFactoryService.PlaintextContentType);

            _textEdit = _textBuffer.CreateEdit();

            ITextInputListView.Items.Clear();
        }

        private void ITextEditApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsITextEditIsNull())
            {
                MessageBox.Show(
                messageBoxText: "Text edit or buffer is null. Cannot continue",
                caption: "Click start");
                return;
            }

            _textEdit.Apply();
            ITextEditApply();
        }

        private void ITextEditResetButton_Click(object sender, RoutedEventArgs e)
        {
            _textBuffer = null;
            _textEdit = null;

            ITextEditInputTextBox.Text = string.Empty;
            ITextEditInputPositionInsertTxtBox.Text = string.Empty;
            ITextEditInputTextInsertTxtBox.Text = string.Empty;
            ITextEditInputPositionDeleteTxtBox.Text = string.Empty;
            ITextEditInputLengthDeleteTxtBox.Text = string.Empty;
            ITextEditOutputTextBlock.Text = string.Empty;

            ITextEditInputPositionReplaceTxtBox.Text = string.Empty;
            ITextEditInputLengthReplaceTxtBox.Text = string.Empty;
            ITextEditInputTextReplaceTxtBox.Text = string.Empty;

            ITextInputListView.Items.Clear();

        }

        #endregion

        #region StringStartApplyReset

        private void stringStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(stringInputTextBox.Text))
            {
                MessageBox.Show(
                messageBoxText: "Insert text of string Manipulation for Insert is empty. Please input some text",
                caption: "No input");
                return;
            }
            _text = stringInputTextBox.Text.Trim();
            StringApply();
        }

        private void stringApplyButton_Click(object sender, RoutedEventArgs e)
        {
            StringApply();
        }

        private void stringResetButton_Click(object sender, RoutedEventArgs e)
        {
            _text = string.Empty;

            stringInputTextBox.Text = string.Empty;
            stringInputPosionInsertTxtBox.Text = string.Empty;
            stringInputTextInsertTxtBox.Text = string.Empty;
            stringInputPositionDeleteTxtBox.Text = string.Empty;
            stringInputLengthDeleteTxtBox.Text = string.Empty;
            stringOutputTextBlock.Text = string.Empty;
        }

        #endregion

        private void AddItemToListView(int startPosition, int length, string operationText, string operation)
        {
            var itemString = string.Empty;
            if (operation == "Insert")
            {
                itemString = $"Opration: {operation}, start: {startPosition}, insert text: {operationText}";
            }
            else if (operation == "Delete")
            {
                itemString = $"Opration: {operation}, start: {startPosition}, length: {length}";
            }
            else if (operation == "Replace")
            {
                itemString = $"Opration: {operation}, start: {startPosition}, length: {length}. Replace text: {operationText}";
            }
            else
            {
                // Unknown operation
                MessageBox.Show($"Unknown operation: {operation}", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Diagnostics.Debugger.Break();
            }
            ITextInputListView.Items.Add(newItem: itemString);
        }

        private bool IsITextEditIsNull()
        {
            if (_textBuffer == null)
            {
                MessageBox.Show(
                messageBoxText: "Text buffer is null, click start button",
                caption: "Click start");
                return true;
            }

            if (_textEdit == null)
            {
                MessageBox.Show(
                messageBoxText: "Text edit is null, click start button",
                caption: "Click start");
                return true;
            }

            return false;
        }

        private void StringApply()
        {
            stringOutputTextBlock.Text = _text;
        }

        private void ITextEditApply()
        {
            // The following doest not work.
            // Apply can be called only once.
            // After calling Apply(), you cannot call methods such as 
            // _textEdit.Insert(0, _text) or delete
            // _textEdit.Apply();
            var sn2 = _textBuffer.CurrentSnapshot;
            ITextEditOutputTextBlock.Text = sn2.GetText();
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