using System.Configuration;

namespace MyCompany.Web.Mvc.Queries.Base
{
    public abstract class AbstractTurn5Query : WebBaseQuery
    {
        // Add base querystring properties that apply to all queries.
        // For Example maybe a debug mode querystring param to indicate whether to spit out debug information on the view.

        protected AbstractTurn5Query()
        {
            BasePath = ConfigurationManager.AppSettings["Turn5BasePath"];

            _debug = new WebQueryStringParameter<bool>{PropertyName = "debug"};
            Parameters.Add(_debug);
        }

        private readonly WebQueryStringParameter<bool> _debug;
        public bool Debug
        {
            get { return _debug.Value; }
            set { _debug.Value = value; }
        }
    }
}
