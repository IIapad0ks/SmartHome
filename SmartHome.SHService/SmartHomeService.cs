using System;
using System.ServiceProcess;
using SmartHome.Core;
using SmartHome.Core.SmartHome;
using System.Data.Entity;
using SmartHome.Core.Entities;
using SmartHome.Service;
using SmartHome.Data.Repositories;
using SmartHome.Core.Repositories;
using SmartHome.DBModelConverter.Repositories;
using SimpleInjector.Extensions;

namespace SmartHome.SHService
{
    public partial class SmartHomeService : ServiceBase
    {
        ISmartHomeHandler shHandler;

        public SmartHomeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SIManager.Container.Register<ISaveEventsManager, SaveEventsManager>();
            SIManager.Container.Register<ISmartHomeHandler, SmartHomeHandler>();
            SIManager.Container.Register<DbContext, SmartHomeDBEntities>();
            SIManager.Container.RegisterOpenGeneric(typeof(ISHRepository<>), typeof(SmartHomeRepository<>));

            SIManager.Container.Register<IActionRepository, ActionRepository>();
            SIManager.Container.Register<IDeviceRepository, DeviceRepository>();
            SIManager.Container.Register<IDeviceTypeRepository, DeviceTypeRepository>();
            SIManager.Container.Register<ISensorRepository, SensorRepository>();
            SIManager.Container.Register<ITriggerRepository, TriggerRepository>();
            SIManager.Container.Register<IEventLogRepository, EventLogRepository>();
            SIManager.Container.Register<ISHConfigRepository, SHConfigRepository>();

            SIManager.Container.Verify();

            this.shHandler = SIManager.Container.GetInstance<ISmartHomeHandler>();
        }

        protected override void OnStop()
        {
            this.shHandler.Stop();
            SIManager.Clear();
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);

            if (!Enum.IsDefined(typeof(SHCommamd), command))
            {
                return;
            }

            SHCommamd clientCommand = (SHCommamd)command;
            switch (clientCommand)
            {
                case SHCommamd.Start:
                    this.shHandler.Start();
                    break;
                case SHCommamd.Stop:
                    this.shHandler.Stop();
                    break;
                case SHCommamd.Restart:
                    this.shHandler.Restart();
                    break;
            }
        }
    }
}
