using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainManagementWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            
            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "TrainManagementWeb.Areas.Admin.Controllers" }
             );
            routes.MapRoute(
               name: "Admin",
               url: "admin",
               defaults: new { controller = "User", action = "List", id = UrlParameter.Optional },
               namespaces: new[] { "TrainManagementWeb.Areas.Admin.Controllers" }
            );

            routes.MapRoute(
              name: "Train Schedule",
              url: "schedule/train-{id}",
              defaults: new { controller = "Schedule", action = "GetTrainSchedule", id = UrlParameter.Optional },
              namespaces: new[] { "TrainManagementWeb.Areas.Admin.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Booking", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TrainManagementWeb.Controllers" }
            );
        }
    }
}
