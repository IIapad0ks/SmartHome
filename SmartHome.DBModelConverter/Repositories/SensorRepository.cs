using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorRepository : DBModelDeviceRepository<SensorModel, Sensor>, ISensorRepository
    {
        public SensorRepository(ISHRepository<Sensor> repository) : base(repository) { }

        public override bool Remove(int id)
        {
            IEventLogRepository eventsLogRepository = SIManager.Container.GetInstance<IEventLogRepository>();
            ITriggerRepository triggerRepository = SIManager.Container.GetInstance<ITriggerRepository>();

            IQueryable<TriggerModel> triggers = SIManager.Container.GetInstance<ITriggerRepository>().GetAll().Where(t => t.Device.ID == id);
            foreach (var trigger in triggers)
            {
                IQueryable<EventLogModel> events = eventsLogRepository.GetAll().Where(e => e.DeviceID == trigger.ID && e.Type.ID == trigger.Type.ID);
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
