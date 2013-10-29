using Couchbase;
using StructureMap.Configuration.DSL;
using Turn5.Web.Mvc.Session;
using Turn5.WebServices;

namespace Turn5.Web.Mvc.IoC
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
