﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AspNetChat
{
  public class MessageSQLRepository : IBaseRepository<Message>
  {
    private static string _lngList = ConfigurationManager.AppSettings["langList"];
    public async Task<IList<Message>> GetAll()
    {
      return await GetAllMessageAsync();
    }

    private async Task<IList<Message>> GetAllMessageAsync()
    {
      IList<Message> msgList = new List<Message>();
      int index = 1;
      try
      {
        await Task.Run(() =>
        {
          Array.ForEach(_lngList.Split(','), x =>
                  {
              msgList.Add(new Message
              {
                ID = index++,
                Name = x,
                Text = x.Substring(0, 2) == "Sp" ? "es" : x.Substring(0, 2) == "Ge" ? "de" : x.Substring(0, 2).ToLower()
              });
            });
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