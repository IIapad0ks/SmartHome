using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.WebAPI.Models.WebAPIModels
{
    public class EventLog
    {
        public int ID { get; set; }
        public Device Device { get; set; }
        public Action Action { get; set; }
        public string DeviceState { get; set; }
        public DateTime EventDatetime { get; set; }
    }
}