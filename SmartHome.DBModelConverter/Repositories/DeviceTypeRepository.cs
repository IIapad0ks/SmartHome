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
    public class DeviceTypeRepository : DBModelNameRepository<Models.DeviceType, Entities.DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ISHRepository<Entities.DeviceType> repository)
        {
            this.repository = repository;
        }

        public override bool Remove(int id)
        {
            IDeviceRepository deviceRepository = SIManager.Container.GetInstance<IDeviceRepository>();
            ISensorRepository sensorRepository = SIManager.Container.GetInstance<ISensorRepository>();
            ITriggerRepository triggerRepository = SIManager.Container.GetInstance<ITriggerRepository>();

            foreach (var device in deviceRepository.GetAll().Where(d => d.Type.ID == id))
            {
                deviceRepository.Remove(device.ID);
            }

            foreach (var sensor in sensorRepository.GetAll().Where(s => s.Type.ID == id))
            {
                sensorRepository.Remove(sensor.ID);
            }

            foreach (var trigger in triggerRepository.GetAll().Where(t => t.Type.ID == id))
            {
                triggerRepository.Remove(trigger.ID);
            }

            return base.Remove(id);
        }

        public override Models.DeviceType DBItemToItem(Entities.DeviceType dbDeviceType)
        {
            if (dbDeviceType == null)
            {
                return null;
            }

            Models.DeviceType parent = dbDeviceType.ParentID != null ? this.Get((int)dbDeviceType.ParentID) : null;
            return new Models.DeviceType { ID = dbDeviceType.ID, Name = dbDeviceType.Name, Parent = parent };
        }

        public override Entities.DeviceType ItemToDBItem(Models.DeviceType deviceType)
        {
            if (deviceType == null)
            {
                return null;
            }

            int? parentID = deviceType.Parent != null ? (int?)deviceType.Parent.ID : null;
            return new Entities.DeviceType { ID = deviceType.ID, Name = deviceType.Name, ParentID = parentID };
        }
    }
}