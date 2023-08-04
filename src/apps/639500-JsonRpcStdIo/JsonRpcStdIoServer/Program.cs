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
            await RespondToRpcRequestsUsingConsoleErrorAsync(FullDuplexStream.Splice(Console.OpenStandardInput(), Console.OpenStandardOutput()), 0);
            // await RespondToRpcRequestsUsingConsoleAsync(FullDuplexStream.Splice(Console.OpenStandardInput(), Console.OpenStandardOutput()), 0);
        }
        return 0;
    }

    private static async Task RespondToRpcRequestsUsingConsoleErrorAsync(Stream stream, int clientId)
    {
        // https://stackoverflow.com/a/51760392/1977871
        await Console.Error.WriteLineAsync($"Connection request #{clientId} received. Spinning off an async Task to cater to requests.");
        var jsonRpc = JsonRpc.Attach(stream, new Server());
        await Console.Error.WriteLineAsync($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");
        await jsonRpc.Completion;
        await Console.Error.WriteLineAsync($"Connection #{clientId} terminated.");
    }

    // The following does not work. Not sure why.
    private static async Task RespondToRpcRequestsUsingConsoleAsync(Stream stream, int clientId)
    {
        Console.WriteLine($"Connection request #{clientId} received. Spinning off an async Task to cater to requests.");
        var jsonRpc = JsonRpc.Attach(stream, new Server());
        Console.WriteLine($"JSON-RPC listener attached to #{clientId}. Waiting for requests...");
        await jsonRpc.Completion;
        Console.WriteLine($"Connection #{clientId} terminated.");
    }
}
