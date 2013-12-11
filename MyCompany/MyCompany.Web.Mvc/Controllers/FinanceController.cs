using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml.Serialization;
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
                DollarAmount = Convert.ToDecimal(formCollection["Amount"])
            };

            var postContent = SerializationHelpers<Turn5Transaction>.ToXmlString(tran);

            var uri = new Uri("http://local.turn5api.com/api/createtransaction");
            var request = HttpWebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/xml";
            request.ContentLength = postContent.Length;

            var dataStream = request.GetRequestStream();
            var bytes = postContent.ToBytesFromString();
            dataStream.Write(bytes, 0, bytes.Length);
            dataStream.Close();

            var response = request.GetResponse();

            var model = new FinanceModel();
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

    [XmlRoot("Transaction")]
    public class Turn5Transaction
    {
        [XmlElement("Transaction_Type")]
        public string TransactionType { get; set; }

        [XmlElement("CardType")]
        public string CardType { get; set; }

        [XmlElement("CardHoldersName")]
        public string CardHoldersName { get; set; }

        [XmlElement("Card_Number")]
        public string CardNumber { get; set; }

        [XmlElement("Expiry_Date")]
        public string CardExpirationDate { get; set; }

        [XmlElement("DollarAmount")]
        public decimal DollarAmount { get; set; }
    }
}
