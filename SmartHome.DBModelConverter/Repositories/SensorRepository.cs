using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHome.Core;
using SmartHome.Core.Repositories;
using Models = SmartHome.Core.Models;
using Entities = SmartHome.Core.Entities;

namespace SmartHome.DBModelConverter.Repositories
{
    public class SensorRepository : DBModelNameRepository<Models.Sensor, Entities.Sensor>, ISensorRepository
    {
        public SensorRepository(ISHRepository<Entities.Sensor> repository)
        {
            this.repository = repository;
        }

        public override Models.Sensor DBItemToItem(Entities.Sensor dbSensor)
        {
            if (dbSensor == null)
            {
                return new Models.Sensor();
            }

            return new Models.Sensor { ID = dbSensor.ID, Name = dbSensor.Name, Type = SIManager.Container.GetInstance<IDeviceTypeRepository>().Get(dbSensor.DeviceTypeID) };
        }

        public override Entities.Sensor ItemToDBItem(Models.Sensor sensor)
        {
            if (sensor == null)
            {
                return new Entities.Sensor();
            }

            return new Entities.Sensor { ID = sensor.ID, Name = sensor.Name, DeviceTypeID = sensor.Type.ID };
        }
    }
}
