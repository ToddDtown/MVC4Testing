namespace MyCompany.Web.Mvc.Queries.Base
{
    public interface IUrlParameter
    {
        string PropertyName { get; set; }
        string GetValue { get; }
    }
}
