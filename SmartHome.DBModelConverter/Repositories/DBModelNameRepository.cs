using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelNameRepository<T, TEntity> : DBModelRepository<T, TEntity>, IDBModelNameRepository<T> where T : class, INameModel where TEntity : class, INameEntity
    {
        public virtual T Get(string name)
        {
            TEntity dbItem = repository.GetAll().FirstOrDefault(d => d.Name == name);
            return this.DBItemToItem(dbItem);
        }
    }
}
