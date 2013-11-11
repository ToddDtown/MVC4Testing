using System.Configuration;
using MyCompany.Web.Mvc.Queries.Base;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebBazaarVoiceReviewSubmitQuery : AbstractTurn5Query
    {
        public override string UriTemplate
        {
            get { return "/data/submitreview.json?[apiversion][passkey][productid][action][rating][reviewtext][title][usernickname]"; }
        }

        public WebBazaarVoiceReviewSubmitQuery()
        {
            BasePath = ConfigurationManager.AppSettings["BazaarVoiceBasePath"];

            _apiVersion = new WebQueryStringParameter<string>("apiversion");
            _passKey = new WebQueryStringParameter<string>("passkey");
            _productId = new WebQueryStringParameter<string>("productid");
            _action = new WebQueryStringParameter<string>("action");
            _rating = new WebQueryStringParameter<string>("rating");
            _reviewText = new WebQueryStringParameter<string>("reviewtext");
            _title = new WebQueryStringParameter<string>("title");
            _userNickname = new WebQueryStringParameter<string>("usernickname");

            Parameters.Add(_apiVersion);
            Parameters.Add(_passKey);
            Parameters.Add(_productId);
            Parameters.Add(_action);
            Parameters.Add(_rating);
            Parameters.Add(_reviewText);
            Parameters.Add(_title);
            Parameters.Add(_userNickname);
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

        private WebQueryStringParameter<string> _productId;
        public string ProductId
        {
            get { return _productId.Value; }
            set { _productId.Value = value; }
        }

        private WebQueryStringParameter<string> _action;
        public string Action
        {
            get { return _action.Value; }
            set { _action.Value = value; }
        }

        private WebQueryStringParameter<string> _rating;
        public string Rating
        {
            get { return _rating.Value; }
            set { _rating.Value = value; }
        }

        private WebQueryStringParameter<string> _reviewText;
        public string ReviewText
        {
            get { return _reviewText.Value; }
            set { _reviewText.Value = value; }
        }

        private WebQueryStringParameter<string> _title;
        public string Title
        {
            get { return _title.Value; }
            set { _title.Value = value; }
        }

        private WebQueryStringParameter<string> _userNickname;
        public string UserNickname
        {
            get { return _userNickname.Value; }
            set { _userNickname.Value = value; }
        }
    }
}
