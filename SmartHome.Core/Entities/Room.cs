using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class Room : INameEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("SHService")]
        public int SHServiceId { get; set; }
        public virtual SHService SHService { get; set; }
    }
}
