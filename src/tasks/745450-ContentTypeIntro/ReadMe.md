
## Demos the significance of ContentType attribute

1. What is Content Type?

2. Content types are the definitions of the kinds of text handled by the editor, for example, "text", "code", or "CSharp". You define a new content type by declaring a variable of the type ContentTypeDefinition and giving the new content type a unique name. To register the content type with the editor, export it together with the following attributes:

[NameAttribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.nameattribute) is the name of the content type.

[BaseDefinitionAttribute](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.basedefinitionattribute) is the name of the content type from which this content type is derived. A content type may inherit from multiple other content types.

Because the [ContentTypeDefinition](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.utilities.contenttypedefinition) class is sealed, you can export it with no type parameter.

The following example shows export attributes on a content type definition. 

3. Also take a look at the example ContentTypeRegistryServiceIntro 

4. Take note that ContentType attribute is needed in the class.

```cs
[ContentType("foo")] // If we comment out this line, this class will not be instanciated. 
// The following break point in the ctor (Debugger.Break();) will not be hit if this is commented out.
[Export(typeof(ILanguageClient))]
[RunOnContext(RunningContext.RunOnHost)]
public class FooLanguageClient : ILanguageClient, ILanguageClientCustomMessage2
{
    public FooLanguageClient()
    {
        Debugger.Break();
        Instance = this;
    }
}
```

5. You may get exception as follows. You can ignore for now.

![File Changes](./images/50_50_InvalidOperationException.jpg)

The exception is because of the following code. ILanguageClient is not fully implimented as it should be and so this exception. 

```cs
public Task<InitializationFailureContext> OnServerInitializeFailedAsync(ILanguageClientInitializationInfo initializationState)
{
    Debugger.Break();
    ...
    return Task.FromResult(failureContext);
}
```

6. Note that a non null connection must be returned as follows from the **ActivateAsync(CancellationToken token)** method. But if you return null, an exception will be thrown as explained above.

```cs
public async Task<Connection> ActivateAsync(CancellationToken token)
{
    // Debugger.Break();

    var stdInPipeName = @"output";
    var stdOutPipeName = @"input";

    var pipeAccessRule = new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite, System.Security.AccessControl.AccessControlType.Allow);
    var pipeSecurity = new PipeSecurity();
    pipeSecurity.AddAccessRule(pipeAccessRule);

    var bufferSize = 256;

    var readerPipe = new NamedPipeServerStream(stdInPipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous, bufferSize, bufferSize, pipeSecurity);
    var writerPipe = new NamedPipeServerStream(stdOutPipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous, bufferSize, bufferSize, pipeSecurity);

    await readerPipe.WaitForConnectionAsync(token);
    await writerPipe.WaitForConnectionAsync(token);

    return null;
}
```

7. Its still not clear what that connection is, need to find more. 

## References
1. https://learn.microsoft.com/en-us/visualstudio/extensibility/language-service-and-editor-extension-points#extend-content-types