using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.Core.Repositories
{
    public interface ISHRepository : IDisposable 
    {
        IQueryable<T> GetAll<T>() where T : class, IEntity;
        IQueryable<T> Get<T>(Func<T, bool> expression) where T : class, IEntity;
        T Get<T>(int id) where T : class, IEntity;
        T Add<T>(T item) where T : class, IEntity;
        bool Remove<T>(int id) where T : class, IEntity;
        bool Update<T>(T item) where T : class, IEntity;
    }
}