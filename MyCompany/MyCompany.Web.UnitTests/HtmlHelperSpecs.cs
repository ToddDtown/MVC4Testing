using NUnit.Framework;
using MyCompany.Web.Mvc.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using MyCompany.Web.UnitTests.HelperSetup;

namespace MyCompany.Web.UnitTests
{
    [TestFixture]
    public class when_using_html_helpers : BaseFixture
    {
        public HtmlHelper _sut;
        public MvcHtmlString _result;
        public XElement _element;

        public override void SetupContext()
        {
            _sut = HelperSetup.MvcTestHelpers.GetHtmlHelper();
        }

        public override void Act()
        {
            _result = _sut.Link("Sample Text", "http://www.cnn.com");
            _element = _result.ToXElement();
        }

        [Test]
        public void ensure_link_renders_correctly()
        {
            Assert.That(_element.Attribute("href").Value, Is.EqualTo("http://www.cnn.com"));
            Assert.That(_element.Value, Is.EqualTo("Sample Text"));
        }
    }
}
