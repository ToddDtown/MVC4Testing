using System.Configuration;
using MyCompany.Web.Mvc.Queries.Base;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebFirstDataQuery : AbstractTurn5Query
    {
        public override string UriTemplate
        {
            get { return "/v12/?[]"; }
        }

        public WebFirstDataQuery()
        {
            BasePath = ConfigurationManager.AppSettings["FirstDataBasePath"];

            _apiVersion = new WebQueryStringParameter<string>("apiversion");

            Parameters.Add(_apiVersion);
        }

        private WebQueryStringParameter<string> _apiVersion;
        public string ApiVersion
        {
            get { return _apiVersion.Value; }
            set { _apiVersion.Value = value; }
        }
    }
}
