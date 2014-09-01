using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels = SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IDeviceTypeRepository
    {
        DeviceType Get(string name);
    }

    public class DeviceTypeRepository : SmartHomeRepository<DeviceType, DBModels.DeviceType>, IDeviceTypeRepository
    {
        public DeviceType Get(string name)
        {
            DBModels.DeviceType dbDeviceType = db.DeviceTypes.FirstOrDefault(dt => dt.Name == name);

            if (dbDeviceType == null)
            {
                return null;
            }

            return this.DBItemToItem(dbDeviceType);
        }

        public override DeviceType DBItemToItem(DBModels.DeviceType dbDeviceType)
        {
            return new DeviceType { ID = dbDeviceType.ID, Name = dbDeviceType.Name, Parent = this.Get(dbDeviceType.ParentID) };
        }

        public override DBModels.DeviceType ItemToDBItem(DeviceType deviceType)
        {
            int? parentID = deviceType.Parent != null ? (int?)deviceType.Parent.ID : null;
            DBModels.DeviceType dbDeviceType =  new DBModels.DeviceType { Name = deviceType.Name, ParentID = parentID };

            if (deviceType.ID != null)
            {
                dbDeviceType.ID = deviceType.ID;
            }

            return dbDeviceType;
        }
    }
}