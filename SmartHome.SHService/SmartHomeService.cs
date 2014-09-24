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
using SimpleInjector;
using SmartHome.Core.Service;
using SmartHome.Core.DBModelConverters;
using SmartHome.Core.Models;

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
            this.InitializeContainer(SIManager.Container);
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

        private void InitializeContainer(Container container)
        {
            container.Register<IWebAPIManager, WebAPIManager>();
            container.Register<ISaveEventsManager, SaveEventsManager>();
            container.Register<ISmartHomeHandler, SmartHomeHandler>();
            container.Register<DbContext, SmartHomeDBContext>();
            container.Register<ISHRepository, SHRepository>();

            container.Register<IActionConverter, ActionConverter>();
            container.Register<IDeviceTypeConverter, DeviceTypeConverter>();
            container.Register<IEventLogConverter, EventLogConverter>();
            container.Register<ISHServiceConverter, SHServiceConverter>();
            container.Register<IDeviceConverter, DeviceConverter>();
            container.Register<IDeviceClassConverter, DeviceClassConverter>();
            container.Register<ITriggerConverter, TriggerConverter>();
            container.Register<IRoomConverter, RoomConverter>();

            container.Verify();
        }
    }
}
