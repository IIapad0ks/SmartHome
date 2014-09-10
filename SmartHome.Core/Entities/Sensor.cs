using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class Sensor : IDeviceEntity
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("DeviceType")]
        public int DeviceTypeID { get; set; }
        public virtual DeviceType DeviceType { get; set; }
    }
}
