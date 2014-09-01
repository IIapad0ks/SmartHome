using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int? id);
        T Add(T item);
        void Remove(int id);
        bool Update(T item);
    }

    public abstract class SmartHomeRepository<T, TEntity> : IRepository<T> where TEntity : class
    {
        protected SmartHomeDBEntities db = new SmartHomeDBEntities();

        public List<T> GetAll()
        {
            List<T> items = new List<T>();
            List<TEntity> dbItems = db.Set<TEntity>().ToList();
            foreach (var dbItem in dbItems)
            {
                items.Add(this.DBItemToItem(dbItem));
            }

            return items;
        }

        public T Get(int? id)
        {
            TEntity dbItem = db.Set<TEntity>().Find(id);

            if (dbItem == null)
            {
                return default(T);
            }

            return this.DBItemToItem(dbItem);
        }

        public T Add(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            db.Set<TEntity>().Add(dbItem);
            db.SaveChanges();
            return this.DBItemToItem(dbItem);
        }

        public void Remove(int id)
        {
            TEntity dbItem = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(dbItem);
            db.SaveChanges();
        }

        public bool Update(T item)
        {
            TEntity dbItem = this.ItemToDBItem(item);
            db.Entry(dbItem).State = System.Data.EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        public abstract T DBItemToItem(TEntity dbItem);
        public abstract TEntity ItemToDBItem(T action);
    }
}