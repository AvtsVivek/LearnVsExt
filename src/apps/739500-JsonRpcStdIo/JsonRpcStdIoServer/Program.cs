using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using JsonRpcStdIoServer;
using Nerdbank.Streams;
using StreamJsonRpc;

class Program
{
    static async Task<int> Main(string[] args)
    {
        // the args check to "stdio" is not necessary here. But kept it any way.
        if (args.Length > 0 && args[0] == "stdio")
        {
            await RespondToRpcRequestsAsync(FullDuplexStream.Splice(Console.OpenStandardInput(), Console.OpenStandardOutput()), 0);
        }
        return 0;
    }

    private static async Task RespondToRpcRequestsAsync(Stream stream, int clientId)
    {
        await Console.Error.WriteLineAsync($"Connection request #{clientId} received. Spinning off an async Task to cater to requests.");
        var jsonRpc = JsonRpc.Attach(stream, new Server());
        await Console.Error.WriteLineAsync($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");
        await jsonRpc.Completion;
        await Console.Error.WriteLineAsync($"Connection #{clientId} terminated.");
    }
}
