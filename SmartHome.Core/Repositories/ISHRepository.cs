using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.Core.Repositories
{
    public interface ISHRepository<T> : IDisposable where T : class, IEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Func<T, bool> expression);
        T Get(int id);
        T Add(T item);
        bool Remove(int id);
        bool Update(T item);
    }
}