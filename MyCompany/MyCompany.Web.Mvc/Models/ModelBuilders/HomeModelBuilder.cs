using MyCompany.Mvc.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders 
{
    public static class HomeModelBuilder
    {
        public static HomeModel CreateModel()
        {
            var model = new HomeModel
            {
                SKU = "headlight-black-proj-2010"
            };
            return model;
        }
    }
}
