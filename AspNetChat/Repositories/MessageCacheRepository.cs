using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AspNetChat
{
  public class MessageCacheRepository : IBaseRepository<Message>
  {
    private readonly Cacher _cache = new Cacher();
    private const string _keyCache = "messager_WO";
    private static List<Message> _storeMessages = new List<Message>() ;
    public async Task<IList<Message>> GetAll()
    {
      return await GetAllMessageAsync();
    }

    private Task<IList<Message>> GetAllMessageAsync()
    {
      throw new NotImplementedException();
    }

    public bool Add(Message msg)
    {
      _storeMessages.Add(msg);
      _storeMessages.Reverse();
      _cache.Add(_keyCache, _storeMessages);
      return true;
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}