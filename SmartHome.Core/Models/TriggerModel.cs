using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class TriggerModel : IDeviceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DeviceTypeModel Type { get; set; }
        public DeviceModel Device { get; set; }
        public SensorModel Sensor { get; set; }
        public string Condition { get; set; }
    }
}
