namespace MyCompany.Web.Mvc.Queries
{
    public class WebQueryStringParameter<T>
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
    }
}
