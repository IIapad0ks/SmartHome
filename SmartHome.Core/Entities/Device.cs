using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class Device : IDeviceEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsOn { get; set; }
        public bool FastAccess { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int WorkingTime { get; set; }

        [ForeignKey("DeviceType")]
        public int DeviceTypeId { get; set; }
        public virtual DeviceType DeviceType { get; set; }
    }
}
