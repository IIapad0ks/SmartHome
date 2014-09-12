using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class DeviceTypeModel : NameModel, INameModel
    {
        public DeviceTypeModel Parent { get; set; }
    }
}