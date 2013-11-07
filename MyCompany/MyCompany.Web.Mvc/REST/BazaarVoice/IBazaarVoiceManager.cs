namespace MyCompany.Web.Mvc.REST.BazaarVoice
{
    public interface IBazaarVoiceManager
    {
        string GetRatings(string productId, int limit = 10, string sort = "Rating:desc");
    }
}
