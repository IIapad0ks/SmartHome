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
    public class EventLogConverter : DBModelConverter<EventLogModel, EventLog>, IEventLogConverter
    {
        public EventLogConverter(ISHRepository repository) : base(repository) { }

        public override EventLogModel Add(EventLogModel item)
        {
            if (item.Action.ID == default(int))
            {
                item.Action = SIManager.Container.GetInstance<IActionConverter>().Add(item.Action);
            }

            return base.Add(item);
        }

        public override bool Update(EventLogModel item)
        {
            if (item.Action.ID != default(int))
            {
                SIManager.Container.GetInstance<IActionConverter>().Update(item.Action);
            }

            return base.Update(item);
        }

        public virtual List<EventLogModel> Get(IDeviceModel device)
        {
            return this.GetAll().Where(e => e.DeviceID == device.ID && e.Type.ID == device.Type.ID).ToList();
        }

        public override EventLogModel DBItemToItem(EventLog dbItem)
        {
            EventLogModel item = base.DBItemToItem(dbItem);
            item.Action = SIManager.Container.GetInstance<IActionConverter>().Get(dbItem.EventActionID);
            item.DeviceID = dbItem.ConfigID;
            item.Type = SIManager.Container.GetInstance<IDeviceTypeConverter>().Get(dbItem.DeviceTypeID);
            item.DeviceState = dbItem.DeviceState;
            item.EventDatetime = dbItem.EventDatetime;
            return item;
        }

        public override EventLog ItemToDBItem(EventLogModel item)
        {
            EventLog dbItem = base.ItemToDBItem(item);
            dbItem.ID = item.ID;
            dbItem.DeviceTypeID = item.Type.ID;
            dbItem.EventActionID = item.Action.ID;
            dbItem.ConfigID = item.DeviceID;
            dbItem.DeviceState = item.DeviceState;
            dbItem.EventDatetime = item.EventDatetime;
            return dbItem;
        }

        public void Add(List<EventLogModel> events)
        {
            foreach (var e in events)
            {
                this.Add(e);
            }
        }
    }
}