using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.Core.Repositories
{
    public interface ISensorRepository : IDBModelNameRepository<SensorModel, Sensor> { }
}
