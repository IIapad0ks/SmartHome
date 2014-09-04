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
    public class TriggerRepository : DBModelDeviceRepository<Models.Trigger, Entities.Trigger>, ITriggerRepository
    {
        public TriggerRepository(ISHRepository<Entities.Trigger> repository)
        {
            this.repository = repository;
        }

        public override Models.Trigger Add(Models.Trigger item)
        {
            IDeviceRepository deviceRepository = SIManager.Container.GetInstance<IDeviceRepository>();
            ISensorRepository sensorRepository = SIManager.Container.GetInstance<ISensorRepository>();

            if (item.Device.ID == default(int))
            {
                item.Device = deviceRepository.Add(item.Device);
            }

            if (item.Sensor.ID == default(int))
            {
                item.Sensor = sensorRepository.Add(item.Sensor);
            }

            return base.Add(item);
        }

        public override bool Update(Models.Trigger item)
        {
            IDeviceRepository deviceRepository = SIManager.Container.GetInstance<IDeviceRepository>();
            ISensorRepository sensorRepository = SIManager.Container.GetInstance<ISensorRepository>();

            if (item.Device.ID != default(int))
            {
                deviceRepository.Update(item.Device);
            }

            if (item.Sensor.ID != default(int))
            {
                sensorRepository.Update(item.Sensor);
            }

            return base.Update(item);
        }

        public override Models.Trigger DBItemToItem(Entities.Trigger dbItem)
        {
            if (dbItem == null)
            {
                return null;
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
                return null;
            }

            Entities.Trigger dbItem = new Entities.Trigger
            {
                ID = item.ID,
                Name = item.Name,
                DeviceTypeID = item.Type.ID,
                DeviceID = item.Device.ID,
                SensorID = item.Sensor.ID,
                Condition = item.Condition
            };

            return dbItem;
        }
    }
}
