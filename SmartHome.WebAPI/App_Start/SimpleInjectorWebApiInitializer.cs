[assembly: WebActivator.PostApplicationStartMethod(typeof(SmartHome.WebAPI.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace SmartHome.WebAPI.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SmartHome.Core.Repositories;
    using SmartHome.Data.Repositories;
    using SmartHome.DBModelConverter.Repositories;
    using SmartHome.Core;
    using SmartHome.Core.SmartHome;
    using System.Data.Entity;
    using SmartHome.Core.Entities;
    using SimpleInjector.Extensions;
    
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: https://bit.ly/YE8OJj.
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            var webAPILifestyle = new WebApiRequestLifestyle();
            container.Register<DbContext, SmartHomeDBEntities>(webAPILifestyle);
            container.RegisterOpenGeneric(typeof(ISHRepository<>), typeof(SmartHomeRepository<>));

            container.Register<IActionRepository, ActionRepository>();
            container.Register<IDeviceRepository, DeviceRepository>();
            container.Register<IDeviceTypeRepository, DeviceTypeRepository>();
            container.Register<ISensorRepository, SensorRepository>();
            container.Register<ITriggerRepository, TriggerRepository>();
            container.Register<IEventLogRepository, EventLogRepository>();
            container.Register<ISHConfigRepository, SHConfigRepository>();
        }
    }
}