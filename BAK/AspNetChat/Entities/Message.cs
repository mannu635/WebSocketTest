using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetChat
{
  public class Message
  {
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public Message()
        {
            ID = this.ID;
            Name = this.Name;
            Text = this.Text;
        }
        public Message(string name, string text)
        {
        ID = Guid.NewGuid();
        Name = name;
        Text = text;
        }
  }
}
