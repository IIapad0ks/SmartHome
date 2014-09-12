using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceTypeRepository : DBModelNameRepository<DeviceTypeModel, DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ISHRepository<DeviceType> repository) : base(repository) { }

        public override bool Remove(int id)
        {
            using (IDeviceRepository deviceRepository = SIManager.Container.GetInstance<IDeviceRepository>())
            {
                foreach (var device in deviceRepository.GetAll().Where(d => d.Type.ID == id))
                {
                    deviceRepository.Remove(device.ID);
                }
            }

            using (ISensorRepository sensorRepository = SIManager.Container.GetInstance<ISensorRepository>())
            {
                foreach (var sensor in sensorRepository.GetAll().Where(s => s.Type.ID == id))
                {
                    sensorRepository.Remove(sensor.ID);
                }
            }

            using (ITriggerRepository triggerRepository = SIManager.Container.GetInstance<ITriggerRepository>())
            {
                foreach (var trigger in triggerRepository.GetAll().Where(t => t.Type.ID == id))
                {
                    triggerRepository.Remove(trigger.ID);
                }
            }

            return base.Remove(id);
        }

        public override DeviceTypeModel DBItemToItem(DeviceType dbItem)
        {
            DeviceTypeModel item = base.DBItemToItem(dbItem);
            item.Parent = dbItem.ParentID != null ? this.Get((int)dbItem.ParentID) : null;
            return item; 
        }

        public override DeviceType ItemToDBItem(DeviceTypeModel item)
        {
            DeviceType dbItem = base.ItemToDBItem(item);
            dbItem.ParentID = item.Parent != null ? (int?)item.Parent.ID : null;
            return dbItem;
        }
    }
}