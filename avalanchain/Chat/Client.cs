using chat.messages;
using System;
using Proto.Remote;
using Proto;
using System.Threading.Tasks;

namespace Chat
{
    public class Client
    {
        public void StartClient()
        {
            Serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
            Remote.Start("127.0.0.1", 0);
            var server = new PID("127.0.0.1:8000", "chatserver");



            var props = Actor.FromFunc(ctx =>
            {
                switch (ctx.Message)
                {
                    case Connected connected:
                        Console.WriteLine(connected.Message);
                        break;
                    case SayResponse sayResponse:
                        Console.WriteLine($"{sayResponse.UserName} {sayResponse.Message}");
                        break;
                    case NickResponse nickResponse:
                        Console.WriteLine($"{nickResponse.OldUserName} is now {nickResponse.NewUserName}");
                        break;
                }
                return Actor.Done;
            });

            var client = Actor.Spawn(props);
            server.Tell(new Connect
            {
                Sender = client
            });
            var nick = "Alex";

            Task.Factory.StartNew(async () =>
            {
                //if (text.Equals("/exit"))
                //{
                //    return;
                //}
                server.Tell(new NickRequest
                {
                    OldUserName = nick,
                    NewUserName = "new user"
                });
                server.Tell(new SayRequest
                {
                    UserName = nick,
                    Message = "Baa baa"
                });
                server.Tell(new SayRequest
                {
                    UserName = nick,
                    Message = "Boo boo"
                });
            });
        }
    }
}
