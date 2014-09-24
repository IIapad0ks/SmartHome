﻿using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.DBModelConverters
{
    public interface IEventLogConverter : IDBModelConverter<EventLogModel, EventLog>
    {
        void Add(List<EventLogModel> events);
    }
}
