using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelNameConverter<T, TEntity> : DBModelConverter<T, TEntity>, IDBModelNameConverter<T, TEntity> where T : class, INameModel where TEntity : class, INameEntity
    {
        public DBModelNameConverter(ISHRepository repository) : base(repository) { }

        public override T Add(T item)
        {
            T existsItem = this.Get(item.Name);
            if (existsItem != null)
            {
                return existsItem;
            }

            return base.Add(item);
        }

        public virtual T Get(string name)
        {
            TEntity dbItem = repository.GetAll<TEntity>().FirstOrDefault(d => d.Name == name);
            return this.DBItemToItem(dbItem);
        }

        public override T DBItemToItem(TEntity dbItem)
        {
            T item = base.DBItemToItem(dbItem);
            item.Name = dbItem.Name;
            return item;
        }

        public override TEntity ItemToDBItem(T item)
        {
            TEntity dbItem = base.ItemToDBItem(item);
            dbItem.Name = item.Name;
            return dbItem;
        }
    }
}
