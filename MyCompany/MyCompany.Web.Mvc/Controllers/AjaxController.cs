using System.Web.Mvc;
using MyCompany.Web.Mvc.Session;

namespace MyCompany.Web.Mvc.Controllers
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
