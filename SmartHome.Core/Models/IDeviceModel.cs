using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Models
{
    public interface IDeviceModel : INameModel
    {
        DeviceTypeModel Type { get; set; }
    }
}
