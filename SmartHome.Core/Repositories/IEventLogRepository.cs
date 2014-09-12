using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IEventLogRepository : IDBModelRepository<EventLogModel, EventLog> 
    {
        void Add(IQueryable<EventLogModel> events);
        IQueryable<EventLogModel> Get(IDeviceModel device);
    }
}
