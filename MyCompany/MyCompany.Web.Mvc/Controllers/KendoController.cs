using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MyCompany.Web.Mvc.Models;

namespace Kendo.Mvc.Examples.Controllers
{
    public class KendoController : Controller
    {
        public ActionResult Get()
        {
            return View("Kendo");
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            var customers = new Customers();

            for (var i = 1; i <= 40; i++)
            {
                var customer = new Customer
                {
                    Id = i,
                    FirstName = "John",
                    LastName = "Doe",
                    Generation = "1973-1999",
                    RegistrationDate = new DateTime(2012, 1, 20)
                };
                customers.CustomerList.Add(customer);
            }

            return Json(customers.CustomerList.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> customers)
        {
            var results = new List<Customer>();

            if (customers != null && ModelState.IsValid)
            {
                foreach (var customer in customers)
                {
                    //productService.Create(product);
                    results.Add(customer);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> products)
        {
            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //productService.Update(product);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> products)
        {
            if (products.Any())
            {
                foreach (var product in products)
                {
                    //productService.Destroy(product);
                }
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }
    }
}