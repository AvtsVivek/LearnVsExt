using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSelectionHighlight
{
    public struct TextViewSelection
    {
        public TextViewPosition StartPosition { get; set; }
        public TextViewPosition EndPosition { get; set; }
        public string Text { get; set; }

        public TextViewSelection(TextViewPosition a, TextViewPosition b, string text)
        {
            StartPosition = TextViewPosition.Min(a, b);
            EndPosition = TextViewPosition.Max(a, b);
            Text = text;
        }

        public override string ToString()
        {
            return $"Text: {Text}, Start: {StartPosition}, End:{EndPosition}";
        }
    }
}
