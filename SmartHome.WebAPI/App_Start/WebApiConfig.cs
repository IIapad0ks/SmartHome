﻿using System.Web.Http;

namespace SmartHome.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SHService",
                routeTemplate: "api/{controller}/{id}/{command}",
                defaults: new { controller = "SmartHome", id = RouteParameter.Optional, command = RouteParameter.Optional }
            );
        }
    }
}
