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
            SHService home = context.SHServices.Add(new SHService { Name = "MyHome", IsOn = true });

            Room room = context.Rooms.Add(new Room { Name = "BedRoom", SHService = home });

            DeviceClass device = context.DeviceClasses.Add(new DeviceClass { Name = "Device" });
            DeviceClass sensor = context.DeviceClasses.Add(new DeviceClass { Name = "Sensor" });

            DeviceType conditioner = context.DeviceTypes.Add(new DeviceType { Name = "Conditioner", DeviceClass = device, NeedTimeControl = false, HasValue = true });
            DeviceType light = context.DeviceTypes.Add(new DeviceType { Name = "Light", DeviceClass = device, NeedTimeControl = false, HasValue = true });
            DeviceType simpleLamp = context.DeviceTypes.Add(new DeviceType { Name = "SimpleLamp", DeviceClass = device, NeedTimeControl = false });
            DeviceType temperatureSensor = context.DeviceTypes.Add(new DeviceType { Name = "TemperatureSensor", DeviceClass = sensor, NeedTimeControl = false, HasValue = true });
            DeviceType lightSensor = context.DeviceTypes.Add(new DeviceType { Name = "LightSensor", DeviceClass = sensor, NeedTimeControl = false, HasValue = true });
            DeviceType window = context.DeviceTypes.Add(new DeviceType { Name = "Window", DeviceClass = device, NeedTimeControl = true, HasValue = true });

            Device roomConditioner = context.Devices.Add(new Device { DeviceType = conditioner, FastAccess = true, IsOn = true, Name = "Room conditioner", Room = room, Value = 23 });
            Device roomLight = context.Devices.Add(new Device { DeviceType = light, FastAccess = true, IsOn = true, Name = "Room light", Room = room, Value = 25 });
            Device roomTableLamp = context.Devices.Add(new Device { DeviceType = simpleLamp, Name = "Table lamp", Room = room });
            Device bedLamp = context.Devices.Add(new Device { DeviceType = simpleLamp, Name = "Bed lamp", Room = room });
            Device termometer = context.Devices.Add(new Device { DeviceType = temperatureSensor, IsOn = true, Name = "Room termometer", Room = room, Value = 18 });
            Device roomBrightness = context.Devices.Add(new Device { DeviceType = lightSensor, IsOn = true, Name = "Room light sensor", Room = room, Value = 25 });
            Device roomWindow = context.Devices.Add(new Device { DeviceType = window, IsOn = true, Name = "Room window", Room = room, Value = 10, WorkingTime = 600 });

            EventAction on = context.Actions.Add(new EventAction { Name = "ON" });
            EventAction off = context.Actions.Add(new EventAction { Name = "OFF" });
            EventAction setValue = context.Actions.Add(new EventAction { Name = "Set value", CanSetValue = true });

            Trigger onConditionerCold = context.Triggers.Add(new Trigger { Condition = "< 18", Device = roomConditioner, EventAction = on, Name = "On conditioner when cold", Sensor = termometer });
            Trigger onConditionerHot = context.Triggers.Add(new Trigger { Condition = "> 26", Device = roomConditioner, EventAction = on, Name = "On conditioner when hot", Sensor = termometer });
            Trigger offConditionerFromCold = context.Triggers.Add(new Trigger { Condition = "> 20", Device = roomConditioner, EventAction = off, Name = "Off conditioner when not cold", Sensor = termometer });
            Trigger offConditionerFromHot = context.Triggers.Add(new Trigger { Condition = "< 24", Device = roomConditioner, EventAction = off, Name = "Off conditioner when not hot", Sensor = termometer });
            Trigger setColdConditionerValue = context.Triggers.Add(new Trigger { Condition = "> 30", Device = roomConditioner, EventAction = setValue, Name = "Set value on hot", Sensor = termometer, SetValue = 18 });
            Trigger setNormColdConditionerValue = context.Triggers.Add(new Trigger { Condition = "> 26", Device = roomConditioner, EventAction = setValue, Name = "Set value on norm cold", Sensor = termometer, SetValue = 22 });
            Trigger setNormHotConditionerValue = context.Triggers.Add(new Trigger { Condition = "< 18", Device = roomConditioner, EventAction = setValue, Name = "Set value on norm hot", Sensor = termometer, SetValue = 22 });
            Trigger setHotConditionerValue = context.Triggers.Add(new Trigger { Condition = "< 14", Device = roomConditioner, EventAction = setValue, Name = "Set value on hot", Sensor = termometer, SetValue = 26 });
        }
    }
}
