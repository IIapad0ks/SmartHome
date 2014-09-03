using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Extensions;
using System.Reflection;

namespace SmartHome.Core
{
    public static class SIManager
    {
        public static Container Container;

        static SIManager()
        {
            Container = new Container();
        }

        public static void AddAssembly(Assembly assembly) {
            var registrations =
                from type in assembly.GetExportedTypes()
                where type.GetInterfaces().Any()
                select new
                {
                    Service = type.GetInterfaces().Single(),
                    Implementation = type
                };

            foreach (var reg in registrations)
            {
                if (reg.Implementation.IsAbstract) continue;

                if (reg.Service.IsGenericType)
                {
                    Container.RegisterOpenGeneric(reg.Service, reg.Implementation);
                }
                else
                {
                    Container.Register(reg.Service, reg.Implementation);
                }
            }
        }
    }
}