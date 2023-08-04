## Understanding JsonRpc and StreamRpc

# References
1. https://github.com/microsoft/vs-streamjsonrpc/blob/main/doc/overview.md
2. https://learn.microsoft.com/en-us/dotnet/api/streamjsonrpc
3. https://github.com/AArnott/StreamJsonRpc.Sample
4. https://www.tpeczek.com/2020/06/json-rpc-in-aspnet-core-with.html
5. https://www.jsonrpc.org/specification

# What is JsonRpc
1. Look at [this overview](https://github.com/microsoft/vs-streamjsonrpc/blob/main/doc/overview.md#protocol-overview). JSON-RPC is a two-party, peer-to-peer based protocol by which one party can request a method's invocation of the other party, and optionally receive a response from the server with the result of that invocation.
2. [StreamJsonRpc](https://github.com/microsoft/vs-streamjsonrpc/blob/main/doc/overview.md#streamjsonrpcs-role) is a .NET library that implements the JSON-RPC protocol and exposes it via a .NET API to easily send and receive RPC requests. StreamJsonRpc works on any transport (e.g. Stream, WebSocket, Pipe).

# Running the example.
1. Just following the commands.sh steps. 


# How the example is created.
1. We need to copy the server output to the client output directory. So do the following.

2. Add a target to the JsonRpcStdIoServer.csproj as follows. The following copies all the files in the output directory. See this [Ms build docs](https://learn.microsoft.com/en-us/visualstudio/msbuild/copy-task#example-2)


```xml
<Target Name="CopyDLLs" AfterTargets="Build">
    <Message Text="Executing CopyDLLs task $(TargetDir)" Importance="High" />
    <ItemGroup>
        <MySourceFiles Include="$(TargetDir)**\*.*"/>
    </ItemGroup>
    <Copy
        SourceFiles="@(MySourceFiles)"
        DestinationFolder="..\JsonRpcStdIoClient\$(OutDir)" />
    <Message Text="Copied build files" Importance="High" />
</Target>
```

# Notes
1. In the server, obsere the following method.

```cs
private static async Task RespondToRpcRequestsUsingConsoleErrorAsync(Stream stream, int clientId)
{
    // https://stackoverflow.com/a/51760392/1977871
    await Console.Error.WriteLineAsync($"Connection request #{clientId} received. Spinning off an async Task to cater to requests.");
    var jsonRpc = JsonRpc.Attach(stream, new Server());
    await Console.Error.WriteLineAsync($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");
    await jsonRpc.Completion;
    await Console.Error.WriteLineAsync($"Connection #{clientId} terminated.");
}
```

If you use Console.WriteLine instead of Console.Error.WriteLineAsync, the example would throw exception. Not sure why. Need to find out. So instead of the above, if I use the following, the example would **NOT** run.


```cs
private static async Task RespondToRpcRequestsUsingConsoleAsync(Stream stream, int clientId)
{
    Console.WriteLine($"Connection request #{clientId} received. Spinning off an async Task to cater to requests.");
    var jsonRpc = JsonRpc.Attach(stream, new Server());
    Console.WriteLine($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");
    await jsonRpc.Completion;
    Console.WriteLine($"Connection #{clientId} terminated.");
}
```

2. Need to understand more. 
   1. What is FullDuplexStream?
   2. Console.OpenStandardInput() and Console.OpenStandardOutput()
   3. FullDuplexStream.Splice();

3. Basically, we need a stream to communicate between the client and server. In this case its obtained using FullDuplexStream. We can use a named pipe as well to connect to these two client and server, there is a subsequent example with NamedPipe. 

