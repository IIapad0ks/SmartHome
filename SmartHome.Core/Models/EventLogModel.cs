using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.Core.Models
{
    public class EventLogModel : IModel
    {
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public DeviceTypeModel Type { get; set; }
        public EventActionModel Action { get; set; }
        public string DeviceState { get; set; }
        public DateTime EventDatetime { get; set; }
    }
}