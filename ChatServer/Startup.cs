using Microsoft.Owin;
using Owin;
using Owin.WebSocket.Extensions;

[assembly: OwinStartup(typeof(ChatServer.Startup))]

namespace ChatServer
{
    /// <summary>
    /// Server for chat web application using WebSockets.
    /// </summary>
    public class Startup
    {
        //string chatAddress = "http://localhost:57575/index.html";
        string chatAddress = "http://chatserver6908.azurewebsites.net/index.html";

        public void Configuration(IAppBuilder app)
        {
            app.MapWebSocketRoute<ChatWebSocket>("/chatws");
            app.Run(context =>
            {
                context.Response.ContentType = "text/html";
                return context.Response.WriteAsync("<!DOCTYPE html><html lang=\"en\"><head></head><body>Continue to <a href=\""
                    + chatAddress
                    + "\">chat room</a>.</body></html>");
            });
        }
    }
}
