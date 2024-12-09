## Objective

1. Introduces `ITextSnapshot` 

2. For the full article, [click here](..\221500-TextBufferIntro\1-ITextBuffer.md)

3. 

## Build and Run
1. Reset Visual Studio Exp instance and then Launch it.

2. Ensure the line numbers are enabled for text files as follows.
   1. Tools -> Options -> Text Editor -> All Languages -> General

   ![Enable Line numbers](Images/50_50_EnableLineNumbers.png)

3. Now open the tool window. View -> Other Windows -> ToolWindowForTextSnapShot

![Open tool window](Images/51_50_ToolWindow.png)

4. Open a file say Class1.cs, then click Refresh button on the tool window. Observe line no fields etc.

![Click Refresh button](Images/52_50_RefreshClass1CsFile.png)

5. Next click any where in side the text file say on the line `public Class()`. Then click the Refresh button again and observe. 

![Full data](Images/53_50_RefreshClass1CsFileFull.png)



## Notes

1. From `wpfTextView`, we get the `textBuffer`, and then from it, we can get the `textSanpshot`. The textSnapshot is a collection of lines(`ITextSnapshotLine`). The `ITextSnapshotLine` has the line number as a property.

```cs
IWpfTextView wpfTextView = GetCurentWpfTextView();

ITextBuffer textBuffer = wpfTextView.TextBuffer;

ITextSnapshot textSnapshot = textBuffer.CurrentSnapshot;

List<ITextSnapshotLine> lines = textSnapshot.Lines.ToList();
```

2. If you want to get the details of a line where the caret is currently present, you can get it as follows.

```cs
ITextCaret caret = wpfTextView.Caret;

CaretPosition caretPosition = caret.Position;

List<IWpfTextViewLine> wpfTextViewLines = wpfTextView.TextViewLines.WpfTextViewLines.ToList();

SnapshotPoint caretPositionSnapshotPoint = caretPosition.BufferPosition;

ITextSnapshotLine caretLine = caretPositionSnapshotPoint.GetContainingLine();
```

## Reference.
1. https://mihailromanov.wordpress.com/2021/11/05/json-on-steroids-2-2-visual-studio-editor-itextbuffer-and-related-types


