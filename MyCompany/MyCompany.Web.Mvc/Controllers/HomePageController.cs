using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
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

        public HomePageController(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public ActionResult Get()
        {
            var modelFactory = new ModelFactory();
            var model = modelFactory.CreateHomeModel();

            return View("HomePage", model);
        }
    }
}
