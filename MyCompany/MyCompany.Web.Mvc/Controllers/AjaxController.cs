using System.Web.Mvc;
using MyCompany.Web.Mvc.Caching;
using MyCompany.Web.Mvc.REST.BazaarVoice;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IBazaarVoiceManager _bazaarVoiceManager;

        public AjaxController()
        {
            if (_bazaarVoiceManager == null)
                _bazaarVoiceManager = new BazaarVoiceManager();
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
