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
    using SmartHome.Core.DBModelConverters;
    using SmartHome.Core.Models;
    
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
            SIManager.Container = container;
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container)
        {
            var webAPILifestyle = new WebApiRequestLifestyle();

            container.Register<DbContext, SmartHomeDBContext>(webAPILifestyle);
            container.Register<ISHRepository, SHRepository>();

            container.Register<IActionConverter, ActionConverter>();
            container.Register<IDeviceTypeConverter, DeviceTypeConverter>();
            container.Register<IDeviceConverter, DeviceConverter>();
            container.Register<IEventLogConverter, EventLogConverter>();
            container.Register<IRoomConverter, RoomConverter>();
            container.Register<ISHServiceConverter, SHServiceConverter>();
            container.Register<IDeviceClassConverter, DeviceClassConverter>();
            container.Register<ITriggerConverter, TriggerConverter>();
        }
    }
}