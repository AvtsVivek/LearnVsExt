## Objective 

1. This example is similar to ITagOne. This example has ITaggerProvider also.
2. 


## How the project is created. 

1. Make this into a MEF component.

![Mef Component](images/50_50_MakeMEFComponent.png)

  

## Build and Run


TextMarkTag

```cs
[Export(typeof(EditorFormatDefinition))]
[Name("TextMarkTagFormatName")]
[UserVisible(true)]
public class TextMarkTaggerFormatDefinition : EditorFormatDefinition
{
    protected TextMarkTaggerFormatDefinition()
    {
        BackgroundColor = Colors.Bisque;
        ForegroundColor = Colors.Black;

        DisplayName = "Demo tagger mark format";
    }
}
```

```cs
[Export(typeof(EditorFormatDefinition))]
[Name("TextMarkTagFormatName")]
[UserVisible(true)]
public class TextMarkTaggerFormatDefinition : EditorFormatDefinition
{
    protected TextMarkTaggerFormatDefinition()
    {
        BackgroundColor = Colors.Red;
        ForegroundColor = Colors.Black;

        DisplayName = "Demo tagger mark format";
    }
}
```

## Todo

1.   