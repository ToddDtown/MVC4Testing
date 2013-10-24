using System.Web.Optimization;

namespace Turn5.Web.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Resources/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Resources/scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Resources/scripts/jquery.unobtrusive*",
                        "~/Resources/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Resources/scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Resources/css/homepage.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Resources/css/products.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Resources/css/viewer.css"));
        }
    }
}