namespace MyCompany.Web.Mvc.Queries
{
    public class WebHomePageQuery : AbstractTurn5Query
    {
        public override string UriTemplate
        {
            get { return "/homepage?[test1][test2][test3]"; }
        }

        public WebHomePageQuery()
        {
            _test1 = new WebQueryStringParameter<string>("test1");
            _test2 = new WebQueryStringParameter<string>("test2");
            _test3 = new WebQueryStringParameter<string>("test3");
            _test4 = new WebQueryStringParameter<int>("test4");

            Parameters.Add(_test1);
            Parameters.Add(_test2);
            Parameters.Add(_test3);
            Parameters.Add(_test4);
        }

        private readonly WebQueryStringParameter<string> _test1;
        public string Test1
        {
            get { return _test1.Value; }
            set { _test1.Value = value; }
        }

        private readonly WebQueryStringParameter<string> _test2;
        public string Test2
        {
            get { return _test2.Value; }
            set { _test2.Value = value; }
        }

        private readonly WebQueryStringParameter<string> _test3;
        public string Test3
        {
            get { return _test3.Value; }
            set { _test3.Value = value; }
        }

        private readonly WebQueryStringParameter<int> _test4;
        public int Test4
        {
            get { return _test4.Value; }
            set { _test4.Value = value; }
        }
    }
}
