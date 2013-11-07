using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ProductModelBuilder : BaseModelBuilder
    {
        public ProductModel CreateModel()
        {
            var model = new ProductModel
                {
                    ProductId = 1, 
                    ProductName = "Carborator", 
                    ProductSKU = "ABC-123"
                };
            return model;
        }
    }
}
