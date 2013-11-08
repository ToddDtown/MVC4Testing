using System;
using System.Text;

namespace MyCompany.Web.Mvc.Queries.Base
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

            var templateArray = UriTemplate.Split('[');
            for (var i = 0; i <= templateArray.Length - 1; i++)
            {
                if (templateArray[i].Contains("?")) continue;

                var parameter = Parameters[templateArray[i].Substring(0, templateArray[i].Length - 1)];

                if (parameter != null)
                {
                    output.Replace("[" + parameter.PropertyName + "]", ("&" + parameter.PropertyName + "=" + parameter.GetValue));
                }
            }
            output.Replace("?&", "?");

            return output.ToString();
        }
    }
}
