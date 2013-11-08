using System.Web.Mvc;
using Couchbase;
using MyCompany.Web.Mvc.Caching;
using MyCompany.Web.Mvc.REST.BazaarVoice;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IBazaarVoiceManager _bazaarVoiceManager;

        public AjaxController(IBazaarVoiceManager bazaarVoiceManager)
        {
            //if (_bazaarVoiceManager == null)
            //    _bazaarVoiceManager = new BazaarVoiceManager(downloader, couchbaseClient);

            _bazaarVoiceManager = bazaarVoiceManager;
        }

        public JsonResult Get(string userkey)
        {
            var userInfo = CouchbaseManager.GetJson(userkey);
            var view = CouchbaseManager.GetView();

            return Json(userInfo);
        }

        public ActionResult GetRatings(string productId)
        {
            var response = _bazaarVoiceManager.GetRatings(productId);

            return Content(response, "application/json");
        }
    }
}
