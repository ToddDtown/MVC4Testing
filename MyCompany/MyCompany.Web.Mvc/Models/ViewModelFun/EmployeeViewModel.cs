using System.Collections.Generic;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Models.ViewModelFun
{
    public class EmployeeViewModel
    {
        public Employee Employee { get; set; }
        public List<Employee> Employees { get; set; } 
        public List<EmployeeRole> EmployeeRoles { get; set; } 

        public SelectList EmployeeSelectList { get; set; }
        public SelectList EmployeeRoleSelectList { get; set; }
    }
}
