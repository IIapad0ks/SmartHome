using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Repositories
{
    public interface ISensorDetailsRepository : IDBModelDeviceDetailsRepository<SensorDetailsModel, Sensor>
    {
    }
}
