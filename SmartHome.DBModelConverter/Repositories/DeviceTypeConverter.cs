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
    public class DeviceTypeConverter : DBModelNameConverter<DeviceTypeModel, DeviceType>, IDeviceTypeConverter
    {
        public DeviceTypeConverter(ISHRepository repository) : base(repository) { }

        public override DeviceTypeModel DBItemToItem(DeviceType dbItem)
        {
            DeviceTypeModel item = base.DBItemToItem(dbItem);

            item.NeedTimeControl = dbItem.NeedTimeControl;
            item.HasValue = dbItem.HasValue;
            item.Class = SIManager.Container.GetInstance<IDeviceClassConverter>().DBItemToItem(dbItem.DeviceClass);

            return item; 
        }

        public override DeviceType ItemToDBItem(DeviceTypeModel item)
        {
            DeviceType dbItem = base.ItemToDBItem(item);

            dbItem.DeviceClassId = item.Class.Id;
            dbItem.HasValue = item.HasValue;
            dbItem.NeedTimeControl = item.NeedTimeControl;

            return dbItem;
        }
    }
}