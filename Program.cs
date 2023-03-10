// using Microsoft.AspNetCore.Builder;
// using TaniusViewerServer;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// var startup = new Startup();
// startup.Configure(app, builder.Environment);

// app.MapGet("/", () => "Hello World!");

// app.Run();

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;


using System.Net.WebSockets;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net;

Console.Title = "Server";
var builder = WebApplication.CreateBuilder();

builder.WebHost.UseUrls("http://localhost:4000");
var app = builder.Build();

JsonSerializerOptions options = new JsonSerializerOptions{
    Converters ={
        new JsonStringEnumConverter()
    }
};

List<WebSocket> clientWebSockets = new List<WebSocket>();


app.UseWebSockets();
app.Map("/ws", async context => {
    if (context.WebSockets.IsWebSocketRequest) {
        byte[] requestBuffer = new byte[4194304];
        int offset = 0;

        using (var webSocket = await context.WebSockets.AcceptWebSocketAsync()) {
            clientWebSockets.Add(webSocket);
            int listenerID = clientWebSockets.Count;
            Console.WriteLine("New Listener " + listenerID);
            await webSocket.SendAsync(Encoding.ASCII.GetBytes(JsonSerializer.Serialize(new SnapshotData(), options)), WebSocketMessageType.Text, true, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open) {
                var requestSegment = new ArraySegment<byte>(requestBuffer, offset, requestBuffer.Length - offset);
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(requestSegment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close) {
                    // Close
                    Console.WriteLine("Listener " + listenerID + " Disconnected");
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
            }
        }
    } else {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});
// await app.RunAsync();

Action<string> sendAll = async (message) =>
{
    List<WebSocket> toRemove = new List<WebSocket>();

    for (int i = clientWebSockets.Count - 1; i >= 0; i--){
        // Send here
        if (clientWebSockets[i].State == WebSocketState.Open) {
            await clientWebSockets[i].SendAsync(Encoding.ASCII.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
        } else {
            // Remove value if websocketstate isn't open so no errors
            clientWebSockets.RemoveAt(i);
        }
    }
};

// 


Thread socketThread = new Thread(() => {app.Run();}) {
    IsBackground = true,
};

Thread updateThread = new Thread(() => {
    while(true) {
        sendAll(JsonSerializer.Serialize(new UpdateData(), options));
        Thread.Sleep(5000);
    }
}) {};

Thread authenticationThread = new Thread(() => {
    var builder = WebApplication.CreateBuilder();

    builder.WebHost.UseUrls("http://localhost:5000");
    
    var app = builder.Build();
    
    app.MapGet("/", () => {
        return "This is the authentication server";
    });

    app.MapGet("/authenticate", () => {
        Console.WriteLine("user");
    });

    app.Run();

    
});

socketThread.Start();
updateThread.Start();
authenticationThread.Start();
// Console.ReadLine();
