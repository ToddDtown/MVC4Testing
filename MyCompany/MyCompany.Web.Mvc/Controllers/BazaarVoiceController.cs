using System.Web.Mvc;
using MyCompany.Web.Mvc.REST.BazaarVoice;

namespace MyCompany.Web.Mvc.Controllers
{
    public class BazaarVoiceController : BaseController
    {
        //private readonly IBazaarVoiceManager _bazaarVoiceManager;

        public BazaarVoiceController()
        {
            //if (_bazaarVoiceManager == null)
            //    _bazaarVoiceManager = new BazaarVoiceManager();
        }

        public ActionResult Get()
        {
            // BazaarVoice API Requests for Ratings and Reviews
            //stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=kuy3zj9pr3n7i0wxajrzj04xo&Filter=ProductId:1000001&Filter=HasComments:true&Sort=Rating:desc&Limit=10
            //api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=iff2ag5avxj8t8mi0d98gkce5&Filter=ProductId:headlight-black-proj-2010&Include=Products&Stats=Reviews&Limit=1
            //stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=whu4rjbhv5ee35t8q7uezefs&Filter=ProductId:headlight-black-proj-2010&HasComments:true&Sort=Rating:desc&Limit=10

            var model = "headlight-black-proj-2010";
            return View("BazaarVoice", null, model);
        }
    }
}
