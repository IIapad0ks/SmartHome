using SmartHome.Core.Models;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IEventLogRepository : IDBModelRepository<EventLogModel> 
    {
        void Add(IQueryable<EventLogModel> events);
    }
}
