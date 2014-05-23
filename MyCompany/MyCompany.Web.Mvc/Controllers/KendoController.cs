using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class KendoController : Controller
    {
        public ActionResult Get()
        {
            return View("Kendo");
        }
    }
}
