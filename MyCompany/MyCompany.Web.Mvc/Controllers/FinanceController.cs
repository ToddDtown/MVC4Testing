using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Helpers;
using MyCompany.Web.Mvc.Helpers.Serialization;
using MyCompany.Web.Mvc.Models;

namespace MyCompany.Web.Mvc.Controllers
{
    public class FinanceController : Controller
    {
        public ActionResult Get()
        {
            var model = new FinanceModel();

            model.CreditCardTypes = GetCreditCardTypes();
            model.TransactionType = "05";
            model.CreditCardNumber = "4111111111111111";
            model.CreditCardType = CardType.VISA;
            model.CreditCardExpiration = "1215";
            model.NameOnCard = "John Doe";
            model.Amount = 0;

            return View("Finance", model);
        }

        [HttpPost]
        public ActionResult DoPost(FormCollection formCollection)
        {
            var tran = new Turn5Transaction
            {
                TransactionType = formCollection["TransactionType"],
                CardType = formCollection["CreditCardType"],
                CardHoldersName = formCollection["NameOnCard"],
                CardNumber = formCollection["CreditCardNumber"],
                CardExpirationDate = formCollection["CreditCardExpiration"],
                DollarAmount = formCollection["Amount"]
            };

            var postContent = SerializationHelpers<Turn5Transaction>.ToXmlString(tran);

            var request = (HttpWebRequest)WebRequest.Create("http://local.turn5api.com/payment/posttransaction");

            request.Headers.Clear();

            var encoding = new ASCIIEncoding();
            var postData = postContent;
            var data = encoding.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            response.Close();
            
            var model = new FinanceModel
            {
                CreditCardTypes = GetCreditCardTypes(), 
                TransactionType = responseString
            };

            model.Response = responseString;

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

    public class Turn5Transaction
    {
        public string TransactionType { get; set; }
        public string CardType { get; set; }
        public string CardHoldersName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string DollarAmount { get; set; }
    }
}
