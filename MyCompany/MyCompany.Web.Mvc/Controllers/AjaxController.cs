using System.Web.Mvc;
using MyCompany.Web.Mvc.Caching;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.REST.BazaarVoice;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IDownloader _downloader;
        private readonly IBazaarVoiceManager _bazaarVoiceManager;

        public AjaxController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();

            if (_bazaarVoiceManager == null)
                _bazaarVoiceManager = new BazaarVoiceManager();
        }

        public JsonResult Get(string userkey)
        {
            var userInfo = CouchbaseManager.GetJson(userkey);

            var view = CouchbaseManager.GetView();
            //var view = CouchbaseManager.GetView<UserInfo>();

            return Json(userInfo);
        }

        public ActionResult GetRatings(string productId)
        {
            var response = _bazaarVoiceManager.GetRatings(productId);

            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateBazaarVoiceModel(productId, response);

            return Content(model.RatingsResponse, "application/json");
        }
    }
}
