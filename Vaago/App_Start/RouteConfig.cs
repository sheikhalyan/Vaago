﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vaago
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapHttpRoute(
          name: "UniqueApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        //    routes.MapRoute(
        //    name: "AdminCustomers",
        //    url: "AdminCustomers",
        //    defaults: new { controller = "AdminCustomers", action = "Index" }
        //);



            /*routes.MapRoute(
                name: "DeleteSelected",
                url: "AdminMenu/DeleteSelected",
                defaults: new { controller = "AdminMenu", action = "DeleteSelected" }
            );*/



        }
    }
}
