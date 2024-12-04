using System.Windows;
using System.Windows.Media;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace CustomizeTextView
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    // [ContentType("code")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class TestViewCreationListener : IWpfTextViewCreationListener
    {
        [Import]
        internal IEditorFormatMapService FormatMapService = null;
        // This method is executed when you open any file in the editor window
        public void TextViewCreated(IWpfTextView textView)
        {
            IEditorFormatMap formatMap = FormatMapService.GetEditorFormatMap(textView);

            ResourceDictionary regularCaretProperties = formatMap.GetProperties("Caret");
            ResourceDictionary overwriteCaretProperties = formatMap.GetProperties("Overwrite Caret");
            ResourceDictionary indicatorMargin = formatMap.GetProperties("Indicator Margin");
            ResourceDictionary visibleWhitespace = formatMap.GetProperties("Visible Whitespace");
            ResourceDictionary selectedText = formatMap.GetProperties("Selected Text");
            ResourceDictionary inactiveSelectedText = formatMap.GetProperties("Inactive Selected Text");

            formatMap.BeginBatchUpdate();

            regularCaretProperties[EditorFormatDefinition.ForegroundBrushId] = Brushes.Magenta;
            formatMap.SetProperties("Caret", regularCaretProperties);

            overwriteCaretProperties[EditorFormatDefinition.ForegroundBrushId] = Brushes.Turquoise;
            formatMap.SetProperties("Overwrite Caret", overwriteCaretProperties);

            indicatorMargin[EditorFormatDefinition.BackgroundColorId] = Colors.LightGreen;
            formatMap.SetProperties("Indicator Margin", indicatorMargin);

            visibleWhitespace[EditorFormatDefinition.ForegroundColorId] = Colors.Yellow;
            visibleWhitespace[EditorFormatDefinition.BackgroundBrushId] = Colors.Red;
            formatMap.SetProperties("Visible Whitespace", visibleWhitespace);

            selectedText[EditorFormatDefinition.BackgroundBrushId] = Brushes.LightPink;
            formatMap.SetProperties("Selected Text", selectedText);

            inactiveSelectedText[EditorFormatDefinition.BackgroundBrushId] = Brushes.DeepPink;
            formatMap.SetProperties("Inactive Selected Text", inactiveSelectedText);

            formatMap.EndBatchUpdate();
        }
    }
}
