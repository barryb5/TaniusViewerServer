// using Microsoft.AspNetCore.Builder;
// using TaniusViewerServer;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// var startup = new Startup();
// startup.Configure(app, builder.Environment);

// app.MapGet("/", () => "Hello World!");

// app.Run();


using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

Console.Title = "Server";
var builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://localhost:4000");
var app = builder.Build();

app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using (var webSocket = await context.WebSockets.AcceptWebSocketAsync())
        {
            while (true)
            {
                await webSocket.SendAsync(Encoding.ASCII.GetBytes($"Test - {DateTime.Now}"), WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(1000);
            }
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});
await app.RunAsync();