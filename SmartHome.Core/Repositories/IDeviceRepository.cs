using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.Core.Repositories
{
    public interface IDeviceRepository : IDBModelNameRepository<DeviceModel, Device> { }
}
