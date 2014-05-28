using System.Web.Mvc;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class ReviewController : BaseController
    {
        private IDownloader _downloader;

        public ReviewController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();

        }
    }
}
