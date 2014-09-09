using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Core.Entities
{
    public interface ISmartHomeDBContext
    {
        DbSet<EventAction> EventActions { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<DeviceType> DeviceTypes { get; set; }
        DbSet<EventLog> EventLogs { get; set; }
        DbSet<Sensor> Sensors { get; set; }
        DbSet<Trigger> Triggers { get; set; }
        DbSet<SmartHomeService> SmartHomeServices { get; set; }
    }
}
