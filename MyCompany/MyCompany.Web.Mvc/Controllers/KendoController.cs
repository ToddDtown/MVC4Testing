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
        public ActionResult Get(string grid = null)
        {
            return View("Kendo", null, grid);
        }

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            var customers = new Customers();

            var rnd = new Random();

            for (var i = 1; i <= 40; i++)
            {
                var customer = new Customer
                {
                    Id = i,
                    FirstName = "John",
                    LastName = "Doe",
                    Generation = "1973-1999",
                    RegistrationDate = new DateTime(rnd.Next(1998, 2014), rnd.Next(1, 12), rnd.Next(1, 30)),
                    IsActive = true,
                    Salary = new Random().Next(60000, 150000)
                };
                customers.CustomerList.Add(customer);
            }

            return Json(customers.CustomerList.ToDataSourceResult(request));
        }

        //######################################################
        // Edit Batch
        //######################################################

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]List<Customer> customers)
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
        public ActionResult Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]List<Customer> customers)
        {
            if (customers != null && ModelState.IsValid)
            {
                foreach (var product in customers)
                {
                    //productService.Update(product);
                }
            }

            return Json(customers.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]List<Customer> customers)
        {
            if (customers.Any())
            {
                foreach (var product in customers)
                {
                    //productService.Destroy(product);
                }
            }

            return Json(customers.ToDataSourceResult(request, ModelState));
        }

        
        
        //######################################################
        // Edit Batch
        //######################################################

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            if (customer != null && ModelState.IsValid)
            {
                //productService.Create(product);
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            if (customer != null && ModelState.IsValid)
            {
                //productService.Update(product);
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Customer customer)
        {
            if (customer != null)
            {
                //productService.Destroy(product);
            }

            return Json(new[] { customer }.ToDataSourceResult(request, ModelState));
        }

        
    }
}