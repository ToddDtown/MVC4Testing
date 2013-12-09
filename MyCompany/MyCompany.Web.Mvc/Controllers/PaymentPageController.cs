using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class PaymentPageController : Controller
    {
        public ActionResult Get()
        {
            return View("PaymentPage");
        }
    }
}
