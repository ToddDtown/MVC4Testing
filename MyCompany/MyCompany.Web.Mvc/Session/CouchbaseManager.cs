using Couchbase;
using Couchbase.Extensions;
using Enyim.Caching.Memcached;

namespace MyCompany.Web.Mvc.Session
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

        public static void Store(string key, string value, StoreMode storeMode)
        {
            _instance.Store(storeMode, key, value);
        }

        public static void StoreJson(string key, UserInfo value, StoreMode storeMode)
        {
            _instance.StoreJson(storeMode, key, value);
        }

        public static void StoreJson(string key, ImageViewerInfo value, StoreMode storeMode)
        {
            _instance.StoreJson(storeMode, key, value);
        }

        public static object Get(string key)
        {
            return _instance.Get(key);
        }

        public static object GetView()
        {
            return _instance.GetView("dev_AMSession", "SessionInfo");
        }

        public static object GetView<T>()
        {
            return _instance.GetView<T>("dev_AMSession", "SessionInfo");
        }

        public static object GetJson(string key)
        {
            return _instance.GetJson<UserInfo>(key);
        }
    }
}
