using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ModelFactory : IModelFactory
    {
        public HomeModel CreateHomeModel()
        {
            return HomeModelBuilder.CreateModel();
        }

        public BazaarVoiceModel CreateBazaarVoiceModel()
        {
            return BazaarVoiceModelBuilder.CreateModel();
        }
    }
}
