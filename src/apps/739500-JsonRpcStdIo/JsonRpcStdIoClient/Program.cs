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
        // The passing of args "stdio" is not necessary here. But kept it any way.
        var psi = new ProcessStartInfo("JsonRpcStdIoServer.exe", "stdio");
        psi.RedirectStandardInput = true;
        psi.RedirectStandardOutput = true;
        var process = Process.Start(psi);
        var stdioStream = FullDuplexStream.Splice(process!.StandardOutput.BaseStream, process.StandardInput.BaseStream);
        await ActAsRpcClientAsync(stdioStream);
    }

    private static async Task ActAsRpcClientAsync(Stream stream)
    {
        Console.WriteLine("Connected. Sending request...");
        using var jsonRpc = JsonRpc.Attach(stream);
        int sum = await jsonRpc.InvokeAsync<int>("Add", 3, 5);
        Console.WriteLine($"3 + 5 = {sum}");
    }
}
