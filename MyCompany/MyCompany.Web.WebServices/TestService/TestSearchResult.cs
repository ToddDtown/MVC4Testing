using System.Collections.Generic;
using MyCompany.BusinessModel.Models;

namespace MyCompany.Web.WebServices
{
    public class TestSearchResult
    {
        public int SearchId { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
