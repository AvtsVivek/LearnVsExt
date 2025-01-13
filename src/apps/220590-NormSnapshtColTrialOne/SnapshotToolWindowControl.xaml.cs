using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NormSnapshtColTrialOne
{
    internal enum LineSelection
    {
        One,
        Two,
        Union,
        UnionReverse,
        Overlap,
        OverlapReverse,
        Intersect,
        IntersectReverse,
        Difference,
        DifferenceReverse
    }
    /// <summary>
    /// Interaction logic for SnapshotToolWindowControl.
    /// </summary>
    public partial class SnapshotToolWindowControl : UserControl
    {
        private List<Span> _spanListOne = new List<Span> ();

        private List<Span> _spanListTwo = new List<Span>();

        private List<Span> _currentSpanList = new List<Span>();

        private NormalizedSpanCollection _spansCollectionOne = new NormalizedSpanCollection ();
        private NormalizedSpanCollection _spansCollectionTwo = new NormalizedSpanCollection ();

        private LineSelection _lineSelection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotToolWindowControl"/> class.
        /// </summary>
        public SnapshotToolWindowControl()
        {
            this.InitializeComponent();
            SetLineAsPerRadioButtonSelected();
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
                "SnapshotToolWindow");
        }

        private void buttonClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();
            spanListView.Items.Clear();


            _spanListOne.Clear();
            _spanListTwo.Clear();
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
                MessageBox.Show("Start value cannot be greater than end value. Please End value is always greater than start value ", "End value is less than the start value", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            span = new Span(start: startValue, length: endValue - startValue);

            if (_currentSpanList.Contains(span.Value))
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

            AddSpanToListView(span.Value);
            ReDrawCanvas();
        }

        private void AddSpanToListView(Span span)
        {
            _currentSpanList.Add(span);
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

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Tuple<Span, StackPanel> tuple = (Tuple<Span, StackPanel>)button.Tag;

            _currentSpanList.Remove(tuple.Item1);
            spanListView.Items.Remove(tuple.Item2);
            ReDrawCanvas();
        }

        public void ReDrawCanvas()
        {
            lineCanvas.Children.Clear();

            _spansCollectionOne = new NormalizedSpanCollection(spans: _spanListOne);
            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionOne, LineSelection.One);

            _spansCollectionTwo = new NormalizedSpanCollection(spans: _spanListTwo);
            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionTwo, LineSelection.Two);

            DrawLinesFromSpanList(spanList: _currentSpanList.ToList(), lineColorBrush: Brushes.Green,
                verticalDisanceFromBottom: 500, seperateLines: true);
        }

        private void DrawLinesFromSpanList(List<Span> spanList, Brush lineColorBrush,
            int verticalDisanceFromBottom, bool seperateLines = false)
        {
            int lineStrokeThickness = 20;
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

            Canvas.SetLeft(textBlock, (line.X1 + line.X2) / 2 - leftAdjustment);
            Canvas.SetTop(textBlock, line.Y1 + 10);
            lineCanvas.Children.Add(textBlock);
        }

        private void ConvertSpanCollectionToLineSetAndAddToCanvas(NormalizedSpanCollection normalizedSpanCollection,
            LineSelection lineSelection)
        {
            Brush lineColorBrush = Brushes.Red;
            int verticalDisanceFromBottom = 500;

            if (lineSelection == LineSelection.Two)
            {
                lineColorBrush = Brushes.Orange;
                verticalDisanceFromBottom = 450;
            }

            if (lineSelection == LineSelection.Union)
            {
                lineColorBrush = Brushes.LightBlue;
                verticalDisanceFromBottom = 400;
            }

            if (lineSelection == LineSelection.UnionReverse)
            {
                lineColorBrush = Brushes.Blue;
                verticalDisanceFromBottom = 350;
            }

            if (lineSelection == LineSelection.Overlap)
            {
                lineColorBrush = Brushes.LightGreen;
                verticalDisanceFromBottom = 300;
            }

            if (lineSelection == LineSelection.OverlapReverse)
            {
                lineColorBrush = Brushes.Green;
                verticalDisanceFromBottom = 250;
            }

            if (lineSelection == LineSelection.Intersect)
            {
                lineColorBrush = Brushes.LightPink;
                verticalDisanceFromBottom = 200;
            }

            if (lineSelection == LineSelection.IntersectReverse)
            {
                lineColorBrush = Brushes.HotPink;
                verticalDisanceFromBottom = 150;
            }

            if (lineSelection == LineSelection.Difference)
            {
                lineColorBrush = Brushes.BlueViolet;
                verticalDisanceFromBottom = 100;
            }

            if (lineSelection == LineSelection.DifferenceReverse)
            {
                lineColorBrush = Brushes.Violet;
                verticalDisanceFromBottom = 50;
            }

            List<Span> spanList = normalizedSpanCollection.ToList();

            DrawLinesFromSpanList(spanList, lineColorBrush, verticalDisanceFromBottom, seperateLines: false);
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton == null)
            {
                // Something is wrong. Just get out from here.
                return;
            }

            SetLineAsPerRadioButtonSelected();

            if (_lineSelection == LineSelection.Two)
            {
                _spanListTwo.Clear();
                _spanListTwo.AddRange(_spanListOne);
            }

            if (_lineSelection == LineSelection.One)
            {
                _spanListOne.Clear();
                _spanListOne.AddRange(_spanListTwo);
            }

            ReDrawCanvas();

        }

        private void SetLineAsPerRadioButtonSelected()
        {
            _lineSelection = LineSelection.One;

            borderForRbTwo.Background = Brushes.Transparent;
            borderForRbOne.Background = Brushes.Transparent;

            if (oneRadioButton.IsChecked == twoRadioButton.IsChecked)
            {
                oneRadioButton.IsChecked = true;
                twoRadioButton.IsChecked = false;
                _lineSelection = LineSelection.One;
                _currentSpanList = _spanListOne;
                borderForRbOne.Background = Brushes.Red;
                return;
            }

            if (oneRadioButton.IsChecked.Value)
            {
                twoRadioButton.IsChecked = false;
                _lineSelection = LineSelection.One;
                _currentSpanList = _spanListOne;
                borderForRbOne.Background = Brushes.Red;
                return;
            }

            if (twoRadioButton.IsChecked.Value)
            {
                oneRadioButton.IsChecked = false;
                _lineSelection = LineSelection.Two;
                _currentSpanList = _spanListTwo;
                borderForRbTwo.Background = Brushes.Orange;
                return;
            }
        }

        private void buttonUnion_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionUnion = NormalizedSpanCollection.Union(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionUnion, LineSelection.Union);
        }

        private void buttonUnionReverse_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionUnionReverse = NormalizedSpanCollection.Union(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionUnionReverse, LineSelection.UnionReverse);
        }

        private void buttonOverlap_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionOverlap = NormalizedSpanCollection.Overlap(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionOverlap, LineSelection.Overlap);
        }

        private void buttonOverlapReverse_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionOverlapReverse = NormalizedSpanCollection.Overlap(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionOverlapReverse, LineSelection.OverlapReverse);
        }

        private void buttonIntersect_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionIntersect = NormalizedSpanCollection.Intersection(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionIntersect, LineSelection.Intersect);
        }

        private void buttonIntersectReverse_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionIntersectReverse = NormalizedSpanCollection.Intersection(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionIntersectReverse, LineSelection.IntersectReverse);
        }

        private void buttonDifference_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionDifference = NormalizedSpanCollection.Difference(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionDifference, LineSelection.Difference);
        }

        private void buttonDifferenceReverse_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionDifferenceReverse = NormalizedSpanCollection.Difference(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionDifferenceReverse, LineSelection.DifferenceReverse);
        }

        private void buttonRedraw_Click(object sender, RoutedEventArgs e)
        {
            ReDrawCanvas();
        }

        private void CheckSpanCollections()
        {
            if (_spansCollectionOne == null)
            {
                MessageBox.Show("The normalized span collection One is null. Cannot continue.",
                    "Normalized span collection is null.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_spansCollectionTwo == null)
            {
                MessageBox.Show("The normalized span collection Two is null. Cannot continue.",
                    "Normalized span collection is null.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAll_Click(object sender, RoutedEventArgs e)
        {
            CheckSpanCollections();

            var _spansCollectionUnion = NormalizedSpanCollection.Union(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionUnion, LineSelection.Union);

            var _spansCollectionUnionReverse = NormalizedSpanCollection.Union(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionUnionReverse, LineSelection.UnionReverse);

            var _spansCollectionOverlap = NormalizedSpanCollection.Overlap(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionOverlap, LineSelection.Overlap);

            var _spansCollectionOverlapReverse = NormalizedSpanCollection.Overlap(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionOverlapReverse, LineSelection.OverlapReverse);

            var _spansCollectionIntersect = NormalizedSpanCollection.Intersection(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionIntersect, LineSelection.Intersect);

            var _spansCollectionIntersectReverse = NormalizedSpanCollection.Intersection(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionIntersectReverse, LineSelection.IntersectReverse);

            var _spansCollectionDifference = NormalizedSpanCollection.Difference(_spansCollectionOne, _spansCollectionTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionDifference, LineSelection.Difference);

            var _spansCollectionDifferenceReverse = NormalizedSpanCollection.Difference(_spansCollectionTwo, _spansCollectionOne);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: _spansCollectionDifferenceReverse, LineSelection.DifferenceReverse);
        }
    }
}