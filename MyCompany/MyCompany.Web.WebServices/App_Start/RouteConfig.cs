using System.Web.Mvc;
using System.Web.Routing;

namespace MyCompany.Web.WebServices
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}",
                new { controller = "Payment", action = "DoCrypto" }
            );
        }
    }
}