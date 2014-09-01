using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public class DeviceType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DeviceType Parent { get; set; }
    }
}