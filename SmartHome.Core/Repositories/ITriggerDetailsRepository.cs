﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;

namespace SmartHome.Core.Repositories
{
    public interface ITriggerDetailsRepository : IDBModelDeviceDetailsRepository<TriggerDetailsModel, Trigger>
    {
    }
}