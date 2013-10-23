using System.Web.Mvc;
using StructureMap;

namespace Turn5.Web.Mvc.IoC
{
    public static class StructureMapBootstrapper
    {
        public static void Bootstrap()
        {
            RegisterControllers();
            InitializeContainer();
        }

        public static void InitializeContainer()
        {
            DependencyResolver.SetResolver(
                new StructureMapDependencyResolver(ObjectFactory.Container));
        }

        public static void RegisterControllers()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new StructureMapControllerRegistry()));

            //ObjectFactory.Configure(x => x.Scan(scan =>
            //{
            //    scan.TheCallingAssembly();
            //    scan.AddAllTypesOf<Controller>();
            //}
            //));
        }
    }
}
