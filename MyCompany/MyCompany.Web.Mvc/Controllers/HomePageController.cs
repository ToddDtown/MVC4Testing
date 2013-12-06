using System;
using System.Configuration;
using System.Web.Mvc;
using Couchbase;
using Enyim.Caching.Memcached;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;
using Newtonsoft.Json;

namespace MyCompany.Web.Mvc.Controllers
{
    public class HomePageController : BaseController
    {
        private readonly IDownloader _downloader;
        private readonly ICouchbaseClient _couchbaseClient;

        public HomePageController(IDownloader downloader, ICouchbaseClient couchbaseClient)
        {
            _downloader = downloader;
            _couchbaseClient = couchbaseClient;
        }

        public ActionResult Get()
        {
            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateHomeModel();

            return View("HomePage", model);
        }

        public ActionResult GetReviews(string productId)
        {
            BazaarVoiceReviews reviews = null;
            const string cachePrefix = "Reviews_";

            var cachedReviews = _couchbaseClient != null ? _couchbaseClient.Get(cachePrefix + productId) : null;

            if (cachedReviews != null)
            {
                reviews = JsonConvert.DeserializeObject<BazaarVoiceReviews>(((DownloaderResponse)cachedReviews).ResponseString);
            }
            else
            {
                var query = new WebBazaarVoiceReviewsQuery
                {
                    ApiVersion = ConfigurationManager.AppSettings["BazaarVoiceApiVersion"],
                    PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                    Filter = "productid:" + productId,
                    HasComments = true,
                    Sort = ConfigurationManager.AppSettings["BazaarVoiceResultSort"],
                    Limit = Convert.ToInt32(ConfigurationManager.AppSettings["BazaarVoiceResultLimit"])
                };

                var uri = new Uri(query.ToString());
                var response = _downloader.GetResponse(uri);

                try
                {

                    reviews = JsonConvert.DeserializeObject<BazaarVoiceReviews>(response.ResponseString);

                }
                catch (Exception exc)
                {

                    var error = exc;

                }

                //_couchbaseClient.Store(StoreMode.Set, cachePrefix + productId, response, DateTime.Now.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["BazaarVoiceCacheExpiration"])));
            }

            return PartialView("_BazaarVoice", reviews);
        }
    }
}
