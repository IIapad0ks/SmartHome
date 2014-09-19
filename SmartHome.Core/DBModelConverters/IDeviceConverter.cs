using SmartHome.Core.Entities;
using SmartHome.Core.Models;

namespace SmartHome.Core.DBModelConverters
{
    public interface IDeviceConverter<T> : IDBModelNameConverter<T, Device> where T : DeviceModel { }
}
