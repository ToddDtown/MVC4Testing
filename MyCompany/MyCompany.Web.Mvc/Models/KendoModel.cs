using System.Collections.Generic;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Models
{
    public class KendoModel
    {
        public KendoModel()
        {
            Generations = new List<SelectListItem>();
        }

        public string GridType { get; set; }
        public IEnumerable<SelectListItem> Generations { get; set; }
    }
}
