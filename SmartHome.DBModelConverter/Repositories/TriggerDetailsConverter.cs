using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.Repositories;
using SmartHome.Core;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class TriggerDetailsConverter : TriggerConverter<TriggerDetailsModel>
    {
        public TriggerDetailsConverter(ISHRepository repository) : base(repository) { }

        public override TriggerDetailsModel DBItemToItem(Trigger dbItem)
        {
            TriggerDetailsModel item = base.DBItemToItem(dbItem);
            item.EventList = SIManager.Container.GetInstance<IEventLogConverter>().Get(e => e.ConfigID == item.ID && e.DeviceTypeID == item.Type.ID).ToList();
            return item;
        }
    }
}
