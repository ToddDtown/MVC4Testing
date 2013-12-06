using MyCompany.Web.Mvc.Models;

namespace MyCompany.Mvc.Models
{
    public class HomeModel : BaseModel
    {
        public string SKU { get; set; }
        public BazaarVoiceReviews Reviews { get; set; }
    }
}
