﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceRepository : DBModelDeviceRepository<Models.Device, Entities.Device>, IDeviceRepository
    {
        public DeviceRepository(ISHRepository<Entities.Device> repository)
        {
            this.repository = repository;
        }

        public override bool Remove(int id)
        {
            IEventLogRepository eventsLogRepository = SIManager.Container.GetInstance<IEventLogRepository>();
            ITriggerRepository triggerRepository = SIManager.Container.GetInstance<ITriggerRepository>();

            IQueryable<Models.Trigger> triggers = SIManager.Container.GetInstance<ITriggerRepository>().GetAll().Where(t => t.Device.ID == id);
            foreach (var trigger in triggers)
            {
                IQueryable<Models.EventLog> events = eventsLogRepository.GetAll().Where(e => e.Device.ID == trigger.ID && e.Type.ID == trigger.Type.ID);
                foreach (var eventLog in events)
                {
                    eventsLogRepository.Remove(eventLog.ID);
                }

                triggerRepository.Remove(trigger.ID);
            }

            return base.Remove(id);
        }

        public override Models.Device DBItemToItem(Entities.Device dbDevice)
        {
            if (dbDevice == null)
            {
                return null;
            }

            return new Models.Device { ID = dbDevice.ID, Name = dbDevice.Name, Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbDevice.DeviceTypeID) };
        }

        public override Entities.Device ItemToDBItem(Models.Device device)
        {
            if (device == null)
            {
                return null;
            }

            return new Entities.Device { ID = device.ID, Name = device.Name, DeviceTypeID = device.Type.ID };
        }
    }
}