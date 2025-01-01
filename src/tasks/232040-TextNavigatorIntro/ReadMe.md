## Objective

1. Introduces `ITextStructureNavigatorSelectorService` and `ITextStructureNavigator`

2. 

## Build and Run
1. 

## Notes
1. `ITextStructureNavigatorSelectorService` is an MEF component, and its obtained as any other MEF component. 

2. Once you have i, get the `ITextStructureNavigator` for the text buffer. 

```cs
ITextStructureNavigator textNavigator = textStructureNavigatorSelectorService
                .GetTextStructureNavigator(textBuffer);
```

3. And then you can get the word as follows.

```cs
ITextCaret caret = wpfTextView.Caret;

SnapshotPoint point;

if (caret.Position.BufferPosition > 0)
      point = caret.Position.BufferPosition - 1;
else
{
      Debug.WriteLine("buffer position is 0. Cannot Continue.");
      return;
}

TextExtent extent = textNavigator.GetExtentOfWord(point);

wordTextBlock.Text = extent.Span.GetText();
```

## Reference.
1. https://mihailromanov.wordpress.com/2021/11/05/json-on-steroids-2-2-visual-studio-editor-itextbuffer-and-related-types
2. https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.operations.itextstructurenavigatorselectorservice
3. 


