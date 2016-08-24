using Owin.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    /// <summary>
    /// Middleware for consuming WebSocket requests.
    /// Resending requests to all connected clients.
    /// </summary>
    public class ChatWebSocket : WebSocketConnection
    {
        /// <summary>
        /// Thread-safe list of acive connections to clients.
        /// </summary>
        static ConcurrentBag<object> connections = new ConcurrentBag<object>();

        /// <summary>
        /// On WebSocket open add the connection to list.
        /// </summary>
        public override void OnOpen()
        {
            connections.Add(this);
        }

        /// <summary>
        /// Handle received messages. Check message type and broadcast.
        /// </summary>
        /// <param name="message">Message received from client</param>
        /// <param name="type">Message type - expecting Text (opcode 0x1)</param>
        /// <returns></returns>
        public override Task OnMessageReceived(ArraySegment<byte> message, WebSocketMessageType type)
        {
            if (type == WebSocketMessageType.Text)
            {
                var text = Encoding.UTF8.GetString(message.Array, message.Offset, message.Count);

                SendToAllClients(text);
            }
            return Task.FromResult(0);
        }

        /// <summary>
        /// Broadcast message to all clients.
        /// </summary>
        /// <param name="toSend">Message to send to all clients</param>
        private void SendToAllClients(String toSend)
        {
            var message = Encoding.UTF8.GetBytes(toSend);
            foreach (object connection in connections)
            {
                if (connection is WebSocketConnection)
                {
                    ((WebSocketConnection)connection).SendText(message, true);
                }
            }
        }
    }
}