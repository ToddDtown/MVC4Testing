using System.Web.Optimization;

namespace MyCompany.Web.UI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // ##############################################################################################################################
            // JAVASCRIPT
            // ##############################################################################################################################

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Resources/scripts/jquery/jquery-1.7.1.js",
                                                                     "~/Resources/scripts/jquery/jquery-ui-1.8.20.js",
                                                                     "~/Resources/scripts/jquery/jquery.unobtrusive*",
                                                                     "~/Resources/scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-mobile").Include("~/Resources/scripts/jquery-mobile/jquery.mobile.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/mycompany").Include("~/Resources/scripts/custom/user-info.js"));

            // ##############################################################################################################################
            // CSS
            // ##############################################################################################################################

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Resources/css/homepage.css",
                                                                 "~/Resources/css/products.css",
                                                                 "~/Resources/css/viewer.css",
                                                                 "~/Resources/css/core.css"));

            bundles.Add(new StyleBundle("~/Content/jquery-mobile").Include("~/Resources/css/jquery-mobile/jquery.mobile.custom.structure.css",
                                                                           "~/Resources/css/jquery-mobile/jquery.mobile.custom.theme.css"));
        }
    }
}