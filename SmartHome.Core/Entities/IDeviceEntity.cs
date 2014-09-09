using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartHome.Core.Entities
{
    public interface IDeviceEntity : INameEntity
    {
        int DeviceTypeID { get; set; }
    }
}
