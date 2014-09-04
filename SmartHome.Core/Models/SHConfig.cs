using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public class SHConfig : IModel
    {
        public int ID { get; set; }
        public string ConfigFilename { get; set; }
        public string LibDirname { get; set; }
        public bool IsOn { get; set; }
    }
}
