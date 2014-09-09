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

        public DbSet<EventAction> EventActions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<SmartHomeService> SmartHomeServices { get; set; }
    }
}
