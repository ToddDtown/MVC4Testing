using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyCompany.Web.Mvc;
using MyCompany.Web.UI.App_Start;

namespace MyCompany.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AuthConfig.RegisterOpenAuth();

            //UnityBootstrapper.Initialise();
            MvcBootstrapper.Bootstrap();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ctx = HttpContext.Current;
            var error = ctx.Server.GetLastError();
            var x = "";
        }
    }
}