namespace MyCompany.Web.Mvc.Queries
{
    public interface IUrlParameter
    {
        string PropertyName { get; set; }
        string GetValue { get; }
    }
}
