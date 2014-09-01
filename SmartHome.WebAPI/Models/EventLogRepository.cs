using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBModels = SmartHome.WebAPI.Models;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public interface IEventLogRepository { }

    public class EventLogRepository : SmartHomeRepository<EventLog, DBModels.EventLog>, IEventLogRepository
    {
        public override EventLog DBItemToItem(DBModels.EventLog dbEventLog)
        {
            return new EventLog { ID = dbEventLog.ID, Action = new ActionRepository().Get(dbEventLog.ActionID), Device = new DeviceRepository().Get(dbEventLog.DeviceID), DeviceState = dbEventLog.DeviceState, EventDatetime = dbEventLog.EventDatetime };
        }

        public override DBModels.EventLog ItemToDBItem(EventLog eventLog)
        {
            DBModels.EventLog dbEventLog = new DBModels.EventLog { ActionID = eventLog.Action.ID, DeviceID = eventLog.Device.ID, DeviceState = eventLog.DeviceState, EventDatetime = eventLog.EventDatetime };

            if (eventLog.ID != null)
            {
                dbEventLog.ID = eventLog.ID;
            }

            return dbEventLog;
        }
    }
}