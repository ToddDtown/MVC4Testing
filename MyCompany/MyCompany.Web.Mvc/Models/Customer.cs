﻿using System;

namespace MyCompany.Web.Mvc.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Generation { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
