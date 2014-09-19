using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.DBModelConverters;
using SmartHome.Core.Service;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelConverter<T, TEntity> : IDBModelConverter<T, TEntity> where T : class, IModel where TEntity : class, IEntity
    {
        protected int perPage = 25;
        protected ISHRepository repository;

        public DBModelConverter(ISHRepository repository)
        {
            this.repository = repository;
        }

        public virtual List<T> GetAll()
        {
            List<T> items = new List<T>();
            List<TEntity> dbItems = repository.GetAll<TEntity>().ToList();
            foreach (var dbItem in dbItems)
            {
                items.Add(this.DBItemToItem(dbItem));
            }

            return items;
        }

        public virtual List<T> Get(Func<TEntity, bool> expression)
        {
            List<TEntity> dbItems = this.repository.Get<TEntity>(expression).ToList();
            return this.DBItemsToItems(dbItems);
        }

        public virtual T Get(int id)
        {
            TEntity dbItem = repository.Get<TEntity>(id);
            return this.DBItemToItem(dbItem);
        }

        public virtual T Add(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            dbItem = repository.Add<TEntity>(dbItem);
            return this.DBItemToItem(dbItem);
        }

        public virtual bool Remove(int id)
        {
            return repository.Remove<TEntity>(id);
        }

        public virtual bool Update(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            return repository.Update<TEntity>(dbItem);
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

        public virtual List<T> DBItemsToItems(List<TEntity> dbItems)
        {
            List<T> items = new List<T>();
            foreach (var dbItem in dbItems)
            {
                items.Add(this.DBItemToItem(dbItem));
            }

            return items;
        }

        public virtual List<TEntity> ItemsToDBItems(List<T> items)
        {
            List<TEntity> dbItems = new List<TEntity>();
            foreach (var item in items)
            {
                dbItems.Add(this.ItemToDBItem(item));
            }

            return dbItems;
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