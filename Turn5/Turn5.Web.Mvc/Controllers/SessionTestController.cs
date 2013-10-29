using System.Web.Mvc;
using Enyim.Caching.Memcached;
using Turn5.Web.Mvc.Session;

namespace Turn5.Web.Mvc.Controllers
{
    public class SessionTestController : BaseController
    {
        private readonly string _key = "user:1234";

        public ActionResult Get()
        {
            CouchbaseManager.Store(_key, "123", StoreMode.Replace);

            var userInfo = CouchbaseManager.Get(_key);

            return View("SessionTest");
        }
    }
}
