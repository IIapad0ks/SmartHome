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
    public class DeviceConverter<T> : DBModelDeviceConverter<T, Device>, IDeviceConverter<T> where T : DeviceModel
    {
        public DeviceConverter(ISHRepository repository) : base(repository) { }

        public override bool Remove(int id)
        {
            IEventLogConverter eventsLogRepository = SIManager.Container.GetInstance<IEventLogConverter>();
            ITriggerConverter<TriggerModel> triggerRepository = SIManager.Container.GetInstance<ITriggerConverter<TriggerModel>>();

            List<TriggerModel> triggers = triggerRepository.GetAll().Where(t => t.Device.ID == id).ToList();
            foreach (var trigger in triggers)
            {
                List<EventLogModel> events = eventsLogRepository.GetAll().Where(e => e.DeviceID == trigger.ID && e.Type.ID == trigger.Type.ID).ToList();
                foreach (var eventLog in events)
                {
                    eventsLogRepository.Remove(eventLog.ID);
                }

                triggerRepository.Remove(trigger.ID);
            }

            return base.Remove(id);
        }
    }
}