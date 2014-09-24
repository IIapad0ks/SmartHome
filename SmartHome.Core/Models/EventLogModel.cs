using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class EventLogModel : IModel
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public string State { get; set; }
        public virtual ActionModel Action { get; set; }
        public virtual DeviceModel Device { get; set; }
    }
}
