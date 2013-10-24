using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Routing;
using Moq;
using Turn5.Web.UI;

namespace Turn5.Web.UnitTests.HelperSetup
{
    public static class MvcTestHelpers
    {
        public static XElement ToXElement(this MvcHtmlString mvcHtmlString)
        {
            return XElement.Parse(mvcHtmlString.ToHtmlString());
        }

        public static HtmlHelper GetHtmlHelper(ViewContext viewContext = null)
        {
            if (RouteTable.Routes.Count == 0) RouteConfig.RegisterRoutes(RouteTable.Routes);

            var httpContextBase = GetHttpContext();

            var controllerContext = new ControllerContext(httpContextBase,
                new RouteData(), new Mock<ControllerBase>().Object);

            var viewData = new ViewDataDictionary();

            viewContext = viewContext ?? new ViewContext(controllerContext,
                new Mock<IView>().Object, viewData,
                new TempDataDictionary(), new Mock<TextWriter>().Object);

            viewContext.RequestContext = new RequestContext(httpContextBase, new RouteData());

            var mockViewDataContainer = new Mock<IViewDataContainer>();
            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            return new HtmlHelper(viewContext, mockViewDataContainer.Object);
        }

        public static UrlHelper GetUrlHelper(RequestContext requestContext = null)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            return new UrlHelper(requestContext ?? new RequestContext(GetHttpContext(), new RouteData()), routes);
        }

        public static HttpContextBase GetHttpContext(Uri uri, NameValueCollection queryString)
        {
            var request = GetHttpRequest(uri, queryString);
            return GetHttpContext(request);
        }

        public static HttpContextBase GetHttpContext(HttpRequestBase httpRequestBase = null,
            HttpResponseBase httpResponseBase = null)
        {
            var mockHttpContextBase = new Mock<HttpContextBase>();

            mockHttpContextBase.Setup(x => x.Request).Returns(httpRequestBase ?? GetHttpRequest());
            mockHttpContextBase.Setup(x => x.Response).Returns(httpResponseBase ?? GetHttpResponse());

            return mockHttpContextBase.Object;
        }

        public static HttpRequestBase GetHttpRequest(Uri uri = null, NameValueCollection queryString = null)
        {
            var mockHttpRequestBase = new Mock<HttpRequestBase>();
            mockHttpRequestBase.Setup(x => x.ApplicationPath).Returns("/");
            mockHttpRequestBase.Setup(x => x.Url).Returns(uri ?? new Uri("http://www.yellowbook.com/",
                UriKind.Absolute));
            mockHttpRequestBase.Setup(x => x.ServerVariables).Returns(
                new NameValueCollection());

            mockHttpRequestBase.Setup(x => x.QueryString).Returns(queryString ?? new NameValueCollection());
            return mockHttpRequestBase.Object;
        }

        public static HttpResponseBase GetHttpResponse()
        {
            var mockHttpResponseBase = new Mock<HttpResponseBase>();
            mockHttpResponseBase.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()))
                .Returns<string>(x => x);

            return mockHttpResponseBase.Object;

        }

        public static RequestContext GetRequestContext(HttpContextBase httpContextBase = null)
        {
            var requestContext = new RequestContext(httpContextBase ?? GetHttpContext(), new RouteData());
            requestContext.RouteData = new RouteData();

            return requestContext;
        }

        //public static HtmlHelper CreateHtmlHelper(ViewDataDictionary vd)
        //{
        //    var mockViewContext = new Mock<ViewContext>(
        //      new ControllerContext(
        //        new Mock<HttpContextBase>().Object,
        //        new RouteData(),
        //        new Mock<ControllerBase>().Object),
        //      new Mock<IView>().Object,
        //      vd,
        //      new TempDataDictionary());

        //    var mockViewDataContainer = new Mock<IViewDataContainer>();
        //    mockViewDataContainer.Setup(v => v.ViewData).Returns(vd);

        //    return new HtmlHelper(mockViewContext.Object, mockViewDataContainer.Object);
        //}
    }
}
