using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceTypeRepository : DBModelNameRepository<Models.DeviceType, Entities.DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ISHRepository<Entities.DeviceType> repository)
        {
            this.repository = repository;
        }

        public override Models.DeviceType DBItemToItem(Entities.DeviceType dbDeviceType)
        {
            if (dbDeviceType == null)
            {
                return new Models.DeviceType();
            }

            Models.DeviceType parent = dbDeviceType.ParentID != null ? this.Get((int)dbDeviceType.ParentID) : null;
            return new Models.DeviceType { ID = dbDeviceType.ID, Name = dbDeviceType.Name, Parent = parent };
        }

        public override Entities.DeviceType ItemToDBItem(Models.DeviceType deviceType)
        {
            if (deviceType == null)
            {
                return new Entities.DeviceType();
            }

            int? parentID = deviceType.Parent != null ? (int?)deviceType.Parent.ID : null;
            Entities.DeviceType dbDeviceType =  new Entities.DeviceType { Name = deviceType.Name, ParentID = parentID };

            if (deviceType.ID != 0)
            {
                dbDeviceType.ID = deviceType.ID;
            }

            return dbDeviceType;
        }
    }
}