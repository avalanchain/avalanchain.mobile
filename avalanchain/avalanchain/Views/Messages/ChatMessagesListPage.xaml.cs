using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PureWebSockets;
using Quobject.SocketIoClientDotNet.Client;
using Xamarin.Forms;

namespace avalanchain
{
	public partial class ChatMessagesListPage : ContentPage
	{
        //private static PureWebSocket _ws;
	    ClientWebSocket client = new ClientWebSocket();
	    CancellationTokenSource cts = new CancellationTokenSource();
        public ChatMessagesListPage ()
		{
			InitializeComponent ();
		    
            SetupChat();
		    try
		    {
		    //ConnectToServerAsync();
		    }
		    catch (Exception e)
		    {
		        var tt = e;
		    }
		    //PureWebSocket();

        }

		public void SetupChat(){
			var chatMessagesList = SampleData.ChatMessagesList;
			User FirstUser = SampleData.ChatMessagesList[0].From;
			View widget;

			for (int index=0; index < chatMessagesList.Count; index++){
				
				if ((chatMessagesList [index] as ChatMessage).From != FirstUser) {
					widget = new ChatRightMessageItemTemplate ();
				} else {
					widget = new ChatLeftMessageItemTemplate ();
				}
				widget.BindingContext = chatMessagesList[index];
				ChatMessagesListView.Children.Add( widget);
			}
		}

	    async void ConnectToServerAsync()
	    {
#if __IOS__
            await client.ConnectAsync(new Uri("ws://localhost:6333/Chat"), cts.Token);
#else
            await client.ConnectAsync(new Uri("ws://localhost:6333/Chat"), cts.Token);
#endif
            //UpdateClientState();

            await Task.Factory.StartNew(async () =>
	        {
	            while (true)
	            {
	                WebSocketReceiveResult result;
	                var message = new ArraySegment<byte>(new byte[4096]);
	                do
	                {
	                    result = await client.ReceiveAsync(message, cts.Token);
	                    var messageBytes = message.Skip(message.Offset).Take(result.Count).ToArray();
	                    string serialisedMessae = Encoding.UTF8.GetString(messageBytes);

	                    try
	                    {
	                        //var msg = JsonConvert.DeserializeObject<PureWebSockets.Message>(serialisedMessae);
	                        //Messages.Add(msg);
	                    }
	                    catch (Exception ex)
	                    {
	                        Console.WriteLine($"Invalide message format. {ex.Message}");
	                    }

	                } while (!result.EndOfMessage);
	            }
	        }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

	        //void UpdateClientState()
	        //{
	        //    OnPropertyChanged(nameof(IsConnected));
	        //    sendMessageCommand.ChangeCanExecute();
	        //    Console.WriteLine($"Websocket state {client.State}");
	        //}
	    }

        async void SendMessageAsync(string message)
	    {
	        //if (!CanSendMessage(message))
	        //    return;

	        var byteMessage = Encoding.UTF8.GetBytes(message);
	        var segmnet = new ArraySegment<byte>(byteMessage);

            await client.SendAsync(segmnet, WebSocketMessageType.Text, true, cts.Token);
	    }

        //public void PureWebSocket()
        //{
        //    var link = "ws://localhost:6333/Chat";

        //       _ws = new PureWebSocket(link, new ReconnectStrategy(10000, 60000));
        //    //_ws.OnStateChanged += Ws_OnStateChanged;
        //    //_ws.OnMessage += Ws_OnMessage;
        //    //_ws.OnClosed += Ws_OnClosed;
        //    //_ws.OnSendFailed += _ws_OnSendFailed;
        //    _ws.Connect();

        //    var timer = new Timer(OnTick, null, 1000, 500);
        //   }

        //private static void OnTick(object state)
        //{
        //    _ws.Send(DateTime.Now.Ticks.ToString());
        //}

    }
}

