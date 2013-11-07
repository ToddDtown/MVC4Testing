using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders 
{
    public static class HomeModelBuilder
    {
        public static HomeModel CreateModel()
        {
            var model = new HomeModel();
            return model;
        }
    }
}
