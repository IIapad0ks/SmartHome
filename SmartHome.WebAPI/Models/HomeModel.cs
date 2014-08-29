using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHome.WebAPI.Models
{
    public class HomeModel
    {
        public Appliance Device { get; set; }
        public ObjectType Type { get; set; }
        public List<EventLog> Events { get; set; }
    }
}