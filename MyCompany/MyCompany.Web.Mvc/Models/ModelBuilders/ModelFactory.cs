using MyCompany.Mvc.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ModelFactory : IModelFactory
    {
        public HomeModel CreateHomeModel()
        {
            return HomeModelBuilder.CreateModel();
        }
    }
}
