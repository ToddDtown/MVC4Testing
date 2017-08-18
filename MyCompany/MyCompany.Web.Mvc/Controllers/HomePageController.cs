﻿using MyCompany.Web.Mvc.Models.ModelBuilders;
using MyCompany.Web.Mvc.REST.Downloaders;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Controllers
{
    public class HomePageController : BaseController
    {
        private readonly IDownloader _downloader;

        public HomePageController(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public ActionResult Get()
        {
            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateHomeModel();

            var x = "";

            var y = "";

            var z = "";

            var changeAfterPR = "";

            return View("HomePage", model);
        }
    }
}
