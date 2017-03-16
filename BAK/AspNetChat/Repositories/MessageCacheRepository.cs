using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            var con = ConfigurationManager.ConnectionStrings["MessengerSTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {

              SqlCommand command = new SqlCommand();
              command.Connection = myConnection;
              command.CommandText = "SELECT * FROM message order by timestamp";
              myConnection.Open();
              using (SqlDataReader reader = command.ExecuteReader())
              {
                while (reader.Read())
                {
                  _storeMessages.Add(new Message()
                  {
                    ID = reader.GetGuid(0),
                    Name = reader["Name"].ToString(),
                    Text = reader["Text"].ToString()
                  });
                }
              }
            }
            _cache.Add(_keyCache, _storeMessages);
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