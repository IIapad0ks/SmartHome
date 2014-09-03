using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class TriggerRepository : DBModelNameRepository<Models.Trigger, Entities.Trigger>, ITriggerRepository
    {
        public TriggerRepository(ISHRepository<Entities.Trigger> repository)
        {
            this.repository = repository;
        }

        public override Models.Trigger DBItemToItem(Entities.Trigger dbItem)
        {
            if (dbItem == null)
            {
                return new Models.Trigger();
            }

            return new Models.Trigger
            {
                ID = dbItem.ID,
                Name = dbItem.Name,
                Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbItem.DeviceTypeID),
                Device = SIManager.Container.GetInstance<IDeviceRepository>().Get(dbItem.DeviceID),
                Sensor = SIManager.Container.GetInstance<ISensorRepository>().Get(dbItem.SensorID),
                Condition = dbItem.Condition
            };
        }

        public override Entities.Trigger ItemToDBItem(Models.Trigger item)
        {
            if (item == null)
            {
                return new Entities.Trigger();
            }

            Entities.Trigger dbItem = new Entities.Trigger
            {
                Name = item.Name,
                DeviceTypeID = item.Type.ID,
                DeviceID = item.Device.ID,
                SensorID = item.Sensor.ID,
                Condition = item.Condition
            };

            if (item.ID != 0)
            {
                dbItem.ID = item.ID;
            }

            return dbItem;
        }
    }
}
