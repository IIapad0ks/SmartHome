using SmartHome.Core.Models;
using System;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IDBModelRepository<T> : IDisposable where T : class, IModel
    {
        IQueryable<T> GetAll();
        T Get(int id);
        T Add(T item);
        bool Remove(int id);
        bool Update(T item);
    }
}
