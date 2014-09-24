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

        public override EventLogModel DBItemToItem(EventLog dbItem)
        {
            EventLogModel item = base.DBItemToItem(dbItem);

            item.Action = SIManager.Container.GetInstance<IActionConverter>().DBItemToItem(dbItem.Action);
            item.Datetime = dbItem.Datetime;
            item.Device = SIManager.Container.GetInstance<IDeviceConverter>().DBItemToItem(dbItem.Device);
            item.State = dbItem.State;

            return item;
        }

        public override EventLog ItemToDBItem(EventLogModel item)
        {
            EventLog dbItem = base.ItemToDBItem(item);

            dbItem.ActionId = item.Action.Id;
            dbItem.Datetime = item.Datetime;
            dbItem.DeviceId = item.Device.Id;
            dbItem.State = item.State;

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