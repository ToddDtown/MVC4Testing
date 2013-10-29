using Couchbase;

namespace Turn5.Web.Mvc.Session
{
    public class SessionManager
    {
        private CouchbaseClient _instance;

        public SessionManager()
        {
            if (_instance == null) 
                _instance = new CouchbaseClient();
        }

        public void Store(string key, string value)
        {
            
        }

        public void Get(string key)
        {
            
        }
    }
}
