﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorRepository : DBModelDeviceRepository<Models.Sensor, Entities.Sensor>, ISensorRepository
    {
        public SensorRepository(ISHRepository<Entities.Sensor> repository)
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

        public override Models.Sensor DBItemToItem(Entities.Sensor dbSensor)
        {
            if (dbSensor == null)
            {
                return null;
            }

            return new Models.Sensor { ID = dbSensor.ID, Name = dbSensor.Name, Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbSensor.DeviceTypeID) };
        }

        public override Entities.Sensor ItemToDBItem(Models.Sensor sensor)
        {
            if (sensor == null)
            {
                return null;
            }

            return new Entities.Sensor { ID = sensor.ID, Name = sensor.Name, DeviceTypeID = sensor.Type.ID };
        }
    }
}