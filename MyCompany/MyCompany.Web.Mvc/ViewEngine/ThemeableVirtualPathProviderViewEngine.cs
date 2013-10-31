using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyCompany.Web.Mvc.ViewEngine
{
    public abstract class ThemeableVirtualPathProviderViewEngine : IViewEngine
    {
        private static readonly string[] _emptyLocations = new string[0];

        private VirtualPathProvider _vpp;

        protected ThemeableVirtualPathProviderViewEngine()
        {
            
        }

        public Func<HttpContextBase, string> CurrentTheme { get; set; }

        public string[] AreaMasterLocationFormats { get; set; }
        public string[] AreaPartialViewLocationFormats { get; set; }
        public string[] AreaViewLocationFormats { get; set; }

        public string[] MasterLocationFormats { get; set; }
        public string[] PartialViewLocationFormats { get; set; }
        public string[] ViewLocationFormats { get; set; }

        public IViewLocationCache ViewLocationCache { get; set; }

        protected VirtualPathProvider VirtualPathProvider
        {
            get { return _vpp ?? (_vpp = HostingEnvironment.VirtualPathProvider); }

            set { _vpp = value; }
        }

        public virtual ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (string.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "viewName");
            }

            string[] viewLocationsSearched;
            string[] masterLocationsSearched;
            bool incompleteMatch = false;

            string controllerName = controllerContext.RouteData.GetRequiredString("controller");

            string viewPath = GetPath(controllerContext, ViewLocationFormats, AreaViewLocationFormats,
                                      "ViewLocationFormats", viewName, controllerName, true, ref incompleteMatch, out viewLocationsSearched);

            string masterPath = GetPath(controllerContext, MasterLocationFormats, AreaMasterLocationFormats,
                                        "MasterLocationFormats", masterName, controllerName, false, ref incompleteMatch,
                                        out masterLocationsSearched);

            if (string.IsNullOrEmpty(viewPath) ||
                (string.IsNullOrEmpty(masterPath) && !string.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
            }

            return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
        }

        public virtual ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
                                                        bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (string.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "partialViewName");
            }

            string[] searched;
            bool incompleteMatch = false;
            string controllerName = controllerContext.RouteData.GetRequiredString("controller");

            string partialPath = GetPath(controllerContext, PartialViewLocationFormats, AreaPartialViewLocationFormats,
                                         "PartialViewLocationFormats", partialViewName, controllerName,
                                         true, ref incompleteMatch, out searched);

            if (string.IsNullOrEmpty(partialPath))
            {
                return new ViewEngineResult(searched);
            }

            return new ViewEngineResult(CreatePartialView(controllerContext, partialPath), this);
        }

        public virtual void ReleaseView(ControllerContext controllerContext, IView view)
        {

        }

        protected virtual bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return VirtualPathProvider.FileExists(virtualPath);
        }

        protected virtual bool? IsValidPath(ControllerContext controllerContext, string virtualPath)
        {
            return null;
        }

        protected abstract IView CreatePartialView(ControllerContext controllerContext, string partialPath);

        protected abstract IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath);

        private static List<ViewLocation> GetViewLocations(IEnumerable<string> viewLocationFormats,
                                                           IEnumerable<string> areaViewLocationFormats)
        {
            var allLocations = new List<ViewLocation>();

            if (areaViewLocationFormats != null)
            {
                allLocations.AddRange(
                    areaViewLocationFormats.Select(
                        areaViewLocationFormat => new AreaAwareViewLocation(areaViewLocationFormat)));
            }

            if (viewLocationFormats != null)
            {
                allLocations.AddRange(
                    viewLocationFormats.Select(viewLocationFormat => new ViewLocation(viewLocationFormat)));
            }

            return allLocations;
        }

        private static bool IsSpecificPath(string name)
        {
            char c = name[0];

            return (c == '~' || c == '/');
        }

        private static string GetAreaName(RouteData routeData)
        {
            object area;

            if (routeData.DataTokens.TryGetValue("area", out area))
            {
                return area as string;
            }

            return GetAreaName(routeData.Route);
        }

        private static string GetAreaName(RouteBase route)
        {
            Route castRoute = route as Route;

            if (castRoute != null && castRoute.DataTokens != null)
            {
                return castRoute.DataTokens["area"] as string;
            }

            return null;
        }

        private string GetPath(ControllerContext controllerContext, IEnumerable<string> locations,
                               IEnumerable<string> areaLocations, string locationsPropertyName,
                               string name, string controllerName, bool checkPathValidity, ref bool incompleteMatch, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;

            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            string areaName = GetAreaName(controllerContext.RouteData);
            bool usingAreas = !string.IsNullOrEmpty(areaName);

            string theme = CurrentTheme(controllerContext.HttpContext);

            List<ViewLocation> viewLocations = GetViewLocations(locations, (usingAreas) ? areaLocations : null);

            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                  "The property '{0}' cannot be null or empty.",
                                                                  locationsPropertyName));
            }

            bool nameRepresentsPath = IsSpecificPath(name);

            return nameRepresentsPath
                       ? GetPathFromSpecificName(controllerContext, name, checkPathValidity,
                                                 ref searchedLocations, ref incompleteMatch)
                       : GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName,
                                                theme, ref searchedLocations);
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, IList<ViewLocation> locations,
                                              string name, string controllerName, string areaName, string theme,
                                              ref string[] searchedLocations)
        {
            string result = string.Empty;
            searchedLocations = new string[locations.Count];

            for (int i = 0; i < locations.Count; i++)
            {
                ViewLocation location = locations[i];

                string virtualPath = location.Format(name, controllerName, areaName, theme);

                if (FileExists(controllerContext, virtualPath))
                {
                    searchedLocations = _emptyLocations;
                    result = virtualPath;
                    break;
                }

                searchedLocations[i] = virtualPath;
            }

            return result;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name,
                                               bool checkPathValidity, ref string[] searchedLocations,
                                               ref bool incompleteMatch)
        {
            string result = name;
            bool fileExists = FileExists(controllerContext, name);

            if (checkPathValidity && fileExists)
            {
                bool? validPath = IsValidPath(controllerContext, name);

                if (validPath == false)
                {
                    fileExists = false;
                }
                else if (validPath == null)
                {
                    incompleteMatch = true;
                }
            }

            if (!fileExists)
            {
                result = string.Empty;
                searchedLocations = new[] {name};
            }

            return result;
        }

        private class AreaAwareViewLocation : ViewLocation
        {
            public AreaAwareViewLocation(string virtualPathFormatString)
                : base(virtualPathFormatString)
            {
            }

            public override string Format(string viewName, string controllerName, string areaName, string theme)
            {
                return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName,
                                     areaName, theme);
            }
        }

        private class ViewLocation
        {
            protected readonly string _virtualPathFormatString;

            public ViewLocation(string virtualPathFormatString)
            {
                _virtualPathFormatString = virtualPathFormatString;
            }

            public virtual string Format(string viewName, string controllerName, string areaName, string theme)
            {
                return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName,
                                     theme);
            }
        }
    }
}
