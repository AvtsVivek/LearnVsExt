using Microsoft.VisualStudio.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NormalSpanColWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Span> _spans = [];
        private NormalizedSpanCollection _spansCollection = new ();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();
            spanListView.Items.Clear();
            _spans.Clear();
        }

        private bool EnsureTextBlockValuesAreValid(out Nullable<Span> span)
        {
            span = null;
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
                MessageBox.Show("Start value cannot be greater than end value. Please ensure End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            span = new Span(start: startValue, length: endValue - startValue);

            if (_spans.Contains(span.Value))
            {
                MessageBox.Show("The span is already present. Please key in different start and end values", "Duplicate Span Value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void buttonAddSpan_Click(object sender, RoutedEventArgs e)
        {
            Nullable<Span> span;
            if (!EnsureTextBlockValuesAreValid(out span))
            {
                return;
            }

            AddSpanToListView(span!.Value);
            ReDrawCanvas();
        }

        private void AddSpanToListView(Span span)
        {
            _spans.Add(span);
            var stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            var labelSpanText = new Label();
            labelSpanText.Content = span;
            var removeButton = new Button();
            removeButton.Content = "  X  ";
            removeButton.Click += RemoveButton_Click;

            stackPanel.Children.Add(labelSpanText);
            stackPanel.Children.Add(removeButton);
            removeButton.Tag = new Tuple<Span, StackPanel>(span, stackPanel);

            spanListView.Items.Add(stackPanel);
        }

        public void ReDrawCanvas()
        {
            lineCanvas.Children.Clear();
            _spansCollection = new NormalizedSpanCollection(spans: _spans);
            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollection, 
                lineColorBrush: Brushes.Red);
            DrawLinesFromSpanList(spanList: _spans.ToList(), lineColorBrush: Brushes.Green, 
                lineStrokeThickness: 20, verticalDisanceFromBottom: 60, seperateLines: true);

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Tuple<Span, StackPanel> tuple = (Tuple<Span, StackPanel>)button.Tag;

            _spans.Remove(tuple.Item1);
            spanListView.Items.Remove(tuple.Item2);
            ReDrawCanvas();
        }

        private void DrawLinesFromSpanList(List<Span> spanList, Brush lineColorBrush, 
            int verticalDisanceFromBottom, int lineStrokeThickness = 20, bool seperateLines = false)
        {
            var multiplier = 10;
            var leftMargin = 30;
            var lineToLineDistanceForSeperateLines = 50;

            for (int i = 0; i < spanList.Count; i++)
            {
                var span = spanList[i];

                var line = new Line();
                line.Visibility = Visibility.Visible;
                line.Stroke = lineColorBrush;
                line.StrokeThickness = lineStrokeThickness;
                line.StrokeEndLineCap = PenLineCap.Round;

                var lineLenght = span.End - span.Start;
                lineLenght = lineLenght * multiplier;
                line.X1 = leftMargin + span.Start * multiplier;
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
                AddTextToLine(line, span.ToString());
            }
        }

        private void AddTextToLine(Line line, string spanString)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = spanString;
            textBlock.Foreground = Brushes.Black;
            var leftAdjustment = 5;
            if (spanString.Length > 6)
                leftAdjustment = 10;
            Canvas.SetLeft(textBlock, (line.X1 + line.X2)/2 - leftAdjustment);
            Canvas.SetTop(textBlock, line.Y1 + 10);
            lineCanvas.Children.Add(textBlock);
        }

        private void ConvertSpanCollectionToLineSetAndAddToCanvas(NormalizedSpanCollection normalizedSpanCollection, 
            Brush lineColorBrush)
        {
            List<Span> spanList = normalizedSpanCollection.ToList();
            
            DrawLinesFromSpanList(spanList, lineColorBrush: Brushes.Red, verticalDisanceFromBottom: 30, 
                lineStrokeThickness: 20, seperateLines: false);
        }
    }
}