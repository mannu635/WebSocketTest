using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AspNetChat
{
  public class MessageSQLRepository : IBaseRepository<Message>
  {
    
    public async Task<IList<Message>> GetAll()
    {
      return await GetAllMessageAsync();
    }

    private async Task<IList<Message>> GetAllMessageAsync()
    {
      IList<Message> msgList = new List<Message>();
      try
      {
        await Task.Run(() =>
        {
            var con = ConfigurationManager.ConnectionStrings["MessengerSTR"].ToString();
            using (SqlConnection myConnection = new SqlConnection(con))
            {

                SqlCommand command = new SqlCommand();
                command.Connection = myConnection;
                command.CommandText = "Select * from Message";
                myConnection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        msgList.Add(new Message()
                        {
                            ID = reader.GetGuid(0),
                            Name = reader["Name"].ToString(),
                            Text = reader["Text"].ToString()
                        });
                    }
                }
            }
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return msgList;
    }

    public bool Add(Message obj)
    {
      throw new NotImplementedException();
    }


    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}