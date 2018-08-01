using System;
using System.Net.Sockets;

namespace ACTradeServer
{
    class Program
    {
        static WSServer _webSocketServer = new WSServer();
        public static void Main()
        {
            try
            {
                _webSocketServer.StartServer();
                //Start();
            }
            catch (Exception e)
            {
                var tt = e;
                Console.WriteLine(e);

            }
        }

        //public static void Start()
        //{
        //    //var link = "ws://127.0.0.1:6333";
        //    var link = "ws://127.0.0.1:2024";
        //    var socket = IO.Socket(link);
        //    //socket.Emit("onConnected");
        //    Console.WriteLine("Start: " + link);
        //    socket.On(Socket.EVENT_CONNECT, () =>
        //    {
        //        //Console.WriteLine("Emit: hi");
        //        //socket.Emit("hi");

        //    });
        //    socket.Emit("hi");
        //    socket.On("hi", (data) =>
        //    {
        //        Console.WriteLine(data);
        //        //socket.Disconnect();
        //    });
        //    Console.ReadLine();

        //}

    }
}
