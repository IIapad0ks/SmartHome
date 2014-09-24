using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class Trigger : INameEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int SetValue { get; set; }

        [ForeignKey("EventAction")]
        public int EventActionId { get; set; }
        public virtual EventAction EventAction { get; set; }

        [ForeignKey("Device")]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        [ForeignKey("Sensor")]
        public int SensorId { get; set; }
        public virtual Device Sensor { get; set; }

        public string Condition { get; set; }
    }
}
