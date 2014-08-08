using System.Web.Mvc;
using MyCompany.Web.Mvc.IoC;
using MyCompany.Web.Mvc.ViewEngine;

namespace MyCompany.Web.Mvc
{
    public static class MvcBootstrapper
    {
        public static void Bootstrap()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterModelBinders(ModelBinders.Binders);

            ViewEngines.Engines.Clear();
            var themeableRazorViewEngine = new MyCompanyViewEngine
                        {
                            CurrentTheme = @base => "MyCompany"
                        };
            ViewEngines.Engines.Add(themeableRazorViewEngine);

            ViewEngines.Engines.Insert(0, new MobileCapableRazorViewEngine());

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
