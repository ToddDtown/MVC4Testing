using MyCompany.Web.Mvc.Models;

namespace MyCompany.Web.Mvc.Helpers
{
    public static class PaymentHelpers
    {
        public static CardType ToCardType(this string input)
        {
            switch (input)
            {
                case "VISA":
                    return CardType.VISA;
                case "MASTERCARD":
                    return CardType.MASTERCARD;
                case "AMEX":
                    return CardType.AMEX;
                case "DINERSCLUB":
                    return CardType.DINERSCLUB;
                case "DISCOVER":
                    return CardType.DISCOVER;
            }
            return CardType.VISA;
        }
    }
}
