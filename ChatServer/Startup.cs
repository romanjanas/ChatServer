using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Owin.WebSocket.Extensions;

[assembly: OwinStartup(typeof(ChatServer.Startup))]

namespace ChatServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapWebSocketRoute<ChatWebSocket>("/chatws");
            app.Run(context =>
            {
                context.Response.ContentType = "text/html";
                return context.Response.WriteAsync("Continue to <a href='http://chatserver6908.azurewebsites.net/chatRoom.html'>chat room</a>.");
            });
        }
    }
}
