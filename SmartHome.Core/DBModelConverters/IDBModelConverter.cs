using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome.Core.DBModelConverters
{
    public interface IDBModelConverter<T, TEntity> : IDisposable where T : class, IModel where TEntity : class, IEntity
    {
        List<T> GetAll();
        List<T> Get(Func<TEntity, bool> expression);
        T Get(int id);
        T Add(T item);
        bool Remove(int id);
        bool Update(T item);

        T DBItemToItem(TEntity dbItem);
        TEntity ItemToDBItem(T dbItem);
        List<T> DBItemsToItems(List<TEntity> dbItems);
        List<TEntity> ItemsToDBItems(List<T> dbItem);
    }
}
