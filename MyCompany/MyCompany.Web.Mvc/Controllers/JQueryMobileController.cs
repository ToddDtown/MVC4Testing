using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class JQueryMobileController : BaseController
    {
        public ActionResult Get() 
        {
            return View("JQueryMobile");
        }
    }
}
