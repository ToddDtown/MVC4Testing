using System.Web.Mvc;
using Turn5.Web.Mvc.Session;

namespace Turn5.Web.Mvc.Controllers
{
    public class SessionTestController : BaseController
    {
        public ActionResult Get()
        {

            //var sessionMgr = new SessionManager();

            //sessionMgr.Store("", "");

            //var sessionMgr2 = new SessionManager();
            
            //sessionMgr2.Store("", "");

            return View("SessionTest");
        }
    }
}
