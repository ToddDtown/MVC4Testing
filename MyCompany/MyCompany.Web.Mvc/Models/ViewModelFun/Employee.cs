namespace MyCompany.Web.Mvc.Models.ViewModelFun
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
