using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelRepository<T, TEntity> : IDBModelRepository<T> where T : class, IModel where TEntity : class, IEntity
    {
        protected ISHRepository<TEntity> repository;

        public virtual IQueryable<T> GetAll()
        {
            List<T> items = new List<T>();
            IQueryable<TEntity> dbItems = repository.GetAll();
            foreach (var dbItem in dbItems)
            {
                items.Add(this.DBItemToItem(dbItem));
            }

            return items.AsQueryable();
        }

        public virtual T Get(int id)
        {
            TEntity dbItem = repository.Get(id);
            return this.DBItemToItem(dbItem);
        }

        public virtual T Add(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            dbItem = repository.Add(dbItem);
            return this.DBItemToItem(dbItem);
        }

        public virtual bool Remove(int id)
        {
            return repository.Remove(id);
        }

        public virtual bool Update(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            return repository.Update(dbItem);
        }

        public abstract T DBItemToItem(TEntity dbItem);
        public abstract TEntity ItemToDBItem(T item);
    }
}