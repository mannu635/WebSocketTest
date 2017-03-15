using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetChat
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        bool Add(TEntity obj);
    }
}