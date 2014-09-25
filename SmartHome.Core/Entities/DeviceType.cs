using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class DeviceType : INameEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool NeedTimeControl { get; set; }
        public bool HasValue { get; set; }
        public string Symbol { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        [ForeignKey("DeviceClass")]
        public int DeviceClassId { get; set; }
        public virtual DeviceClass DeviceClass { get; set; }
    }
}
