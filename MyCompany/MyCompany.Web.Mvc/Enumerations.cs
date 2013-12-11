namespace MyCompany.Web.Mvc
{
    public enum HttpRequestMethod
    {
        GET,
        POST
    }

    public enum TransactionType
    {
        PURCHASE,
        PREAUTH,
        PREAUTHONLY,
        PREAUTHCOMPLETION,
        FORCEDPOST,
        REFUND,
        VOID
    }

    public enum CreditCardType
    {
        VISA,
        MASTERCARD,
        DISCOVER,
        AMEX,
        DINERSCLUB
    }
}
