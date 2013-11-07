namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class BazaarVoiceModelBuilder : BaseModelBuilder
    {
        public BazaarVoiceModel CreateModel(string bvResponse)
        {
            var model = new BazaarVoiceModel
            {
                RatingsResponse = bvResponse
            };

            return model;
        }
    }
}
