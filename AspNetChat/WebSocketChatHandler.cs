using System.Collections.Generic;
using Microsoft.Web.WebSockets;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;
using System.Diagnostics;

namespace AspNetChat
{
  public class WebSocketChatHandler : WebSocketHandler
  {
    private static WebSocketCollection clients = new WebSocketCollection();
    private readonly MessageService _service = new MessageService();
    private static IList<Message> _storeMessages;
    private Stopwatch stopWatch = new Stopwatch();
    private string name;

    public override void OnOpen()
    {
      stopWatch.Start();
      this.name = this.WebSocketContext.QueryString["username"];
      if (name == "") { name = "NoName"; }
      clients.Add(this);
      
      _storeMessages = Task.Run(() => _service.GetAll()).Result;
            this.Send("<span style='color:grey;font-size: 15px;'>Welcome to CIMP Messenger</span>");
      foreach( var message in _storeMessages)
            {
                this.Send(string.Format("<span style='color:grey;font-size: 9px;'>{0} : {1}</span>", message.Name, message.Text));
            }
            this.Send("<hr/>");
            clients.Broadcast(string.Format("<strong><small> {0} joined. </small></strong>", name));
            stopWatch.Stop();
            sendtimestamp(string.Format("Open connection, Read & Send '{0}': <small>{1}ms</small>",this.name, stopWatch.ElapsedMilliseconds));
            stopWatch.Reset();
        }

        public override void OnMessage(string message)
        {
            stopWatch.Start();
            clients.Broadcast(string.Format("{0} : {1}", name, message));
            _service.Add(new Message(name, message));
            stopWatch.Stop();
            sendtimestamp(string.Format("Receive and Save Msg '{0}': <small>{1}ms</small>", this.name, stopWatch.ElapsedMilliseconds));
            stopWatch.Reset();
        }

    public override void OnClose()
    {
      clients.Remove(this);
      clients.Broadcast(string.Format("<small style='color:red;font-size: 9px;'>{0} left.</small>", name));
    }
        public void sendtimestamp(string T)
        {
            clients.Broadcast(string.Format("~"+ T));
        }
  }
}