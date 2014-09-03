using SmartHome.Core.Models;
using System.Linq;

namespace SmartHome.Core.Repositories
{
    public interface IEventLogRepository : IDBModelRepository<EventLog> 
    {
        void Add(IQueryable<EventLog> events);
    }
}
