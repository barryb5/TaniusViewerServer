using System;
using System.Threading;
using Websocket.Client;
using Newtonsoft.Json;

namespace TaniusViewerServer
{
    class Program
    {
        private string streaming_API_Key = "YOUR_USER_KEY";
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.Initialize();
        }
        private void Initialize()
        {
            Console.CursorVisible = false;
            try
            {
                var exitEvent = new ManualResetEvent(false);
                var url = new Uri("wss://marketdata.tradermade.com/feedadv");
                
                using (var client = new WebsocketClient(url))
                {
                    client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                    client.ReconnectionHappened.Subscribe(info =>
                    {
                        Console.WriteLine("Reconnection happened, type: " + info.Type);
                    });
                    client.MessageReceived.Subscribe(msg =>
                    {
                        Console.WriteLine("Message received: " + msg);
                        if (msg.ToString().ToLower() == "connected")
                        {
                            string data = "{\"userKey\":\"" + streaming_API_Key + "\", \"symbol\":\"EURUSD,GBPUSD,USDJPY\"}";
                            client.Send(data);
                        }
                    });
                    client.Start();
                    Task.Run(() => client.Send("{ message }"));
                    exitEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
            Console.ReadKey();
        }
    }
}