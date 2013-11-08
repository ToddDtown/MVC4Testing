using System.Web.Mvc;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.WebServices;

namespace MyCompany.Web.Mvc.Controllers
{
    public class HomePageController : BaseController
    {
        protected ITestService _testService;

        public HomePageController(ITestService testService)
        {
            _testService = testService;
        }

        public ActionResult Get()
        {
            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateHomeModel();

            return View("HomePage", model);
        }
    }
}
