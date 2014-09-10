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
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("Parent")]
        public int? ParentID { get; set; }
        public virtual DeviceType Parent { get; set; }
    }
}
