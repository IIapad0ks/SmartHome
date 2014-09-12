﻿using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Repositories
{
    public interface IDeviceDetailsRepository : IDBModelDeviceDetailsRepository<DeviceDetailsModel, Device>
    {
    }
}