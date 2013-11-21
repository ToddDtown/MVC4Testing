using System.Web.Mvc;
using MyCompany.Mvc.Models;
using MyCompany.Web.Mvc.Models.ModelBuilders;

namespace MyCompany.Web.Mvc.Controllers
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
