using SmartHome.Core.Models;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IDBModelRepository<T> where T : class, IModel
    {
        IQueryable<T> GetAll();
        T Get(int id);
        T Add(T item);
        bool Remove(int id);
        bool Update(T item);
    }
}
