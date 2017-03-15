using System.Collections.Generic;
using Microsoft.Web.WebSockets;
using System.Threading.Tasks;

namespace AspNetChat
{
  public class WebSocketChatHandler : WebSocketHandler
  {
    private static WebSocketCollection clients = new WebSocketCollection();
    private readonly MessageService _service = new MessageService();
    private static IList<Message> _storeMessages;
    private string name;

    public override void OnOpen()
    {
      this.name = this.WebSocketContext.QueryString["username"];
      if (name == "") { name = "NoName"; }
      clients.Add(this);

      _storeMessages = Task.Run(() => _service.GetAll()).Result;
      foreach (var message in _storeMessages)
      {
        this.Send(string.Format("<span style='color:grey;font-size: 9px;'>{0} : {1}</span>", message.Name, message.Text));
      }
      this.Send("<hr/>");
      clients.Broadcast(string.Format("<strong><small> {0} joined. </small></strong>", name));
    }

    public override void OnMessage(string message)
    {
      clients.Broadcast(string.Format("{0} : {1}", name, message));
      _service.Add(new Message(name, message));
    }

    public override void OnClose()
    {
      clients.Remove(this);
      clients.Broadcast(string.Format("<small style='color:red;font-size: 9px;'>{0} left.</small>", name));
    }
  }
}