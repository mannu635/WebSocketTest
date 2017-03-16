using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web;

namespace AspNetChat
{
  public class MessageService : IBaseRepository<Message>
  {
    private readonly IBaseRepository<Message> _repository;
        Index obj = new Index();
    public MessageService()
    {
           if(obj.provid == "sql")
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