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
            Clear();
        }

        public static void Clear()
        {
            Container = new Container();
        }
    }
}