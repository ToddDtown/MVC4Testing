using MyCompany.Mvc.Models;

namespace MyCompany.Web.Mvc.Models.ModelBuilders
{
    public class ProductModelBuilder
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
