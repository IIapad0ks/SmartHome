using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class DeviceModel : IDeviceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DeviceTypeModel Type { get; set; }
    }
}