using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SmartHome.Core.Entities
{
    public class SmartHomeDBContext : DbContext, ISmartHomeDBContext
    {
        public SmartHomeDBContext() : base("name=SmartHomeDBEntities") { }

        public DbSet<EventAction> Actions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<DeviceClass> DeviceClasses { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<SHService> SHServices { get; set; }
    }
}
