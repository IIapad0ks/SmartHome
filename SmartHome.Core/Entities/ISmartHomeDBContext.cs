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
        DbSet<EventAction> Actions { get; set; }
        DbSet<Device> Devices { get; set; }
        DbSet<DeviceType> DeviceTypes { get; set; }
        DbSet<Trigger> Triggers { get; set; }
        DbSet<SHService> SHServices { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<DeviceClass> DeviceClasses { get; set; }
    }
}
