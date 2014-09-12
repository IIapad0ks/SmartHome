using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Repositories;

namespace SmartHome.DBModelConverter.Repositories
{
    public class DeviceDetailsRepository : DBModelDeviceDetailsRepository<DeviceDetailsModel, Device>, IDeviceDetailsRepository
    {
        public DeviceDetailsRepository(ISHRepository<Device> repository) : base(repository) { }
    }
}
