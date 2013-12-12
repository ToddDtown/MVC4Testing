using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Models
{
    public class FinanceModel
    {
        public string TransactionType { get; set; }
        public string NameOnCard { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiration { get; set; }
        public CardType CreditCardType { get; set; }
        public IEnumerable<SelectListItem> CreditCardTypes { get; set; }
        public decimal Amount { get; set; }
        public string Response { get; set; }
    }

    public enum CardType
    {
        VISA,
        MASTERCARD,
        AMEX,
        DINERSCLUB,
        DISCOVER
    }
}
