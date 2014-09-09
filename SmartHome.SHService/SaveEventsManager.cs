using System;
using System.Linq;
using SmartHome.Core.SmartHome;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

namespace SmartHome.Service
{
    public class SaveEventsManager : ISaveEventsManager
    {
        private IEventLogRepository repository;
        private object lockEvents = new object();

        public List<EventLogModel> Events { get; private set; }

        public SaveEventsManager(IEventLogRepository repository)
        {
            this.repository = repository;
            this.Events = new List<EventLogModel>();
        }

        public void AddEvent(IConfig config, string actionName)
        {
            StringBuilder configState = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(configState);
            config.WriteXml(writer);

            lock (this.lockEvents)
            {
                this.Events.Add(new EventLogModel
                {
                    Type = new DeviceTypeModel { ID = config.TypeID },
                    Device = new DeviceModel { ID = config.ID },
                    DeviceState = configState.ToString(),
                    Action = new SmartHome.Core.Models.ActionModel { Name = actionName },
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