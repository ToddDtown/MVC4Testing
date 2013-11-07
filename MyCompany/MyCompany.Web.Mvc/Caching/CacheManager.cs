using Couchbase;
using Couchbase.Extensions;

namespace MyCompany.Web.Mvc.Caching
{
    public static class CacheManager
    {
        private static readonly CouchbaseClient _couchbaseClientInstance;

        static CacheManager()
        {
            if (_couchbaseClientInstance == null)
                _couchbaseClientInstance = new CouchbaseClient();
        }

        public static void Do()
        {
            //_couchbaseClientInstance.GetJson<>()
        }
    }
}
