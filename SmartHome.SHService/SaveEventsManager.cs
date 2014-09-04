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
        IEventLogRepository repository;
        public List<EventLog> Events { get; private set; }

        public SaveEventsManager(IEventLogRepository repository)
        {
            this.repository = repository;
            this.Events = new List<EventLog>();
        }

        public void AddEvent(IConfig config, string actionName)
        {
            this.Events.Add(new EventLog
            {
                Type = new DeviceType { ID = config.TypeID },
                Device = new Device { ID = config.ID },
                DeviceState = config.WriteXml(),
                Action = new SmartHome.Core.Models.Action { Name = actionName }
            });
        }

        public void SaveEvents()
        {
            repository.Add(this.Events.AsQueryable());
        }
    }
}