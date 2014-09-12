using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class TriggerDetailsModel : TriggerModel, IDeviceDetailsModel
    {
        public List<EventLogModel> EventList { get; set; }
    }
}
