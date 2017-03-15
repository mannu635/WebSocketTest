using AspNetChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Worklio.Services
{
    public class MessageService : IBaseRepository<Message>
    {
        private readonly IBaseRepository<Message> _repository;
        public MessageService()
        {
            this._repository = new MessageSQLRepository();
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