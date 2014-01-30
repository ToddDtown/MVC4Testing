using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class ImageViewerController : Controller
    {
        public ActionResult Index()
        {
            var model = "Hello World";

            return View("ImageViewer", null, model);
        }
    }
}
