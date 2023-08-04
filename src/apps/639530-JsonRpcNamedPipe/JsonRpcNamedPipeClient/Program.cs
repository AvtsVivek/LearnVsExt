using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Reflection;
using System.Threading.Tasks;
using Nerdbank.Streams;
using StreamJsonRpc;

class Program
{
    static async Task Main()
    {

        Console.WriteLine("Connecting to server...");
        using (var stream = new NamedPipeClientStream(".", "StreamJsonRpcSamplePipe", PipeDirection.InOut, PipeOptions.Asynchronous))
        {
            await stream.ConnectAsync();
            await ActAsRpcClientAsync(stream);
            Console.WriteLine("Terminating stream...");
        }
        
    }

    private static async Task ActAsRpcClientAsync(Stream stream)
    {
        Console.WriteLine("Connected. Sending request...");
        using var jsonRpc = JsonRpc.Attach(stream);
        int sum = await jsonRpc.InvokeAsync<int>("Add", 3, 5);
        Console.WriteLine($"3 + 5 = {sum}");
    }
}
