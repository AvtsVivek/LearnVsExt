using Microsoft.VisualStudio.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NormaliseSpanColVisualiseCodeBehind
{
    /// <summary>
    /// Interaction logic for VisualizeNormalizeSpanToolWindowControl.
    /// </summary>
    public partial class VisualizeNormalizeSpanToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisualizeNormalizeSpanToolWindowControl"/> class.
        /// </summary>
        public VisualizeNormalizeSpanToolWindowControl()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void buttonClickMe_Click(object sender, RoutedEventArgs e)
        {
            var spanArrayOne = new Span[] {
                        new Span(4, 7),  // [4 .. 11)
                        new Span(13, 4), // [13 .. 17)
                        new Span(1, 5),  // [1 .. 6)
                        new Span(0, 1),  // [0 .. 1)
                    };

            var normalizedSpanCollection1 =
                new NormalizedSpanCollection(spans: spanArrayOne);

            var spanArrayTwo = new Span[] {
                        //new Span(0, 110),  // [0 .. 11)
                        //new Span(130, 40)   // [13 .. 17)
                        new Span(0, 11),  // [0 .. 11)
                        new Span(13, 4)   // [13 .. 17)
                    };

            var normalizedSpanCollection2 =
                new NormalizedSpanCollection(spans: spanArrayTwo);

            ConvertSpanCollectionToLineSetAndAddToCanvas(normalizedSpanCollection: normalizedSpanCollection1, lineColorBrush: Brushes.Red);
            DrawLinesFromSpanList(spanList: spanArrayOne.ToList(), lineColorBrush: Brushes.Green, lineStrokeThickness: 20, verticalDisanceFromBottom: 60, seperateLines: true);
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
            }

        }

        private void ConvertSpanCollectionToLineSetAndAddToCanvas(NormalizedSpanCollection normalizedSpanCollection, Brush lineColorBrush)
        {
            List<Span> spanList = normalizedSpanCollection.ToList();
            DrawLinesFromSpanList(spanList, lineColorBrush: Brushes.Red, verticalDisanceFromBottom: 30, lineStrokeThickness: 20, seperateLines: false);
        }

        private void buttonClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            lineCanvas.Children.Clear();
        }
    }
}