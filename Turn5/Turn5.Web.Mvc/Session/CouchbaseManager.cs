using Couchbase;
using Enyim.Caching.Memcached;

namespace Turn5.Web.Mvc.Session
{
    public static class CouchbaseManager
    {
        private readonly static CouchbaseClient _instance;

        static CouchbaseManager()
        {
            if (_instance == null) 
                _instance = new CouchbaseClient();
        }

        public static CouchbaseClient Instance
        {
            get { return _instance; }
        }

        public static void Store(string key, string value, StoreMode storeType)
        {
            _instance.Store(storeType, key, value);
        }

        public static object Get(string key)
        {
            return _instance.Get(key);
        }
    }
}
