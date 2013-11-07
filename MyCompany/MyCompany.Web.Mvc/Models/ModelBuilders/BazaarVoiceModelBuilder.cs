namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public static class BazaarVoiceModelBuilder
    {
        public static BazaarVoiceModel CreateModel(string bvResponse)
        {
            var model = new BazaarVoiceModel
            {
                RatingsResponse = bvResponse
            };

            return model;
        }
    }
}
