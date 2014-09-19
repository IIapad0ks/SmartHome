using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.Core.DBModelConverters
{
    public interface ISensorConverter<T> : IDBModelNameConverter<T, Sensor> where T : SensorModel { }
}
