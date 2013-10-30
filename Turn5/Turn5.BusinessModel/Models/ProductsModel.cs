using System.Collections.Generic;
using System.ComponentModel;

namespace Turn5.BusinessModel.Models
{
    public class ProductsModel
    {
        [DefaultValue(0)]
        public int? Page { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
