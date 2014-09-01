using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels = SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IDeviceRepository
    {
        Device Get(string name);
    }

    public class DeviceRepository : SmartHomeRepository<Device, DBModels.Device>, IDeviceRepository
    {
        public Device Get(string name)
        {
            DBModels.Device dbDevice = db.Devices.FirstOrDefault(d => d.Name == name);

            if (dbDevice == null)
            {
                return null;
            }

            return this.DBItemToItem(dbDevice);
        }

        public override Device DBItemToItem(DBModels.Device dbDevice)
        {
            return new Device { ID = dbDevice.ID, Name = dbDevice.Name, Type = new DeviceTypeRepository().Get(dbDevice.DeviceTypeID), Config = new ConfigTypeRepository().Get(dbDevice.ConfigTypeID) };
        }

        public override DBModels.Device ItemToDBItem(Device device)
        {
            DBModels.Device dbDevice = new DBModels.Device { Name = device.Name, DeviceTypeID = device.Type.ID, ConfigTypeID = device.Config.ID };

            if (device.ID != null)
            {
                dbDevice.ID = device.ID;
            }

            return dbDevice;
        }
    }
}