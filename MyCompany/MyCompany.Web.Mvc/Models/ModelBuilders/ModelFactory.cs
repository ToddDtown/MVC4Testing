namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ModelFactory : IModelFactory
    {
        public BazaarVoiceModel CreateBazaarVoiceModel(string bvResponse)
        {
            return new BazaarVoiceModelBuilder().CreateModel(bvResponse);
        }
    }
}
