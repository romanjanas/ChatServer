using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Web;
using Owin.WebSocket;
using System.Text;
using System.Collections.Concurrent;

namespace ChatServer
{
    public class ChatWebSocket : WebSocketConnection
    {
        static ConcurrentBag<object> clients = new ConcurrentBag<object>();

        public override void OnOpen()
        {
            clients.Add(new Client(this));
        }

        public override Task OnMessageReceived(ArraySegment<byte> message, WebSocketMessageType type)
        {
            var text = Encoding.UTF8.GetString(message.Array, message.Offset, message.Count);

            ProcessMessageFromClient(text);
            
            return Task.FromResult(0);
        }

        public override void OnClose(WebSocketCloseStatus? closeStatus, string closeStatusDescription)
        {
            base.OnClose(closeStatus, closeStatusDescription);
            SendToAllClients("Client disconnected");
        }

        private void ProcessMessageFromClient(String message)
        {
            SendToAllClients(message);
        }

        private void SendToAllClients(String toSend)
        {
            var message = Encoding.UTF8.GetBytes(toSend);
            foreach (object client in clients)
            {
                if (client is Client)
                {
                    ((Client)client).connection.SendText(message, true);
                }
            }
        }

        private class Client
        {
            public ChatWebSocket connection { get; set; }
            public String nickname { get; set; }
            public String color { get; set; }

            public Client(ChatWebSocket connection)
            {
                this.connection = connection;
            }
        }
    }
}