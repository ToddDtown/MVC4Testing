using System.Web.Mvc;
using Turn5.Web.Mvc.Session;

namespace Turn5.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        public JsonResult Get(string userkey)
        {
            var userInfo = CouchbaseManager.GetJson(userkey);

            var view = CouchbaseManager.GetView();
            //var view = CouchbaseManager.GetView<UserInfo>();

            return Json(userInfo);
        }
    }
}
