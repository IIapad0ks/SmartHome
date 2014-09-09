using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public class SmartHomeService : INameEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ConfigFilename { get; set; }
        public string LibsDirname { get; set; }
        public bool IsOn { get; set; }
    }
}
