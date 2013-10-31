using System.Web.Optimization;

namespace MyCompany.Web.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Resources/scripts/jquery/jquery-1.7.1.js",
                                                                     "~/Resources/scripts/jquery/jquery-ui-1.8.20.js",
                                                                     "~/Resources/scripts/jquery/jquery.unobtrusive*",
                                                                     "~/Resources/scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/mycompany").Include("~/Resources/scripts/custom/user-info.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Resources/scripts/modernizer/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Resources/css/homepage.css",
                                                                 "~/Resources/css/products.css",
                                                                 "~/Resources/css/viewer.css"));
        }
    }
}