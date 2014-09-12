using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class TriggerRepository : DBModelDeviceRepository<TriggerModel, Trigger>, ITriggerRepository
    {
        public TriggerRepository(ISHRepository<Trigger> repository) : base(repository) { }

        public override TriggerModel Add(TriggerModel item)
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

        public override bool Update(TriggerModel item)
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

        public override TriggerModel DBItemToItem(Trigger dbItem)
        {
            TriggerModel item = base.DBItemToItem(dbItem);
            item.Device = SIManager.Container.GetInstance<IDeviceRepository>().Get(dbItem.DeviceID);
            item.Sensor = SIManager.Container.GetInstance<ISensorRepository>().Get(dbItem.SensorID);
            item.Condition = dbItem.Condition;
            return item;
        }

        public override Trigger ItemToDBItem(TriggerModel item)
        {
            Trigger dbItem = base.ItemToDBItem(item);
            dbItem.DeviceID = item.Device.ID;
            dbItem.SensorID = item.Sensor.ID;
            dbItem.Condition = item.Condition;
            return dbItem;
        }
    }
}
