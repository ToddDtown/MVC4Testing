using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Models
{
    public class FinanceModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string CreditCardNumber { get; set; }
        public DateTime CreditCardExpiration { get; set; }
        public CardType CreditCardType { get; set; }
        public IEnumerable<SelectListItem> CreditCardTypes { get; set; }
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
