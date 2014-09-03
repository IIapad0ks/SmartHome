using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class Trigger : IDeviceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DeviceType Type { get; set; }
        public Device Device { get; set; }
        public Sensor Sensor { get; set; }
        public string Condition { get; set; }
    }
}
