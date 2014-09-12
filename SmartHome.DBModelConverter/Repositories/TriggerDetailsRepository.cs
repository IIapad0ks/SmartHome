using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Models;
using SmartHome.Core.Entities;
using SmartHome.Core.Repositories;

namespace SmartHome.DBModelConverter.Repositories
{
    public class TriggerDetailsRepository : DBModelDeviceDetailsRepository<TriggerDetailsModel, Trigger>, ITriggerDetailsRepository
    {
        public TriggerDetailsRepository(ISHRepository<Trigger> repository) : base(repository) { }
    }
}
