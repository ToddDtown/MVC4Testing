using StructureMap.Configuration.DSL;
using Turn5.WebServices;

namespace Turn5.Web.Mvc.IoC
{
    public class StructureMapControllerRegistry : Registry
    {
        public StructureMapControllerRegistry()
        {
            For<ITestService>().Use<TestService>();
        }
    }
}
