using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class EventLog : IModel
    {
        public int ID { get; set; }
        public INameModel Device { get; set; }
        public DeviceType Type { get; set; }
        public Action Action { get; set; }
        public string DeviceState { get; set; }
        public DateTime EventDatetime { get; set; }
    }
}