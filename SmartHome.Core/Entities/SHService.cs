using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class SHService : INameEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
    }
}
