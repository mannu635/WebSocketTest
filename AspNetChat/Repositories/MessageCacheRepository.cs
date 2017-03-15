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
    private static IList<Message> _storeMessages = new List<Message>() ;
    public async Task<IList<Message>> GetAll()
    {
      return await GetAllMessageAsync();
    }

    private async Task<IList<Message>> GetAllMessageAsync()
    {
      try
      {
        await Task.Run(() =>
        {
          if(_cache.Contains(_keyCache))
          {
            return (IList <Message>)_cache.Get(_keyCache);
          }
          else
          {
            return _storeMessages;
          }
         
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return _storeMessages;
    }


    public bool Add(Message msg)
    {
      _storeMessages.Add(msg);
      _cache.Add(_keyCache, _storeMessages);
      return true;
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}