## Objective

1. Introduces
   1. [Tracking](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.tracking)
   2. [SpanTrackingMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.spantrackingmode)
   3. [PointTrackingMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.pointtrackingmode)
   4. [ITrackingPoint](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.itrackingpoint)
   5. [ITrackingSpan](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.text.itrackingspan)

2. 

![Point Tracking Mode](..\221500-TextBufferIntro\Images\68_50_EdgePositive.png)

EdgeExclusive - only characters from the old interval should be included in the final interval and not newly inserted characters. To do this, the initial position of the interval works in the Positive mode, and the end position works in the Negative mode

![Edge Exclusive](Images/50_50_EdgeExclusiveInsert.png)

EdgeInclusive is the opposite of EdgeExclusive, i.e. all intersecting intervals are included in the original one. Therefore, boundaries are tracked in opposite modes: the beginning in Negative and the end in Positive

![Edge Insclusive](Images/51_50_EdgeInclusiveInsert.png)

EdgePositive – Both Borders in Pisitive Mode

![Edge Positive](Images/52_50_EdgePositiveInsert.png)

EdgeNegative – Both boundaries in Negative mode

![Edge Negative](Images/53_50_EdgeNegativeInsert.png)

## Build and Run


## Reference.

1. https://mihailromanov.wordpress.com/2021/11/05/json-on-steroids-2-2-visual-studio-editor-itextbuffer-and-related-types


## ToDo

1. .CreateTrackingSpan( is found in the following exmaples. Need to look into them. Also 1 and 4 seem to be the same AsyncQuickInfoSourceIntro
   1. src\apps\252230-AsyncQuickInfoSourceIntro\LineAsyncQuickInfoSource.cs
   2. src\apps\252250-QuickInfoSourceNoAttribute\LineAsyncQuickInfoSource.cs
   3. src\apps\252275-FilterInsideQuickInfoSource\LineAsyncQuickInfoSource.cs
   4. src\apps\255550-AsyncQuickInfoSourceIntro\LineAsyncQuickInfoSource.cs
   5. src\apps\255570-AsyncQuickInfoExTwo\EvenLineAsyncQuickInfoSource.cs
   6. src\apps\255600-QuickInfoCustomContentType\LineAsyncQuickInfoSource.cs
   7. src\apps\750580-MenuCommentAdornment\CommentAdornmentTest\CommentAdornment.cs
   8. src\apps\800500-ColorfulEditor\ColorfulCompletionSource.cs
   9.  src\apps\800750-AsyncQuickInfo\LineAsyncQuickInfoSource.cs
   10. src\apps\800950-OokLanguage\Intellisence\OokCompletionSource.cs
   11. src\apps\800950-OokLanguage\Intellisence\OokQuickInfoSource.cs
   12. src\apps\900921-Temp\src\VsctCompletionShared\QuickInfo\IdQuickInfoSource.cs
   13. src\apps\900925-AsyncCompletionSourceIntro\JsonElementCompletion\SampleCompletionSource.cs


