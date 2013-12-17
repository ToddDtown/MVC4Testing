using Couchbase;
using MyCompany.Web.Mvc.REST.Downloaders;
using MyCompany.Web.Mvc.Services;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace MyCompany.Web.Mvc.IoC
{
    public class StructureMapControllerRegistry : Registry
    {
        public StructureMapControllerRegistry()
        {
            For<IDownloader>().Use<HttpDownloader>();

            For<IBazaarVoiceService>().Use<BazaarVoiceService>()
                .Ctor<IDownloader>().Is(ObjectFactory.GetInstance<HttpDownloader>())
                .Ctor<ICouchbaseClient>().Is(For<ICouchbaseClient>().Singleton().Use(new CouchbaseClient()));
        }
    }
}
