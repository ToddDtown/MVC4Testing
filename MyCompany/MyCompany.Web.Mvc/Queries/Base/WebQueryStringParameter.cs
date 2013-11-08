namespace MyCompany.Web.Mvc.Queries.Base
{
    public class WebQueryStringParameter<T> : IUrlParameter
    {
        public virtual T Value { get; set; }
        public string PropertyName { get; set; }

        public WebQueryStringParameter() : this(string.Empty)
        {

        } 

        public WebQueryStringParameter(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string GetValue
        {
            get
            {
                return Value.ToString();
            }
        }
    }
}
