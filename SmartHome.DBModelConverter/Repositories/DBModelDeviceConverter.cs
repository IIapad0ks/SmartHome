using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public abstract class DBModelDeviceConverter<T, TEntity> : DBModelNameConverter<T, TEntity> where T : class, IDeviceModel where TEntity : class, IDeviceEntity
    {
        public DBModelDeviceConverter(ISHRepository repository) : base(repository) { }

        public override T Add(T item)
        {
            if (item.Type.ID == default(int))
            {
                item.Type = SIManager.Container.GetInstance<IDeviceTypeConverter>().Add(item.Type);
            }

            return base.Add(item);
        }

        public override bool Update(T item)
        {
            if (item.Type.ID != default(int))
            {
                SIManager.Container.GetInstance<IDeviceTypeConverter>().Update(item.Type);
            }

            return base.Update(item);
        }

        public override bool Remove(int id)
        {
            IEventLogConverter eventsLogRepository = SIManager.Container.GetInstance<IEventLogConverter>();

            IDeviceModel device = this.Get(id);
            foreach (var eventLog in eventsLogRepository.GetAll().Where(e => e.DeviceID == device.ID && e.Type.ID == device.Type.ID))
            {
                eventsLogRepository.Remove(eventLog.ID);
            }

            return base.Remove(id);
        }

        public override T DBItemToItem(TEntity dbItem)
        {
            T item = base.DBItemToItem(dbItem);
            item.Type = SIManager.Container.GetInstance<IDeviceTypeConverter>().Get(dbItem.DeviceTypeID);
            return item;
        }

        public override TEntity ItemToDBItem(T item)
        {
            TEntity dbItem = base.ItemToDBItem(item);
            dbItem.DeviceTypeID = item.Type.ID;
            return dbItem;
        }
    }
}
