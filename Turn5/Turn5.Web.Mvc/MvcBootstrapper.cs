using System.Web.Mvc;
using Turn5.Web.Mvc.IoC;
using Turn5.Web.Mvc.ViewEngine;

namespace Turn5.Web.Mvc
{
    public static class MvcBootstrapper
    {
        public static void Bootstrap()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterModelBinders(ModelBinders.Binders);

            ViewEngines.Engines.Clear();
            var themeableRazorViewEngine = new Turn5ViewEngine
                        {
                            CurrentTheme = @base => "AmericanMuscle"
                        };
            ViewEngines.Engines.Add(themeableRazorViewEngine);

            StructureMapBootstrapper.Bootstrap();
        }

        public static void RegisterModelBinders(ModelBinderDictionary binders)
        {
            //binders.Add(typeof(AbstractYellowBookWebQuery), new QueryModelBinder());
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
        }
    }
}
