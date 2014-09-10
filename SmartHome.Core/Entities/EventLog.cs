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
        public int ID { get; set; }
        public int ConfigID { get; set; }

        [ForeignKey("DeviceType")]
        public int DeviceTypeID { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        [ForeignKey("EventAction")]
        public int EventActionID { get; set; }
        public virtual EventAction EventAction { get; set; }

        public string DeviceState { get; set; }
        public DateTime EventDatetime { get; set; }
    }
}
