using System.Collections.Generic;
using Turn5.BusinessModel.Models;

namespace TestItOut.WebServices
{
    public class TestSearchResult
    {
        public int SearchId { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
