using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Helpers;
using MyCompany.Web.Mvc.Models;

namespace MyCompany.Web.Mvc.Controllers
{
    public class FinanceController : Controller
    {
        public ActionResult Get()
        {
            var model = new FinanceModel();

            model.CreditCardTypes = GetCreditCardTypes();

            return View("Finance", model);
        }

        [HttpPost]
        public ActionResult DoInfo(FormCollection formCollection)
        {
            var model = new FinanceModel();

            model.FirstName = formCollection["FirstName"];
            model.LastName = formCollection["LastName"];
            model.Address1 = formCollection["Address1"];
            model.Address2 = formCollection["Address2"];
            model.City = formCollection["City"];
            model.State = formCollection["State"];
            model.Zip = formCollection["Zip"];
            model.CreditCardNumber = formCollection["CreditCardNumber"];
            model.CreditCardExpiration = Convert.ToDateTime(formCollection["CreditCardExpiration"]);
            model.CreditCardTypes = GetCreditCardTypes();
            model.CreditCardType = formCollection["CreditCardType"].ToCardType();
            
            return View("Finance", model);
        }

        public IEnumerable<SelectListItem> GetCreditCardTypes()
        {
            return new List<SelectListItem>
                           {
                               new SelectListItem {Text = "Visa", Value = "VISA"},
                               new SelectListItem {Text = "Mastercard", Value = "MASTERCARD"},
                               new SelectListItem {Text = "American Express", Value = "AMEX"},
                               new SelectListItem {Text = "Diners Club", Value = "DINERSCLUB"},
                               new SelectListItem {Text = "Discover", Value = "DISCOVER"},
                           };
        }
    }
}
