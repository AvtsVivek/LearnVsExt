using System.Net;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var rand = new Random();

        while (true)
        {
            var now = DateTime.Now;
            byte[] dataToSend = Encoding.ASCII.GetBytes($"{now}");
            await webSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, CancellationToken.None);
            await Task.Delay(1000);

            long r = rand.NextInt64(0, 10);

            if (r == 7)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                    "random closing", CancellationToken.None);

                return;
            }
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});

app.Run("http://localhost:5050");
