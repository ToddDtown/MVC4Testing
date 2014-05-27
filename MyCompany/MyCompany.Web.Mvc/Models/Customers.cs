using System.Collections.Generic;

namespace MyCompany.Web.Mvc.Models
{
    public class Customers
    {
        public Customers()
        {
            CustomerList = new List<Customer>();
        }

        public List<Customer> CustomerList { get; set; } 
    }
}
