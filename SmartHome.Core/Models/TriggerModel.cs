using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class TriggerModel : NameModel, IDeviceModel
    {
        public DeviceTypeModel Type { get; set; }
        public DeviceModel Device { get; set; }
        public SensorModel Sensor { get; set; }
        public string Condition { get; set; }
    }
}
