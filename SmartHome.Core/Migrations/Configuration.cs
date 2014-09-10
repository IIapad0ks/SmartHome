using SmartHome.Core.Entities;

namespace SmartHome.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartHomeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartHomeDBContext context)
        {
            context.SmartHomeServices.Add(new SmartHomeService { Name = "SHService", IsOn = false, ConfigFilename = @"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.SHService\bin\Debug\SmartHome.xml", LibsDirname = @"E:\Bohdan\dotnet_workspace\SmartHome\SmartHome.SHService\bin\Debug\libs\" });
        }
    }
}
