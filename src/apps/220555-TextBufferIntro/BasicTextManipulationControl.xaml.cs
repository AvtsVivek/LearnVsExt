﻿using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.ComponentModelHost;

namespace TextBufferIntro
{
    /// <summary>
    /// Interaction logic for BasicTextManipulationControl.
    /// </summary>
    public partial class BasicTextManipulationControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicTextManipulationControl"/> class.
        /// </summary>
        public BasicTextManipulationControl()
        {
            this.InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));

            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
                throw new Exception($"{nameof(_textBufferFactoryService)} is null. Cannot continue!!!");
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
            string text = textBox1.Text;

            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Enter some valid text", "No text entered", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            // Create the text buffer
            ITextBuffer textBuffer = _textBufferFactoryService.CreateTextBuffer(text, contentType: _textBufferFactoryService.PlaintextContentType);

            ITextSnapshot fullTextCurrentSnapshot = textBuffer.CurrentSnapshot;


            //ITextSearchService2 _textSearchService;
            //var fullSnapshotSpan = new SnapshotSpan(snapshot, new Span(0, snapshot.Length));
            //var helloWords = _textSearchService.FindAll(fullSnapshotSpan, "hello", FindOptions.WholeWord);

            string resultNumberSubString = Regex.Match(fullTextCurrentSnapshot.GetText(), @"\d+").Value;

            if (string.IsNullOrEmpty(resultNumberSubString))
            {
                MessageBox.Show("Text does not contain number", "No number", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            int resultNumberSubStringIndex = fullTextCurrentSnapshot.GetText().IndexOf(resultNumberSubString, 0,
                fullTextCurrentSnapshot.GetText().Length, StringComparison.CurrentCulture);

            SnapshotSpan numberSpan = new SnapshotSpan(fullTextCurrentSnapshot, span: new Span(start: resultNumberSubStringIndex,
               length: resultNumberSubString.Length));

            finalNumberText.Text = numberSpan.GetText();

            var newSnapshotText = numberSpan.Snapshot.GetText();

            finalSnapshotText.Text = newSnapshotText;

            // The following throws exception. 
            // The range or the span should be a subset of the text range.
            //SnapshotSpan largerSnapshotSpan = new SnapshotSpan(snapshot, span: new Span(start: resultNumberSubStringIndex,
            //    length: resultNumberSubString.Length + 5));

            //var largerText = largerSnapshotSpan.GetText();
        }
    }
}