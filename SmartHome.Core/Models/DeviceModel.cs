using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class DeviceModel : IDeviceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsOn { get; set; }
        public bool FastAccess { get; set; }
        public RoomModel Room { get; set; }
        public int WorkingTime { get; set; }
        public DeviceTypeModel Type { get; set; }
    }
}