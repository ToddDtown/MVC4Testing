﻿using System;
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
            var model = new KendoModel
            {
                GridType = grid, 
                Generations = GetGenerations()
            };

            return View("Kendo", null, model);
        }

        public List<Generation> GetGenerations()
        {
            var generations = new List<Generation>();

            var gen = new Generation {GenerationId = 1, GenerationName = "2010-2014"};
            generations.Add(gen);

            gen = new Generation {GenerationId = 1, GenerationName = "2005-2009"};
            generations.Add(gen);

            gen = new Generation {GenerationId = 1, GenerationName = "1999-2004"};
            generations.Add(gen);

            gen = new Generation {GenerationId = 1, GenerationName = "1994-1998"};
            generations.Add(gen);

            gen = new Generation {GenerationId = 1, GenerationName = "1979-1993"};
            generations.Add(gen);

            ViewData["GenerationList"] = generations;

            return generations;
        } 

        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            var customers = new Customers();

            var rnd = new Random();

            for (var i = 1; i <= 300; i++)
            {
                var customer = new Customer
                {
                    Id = i,
                    FirstName = "John",
                    LastName = "Doe",
                    Generation = GetGeneration(rnd.Next(0, 4)),
                    RegistrationDate = new DateTime(2014, 1, 1),
                    IsActive = (i != 2 && i != 4),
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


        //######################################################
        // Edit Batch with Dropdown
        //######################################################

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> customers)
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
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> products)
        {
            var results = new List<Customer>();

            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //productService.Create(product);

                    results.Add(product);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Customer> products)
        {
            foreach (var product in products)
            {
                //productService.Destroy(product);
            }

            return Json(products.ToDataSourceResult(request, ModelState));
        }

        private string GetGeneration(int index)
        {
            var generations = new List<string> {"2010-2014", "2005-2009", "1999-2004", "1994-1998", "1979-1993"};
            return generations[index];
        }
    }
}