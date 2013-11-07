using System.Configuration;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebBazaarVoiceQuery : AbstractTurn5Query
    {
        public override string UriTemplate
        {
            get { return "/data/reviews.json?[apiversion][passkey][filter][productid][hascomments][sort][limit]"; }
        }

        public WebBazaarVoiceQuery()
        {
            BasePath = ConfigurationManager.AppSettings["BazaarVoiceBasePath"];

            _apiVersion = new WebQueryStringParameter<string>("apiversion");
            _passKey = new WebQueryStringParameter<string>("passkey");
            _filter = new WebQueryStringParameter<string>("filter");
            _productId = new WebQueryStringParameter<string>("productid");
            _hasComments = new WebQueryStringParameter<bool>("hascomments");
            _sort = new WebQueryStringParameter<string>("sort");
            _limit = new WebQueryStringParameter<int>("limit");

            Parameters.Add(_apiVersion);
            Parameters.Add(_passKey);
            Parameters.Add(_filter);
            Parameters.Add(_productId);
            Parameters.Add(_hasComments);
            Parameters.Add(_sort);
            Parameters.Add(_limit);
        }

        private WebQueryStringParameter<string> _apiVersion;
        public string ApiVersion
        {
            get { return _apiVersion.Value; }
            set { _apiVersion.Value = value; }
        }

        private WebQueryStringParameter<string> _passKey;
        public string PassKey
        {
            get { return _passKey.Value; }
            set { _passKey.Value = value; }
        }

        private WebQueryStringParameter<string> _filter;
        public string Filter
        {
            get { return _filter.Value; }
            set { _filter.Value = value; }
        }

        private WebQueryStringParameter<string> _productId;
        public string ProductId
        {
            get { return _productId.Value; }
            set { _productId.Value = value; }
        }

        private WebQueryStringParameter<bool> _hasComments;
        public bool HasComments
        {
            get { return _hasComments.Value; }
            set { _hasComments.Value = value; }
        }

        private WebQueryStringParameter<string> _sort;
        public string Sort
        {
            get { return _sort.Value; }
            set { _sort.Value = value; }
        }

        private WebQueryStringParameter<int> _limit;
        public int Limit
        {
            get { return _limit.Value; }
            set { _limit.Value = value; }
        }
    }
}
