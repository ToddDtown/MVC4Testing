﻿using System;
using System.Configuration;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Caching;
using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.Queries;
using MyCompany.Web.Mvc.REST.Downloaders;

namespace MyCompany.Web.Mvc.Controllers
{
    public class AjaxController : BaseController
    {
        private IDownloader _downloader;

        public AjaxController()
        {
            if (_downloader == null)
                _downloader = new HttpDownloader();
        }

        public JsonResult Get(string userkey)
        {
            var userInfo = CouchbaseManager.GetJson(userkey);

            var view = CouchbaseManager.GetView();
            //var view = CouchbaseManager.GetView<UserInfo>();

            return Json(userInfo);
        }

        public ActionResult GetRatings()
        {
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
