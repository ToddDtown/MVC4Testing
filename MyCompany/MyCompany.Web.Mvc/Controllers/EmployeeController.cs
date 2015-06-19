using System.Collections.Generic;
using System.Web.Mvc;
using MyCompany.Web.Mvc.Models.ViewModelFun;

namespace MyCompany.Web.Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            var employee = new Employee {Id = 1, FirstName = "John", LastName = "Doe"};
            var role = new EmployeeRole {Id = 1, Name = "President"};
            employee.EmployeeRole = role;

            var employees = new List<Employee>
            {
                new Employee {Id = 1, FirstName = "John", LastName = "Doe"},
                new Employee {Id = 2, FirstName = "Dexter", LastName = "Morgan"},
                new Employee {Id = 3, FirstName = "Lex", LastName = "Luther"},
                new Employee {Id = 4, FirstName = "Ron", LastName = "Jeremy"}
            };
            var roles = new List<EmployeeRole>
            {
                new EmployeeRole {Id = 1, Name = "President"},
                new EmployeeRole {Id = 2, Name = "Vice President"},
                new EmployeeRole {Id = 3, Name = "CTO"},
                new EmployeeRole {Id = 4, Name = "CFO"}
            };

            var employeeSelectList = new SelectList(employees, "Id", "FullName");
            var employeeRoleSelectList = new SelectList(roles, "Id", "Name");

            var model = new EmployeeViewModel
            {
                Employee = employee,
                Employees = employees,
                EmployeeRoles = roles,
                EmployeeSelectList = employeeSelectList,
                EmployeeRoleSelectList = employeeRoleSelectList
            };

            return View("Employee", model);
        }

        [HttpPost]
        public ActionResult SaveEmployee(EmployeeViewModel model)
        {
            var employees = new List<Employee>
            {
                new Employee {Id = 1, FirstName = "John", LastName = "Doe"},
                new Employee {Id = 2, FirstName = "Dexter", LastName = "Morgan"},
                new Employee {Id = 3, FirstName = "Lex", LastName = "Luther"},
                new Employee {Id = 4, FirstName = "Ron", LastName = "Jeremy"}
            };
            var roles = new List<EmployeeRole>
            {
                new EmployeeRole {Id = 1, Name = "President"},
                new EmployeeRole {Id = 2, Name = "Vice President"},
                new EmployeeRole {Id = 3, Name = "CTO"},
                new EmployeeRole {Id = 4, Name = "CFO"}
            };

            var employeeSelectList = new SelectList(employees, "Id", "FullName");
            var employeeRoleSelectList = new SelectList(roles, "Id", "Name");

            model.Employees = employees;
            model.EmployeeRoles = roles;

            model.EmployeeSelectList = employeeSelectList;
            model.EmployeeRoleSelectList = employeeRoleSelectList;

            return View("Employee", model);
        }
    }
}
