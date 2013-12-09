using System.Configuration;
using MyCompany.Web.Mvc.Queries.Base;

namespace MyCompany.Web.Mvc.Queries
{
    public class WebFirstDataQuery : AbstractTurn5Query
    {
        public override string UriTemplate
        {
            get { return "/v12/?[amount][authorization_num][transaction_type][transaction_tag]"; }
        }

        public WebFirstDataQuery()
        {
            //api.globalgatewaye4.firstdata.com/transaction?amount=15.75&authorization_num=ET4653&transaction_tag=902006933&transaction_type=34/902010341

            BasePath = ConfigurationManager.AppSettings["FirstDataBasePath"];

            _amount = new WebQueryStringParameter<decimal>("amount");

            Parameters.Add(_amount);
        }

        private WebQueryStringParameter<decimal> _amount;
        public decimal Amount
        {
            get { return _amount.Value; }
            set { _amount.Value = value; }
        }
    }
}
