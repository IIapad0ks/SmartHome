using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelRepository<T, TEntity> : IDBModelRepository<T, TEntity> where T : class, IModel where TEntity : class, IEntity
    {
        protected ISHRepository<TEntity> repository;

        public DBModelRepository(ISHRepository<TEntity> repository)
        {
            this.repository = repository;
        }

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

        public virtual IQueryable<T> Get(Func<TEntity, bool> expression)
        {
            IQueryable<TEntity> dbItems = this.repository.Get(expression);
            return this.DBItemsToItems(dbItems);
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

        public virtual T DBItemToItem(TEntity dbItem)
        {
            T item = Activator.CreateInstance<T>();
            item.ID = dbItem.ID;
            return item;
        }

        public virtual TEntity ItemToDBItem(T item)
        {
            TEntity dbItem = Activator.CreateInstance<TEntity>();
            dbItem.ID = item.ID;
            return dbItem;
        }

        public virtual IQueryable<T> DBItemsToItems(IQueryable<TEntity> dbItems)
        {
            List<T> items = new List<T>();
            foreach (var dbItem in dbItems)
            {
                items.Add(this.DBItemToItem(dbItem));
            }

            return items.AsQueryable();
        }

        public virtual IQueryable<TEntity> ItemsToDBItems(IQueryable<T> items)
        {
            List<TEntity> dbItems = new List<TEntity>();
            foreach (var item in items)
            {
                dbItems.Add(this.ItemToDBItem(item));
            }

            return dbItems.AsQueryable();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool isDispose)
        {
            repository.Dispose();
        }
    }
}