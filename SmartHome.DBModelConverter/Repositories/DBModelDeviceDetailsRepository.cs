using SmartHome.Core;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DBModelDeviceDetailsRepository<T, TEntity> : DBModelDeviceRepository<T, TEntity>, IDBModelDeviceDetailsRepository<T, TEntity> where T : class, IDeviceDetailsModel where TEntity : class, IDeviceEntity
    {
        public DBModelDeviceDetailsRepository(ISHRepository<TEntity> repository) : base(repository) { }

        public override T DBItemToItem(TEntity dbItem)
        {
            T item = base.DBItemToItem(dbItem);
            item.EventList = SIManager.Container.GetInstance<IEventLogRepository>().Get(e => e.ConfigID == item.ID && e.DeviceTypeID == item.Type.ID).ToList();
            return item;
        }
    }
}
