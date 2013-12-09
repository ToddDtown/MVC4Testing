using System;
using System.Web.Mvc;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.WebServices.Controllers
{
    //[RequireHttps]
    public class PaymentController : BaseController
    {
        private IDownloader _downloader;

        public PaymentController(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public ActionResult GetToken()
        {
            var firstDataRequest = new Uri("https://api.globalgatewaye4.firstdata.com/transaction/v12");
            var response = _downloader.GetResponse(firstDataRequest);

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
