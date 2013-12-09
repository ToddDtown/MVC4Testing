using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Models
{
    public class BazaarVoiceReviews
    {
        public List<BazaarVoiceReview> Results { get; set; }
        public int Limit { get; set; }
        public Product Product { get; set; }        
    }

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

    public class Product
    {
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductPageUrl { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
    }
}
