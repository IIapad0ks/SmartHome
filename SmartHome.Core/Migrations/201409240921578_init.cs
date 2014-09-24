namespace SmartHome.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.Int(nullable: false),
                        IsOn = c.Boolean(nullable: false),
                        FastAccess = c.Boolean(nullable: false),
                        RoomId = c.Int(nullable: false),
                        WorkingTime = c.Int(nullable: false),
                        DeviceTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceTypes", t => t.DeviceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.DeviceTypeId);
            
            CreateTable(
                "dbo.DeviceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NeedTimeControl = c.Boolean(nullable: false),
                        DeviceClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeviceClasses", t => t.DeviceClassId, cascadeDelete: true)
                .Index(t => t.DeviceClassId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SHServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SHServices", t => t.SHServiceId, cascadeDelete: true)
                .Index(t => t.SHServiceId);
            
            CreateTable(
                "dbo.SHServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsOn = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Triggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SetValue = c.Int(nullable: false),
                        EventActionId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        SensorId = c.Int(nullable: false),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId)
                .ForeignKey("dbo.EventActions", t => t.EventActionId, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.SensorId)
                .Index(t => t.EventActionId)
                .Index(t => t.DeviceId)
                .Index(t => t.SensorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Triggers", "SensorId", "dbo.Devices");
            DropForeignKey("dbo.Triggers", "EventActionId", "dbo.EventActions");
            DropForeignKey("dbo.Triggers", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.Devices", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "SHServiceId", "dbo.SHServices");
            DropForeignKey("dbo.Devices", "DeviceTypeId", "dbo.DeviceTypes");
            DropForeignKey("dbo.DeviceTypes", "DeviceClassId", "dbo.DeviceClasses");
            DropIndex("dbo.Triggers", new[] { "SensorId" });
            DropIndex("dbo.Triggers", new[] { "DeviceId" });
            DropIndex("dbo.Triggers", new[] { "EventActionId" });
            DropIndex("dbo.Rooms", new[] { "SHServiceId" });
            DropIndex("dbo.DeviceTypes", new[] { "DeviceClassId" });
            DropIndex("dbo.Devices", new[] { "DeviceTypeId" });
            DropIndex("dbo.Devices", new[] { "RoomId" });
            DropTable("dbo.Triggers");
            DropTable("dbo.SHServices");
            DropTable("dbo.Rooms");
            DropTable("dbo.DeviceTypes");
            DropTable("dbo.Devices");
            DropTable("dbo.DeviceClasses");
            DropTable("dbo.EventActions");
        }
    }
}
