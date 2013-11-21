using System.Collections.Generic;
using System.ComponentModel;

namespace MyCompany.Mvc.Models
{
    public class ProductsModel
    {
        [DefaultValue(0)]
        public int? Page { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
