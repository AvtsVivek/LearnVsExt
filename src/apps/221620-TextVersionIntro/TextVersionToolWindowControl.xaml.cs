using EnvDTE80;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.UI.Design;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TextVersionIntro
{
    /// <summary>
    /// Interaction logic for TextVersionToolWindowControl.
    /// </summary>
    public partial class TextVersionToolWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        private ITextBuffer _textBuffer = null;

        private ITextEdit _textEdit = null;

        private List<OperationData> _textOperationList;
        /// <summary>
        /// Initializes a new instance of the <see cref="TextVersionToolWindowControl"/> class.
        /// </summary>
        public TextVersionToolWindowControl()
        {
            this.InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");

            _textOperationList = new List<OperationData>();
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
                "TextVersionToolWindow");
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

            PublishCurrentSnapshotAfterOperation();

            _textOperationList.Add(new OperationData { Position = position, Length = length, Operation = TextOperation.Replace, OperationText = replaceString });

        }

        private void ITextEditInsertButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextBufferNull())
            {
                return;
            }

            var inputText = ITextEditInputTextBox.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show(messageBoxText: "Source text of ITextEdit Manipulation for Insert is empty. Please input some text",
                    caption: "No input");
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputPositionInsertTxtBox, textBoxValue: out int position))
            {
                MessageBox.Show(
                messageBoxText: "Position int is null. Cannot continue",
                caption: "No input");
                return;
            }

            var insertString = string.Empty;
            if (!string.IsNullOrEmpty(value: ITextEditInputTextReplaceTxtBox.Text))
            {
                insertString = ITextEditInputTextReplaceTxtBox.Text;
            }

            if (!_textBuffer.EditInProgress)
            {
                _textEdit = _textBuffer.CreateEdit();
            }

            // _textEdit.Replace(startPosition: position, charsToReplace: length, replaceWith: replaceString);



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

            PublishCurrentSnapshotAfterOperation();

            _textOperationList.Add(new OperationData { Position = position, Operation = TextOperation.Insert, OperationText = insertString });


            // ITextEditApply();
            // AddItemToListView(startPosition: position, 0, operationText: ITextEditInputTextInsertTxtBox.Text, operation: "Insert");
        }

        private void ITextEditDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsTextBufferNull())
            {
                return;
            }

            var inputText = ITextEditInputTextBox.Text;

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show(messageBoxText: "Source text of ITextEdit Manipulation for Insert is empty. Please input some text",
                    caption: "No input");
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputPositionDeleteTxtBox, textBoxValue: out int position))
            {
                MessageBox.Show(
                messageBoxText: "Position int is null. Cannot continue",
                caption: "No input");
                return;
            }

            if (!TryParseTextBoxToInt(textBox: ITextEditInputLengthDeleteTxtBox, textBoxValue: out int length))
            {
                MessageBox.Show(
                messageBoxText: "Length int is null. Cannot continue",
                caption: "No input");
                return;
            }
            if (!_textBuffer.EditInProgress)
            {
                _textEdit = _textBuffer.CreateEdit();
            }
            _textEdit.Delete(startPosition: position, charsToDelete: length);

            PublishCurrentSnapshotAfterOperation();

            _textOperationList.Add(new OperationData { Position = position, Length = length, Operation = TextOperation.Delete});

            // ITextEditApply();
            // AddItemToListView(startPosition: position, length, operationText: string.Empty, operation: "Delete");
        }


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

            applyListView.Items.Clear();
            //AddVersionAfterApplyClick(_textBuffer.CurrentSnapshot);
        }

        private StackPanel CreateStackPanelAndAddToListView()
        {
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Vertical;
            var stackBorder = new Border();
            stackBorder.CornerRadius = new CornerRadius(5);

            stackBorder.BorderThickness = new Thickness(5);
            stackBorder.BorderBrush = Brushes.Red;
            stackBorder.Child = stackPanel;
            applyListView.Items.Add(stackBorder);
            return stackPanel;
        }      

        private void ITextEditApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ITextSnapshot textSnapshot;

            try
            {
                textSnapshot = _textEdit.Apply();
            }
            catch (Exception exception)
            {
                if (exception is InvalidOperationException 
                    && exception.Message == "Attempted to reuse an already applied edit.")
                {
                    MessageBox.Show(
                    messageBoxText: $"{exception.Message} Looks like Apply is clicked. So attempt an opration such as Replace.",
                    caption: "Invalid Operation");
                    return;
                }

                if (exception is InvalidOperationException 
                    && exception.Message == "Operation is not valid due to the current state of the object.")
                {
                    MessageBox.Show(
                    messageBoxText: $"{exception.Message} Looks like Apply is clicked. So attempt an opration such as Replace.",
                    caption: "Invalid Operation");
                    return;
                }

                throw exception;
            }
            
            AddVersionAfterApplyClick(textSnapshot);
            PublishCurrentSnapshotAfterOperation();
            _textOperationList.Clear();
        }

        private void AddVersionAfterApplyClick(ITextSnapshot textSnapshot)
        {

            var versionInfoString = string.Empty;

            ITextVersion versionInfo;

            if (_textEdit != null)
            {
                versionInfo = _textEdit.Snapshot.Version;
            }
            else
            {
                versionInfo = textSnapshot.Version;
            }

            versionInfoString = $"Version: {versionInfo.VersionNumber}";

            var stackPanel = CreateStackPanelAndAddToListView();
            var textBlock = new TextBlock();
            textBlock.Text = versionInfoString;
            stackPanel.Children.Add(textBlock);

            AddOprationsToListView(stackPanel);

            if (versionInfo.Changes == null)
                return;

            foreach (ITextChange change in _textEdit.Snapshot.Version.Changes)
            {
                textBlock = new TextBlock();
                textBlock.Text = $"Old Text: {change.OldText} - New Text: {change.NewText}";
                stackPanel.Children.Add(textBlock);
            }
        }

        private void ITextEditResetButton_Click(object sender, RoutedEventArgs e)
        {
            _textBuffer = null;
            _textEdit = null;

            ITextEditInputTextBox.Text = string.Empty;
            //ITextEditInputSpanLengthTextBox.Text = string.Empty;
            //ITextEditInputSpanStartTextBox.Text = string.Empty;
            ITextEditInputPositionReplaceTxtBox.Text = string.Empty;
            ITextEditInputLengthReplaceTxtBox.Text = string.Empty;
            ITextEditInputTextReplaceTxtBox.Text = string.Empty;

            ITextEditOutputTextBlock.Text = string.Empty;
            //readonlyExtentsTextBlock.Text = string.Empty;


            applyListView.Items.Clear();
        }

        private void PublishCurrentSnapshotAfterOperation()
        {
            var sn2 = _textBuffer.CurrentSnapshot;

            ITextEditOutputTextBlock.Text = sn2.GetText();
        }

        private void AddOprationsToListView(StackPanel stackPanel)
        {
            foreach (var textOperation in _textOperationList)
            {
                var textBlock = new TextBlock();
                textBlock.Foreground = Brushes.Green;
                var itemString = string.Empty;
                if (textOperation.Operation == TextOperation.Replace)
                {
                    itemString = $"Opration: {textOperation.Operation}, start: {textOperation.Position}, length: {textOperation.Length}. Replace text: {textOperation.OperationText}";
                }
                else if (textOperation.Operation == TextOperation.Delete)
                {
                    itemString = $"Opration: {textOperation.Operation}, start: {textOperation.Position}, length: {textOperation.Length}";
                }
                else if (textOperation.Operation == TextOperation.Insert)
                {
                    itemString = $"Opration: {textOperation.Operation}, start: {textOperation.Position}, Insert text: {textOperation.OperationText}";
                }
                else
                {
                    MessageBox.Show($"Unknown operation: {textOperation.Operation}", "Invalid operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    System.Diagnostics.Debugger.Break();
                }

                textBlock.Text = itemString;
                stackPanel.Children.Add(textBlock);
            }
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
    }
}