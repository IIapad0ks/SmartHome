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
    public class EventLogRepository : DBModelRepository<Models.EventLogModel, Entities.EventLog>, IEventLogRepository
    {
        public EventLogRepository(ISHRepository<Entities.EventLog> repository)
        {
            this.repository = repository;
        }

        public override Models.EventLogModel Add(Models.EventLogModel item)
        {
            if (item.Action.ID == default(int))
            {
                item.Action = SIManager.Container.GetInstance<IActionRepository>().Add(item.Action);
            }

            return base.Add(item);
        }

        public override bool Update(Models.EventLogModel item)
        {
            if (item.Action.ID != default(int))
            {
                SIManager.Container.GetInstance<IActionRepository>().Update(item.Action);
            }

            return base.Update(item);
        }

        public override Models.EventLogModel DBItemToItem(Entities.EventLog dbEventLog)
        {
            if (dbEventLog == null)
            {
                return null;
            }

            return new Models.EventLogModel 
            { 
                ID = dbEventLog.ID, 
                Action = SIManager.Container.GetInstance<IActionRepository>().Get(dbEventLog.ActionID), 
                Device = this.GetDevice(dbEventLog.ConfigID, dbEventLog.DeviceTypeID), 
                Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbEventLog.DeviceTypeID), 
                DeviceState = dbEventLog.DeviceState, 
                EventDatetime = dbEventLog.EventDatetime 
            };
        }

        public override Entities.EventLog ItemToDBItem(Models.EventLogModel eventLog)
        {
            if (eventLog == null)
            {
                return null;
            }

            Entities.EventLog dbEventLog = new Entities.EventLog { ID = eventLog.ID, ActionID = eventLog.Action.ID, ConfigID = eventLog.Device.ID, DeviceState = eventLog.DeviceState, EventDatetime = eventLog.EventDatetime };

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

        public void Add(IQueryable<Models.EventLogModel> events)
        {
            foreach (var e in events)
            {
                this.Add(e);
            }
        }
    }
}