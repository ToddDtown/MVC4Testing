using System.Web.Mvc;
using System.Web.Routing;

namespace MyCompany.Web.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "UIControls", action = "Get" }
            );

            routes.MapRoute(
                "Ajax",
                "ajax/{action}",
                new { controller = "Ajax" }
            );
        }
    }
}