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
    public class DeviceRepository : DBModelDeviceRepository<Models.DeviceModel, Entities.Device>, IDeviceRepository
    {
        public DeviceRepository(ISHRepository<Entities.Device> repository)
        {
            this.repository = repository;
        }

        public override bool Remove(int id)
        {
            IEventLogRepository eventsLogRepository = SIManager.Container.GetInstance<IEventLogRepository>();
            ITriggerRepository triggerRepository = SIManager.Container.GetInstance<ITriggerRepository>();

            IQueryable<Models.TriggerModel> triggers = triggerRepository.GetAll().Where(t => t.Device.ID == id);
            foreach (var trigger in triggers)
            {
                IQueryable<Models.EventLogModel> events = eventsLogRepository.GetAll().Where(e => e.DeviceID == trigger.ID && e.Type.ID == trigger.Type.ID);
                foreach (var eventLog in events)
                {
                    eventsLogRepository.Remove(eventLog.ID);
                }

                triggerRepository.Remove(trigger.ID);
            }

            return base.Remove(id);
        }

        public override Models.DeviceModel DBItemToItem(Entities.Device dbDevice)
        {
            if (dbDevice == null)
            {
                return null;
            }

            return new Models.DeviceModel { ID = dbDevice.ID, Name = dbDevice.Name, Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbDevice.DeviceTypeID) };
        }

        public override Entities.Device ItemToDBItem(Models.DeviceModel device)
        {
            if (device == null)
            {
                return null;
            }

            return new Entities.Device { ID = device.ID, Name = device.Name, DeviceTypeID = device.Type.ID };
        }
    }
}