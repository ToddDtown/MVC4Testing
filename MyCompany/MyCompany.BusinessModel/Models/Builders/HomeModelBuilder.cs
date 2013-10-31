namespace MyCompany.BusinessModel.Models.Builders
{
    public class HomeModelBuilder : BaseModelBuilder
    {
        public HomeModelBuilder()
        {
            
        }

        public HomeModel CreateModel()
        {
            var model = new HomeModel();
            return model;
        }
    }
}
