using MyCompany.Web.Mvc.Models;

namespace MyCompany.Web.Mvc.Services
{
    public interface IBazaarVoiceService
    {
        BazaarVoiceReviews GetReviews(string productId);
    }
}
