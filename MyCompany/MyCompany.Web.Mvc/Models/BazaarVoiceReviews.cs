using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCompany.Web.Mvc.Models
{
    public class BazaarVoiceReviews
    {
        public List<BazaarVoiceReview> Results { get; set; }
        public int Limit { get; set; }
        //public Includes Includes { get; set; }
    }

    //public class Includes
    //{
    //    public Products Products { get; set; }
    //}

    //public class Products
    //{
    //    [JsonProperty(PropertyName = "headlight-black-proj-2010")]
    //    public object Child { get; set; }
    //}

    public class BazaarVoiceReview
    {
        public long Id { get; set; }
        public string UserNickname { get; set; }
        public int Rating { get; set; }
        public int RatingRange { get; set; }
        public string ReviewText { get; set; }
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string CampaignId { get; set; }
        public string SubmissionTime { get; set; }
        public string LastModificationTime { get; set; }
        public string ModerationStatus { get; set; }
    }
}
