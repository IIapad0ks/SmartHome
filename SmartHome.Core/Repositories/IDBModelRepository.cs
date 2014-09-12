using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IDBModelRepository<T, TEntity> : IDisposable where T : class, IModel where TEntity : class, IEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Func<TEntity, bool> expression);
        T Get(int id);
        T Add(T item);
        bool Remove(int id);
        bool Update(T item);

        T DBItemToItem(TEntity dbItem);
        TEntity ItemToDBItem(T dbItem);
        IQueryable<T> DBItemsToItems(IQueryable<TEntity> dbItems);
        IQueryable<TEntity> ItemsToDBItems(IQueryable<T> dbItem);
    }
}
