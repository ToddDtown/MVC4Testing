using System;
using System.Web.Mvc;

namespace Turn5.Web.Mvc.ViewEngine
{
    public class TestItOutViewEngine : ThemeableBuildManagerViewEngine
    {
        public TestItOutViewEngine()
        {
            // AREAS
            // -----------------------------------------
            // 0        | 1              | 2        | 3
            // viewName | controllerName | areaName | theme
            AreaViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{3}/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{3}/Shared/{0}.cshtml",
            };

            AreaMasterLocationFormats = new[] {
                "~/Areas/{2}/Views/{3}/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{3}/Shared/{0}.cshtml",
            };

            AreaPartialViewLocationFormats = new[] {
                "~/Areas/{2}/Views/{3}/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/{3}/Shared/{0}.cshtml",
            };

            // VIEWS
            // -----------------------------------------
            // 0        | 1              | 2
            // viewName | controllerName | theme
            ViewLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/Shared/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };

            MasterLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/Shared/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };

            PartialViewLocationFormats = new[] {
                "~/Views/{2}/{1}/{0}.cshtml",
                "~/Views/{2}/Shared/{0}.cshtml",
                "~/Views/Shared/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
            };

            ViewStartFileExtensions = new[] { "cshtml" };
        }

        public string[] ViewStartFileExtensions { get; set; }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, ViewStartFileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, true, ViewStartFileExtensions);
        }

        protected override bool IsValidCompiledType(ControllerContext controllerContext, string virtualPath, Type compiledType)
        {
            return typeof(WebViewPage).IsAssignableFrom(compiledType);
        }
    }
}
