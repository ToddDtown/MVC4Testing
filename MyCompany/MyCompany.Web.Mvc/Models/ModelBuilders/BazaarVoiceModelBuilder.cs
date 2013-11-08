namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public static class BazaarVoiceModelBuilder
    {
        public static BazaarVoiceModel CreateModel(string productId, string bvResponse)
        {
            var model = new BazaarVoiceModel
            {
                ProductId = productId,
                RatingsResponse = bvResponse
            };

            return model;
        }
    }
}
