namespace SmartHome.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixSomeSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventActions", "CanSetValue", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviceTypes", "HasValue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceTypes", "HasValue");
            DropColumn("dbo.EventActions", "CanSetValue");
        }
    }
}
