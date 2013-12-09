using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Couchbase;
using Enyim.Caching.Memcached;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            BazaarVoiceReviews reviews;

            var filter = "productid:" + productId;
            //var filter = "productid:" + productId + "&filter=Rating:lt:5";
            //var filter = "productid:" + productId + "&filter=Rating:lt:5&filter=HasComments:false";

            var include = "products";

            var query = new WebBazaarVoiceReviewsQuery
            {
                ApiVersion = ConfigurationManager.AppSettings["BazaarVoiceApiVersion"],
                PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
                Filter = filter,
                Include = include,
                HasComments = true,
                Sort = ConfigurationManager.AppSettings["BazaarVoiceResultSort"],
                Limit = Convert.ToInt32(ConfigurationManager.AppSettings["BazaarVoiceResultLimit"])
            };

            var cacheKey = GetKey(query.ToString());
            var cachedReviews = _couchbaseClient != null ? _couchbaseClient.Get(cacheKey) : null;

            if (cachedReviews != null)
            {
                reviews = JsonConvert.DeserializeObject<BazaarVoiceReviews>(((DownloaderResponse)cachedReviews).ResponseString);
            }
            else
            {
                var uri = new Uri(query.ToString());
                var response = _downloader.GetResponse(uri);

                reviews = JsonConvert.DeserializeObject<BazaarVoiceReviews>(response.ResponseString);

                var obj = JObject.Parse(response.ResponseString);
                if (obj["Includes"] != null && obj["Includes"]["Products"] != null && obj["Includes"]["Products"][productId] != null)
                {
                    reviews.Product = new Product();
                    if (obj["Includes"]["Products"][productId]["Brand"] != null)
                    {
                        reviews.Product.BrandId = (string) obj["Includes"]["Products"][productId]["Brand"]["Id"];
                        reviews.Product.BrandName = (string) obj["Includes"]["Products"][productId]["Brand"]["Name"];
                    }
                    reviews.Product.Name = (string) obj["Includes"]["Products"][productId]["Name"];
                    reviews.Product.ProductPageUrl = (string) obj["Includes"]["Products"][productId]["ProductPageUrl"];
                    reviews.Product.ImageUrl = (string) obj["Includes"]["Products"][productId]["ImageUrl"];
                    reviews.Product.CategoryId = (string) obj["Includes"]["Products"][productId]["CategoryId"];
                }

                _couchbaseClient.Store(StoreMode.Set, cacheKey, response.ResponseString, DateTime.Now.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["BazaarVoiceCacheExpiration"])));
            }

            return PartialView("_BazaarVoice", reviews);
        }

        public string GetKey(string keyInput)
        {
            var sb = new StringBuilder(32);
            foreach (var num in new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(keyInput)))
                sb.Append(num.ToString("x2"));
            return sb.ToString();
        }
    }
}
