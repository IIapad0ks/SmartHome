using System.ServiceModel;
using SmartHome.Core.SmartHome;
using SmartHome.Core.Service;
using SmartHome.Core.Repositories;
using SmartHome.Core.Entities;
using SmartHome.Core;
using SmartHome.DBModelConverter.Repositories;
using SmartHome.Data.Repositories;
using SimpleInjector.Extensions;
using System.Linq;
using SimpleInjector;
using System.Data.Entity;

namespace SmartHome.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SmartHomeService : ISmartHomeService
    {
        private ISmartHomeHandler home;

        public SmartHomeService()
        {
            this.home = SIManager.Container.GetInstance<ISmartHomeHandler>();
        }

        public bool Start()
        {
            return home.Start();
        }

        public bool Stop()
        {
            return home.Stop();
        }

        public bool Restart()
        {
            return home.Restart();
        }

        public bool IsOn()
        {
            return home.IsOn;
        }
    }
}
