using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class UIControlsController : Controller
    {
        public ActionResult Get()
        {
            return View("UIControls");
        }
    }
}
