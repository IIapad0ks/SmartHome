using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;
using SmartHome.Core;
using SmartHome.Core.DBModelConverters;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorDetailsConverter : SensorConverter<SensorDetailsModel>
    {
        public SensorDetailsConverter(ISHRepository repository) : base(repository) { }

        public override SensorDetailsModel DBItemToItem(Sensor dbItem)
        {
            SensorDetailsModel item = base.DBItemToItem(dbItem);
            item.EventList = SIManager.Container.GetInstance<IEventLogConverter>().Get(e => e.ConfigID == item.ID && e.DeviceTypeID == item.Type.ID).ToList();
            return item;
        }
    }
}
