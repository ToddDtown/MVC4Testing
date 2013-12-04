using System.Web.Mvc;

namespace MyCompany.Web.WebServices.Controllers
{
    //[RequireHttps]
    public class PaymentController : BaseController
    {
        public ActionResult DoCrypto()
        {
            if (Request.IsSecureConnection) {


            }

            return Content("");
        }

        public ActionResult GetToken()
        {
            return Content("");
        }

        public ActionResult GetAuth()
        {
            return Content("");
        }

        public ActionResult GetTokenAndAuth()
        {
            return Content("");
        }

        public ActionResult CapturePaymentOrRefund()
        {
            return Content("");
        }
    }
}
