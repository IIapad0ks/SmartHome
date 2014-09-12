using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core.Entities;
using SmartHome.Core.Models;
using SmartHome.Core.Repositories;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorDetailsRepository : DBModelDeviceDetailsRepository<SensorDetailsModel, Sensor>, ISensorDetailsRepository
    {
        public SensorDetailsRepository(ISHRepository<Sensor> repository) : base(repository) { }
    }
}
