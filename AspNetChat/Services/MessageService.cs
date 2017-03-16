using AspNetChat;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AspNetChat
{
  public class MessageService : IBaseRepository<Message>
  {
    private readonly IBaseRepository<Message> _repository;
    public MessageService()
    {
            if (ConfigurationManager.AppSettings["provider"] == "sql")
            {
                this._repository = new MessageSQLRepository();
            }

            else
            {
                this._repository = new MessageCacheRepository();
            }
    }
    public Task<IList<Message>> GetAll()
    {
      return _repository.GetAll();
    }

    public bool Add(Message msg)
    {
      return _repository.Add(msg);
    }

    public void Dispose()
    {
    }
  }
}