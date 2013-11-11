using System.Web.Mvc;
using Couchbase;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class ReviewController : BaseController
    {
        private IDownloader _downloader;
        private ICouchbaseClient _couchbaseClient;

        public ReviewController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();

            if (_couchbaseClient == null) 
                _couchbaseClient = new CouchbaseClient();
        }

        [HttpPost]
        public ActionResult AddReview(BazaarVoiceModel bazaarVoiceModel)
        {
            //var bvModel = _bazaarVoiceManager.AddReview(bazaarVoiceModel);

            return View("Review", null);
        }
    }
}
