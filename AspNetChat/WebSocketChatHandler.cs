using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Web.WebSockets;

namespace AspNetChat
{
    public class WebSocketChatHandler : WebSocketHandler
    {
        private static WebSocketCollection clients = new WebSocketCollection();
        private string name;
        
        public override void OnOpen()
        {
            this.name = this.WebSocketContext.QueryString["username"];
            if (name =="") { name = "NoName"; }
            clients.Add(this);            
            clients.Broadcast(string.Format("<strong><small> {0} joined. </small></strong>", name));
        }
        
        public override void OnMessage(string message)
        {
            clients.Broadcast(string.Format("{0} : {1}", name, message));
        } 

        public override void OnClose()
        {
            clients.Remove(this);
            clients.Broadcast(string.Format("<small>{0} left.</small>", name));
        }
    }
}