using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class EventLog : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public string State { get; set; }

        [ForeignKey("Action")]
        public int ActionId { get; set; }
        public virtual EventAction Action { get; set; }

        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }
}
