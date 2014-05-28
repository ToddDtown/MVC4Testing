using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MyCompany.Web.Mvc.Models;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private readonly IDownloader _downloader;

        public AjaxController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();
        }

        public JsonResult GetImages()
        {
            var model = CreateImageViewerObject();

            var json = JsonConvert.SerializeObject(model);

            return Json(json, JsonRequestBehavior.AllowGet);
        }



        private ImageViewer CreateImageViewerObject()
        {
            var imageViewer = new ImageViewer
            {
                ProductImages = new List<Image>
                {
                    new Image()
                    {
                        ImageUrl = "http://images.americanmuscle.com/ir/render/Turn5Render/37245",
                        AltText = string.Empty,
                        IsColor = true
                    },
                    new Image()
                    {
                        ImageUrl = "http://images.americanmuscle.com/ir/render/Turn5Render/37245_alt1",
                        AltText = string.Empty,
                        IsColor = true
                    },
                    new Image()
                    {
                        ImageUrl = "http://images.americanmuscle.com/ir/render/Turn5Render/37245_alt2",
                        AltText = string.Empty,
                        IsColor = false
                    },
                },
                YearColors = new List<YearColors>()
            };

            var yearColors = new YearColors
            {
                Year = "2005",
                Colors = new List<ColorInfo>()
                {
                    new ColorInfo
                    {
                        Name = "Sliver Metallic",
                        Code = string.Empty,
                        Hex = "#999999",
                        Rgb = "183,193,205"
                    }
                }
            };
            imageViewer.YearColors.Add(yearColors);


            yearColors = new YearColors
            {
                Year = "2006",
                Colors = new List<ColorInfo>()
                {
                    new ColorInfo
                    {
                        Name = "Vista Blue",
                        Code = string.Empty,
                        Hex = "#3366CC",
                        Rgb = "32,41,74"
                    }
                }
            };
            imageViewer.YearColors.Add(yearColors);

            return imageViewer;
        }

        






        public ActionResult GetReviews(string productId)
        {
            const string cachePrefix = "Reviews_";

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

        //public ActionResult GetReview(string reviewId)
        //{
        //    const string cachePrefix = "Review_";

        //    var cachedRating = _couchbaseClient != null ? _couchbaseClient.Get(cachePrefix + reviewId) : null;

        //    if (cachedRating != null)
        //        return Content(((DownloaderResponse)cachedRating).ResponseString);

        //    var bazaarVoiceQuery = new WebBazaarVoiceReviewsQuery
        //    {
        //        ApiVersion = ConfigurationManager.AppSettings["BazaarVoiceApiVersion"],
        //        PassKey = ConfigurationManager.AppSettings["BazaarVoiceKey"],
        //        Filter = "id:" + reviewId
        //    };

        //    return Content(RequestReviews(reviewId, cachePrefix, bazaarVoiceQuery));
        //}

        private string RequestReviews(string id, string cachePrefix, WebBazaarVoiceReviewsQuery query)
        {
            var uri = new Uri(query.ToString());
            var response = _downloader.GetResponse(uri);

            var reviews = JsonConvert.DeserializeObject<BazaarVoiceReviews>(response.ResponseString);

            //_couchbaseClient.Store(StoreMode.Set, cachePrefix + id, response, DateTime.Now.AddDays(Convert.ToDouble(ConfigurationManager.AppSettings["BazaarVoiceCacheExpiration"])));

            return response.ResponseString;
        }
    }
}
