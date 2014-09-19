using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome.Core.DBModelConverters
{
    public interface IEventLogConverter : IDBModelConverter<EventLogModel, EventLog> 
    {
        void Add(List<EventLogModel> events);
        List<EventLogModel> Get(IDeviceModel device);
    }
}
