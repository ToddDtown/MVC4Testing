using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MyCompany.WebServices;
using Unity.Mvc3;

namespace MyCompany.Web.Mvc
{
    public static class UnityBootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<ITestService, TestService>();

            //container.RegisterType<IController, StoreController>("Store");
            return container;
        }
    }
}
