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
using SmartHome.Core.Service;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.Service
{
    public class SaveEventsManager : ISaveEventsManager
    {
        private IEventLogConverter repository;
        private IWebAPIManager webAPIManager;
        private object lockEvents = new object();

        public List<EventLogModel> Events { get; private set; }

        public SaveEventsManager(IEventLogConverter repository, IWebAPIManager webAPIManager)
        {
            this.repository = repository;
            this.webAPIManager = webAPIManager;
            this.Events = new List<EventLogModel>();
        }

        public void AddEvent(IConfig config, string actionName)
        {
            StringWriter writer = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(writer);
            config.WriteXml(xmlWriter);

            lock (this.lockEvents)
            {
                this.Events.Add(new EventLogModel
                {
                    Device = new DeviceModel { Id = config.ID },
                    State = writer.ToString(),
                    Action = new SmartHome.Core.Models.ActionModel { Name = actionName },
                    Datetime = DateTime.Now
                });
            }
        }

        public void SaveEvents()
        {
            List<EventLogModel> saveEventList; 
            lock (lockEvents)
            {
                saveEventList = this.Events.ToList();
                this.Events.Clear();
            }

            this.webAPIManager.SaveEvents(saveEventList);
        }
    }
}