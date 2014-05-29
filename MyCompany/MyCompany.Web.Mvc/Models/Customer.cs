using System;

namespace MyCompany.Web.Mvc.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Generation { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public int Salary { get; set; }
    }

    public class Generation
    {
        public int GenerationId { get; set; }
        public string GenerationName { get; set; }
    }
}
