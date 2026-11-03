using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HK_EDI.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               "HomePage",
               "",
               new { controller = "Home", action = "Index", id = "" },
               new[] { "HK_EDI.Web.Controllers" }
           );

            routes.MapRoute(
               "Login",
               "Login",
               new { controller = "Customer", action = "Login", id = "" },
               new[] { "HK_EDI.Web.Controllers" }
           );

            routes.MapRoute(
               "Logout",
               "Logout",
               new { controller = "Customer", action = "Logout", id = "" },
               new[] { "HK_EDI.Web.Controllers" }
           );

            routes.MapRoute(
               "Admin_default",
               "Admin/{controller}/{action}/{id}",
               new { id = UrlParameter.Optional },
               new[] { "HK_EDI.Web.Controllers" }
           );

        }
    }
}
