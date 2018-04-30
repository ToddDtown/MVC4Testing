using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using TestSite.Web.UI.Models;

namespace TestSite.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeModel();
            return View("Index", model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetProducts()
        {
            var productGrid = new ProductGrid
            {
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Id = 1,
                        Name = "Raxiom Headlights",
                        Description = "Raxiom headlights will change your life.  These headlights kick ass.",
                        Price = new decimal(599.99)
                    },
                    new ProductModel
                    {
                        Id = 1,
                        Name = "Catback Exhausts",
                        Description = "Upgrade your 2011-2014 GT exhaust to a Pypes Pype-Bomb Super System Cat-Back Exhaust with Black Tips and yield an instant increase in horsepower at the rear wheels.",
                        Price = new decimal(881.78)
                    },
                    new ProductModel
                    {
                        Id = 1,
                        Name = "OPR Front Brake Caliper",
                        Description = "Replace those old worn out or damaged front brake calipers with a set of re-manufactured OPR calipers to improve the stopping performance of your S197 Mustang.",
                        Price = new decimal(128.99)
                    }
                }
            };

            return View("_Products", productGrid);
        }

        [HttpPost]
        public ActionResult GetData(string searchTerm)
        {
            var model = new HomeModel();

            var mongoClient = ConnectToMongo();

            return View("Index", model);
        }

        public MongoClient ConnectToMongo()
        {
            const string connectionString = @"mongodb://testcosmodb2525:IVdaZpVuik2vGEQkew4CNUS7UKiaSNlbHpuXwSSuyFthxvOANpuAvJBs2RD40b0FW7bIQVGcH3Xiymjtx0Ldmw==@testcosmodb2525.documents.azure.com:10255/?ssl=true&replicaSet=globaldb";
            var settings = MongoClientSettings.FromUrl(
                new MongoUrl(connectionString)
            );

            settings.SslSettings = new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            return new MongoClient(settings);
        }
    }
}