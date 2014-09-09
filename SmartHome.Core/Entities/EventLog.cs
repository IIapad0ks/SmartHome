using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class EventLog : IEntity
    {
        public int ID { get; set; }

        public int ConfigID { get; set; }
        public virtual IDeviceEntity Device { get; set; }

        public int DeviceTypeID { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        public int ActionID { get; set; }
        public virtual EventAction Action { get; set; }

        public string DeviceState { get; set; }
        public DateTime EventDatetime { get; set; }
    }
}
