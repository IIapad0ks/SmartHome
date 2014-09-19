using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorConverter<T> : DBModelDeviceConverter<T, Sensor>, ISensorConverter<T> where T : SensorModel
    {
        public SensorConverter(ISHRepository repository) : base(repository) { }

        public override bool Remove(int id)
        {
            IEventLogConverter eventsLogRepository = SIManager.Container.GetInstance<IEventLogConverter>();
            ITriggerConverter<TriggerModel> triggerRepository = SIManager.Container.GetInstance<ITriggerConverter<TriggerModel>>();

            List<TriggerModel> triggers = SIManager.Container.GetInstance<ITriggerConverter<TriggerModel>>().GetAll().Where(t => t.Device.ID == id).ToList();
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
