using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Web.WebSockets;
using System.Configuration;
using System.Data.SqlClient;

namespace AspNetChat
{
  public class WebSocketChatHandler : WebSocketHandler
  {
    private static WebSocketCollection clients = new WebSocketCollection();
    private readonly MessageService _service = new MessageService();
        private static List<Message> _storeMessages = new List<Message>();
        private string name;

    public override void OnOpen()
    {
      this.name = this.WebSocketContext.QueryString["username"];
      if (name == "") { name = "NoName"; }
      clients.Add(this);
      clients.Broadcast(string.Format("<strong><small> {0} joined. </small></strong>", name));
     _storeMessages=_service.GetAll();
      //Mickey: I need to broadcast the previous messages here
       }

    public override void OnMessage(string message)
    {
      clients.Broadcast(string.Format("{0} : {1}", name, message));
      _service.Add(new Message(name, message));
    }

    public override void OnClose()
    {
      clients.Remove(this);
      clients.Broadcast(string.Format("<small>{0} left.</small>", name));
    }
  }
}