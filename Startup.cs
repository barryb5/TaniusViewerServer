using System.Net;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TaniusViewerServer {
    public class Startup {
        
        // public async void Start() {
            
        // }
        public void ConfigureServices(IServiceCollection services) {

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            Console.WriteLine("Configuring");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // app.UseEndpoints(endpoints => {
            //     endpoints.MapGet("/", async context => {
            //         await context.Response.WriteAsync("Hello World!");
            //     });
            // });

            var wsOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
            app.UseWebSockets(wsOptions);

            app.Use( async (context, next) => {
                if (context.Request.Path == "/send") {
                    if (context.WebSockets.IsWebSocketRequest) {
                        using(WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync()) {
                            await Send(context, webSocket);
                        }
                    }
                }
                 else {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                 }

                await next.Invoke();
            });
        }

        private async Task Send(HttpContext context, WebSocket webSocket) {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            if (result != null) {
                while(!result.CloseStatus.HasValue) {
                    string msg = Encoding.UTF8.GetString(new ArraySegment<byte>(buffer, 0, result.Count));
                    Console.WriteLine($"Client Says: {msg}");
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes($"Server says: {DateTime.UtcNow:f}")), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None );
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                    // Console.WriteLine(result);
                }
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
        }
    }
}