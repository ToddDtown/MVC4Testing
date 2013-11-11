using System;
using System.Configuration;
using System.Web.Mvc;
using Couchbase;
using Enyim.Caching.Memcached;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IDownloader _downloader;
        private readonly ICouchbaseClient _couchbaseClient;

        public AjaxController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();

            if (_couchbaseClient == null)
                _couchbaseClient = new CouchbaseClient();
        }

        public ActionResult GetReviews(string productId)
        {
            const string cachePrefix = "Reviews_";

            var cachedRating = _couchbaseClient != null ? _couchbaseClient.Get(cachePrefix + productId) : null;

            if (cachedRating != null)
                return Content(((DownloaderResponse)cachedRating).ResponseString);

            var bazaarVoiceQuery = new WebBazaarVoiceReviewsQuery
            {
                ApiVersion = ConfigurationManager.AppSettings["BazaarVoiceApiVersion"],
                PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                Filter = "productid:" + productId,
                HasComments = true,
                Sort = ConfigurationManager.AppSettings["BazaarVoiceResultSort"],
                Limit = Convert.ToInt32(ConfigurationManager.AppSettings["BazaarVoiceResultLimit"])
            };

            return Content(RequestReviews(productId, cachePrefix, bazaarVoiceQuery));
        }

        public ActionResult GetReview(string reviewId)
        {
            const string cachePrefix = "Review_";

            var cachedRating = _couchbaseClient != null ? _couchbaseClient.Get(cachePrefix + reviewId) : null;

            if (cachedRating != null)
                return Content(((DownloaderResponse)cachedRating).ResponseString);

            var bazaarVoiceQuery = new WebBazaarVoiceReviewsQuery
            {
                ApiVersion = ConfigurationManager.AppSettings["BazaarVoiceApiVersion"],
                PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                Filter = "id:" + reviewId
            };

            return Content(RequestReviews(reviewId, cachePrefix, bazaarVoiceQuery));
        }

        private string RequestReviews(string id, string cachePrefix, WebBazaarVoiceReviewsQuery query)
        {
            var uri = new Uri(query.ToString());
            var response = _downloader.GetResponse(uri);

            _couchbaseClient.Store(StoreMode.Set, cachePrefix + id, response, DateTime.Now.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["BazaarVoiceCacheExpiration"])));

            return response.ResponseString;
        }
    }
}
