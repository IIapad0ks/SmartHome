using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceConverter : DBModelNameConverter<DeviceModel, Device>, IDeviceConverter
    {
        public DeviceConverter(ISHRepository repository) : base(repository) { }

        public override DeviceModel DBItemToItem(Device dbItem)
        {
            DeviceModel item = base.DBItemToItem(dbItem);

            item.FastAccess = dbItem.FastAccess;
            item.IsOn = dbItem.IsOn;
            item.Room = SIManager.Container.GetInstance<IRoomConverter>().DBItemToItem(dbItem.Room);
            item.Type = SIManager.Container.GetInstance<IDeviceTypeConverter>().DBItemToItem(dbItem.DeviceType);
            item.Value = dbItem.Value;
            item.WorkingTime = dbItem.WorkingTime;

            return item;
        }

        public override Device ItemToDBItem(DeviceModel item)
        {
            Device dbItem = base.ItemToDBItem(item);

            dbItem.FastAccess = item.FastAccess;
            dbItem.IsOn = item.IsOn;
            dbItem.RoomId = item.Room.Id;
            dbItem.DeviceTypeId = item.Type.Id;
            dbItem.Value = item.Value;
            dbItem.WorkingTime = item.WorkingTime;

            return dbItem;
        }
    }
}