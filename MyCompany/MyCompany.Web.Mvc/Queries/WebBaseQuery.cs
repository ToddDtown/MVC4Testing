using System;
using System.Text;

namespace MyCompany.Web.Mvc.Queries
{
    public abstract class WebBaseQuery
    {
        private string _basePath;

        public abstract string UriTemplate { get; }

        public WebQueryStringParameters Parameters { get; set; }

        protected WebBaseQuery()
        {
            Parameters = new WebQueryStringParameters();
        }

        public virtual string BasePath
        {
            get { return _basePath; }
            set
            {
                if (!string.IsNullOrEmpty(_basePath) && value.Contains("?"))
                {
                    throw new Exception("Base path cannot contain querystring parameters.");
                }
                _basePath = value;
            }
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            if (!string.IsNullOrEmpty(_basePath) && _basePath.EndsWith("/"))
                _basePath = _basePath.Substring(0, _basePath.Length - 1);
            
            output.Append(_basePath);
            output.Append(UriTemplate);

            var count = 0;
            object propName = null;
            object propValue = null;
            foreach (var parameter in Parameters.ParameterList())
            {
                if (parameter.Value is WebQueryStringParameter<string>)
                {
                    propName = ((WebQueryStringParameter<string>)parameter.Value).PropertyName;
                    propValue = ((WebQueryStringParameter<string>)parameter.Value).Value;
                }
                else if (parameter.Value is WebQueryStringParameter<int>)
                {
                    propName = ((WebQueryStringParameter<int>)parameter.Value).PropertyName;
                    propValue = ((WebQueryStringParameter<int>)parameter.Value).Value;
                }
                else if (parameter.Value is WebQueryStringParameter<bool>)
                {
                    propName = ((WebQueryStringParameter<bool>)parameter.Value).PropertyName;
                    propValue = ((WebQueryStringParameter<bool>)parameter.Value).Value;
                }

                output.Replace(parameter.Key, (count > 0 ? "&" : string.Empty) + propName + "=" + propValue);
                count++;
            }

            return output.ToString();
        }
    }
}
