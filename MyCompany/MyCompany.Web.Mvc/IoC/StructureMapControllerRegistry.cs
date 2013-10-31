using StructureMap.Configuration.DSL;
using MyCompany.WebServices;

namespace MyCompany.Web.Mvc.IoC
{
    public class StructureMapControllerRegistry : Registry
    {
        public StructureMapControllerRegistry()
        {
            For<ITestService>().Use<TestService>();

            //For<ICouchbaseClient>().Singleton().Use<SessionManager>();
        }
    }
}
