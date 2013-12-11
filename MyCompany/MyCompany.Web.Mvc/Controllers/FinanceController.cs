using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
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
            var request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/xml; charset=utf-16";
            request.ContentLength = postContent.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(postContent);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var response = request.GetResponse();





            var receiveStream = response.GetResponseStream();
            var encode = Encoding.GetEncoding("utf-8");
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            var readStream = new StreamReader(receiveStream, encode);
            var read = new Char[256];
            // Reads 256 characters at a time.     
            var count = readStream.Read(read, 0, 256);
            var sb = new StringBuilder();
            while (count > 0)
            {
                // Dumps the 256 characters on a string and displays the string to the console.
                var str = new String(read, 0, count);
                sb.Append(str);
                count = readStream.Read(read, 0, 256);
            }
            response.Close();
            readStream.Close();
            

            var model = new FinanceModel();
            model.CreditCardTypes = GetCreditCardTypes();
            model.TransactionType = sb.ToString();
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
        public decimal DollarAmount { get; set; }
    }
}
