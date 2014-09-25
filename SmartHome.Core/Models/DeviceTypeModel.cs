using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class DeviceTypeModel : INameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NeedTimeControl { get; set; }
        public bool HasValue { get; set; }
        public string Symbol { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public DeviceClassModel Class { get; set; }
    }
}