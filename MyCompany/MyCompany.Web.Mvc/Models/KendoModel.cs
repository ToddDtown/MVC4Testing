using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Models
{
    public class KendoModel
    {
        public KendoModel()
        {
            Generations = new List<Generation>();
        }

        public string GridType { get; set; }
        public IEnumerable<Generation> Generations { get; set; }
    }
}
