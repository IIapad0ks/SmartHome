using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using System.Data.Entity;

namespace SmartHome.Data.Repositories
{
    public class SHRepository : ISHRepository
    {
        protected DbContext db;

        public SHRepository(DbContext db) {
            this.db = db;
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return db.Set<T>().AsQueryable();
        }

        public IQueryable<T> Get<T>(Func<T, bool> expression) where T : class, IEntity
        {
            return db.Set<T>().Where(expression).AsQueryable();
        }

        public T Get<T>(int id) where T : class, IEntity
        {
            return db.Set<T>().Find(id);
        }

        public T Add<T>(T item) where T : class, IEntity
        {
            db.Set<T>().Add(item);
            db.SaveChanges();
            return item;
        }

        public bool Remove<T>(int id) where T : class, IEntity
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

        public bool Update<T>(T item) where T : class, IEntity
        {
            try
            {
                db.Entry<T>(item).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {

            }

            return false;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool isDispose)
        {
            this.db.Dispose();
        }
    }
}