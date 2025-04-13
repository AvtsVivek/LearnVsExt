## Objective

1. Introduces
   1. [Tracking](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.tracking)
   2. [SpanTrackingMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.spantrackingmode)
   3. [PointTrackingMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.pointtrackingmode)
   4. [ITrackingPoint](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.itrackingpoint)
   5. [ITrackingSpan](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.itrackingspan)

2. This example attempts to explore different types value of the enum SpanTrackingMode namely EdgeExclusive, EdgeInclusive, EdgePositive, EdgeNegative. 

![Open tool window](Images/51_50_ToolWindow.png)

But all of the 4 are giving the same result. Need to explore more.


## Build and Run
1. Reset Visual Studio Exp instance and then Launch it.

2. Ensure the line numbers are enabled for text files as follows.
   1. Tools -> Options -> Text Editor -> All Languages -> General

   ![Enable Line numbers](Images/50_50_EnableLineNumbers.png)

3. Now open the tool window. View -> Other Windows -> ToolWindowForTextSnapShot

![Open tool window](Images/51_50_ToolWindow.png)



## Reference.

1. https://mihailromanov.wordpress.com/2021/11/05/json-on-steroids-2-2-visual-studio-editor-itextbuffer-and-related-types

2. For the full article, [click here](..\220560-TextBufferIntro\1-ITextBuffer.md)

## ToDo

1. .CreateTrackingSpan() is found in the following examples. Need to look into them. Also 1 and 4 seem to be the same AsyncQuickInfoSourceIntro
   1. src\apps\252230-AsyncQuickInfoSourceIntro\LineAsyncQuickInfoSource.cs
   2. src\apps\252250-QuickInfoSourceNoAttribute\LineAsyncQuickInfoSource.cs
   3. src\apps\252275-FilterInsideQuickInfoSource\LineAsyncQuickInfoSource.cs
   4. src\apps\255550-AsyncQuickInfoSourceIntro\LineAsyncQuickInfoSource.cs
   5. src\apps\255570-AsyncQuickInfoExTwo\EvenLineAsyncQuickInfoSource.cs
   6. src\apps\255600-QuickInfoCustomContentType\LineAsyncQuickInfoSource.cs
   7. src\apps\750580-MenuCommentAdornment\CommentAdornmentTest\CommentAdornment.cs
   8. src\apps\800500-ColorfulEditor\ColorfulCompletionSource.cs
   9.  src\apps\800750-AsyncQuickInfo\LineAsyncQuickInfoSource.cs
   10. src\apps\800950-OokLanguage\Intellisense\OokCompletionSource.cs
   11. src\apps\800950-OokLanguage\Intellisense\OokQuickInfoSource.cs
   12. src\apps\900921-Temp\src\VsctCompletionShared\QuickInfo\IdQuickInfoSource.cs
   13. src\apps\900925-AsyncCompletionSourceIntro\JsonElementCompletion\SampleCompletionSource.cs


