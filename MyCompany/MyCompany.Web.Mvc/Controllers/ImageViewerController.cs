using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class ImageViewerController : Controller
    {
        public ActionResult Index()
        {
            return View("ImageViewer");
        }
    }
}
