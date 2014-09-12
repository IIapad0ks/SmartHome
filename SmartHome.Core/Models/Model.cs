using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public abstract class Model : IModel
    {
        public int ID { get; set; }
    }
}
