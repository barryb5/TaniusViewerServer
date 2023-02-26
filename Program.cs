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

using System.Text.Json;
using System.Text.Json.Serialization;

Console.Title = "Server";
var builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://localhost:4000");
var app = builder.Build();

JsonSerializerOptions options = new JsonSerializerOptions{
    Converters ={
        new JsonStringEnumConverter()
    }
};


app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using (var webSocket = await context.WebSockets.AcceptWebSocketAsync())
        {
            Console.WriteLine("New Listener");
            await webSocket.SendAsync(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new SnapshotData(), options)), WebSocketMessageType.Text, true, CancellationToken.None);

            while (true)
            {
                await webSocket.SendAsync(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new UpdateData(), options)), WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(5000);
            }
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});
await app.RunAsync();