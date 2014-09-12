using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public interface IDeviceDetailsModel : IDeviceModel
    {
        List<EventLogModel> EventList { get; set; }
    }
}
