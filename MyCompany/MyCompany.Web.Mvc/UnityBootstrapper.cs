using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MyCompany.Web.Mvc.REST.Downloaders;
using MyCompany.Web.Mvc.Services;
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

            var container = new UnityContainer()
                .RegisterType<IDownloader, HttpDownloader>();

            container.RegisterType<IBazaarVoiceService, BazaarVoiceService>(
                                        new InjectionConstructor(container.Resolve<IDownloader>()));


            return container;
        }
    }
}
