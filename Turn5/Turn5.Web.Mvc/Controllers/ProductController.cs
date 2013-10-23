using System.Web.Mvc;
using Turn5.BusinessModel.Models;
using Turn5.BusinessModel.Models.Builders;

namespace Turn5.Web.Mvc.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Get()
        {
            var productsBuilder = new ProductModelBuilder();
            var model = productsBuilder.CreateModel();

            return View("Product", model);
        }

        [HttpPost]
        public ActionResult Update(int id, string productname, string productsku)
        {
            var model = new ProductModel
            {
                ProductId = id,
                ProductName = productname,
                ProductSKU = productsku
            };
            return View("Product", model);
        }
    }
}
