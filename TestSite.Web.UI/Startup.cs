using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestSite.Web.UI.Startup))]
namespace TestSite.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
