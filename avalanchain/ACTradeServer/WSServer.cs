using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace ACTradeServer
{
    public class WSServer
    {
        WebSocketServer _wssv = new WebSocketServer(6333);

        //public WSServer(WebSocketServer wss)
        //{
        //    _wssv = wss;
        //}
        public void StartServer()
        {
            
            //var httpsv = new HttpServer(6333);
            //httpsv.AddWebSocketService<Chat>("/Chat");
            //var wssv = new WebSocketServer(6333);
            _wssv.AddWebSocketService<Chat>("/Chat");

            _wssv.Start();
            if (_wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", _wssv.Port);
                foreach (var path in _wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            //Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();

            _wssv.Stop();
        }

        public void StopServer()
        {
            _wssv.Stop();
        }
    }

    public class Chat : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;

        public Chat()
            : this(null)
        {
        }

        public Chat(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "user#";
        }

        private string getName()
        {
            var name = Context.QueryString["username"];
            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Sessions.Broadcast(String.Format("{0} got logged off...", _name));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(String.Format("{{\"username\": \"{0}\", \"message\": {1}}}", _name, e.Data));
        }

        protected override void OnOpen()
        {
            _name = getName();
        }
    }
}
