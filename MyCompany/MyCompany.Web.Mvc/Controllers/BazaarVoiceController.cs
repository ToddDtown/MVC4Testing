using System;
using System.Configuration;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class BazaarVoiceController : BaseController
    {
        private IDownloader _downloader;

        public BazaarVoiceController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();
        }

        public ActionResult Get()
        {
            //stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=kuy3zj9pr3n7i0wxajrzj04xo&Filter=ProductId:1000001&Filter=HasComments:true&Sort=Rating:desc&Limit=10
             
            //api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=iff2ag5avxj8t8mi0d98gkce5&Filter=ProductId:headlight-black-proj-2010&Include=Products&Stats=Reviews&Limit=1
            //stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=whu4rjbhv5ee35t8q7uezefs&Filter=ProductId:headlight-black-proj-2010&HasComments:true&Sort=Rating:desc&Limit=10

            var bazaarVoiceQuery = new WebBazaarVoiceQuery
            {
                ApiVersion = "5.4",
                PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                Filter = string.Empty,
                ProductId = "headlight-black-proj-2010",
                HasComments = true,
                Sort = "Rating:desc",
                Limit = 10
            };

            var uri = new Uri(bazaarVoiceQuery.ToString());

            var response = _downloader.GetResponse(uri);

            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateBazaarVoiceModel(response.ResponseString);

            //var uri = new Uri("http://stg.api.bazaarvoice.com/data/reviews.json?apiversion=5.4&passkey=whu4rjbhv5ee35t8q7uezefs&Filter=ProductId:headlight-black-proj-2010&HasComments:true&Sort=Rating:desc&Limit=10");

            //return View("BazaarVoice", model);
            return Content(model.RatingsResponse, "application/json");
        }
    }
}
