﻿using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TextBufferUndoManager
{
    /// <summary>
    /// Interaction logic for TestToolWindowControl.
    /// </summary>
    public partial class TestToolWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        private ITextBufferUndoManagerProvider _textBufferUndoManagerProvider = null;

        private ITextBufferUndoManager _textBufferUndoManager = null;

        private ITextBuffer _textBuffer = null;

        private ITextEdit _textEdit = null;

        private List<OperationData> _textOperationList;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestToolWindowControl"/> class.
        /// </summary>
        public TestToolWindowControl()
        {
            this.InitializeComponent();
            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");

            _textOperationList = new List<OperationData>();

            _textBufferUndoManagerProvider = 
                componentModel.GetService<ITextBufferUndoManagerProvider>();

            if (_textBufferUndoManagerProvider == null)
                throw new Exception($"{nameof(_textBufferUndoManagerProvider)} is null. Cannot continue!!!");

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

            _textOperationList.Add(new OperationData { Position = position, Length = length, Operation = TextOperation.Delete });

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

            AddVersionAfterApplyClick(_textBuffer.CurrentSnapshot);

            _textBufferUndoManager = _textBufferUndoManagerProvider.GetTextBufferUndoManager(_textBuffer);

            _textBufferUndoManager.TextBufferUndoHistory.UndoRedoHappened += TextBufferUndoHistory_UndoRedoHappened;

            _textBufferUndoManager.TextBufferUndoHistory.UndoTransactionCompleted += TextBufferUndoHistory_UndoTransactionCompleted;

            return;
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

            ITextVersion versionInfo = textSnapshot.Version;

            versionInfoString = $"Version: {versionInfo.VersionNumber}";

            StackPanel stackPanel = CreateStackPanelAndAddToListView();

            TextBlock textBlock = new TextBlock();

            textBlock.Text = versionInfoString;

            stackPanel.Children.Add(textBlock);

            AddOprationsToListView(stackPanel);

            if (_textEdit == null)
                return;

            foreach (ITextChange change in _textEdit?.Snapshot?.Version.Changes)
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

            ITextEditInputPositionReplaceTxtBox.Text = string.Empty;
            ITextEditInputLengthReplaceTxtBox.Text = string.Empty;
            ITextEditInputTextReplaceTxtBox.Text = string.Empty;

            ITextEditInputPositionInsertTxtBox.Text = string.Empty;
            ITextEditInputLengthInsertTxtBox.Text = string.Empty;
            ITextEditInputTextInsertTxtBox.Text = string.Empty;

            ITextEditInputPositionDeleteTxtBox.Text = string.Empty;
            ITextEditInputLengthDeleteTxtBox.Text = string.Empty;
            ITextEditInputTextDeleteTxtBox.Text = string.Empty;

            ITextEditOutputTextBlock.Text = string.Empty;

            undoRedoTextTextBloc.Text = string.Empty;

            applyListView.Items.Clear();

            // _textBufferUndoManager.TextBufferUndoHistory.UndoRedoHappened += TextBufferUndoHistory_UndoRedoHappened;
            // The reason we are first subscribing(above, now uncommented), and then unsubscribing(below) is the
            // below can sometimes throw exception.
            // If say the user clicks the Reset button twice successively, then the following throws exception.
            // So just to be sure, first subscribe and then unsubscribe. 
            _textBufferUndoManager.TextBufferUndoHistory.UndoRedoHappened -= TextBufferUndoHistory_UndoRedoHappened;

            _textBufferUndoManager.TextBufferUndoHistory.UndoTransactionCompleted += TextBufferUndoHistory_UndoTransactionCompleted;
            _textBufferUndoManager.TextBufferUndoHistory.UndoTransactionCompleted -= TextBufferUndoHistory_UndoTransactionCompleted;

            _textBufferUndoManager = null;

            messagesListView.Items.Clear();
        }

        private void TextBufferUndoHistory_UndoRedoHappened(object sender, TextUndoRedoEventArgs e)
        {
            messagesListView.Items.Add($"{e.State}");
        }

        private void TextBufferUndoHistory_UndoTransactionCompleted(object sender, TextUndoTransactionCompletedEventArgs e)
        {
            ITextUndoTransaction textUndoTransaction = e.Transaction;
            TextUndoTransactionCompletionResult textUndoTransactionCompletionResult = e.Result;
            messagesListView.Items.Add($"Transaction - {textUndoTransactionCompletionResult}");
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
                    Debugger.Break();
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

        private void undoTextVersionButton_Click(object sender, RoutedEventArgs e)
        {
            var textBufferUndoHistory = _textBufferUndoManager.TextBufferUndoHistory;
            textBufferUndoHistory.Undo(1);
            undoRedoTextTextBloc.Text = _textBuffer.CurrentSnapshot.GetText();
        }

        private void redoTextVersionButton_Click(object sender, RoutedEventArgs e)
        {
            var textBufferUndoHistory = _textBufferUndoManager.TextBufferUndoHistory;
            textBufferUndoHistory.Redo(1);
            undoRedoTextTextBloc.Text = _textBuffer.CurrentSnapshot.GetText();
        }
    }
}