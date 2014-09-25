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
    public class TriggerConverter : DBModelNameConverter<TriggerModel, Trigger>, ITriggerConverter
    {
        public TriggerConverter(ISHRepository repository) : base(repository) { }

        public override TriggerModel DBItemToItem(Trigger dbItem)
        {
            TriggerModel item = base.DBItemToItem(dbItem);

            item.Action = SIManager.Container.GetInstance<IActionConverter>().Get(dbItem.EventActionId);
            item.Condition = dbItem.Condition;
            item.Device = SIManager.Container.GetInstance<IDeviceConverter>().Get(dbItem.DeviceId);
            item.Sensor = SIManager.Container.GetInstance<IDeviceConverter>().Get(dbItem.SensorId);
            item.SetValue = dbItem.SetValue;

            return item;
        }

        public override Trigger ItemToDBItem(TriggerModel item)
        {
            Trigger dbItem = base.ItemToDBItem(item);

            dbItem.EventActionId = item.Action.Id;
            dbItem.Condition = item.Condition;
            dbItem.DeviceId = item.Device.Id;
            dbItem.SensorId = item.Sensor.Id;
            dbItem.SetValue = item.SetValue;

            return dbItem;
        }
    }
}
