using System.Collections.Generic;
using System.Threading.Tasks;


namespace AspNetChat
{
  public class MessageService : IBaseRepository<Message>
  {
    private readonly IBaseRepository<Message> _repository;
    public MessageService()
    {
      //this._repository = new MessageSQLRepository();
      this._repository = new MessageCacheRepository();
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