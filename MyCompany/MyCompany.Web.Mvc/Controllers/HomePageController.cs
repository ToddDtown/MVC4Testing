using System.Web.Mvc;
using MyCompany.Web.Mvc.Models.ModelBuilders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class HomePageController : BaseController
    {
        public ActionResult Get()
        {
            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateHomeModel();

            return View("HomePage", model);
        }
    }
}
