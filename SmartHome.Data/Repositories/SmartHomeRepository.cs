using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using System.Data.Entity;

namespace SmartHome.Data.Repositories
{
    public class SmartHomeRepository<T> : ISHRepository<T> where T : class, IEntity
    {
        protected DbContext db;

        public SmartHomeRepository(DbContext db) {
            this.db = db;
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>().AsQueryable();
        }

        public T Get(int id)
        {
            return db.Set<T>().Find(id);
        }

        public T Add(T item)
        {
            db.Set<T>().Add(item);
            db.SaveChanges();
            return item;
        }

        public bool Remove(int id)
        {
            try
            {
                T item = db.Set<T>().Find(id);
                db.Set<T>().Remove(item);
                db.SaveChanges();

                return true;
            }
            catch
            {

            }

            return false;
        }

        public bool Update(T item)
        {
            try
            {
                db.Entry<T>(item).State = System.Data.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {

            }

            return false;
        }
    }
}