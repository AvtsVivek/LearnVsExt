using System.Net.WebSockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.Title = "Client";

Console.WriteLine("Here we go client...");

using var clientWebSocket = new ClientWebSocket();

Console.WriteLine($"clientWebSocket.Options.DangerousDeflateOptions {clientWebSocket.Options.DangerousDeflateOptions}");

await clientWebSocket.ConnectAsync(new Uri("ws://localhost:5050/"), CancellationToken.None);
byte[] buf = new byte[1056];

Console.WriteLine(clientWebSocket.State);

while (clientWebSocket.State == WebSocketState.Open)
{
    Console.WriteLine("Test 1");

    var encoded = Encoding.UTF8.GetBytes("Message from Client.");
    var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);

    try
    {
        await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        // await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, WebSocketMessageFlags.EndOfMessage | WebSocketMessageFlags.DisableCompression, CancellationToken.None);
    }
    catch (Exception excep)
    {
        Console.WriteLine($"Exception after Send Async{excep.Message}");
    }


    try
    {
        var webSocketReceiveResult = await clientWebSocket.ReceiveAsync(buf, CancellationToken.None);

        Console.WriteLine(webSocketReceiveResult.Count);

        Console.WriteLine("Test 2");

        if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
        {
            await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            Console.WriteLine(webSocketReceiveResult.CloseStatusDescription);
        }
        else
        {
            Console.WriteLine(Encoding.ASCII.GetString(buf, 0, webSocketReceiveResult.Count));
        }
    }
    catch (Exception excep)
    {
        Console.WriteLine($"Exception after Receive Async.{Environment.NewLine}{excep.Message}");
    }
}