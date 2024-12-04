## Objective 

1. In the previous example(AsyncQuickInfoSourceIntro), `IAsyncQuickInfoSourceProvider` is introduced.
2. This almost same, and hence it demos the same.
3. The difference is in the `LineAsyncQuickInfoSourceProvider` class. The difference is just one.
4. And thats, the ContentType Attribute. Earlier the class look like this.
```cs
[Export(typeof(IAsyncQuickInfoSourceProvider))]
[Name("Line Async Quick Info Provider")]
[ContentType(CustomContentTypeConstants.ContentTypeName)]
[Order]
internal sealed class LineAsyncQuickInfoSourceProvider : IAsyncQuickInfoSourceProvider
{
    public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
    {
        // This ensures only one instance per textbuffer is created
        return textBuffer.Properties.GetOrCreateSingletonProperty(() => new LineAsyncQuickInfoSource(textBuffer));
    }
}
```
5. Now the class looks like the following.

```cs
[Export(typeof(IAsyncQuickInfoSourceProvider))]
[Name("Line Async Quick Info Provider")]
[ContentType("any")] // This line is needed. If you completely remove this attribute, the following method is not called at all.
[Order]
internal sealed class LineAsyncQuickInfoSourceProvider 
    : IAsyncQuickInfoSourceProvider
{
    public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer)
    {
        return textBuffer.Properties.GetOrCreateSingletonProperty(() => new LineAsyncQuickInfoSource(textBuffer));

    }
}
```

6. So there is ContentType attribute. Earlier the content type is `CustomContentTypeConstants.ContentTypeName`. Now its `any`. Note that this `[ContentType("any")]` is necessary. If you comment it out and remove it completely, then the method `TryCreateQuickInfoSource` is not called at all.

7. Next `LineAsyncQuickInfoSource` class. In here, we have added the following if condition right at the onset of the method.

```cs
public Task<QuickInfoItem> GetQuickInfoItemAsync(IAsyncQuickInfoSession session, CancellationToken cancellationToken)
{
    if (_textBuffer.CurrentSnapshot.ContentType.TypeName != CustomContentTypeConstants.ContentTypeName)
        return Task.FromResult<QuickInfoItem>(null);
        
    ///// Rest of the code.
}
```

8. So if the content type is say `CSharp`, this method returns immediately. Only if the content type is fooabcd the method executes completely.

9. Also note that this method is called each time, mouse hover happens. On the contrary, `TryCreateQuickInfoSource` from the `LineAsyncQuickInfoSourceProvider` is only called each time a file is opened, not each time mouse is hovered.


10. Try it. Test the say way by opening `FooAbcdTextFile.FooAbcd` as well as Class1.cs


