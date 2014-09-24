using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class TriggerModel : INameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SetValue { get; set; }
        public ActionModel Action { get; set; }
        public DeviceModel Device { get; set; }
        public DeviceModel Sensor { get; set; }
        public string Condition { get; set; }
    }
}
