namespace SmartHome.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSymbolAndRange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviceTypes", "Symbol", c => c.String());
            AddColumn("dbo.DeviceTypes", "MinValue", c => c.Int(nullable: false));
            AddColumn("dbo.DeviceTypes", "MaxValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviceTypes", "MaxValue");
            DropColumn("dbo.DeviceTypes", "MinValue");
            DropColumn("dbo.DeviceTypes", "Symbol");
        }
    }
}
