using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class TriggerConverter<T> : DBModelDeviceConverter<T, Trigger>, ITriggerConverter<T> where T : TriggerModel
    {
        public TriggerConverter(ISHRepository repository) : base(repository) { }

        public override T Add(T item)
        {
            IDeviceConverter<DeviceModel> deviceRepository = SIManager.Container.GetInstance<IDeviceConverter<DeviceModel>>();
            ISensorConverter<SensorModel> sensorRepository = SIManager.Container.GetInstance<ISensorConverter<SensorModel>>();

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

        public override bool Update(T item)
        {
            IDeviceConverter<DeviceModel> deviceRepository = SIManager.Container.GetInstance<IDeviceConverter<DeviceModel>>();
            ISensorConverter<SensorModel> sensorRepository = SIManager.Container.GetInstance<ISensorConverter<SensorModel>>();

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

        public override T DBItemToItem(Trigger dbItem)
        {
            T item = base.DBItemToItem(dbItem);
            item.Device = SIManager.Container.GetInstance<IDeviceConverter<DeviceModel>>().Get(dbItem.DeviceID);
            item.Sensor = SIManager.Container.GetInstance<ISensorConverter<SensorModel>>().Get(dbItem.SensorID);
            item.Condition = dbItem.Condition;
            return item;
        }

        public override Trigger ItemToDBItem(T item)
        {
            Trigger dbItem = base.ItemToDBItem(item);
            dbItem.DeviceID = item.Device.ID;
            dbItem.SensorID = item.Sensor.ID;
            dbItem.Condition = item.Condition;
            return dbItem;
        }
    }
}
