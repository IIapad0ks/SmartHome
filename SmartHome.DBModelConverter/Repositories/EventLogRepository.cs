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
    public class EventLogRepository : DBModelRepository<Models.EventLog, Entities.EventLog>, IEventLogRepository
    {
        public EventLogRepository(ISHRepository<Entities.EventLog> repository)
        {
            this.repository = repository;
        }

        public override Models.EventLog DBItemToItem(Entities.EventLog dbEventLog)
        {
            if (dbEventLog == null)
            {
                return new Models.EventLog();
            }

            return new Models.EventLog 
            { 
                ID = dbEventLog.ID, 
                Action = SIManager.Container.GetInstance<IActionRepository>().Get(dbEventLog.ActionID), 
                Device = this.GetDevice(dbEventLog.ConfigID, dbEventLog.DeviceTypeID), 
                Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbEventLog.DeviceTypeID), 
                DeviceState = dbEventLog.DeviceState, 
                EventDatetime = dbEventLog.EventDatetime 
            };
        }

        public override Entities.EventLog ItemToDBItem(Models.EventLog eventLog)
        {
            Entities.EventLog dbEventLog = new Entities.EventLog { ActionID = eventLog.Action.ID, ConfigID = eventLog.Device.ID, DeviceState = eventLog.DeviceState, EventDatetime = eventLog.EventDatetime };

            if (eventLog.ID != 0)
            {
                dbEventLog.ID = eventLog.ID;
            }

            return dbEventLog;
        }

        private Models.IDeviceModel GetDevice(int id, int typeID)
        {
            Models.IDeviceModel device = null;
            List<IDBModelNameRepository<Models.IDeviceModel>> repositories = new List<IDBModelNameRepository<Models.IDeviceModel>>();
            foreach (var repository in repositories)
            {
                device = repository.GetAll().FirstOrDefault(nm => nm.ID == id && nm.Type.ID == typeID);
            }

            return device;
        }

        public void Add(IQueryable<Models.EventLog> events)
        {
            foreach (var e in events)
            {
                this.Add(e);
            }
        }
    }
}