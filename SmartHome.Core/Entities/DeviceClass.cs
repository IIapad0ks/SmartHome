using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class DeviceClass : INameEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
