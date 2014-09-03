using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public interface INameEntity : IEntity
    {
        string Name { get; set; }
    }
}
