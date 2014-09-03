using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceRepository : DBModelNameRepository<Models.Device, Entities.Device>, IDeviceRepository
    {
        public DeviceRepository(ISHRepository<Entities.Device> repository)
        {
            this.repository = repository;
        }

        public override Models.Device DBItemToItem(Entities.Device dbDevice)
        {
            if (dbDevice == null)
            {
                return new Models.Device();
            }

            return new Models.Device { ID = dbDevice.ID, Name = dbDevice.Name, Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbDevice.DeviceTypeID) };
        }

        public override Entities.Device ItemToDBItem(Models.Device device)
        {
            if (device == null)
            {
                return new Entities.Device();
            }

            Entities.Device dbDevice = new Entities.Device { Name = device.Name, DeviceTypeID = device.Type.ID };

            if (device.ID != 0)
            {
                dbDevice.ID = device.ID;
            }

            return dbDevice;
        }
    }
}