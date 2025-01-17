using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NormSnapshtColTrialOne
{
    /// <summary>
    /// Interaction logic for SnapshotToolWindowControl.
    /// </summary>
    public partial class SnapshotToolWindowControl : UserControl
    {
        private ITextBufferFactoryService _textBufferFactoryService = null;

        private ITextBuffer _textBuffer = null;

        private List<SnapshotSpan> _snapshotSpans = new List<SnapshotSpan>();
        
        private NormalizedSnapshotSpanCollection _normalizedSnapshotSpansCollection = 
            new NormalizedSnapshotSpanCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotToolWindowControl"/> class.
        /// </summary>
        public SnapshotToolWindowControl()
        {
            this.InitializeComponent();

            var componentModel = (IComponentModel)Package.GetGlobalService(typeof(SComponentModel));
            _textBufferFactoryService = componentModel.GetService<ITextBufferFactoryService>();

            if (_textBufferFactoryService == null)
            {
                MessageBox.Show($"{nameof(ITextBufferFactoryService)} service is null. Cannot continue.");
            }

            ((INotifyCollectionChanged)spanListView.Items).CollectionChanged += SnapshotToolWindowControl_CollectionChanged;

        }

        private void SnapshotToolWindowControl_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Nothing here.
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // Nothing here.
            }

            if (spanListView.Items.Count > 0)
            {
                txtSnapshotText.IsEnabled = false;
                return;
            }

            txtSnapshotText.IsEnabled = true;
        }

        private void buttonClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();
            spanListView.Items.Clear();
            _snapshotSpans.Clear();
            _textBuffer = null;
        }

        private bool EnsureTextBlockValuesAreValid(out Nullable<Span> span)
        {
            span = null;

            if (string.IsNullOrWhiteSpace(txtSnapshotText.Text))
            {
                MessageBox.Show("Please put some snapshot text.", "No snap shot text", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(endTextBox.Text, out int endValue))
            {
                MessageBox.Show("End Span Value is not an integer. Please enter an int value!", "End not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!int.TryParse(startTextBox.Text, out int startValue))
            {
                MessageBox.Show("Start Span Value is not an integer. Please enter an int value!", "Start not an int", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValue < 0 || startValue < 0)
            {
                MessageBox.Show("Start or End Span Value cannot be -ve. Please ensure a +ve value for both", "Start or End not a +ve. value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (endValue <= startValue)
            {
                MessageBox.Show("Start value cannot be greater than end value. Please End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (_snapshotSpans.Where(snapshotSpan => snapshotSpan.Span.Start == startValue && snapshotSpan.Span.End == endValue).Any())
            {
                MessageBox.Show("The span is already present. Please key in different start and end values",
                    "Duplicate Span Value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            span = new Span(start: startValue, length: endValue - startValue);

            return true;
        }



        private void buttonAddSpanshotSpan_Click(object sender, RoutedEventArgs e)
        {
            Nullable<Span> span;
            
            if (!EnsureTextBlockValuesAreValid(out span))
                return;

            AddSpanToListView(span.Value);

            ReDrawCanvas();
        }

        private void AddSpanToListView(Span span)
        {
            string snapshotText = txtSnapshotText.Text;

            if (string.IsNullOrEmpty(snapshotText))
            {
                MessageBox.Show("No text entered for snapshot text. Enter some valid text", "No text", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            if (_textBuffer == null)
            {
                _textBuffer = _textBufferFactoryService.CreateTextBuffer(snapshotText, contentType: _textBufferFactoryService.PlaintextContentType);
            }

            ITextSnapshot currentTextSnapshot = _textBuffer.CurrentSnapshot;

            if (span.Start + span.Length > currentTextSnapshot.Length)
            {
                MessageBox.Show($"The range represented by the span is out of range of the " +
                    $"current spanshot represented by the text you entred. " +
                    $"The lenght of the text is {currentTextSnapshot.Length}, but 'span.Start + span.Length' = {span.Start + span.Length} " +
                    $"is greater." +Environment.NewLine + "Cannot Continue", "Span out of Range", 
                    
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                return;
            }

            SnapshotSpan snapShotSpan = new SnapshotSpan(currentTextSnapshot, span);

            StackPanel stackPanel = new StackPanel();
            
            stackPanel.Orientation = Orientation.Horizontal;

            Label labelSpanText = new Label();
            
            labelSpanText.Content = span;

            Button removeButton = new Button();
            
            removeButton.Content = "  X  ";

            removeButton.Click += RemoveButton_Click;

            stackPanel.Children.Add(labelSpanText);

            stackPanel.Children.Add(removeButton);
            
            spanListView.Items.Add(stackPanel);

            _snapshotSpans.Add(snapShotSpan);

            removeButton.Tag = new Tuple<SnapshotSpan, StackPanel>(snapShotSpan, stackPanel);
        }

        private void ReDrawCanvas()
        {
            lineCanvas.Children.Clear();

            var temp_snapshotSpanTextDictionary = new Dictionary<SnapshotSpan, string>();

            _normalizedSnapshotSpansCollection = new NormalizedSnapshotSpanCollection(_snapshotSpans);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSnapshotSpanCollection: _normalizedSnapshotSpansCollection, 
                lineColorBrush: Brushes.Red);

            DrawLinesFromSpanList(snapshotSpanList: _snapshotSpans, lineColorBrush: Brushes.Green, 
                lineStrokeThickness: 20, verticalDisanceFromBottom: 80, seperateLines: true);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            Tuple<SnapshotSpan, StackPanel> tuple = (Tuple<SnapshotSpan, StackPanel>)button.Tag;

            _snapshotSpans.Remove(tuple.Item1);

            spanListView.Items.Remove(tuple.Item2);

            ReDrawCanvas();
        }

        private void DrawLinesFromSpanList(List<SnapshotSpan> snapshotSpanList, Brush lineColorBrush,
            int verticalDisanceFromBottom, int lineStrokeThickness = 20, bool seperateLines = false)
        {
            var multiplier = 10;
            var leftMargin = 30;
            var lineToLineDistanceForSeperateLines = 50;

            for (int i = 0; i < snapshotSpanList.Count; i++)
            {
                var snapshotSpan = snapshotSpanList[i];

                var line = new Line();
                line.Visibility = Visibility.Visible;
                line.Stroke = lineColorBrush;
                line.StrokeThickness = lineStrokeThickness;
                line.StrokeEndLineCap = PenLineCap.Round;

                var lineLenght = snapshotSpan.End - snapshotSpan.Start;
                lineLenght = lineLenght * multiplier;
                line.X1 = leftMargin + snapshotSpan.Start * multiplier;
                line.X2 = line.X1 + lineLenght - lineStrokeThickness / 2;

                if (seperateLines)
                {
                    line.Y1 = lineCanvas.ActualHeight - verticalDisanceFromBottom - lineToLineDistanceForSeperateLines * (i + 1);
                    line.Y2 = line.Y1;
                }
                else
                {
                    line.Y1 = lineCanvas.ActualHeight - verticalDisanceFromBottom;
                    line.Y2 = line.Y1;
                }

                lineCanvas.Children.Add(element: line);
                AddTextToLine(line, snapshotSpan);
            }
        }

        private void AddTextToLine(Line line, SnapshotSpan snapshotSpan)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.Text = snapshotSpan.Span.ToString() + Environment.NewLine + snapshotSpan.GetText();

            textBlock.Foreground = Brushes.Black;

            int leftAdjustment = 5;

            if (snapshotSpan.Span.ToString().Length > 6)
                leftAdjustment = 10;

            Canvas.SetLeft(textBlock, (line.X1 + line.X2) / 2 - leftAdjustment);

            Canvas.SetTop(textBlock, line.Y1 + 10);

            lineCanvas.Children.Add(textBlock);
        }

        private void ConvertSpanCollectionToLineSetAndAddToCanvas(NormalizedSnapshotSpanCollection normalizedSnapshotSpanCollection, Brush lineColorBrush)
        {
            List<SnapshotSpan> snapshotSpanList = normalizedSnapshotSpanCollection.ToList();

            DrawLinesFromSpanList(snapshotSpanList, lineColorBrush: Brushes.Red, verticalDisanceFromBottom: 50,
                lineStrokeThickness: 20, seperateLines: false);
        }

        private void txtSnapshotText_TextChanged(object sender, TextChangedEventArgs e)
        {
            _textBuffer = null;
        }
    }
}