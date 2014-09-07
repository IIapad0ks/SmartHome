using System;
using System.Linq;
using SmartHome.Core.SmartHome;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System.Collections.Generic;

namespace SmartHome.Service
{
    public class SaveEventsManager : ISaveEventsManager
    {
        private IEventLogRepository repository;
        private object lockEvents = new object();

        public List<EventLog> Events { get; private set; }

        public SaveEventsManager(IEventLogRepository repository)
        {
            this.repository = repository;
            this.Events = new List<EventLog>();
        }

        public void AddEvent(IConfig config, string actionName)
        {
            lock (this.lockEvents)
            {
                this.Events.Add(new EventLog
                {
                    Type = new DeviceType { ID = config.TypeID },
                    Device = new Device { ID = config.ID },
                    DeviceState = config.WriteXml(),
                    Action = new SmartHome.Core.Models.Action { Name = actionName },
                    EventDatetime = DateTime.Now
                });
            }
        }

        public void SaveEvents()
        {
            lock (lockEvents)
            {
                repository.Add(this.Events.AsQueryable());
                this.Events.Clear();
            }
        }
    }
}