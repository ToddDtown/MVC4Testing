using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ModelFactory : IModelFactory
    {
        public BazaarVoiceModel CreateBazaarVoiceModel(string bvResponse)
        {
            return BazaarVoiceModelBuilder.CreateModel(bvResponse);
        }

        public HomeModel CreateHomeModel()
        {
            return HomeModelBuilder.CreateModel();
        }

    }
}
