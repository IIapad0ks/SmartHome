namespace SmartHome.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeviceTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeID, cascadeDelete: true)
                .Index(t => t.DeviceTypeID);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DeviceTypes", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.EventActions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EventLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConfigID = c.Int(nullable: false),
                        DeviceTypeID = c.Int(nullable: false),
                        EventActionID = c.Int(nullable: false),
                        DeviceState = c.String(),
                        EventDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeID, cascadeDelete: true)
                .ForeignKey("dbo.EventActions", t => t.EventActionID, cascadeDelete: true)
                .Index(t => t.DeviceTypeID)
                .Index(t => t.EventActionID);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeviceTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeID, cascadeDelete: true)
                .Index(t => t.DeviceTypeID);
            
            CreateTable(
                "dbo.SmartHomeServices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ConfigFilename = c.String(),
                        LibsDirname = c.String(),
                        IsOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeviceTypeID = c.Int(nullable: false),
                        DeviceID = c.Int(nullable: false),
                        SensorID = c.Int(nullable: false),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Devices", t => t.DeviceID, cascadeDelete: false)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeID, cascadeDelete: false)
                .ForeignKey("dbo.Sensors", t => t.SensorID, cascadeDelete: false)
                .Index(t => t.DeviceTypeID)
                .Index(t => t.DeviceID)
                .Index(t => t.SensorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Triggers", "SensorID", "dbo.Sensors");
            DropForeignKey("dbo.Triggers", "DeviceTypeID", "dbo.DeviceTypes");
            DropForeignKey("dbo.Triggers", "DeviceID", "dbo.Devices");
            DropForeignKey("dbo.Sensors", "DeviceTypeID", "dbo.DeviceTypes");
            DropForeignKey("dbo.EventLogs", "EventActionID", "dbo.EventActions");
            DropForeignKey("dbo.EventLogs", "DeviceTypeID", "dbo.DeviceTypes");
            DropForeignKey("dbo.Devices", "DeviceTypeID", "dbo.DeviceTypes");
            DropForeignKey("dbo.DeviceTypes", "ParentID", "dbo.DeviceTypes");
            DropIndex("dbo.Triggers", new[] { "SensorID" });
            DropIndex("dbo.Triggers", new[] { "DeviceID" });
            DropIndex("dbo.Triggers", new[] { "DeviceTypeID" });
            DropIndex("dbo.Sensors", new[] { "DeviceTypeID" });
            DropIndex("dbo.EventLogs", new[] { "EventActionID" });
            DropIndex("dbo.EventLogs", new[] { "DeviceTypeID" });
            DropIndex("dbo.DeviceTypes", new[] { "ParentID" });
            DropIndex("dbo.Devices", new[] { "DeviceTypeID" });
            DropTable("dbo.Triggers");
            DropTable("dbo.SmartHomeServices");
            DropTable("dbo.Sensors");
            DropTable("dbo.EventLogs");
            DropTable("dbo.EventActions");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Devices");
        }
    }
}
