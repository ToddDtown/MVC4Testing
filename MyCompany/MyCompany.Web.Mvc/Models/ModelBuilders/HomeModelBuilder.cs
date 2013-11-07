using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders 
{
    public class HomeModelBuilder : BaseModelBuilder
    {
        public HomeModel CreateModel()
        {
            var model = new HomeModel();
            return model;
        }
    }
}
