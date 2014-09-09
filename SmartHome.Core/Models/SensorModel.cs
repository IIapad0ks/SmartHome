using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class SensorModel : IDeviceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DeviceTypeModel Type { get; set; }
    }
}
