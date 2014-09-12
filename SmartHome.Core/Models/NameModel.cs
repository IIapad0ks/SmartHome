using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public abstract class NameModel : Model, INameModel
    {
        public string Name { get; set; }
    }
}
