using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ModelFactory : IModelFactory
    {
        public BazaarVoiceModel CreateBazaarVoiceModel(string productId, string bvResponse)
        {
            return BazaarVoiceModelBuilder.CreateModel(productId, bvResponse);
        }

        public HomeModel CreateHomeModel()
        {
            return HomeModelBuilder.CreateModel();
        }

    }
}
