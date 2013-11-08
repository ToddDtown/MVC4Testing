﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace MyCompany.Web.Mvc.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString Link(this HtmlHelper helper, string text, string url)
        {
            return helper.Link(text, url, null);
        }

        public static MvcHtmlString Link(this HtmlHelper helper, string text, string url, Dictionary<string, object> attrs)
        {
            var tb = new TagBuilder("a");

            if (attrs != null)
                tb.MergeAttributes(attrs);

            tb.InnerHtml = text;
            tb.MergeAttribute("href", url);

            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString HiddenField(this HtmlHelper helper, string name, string value)
        {
            var tb = new TagBuilder("input");

            tb.MergeAttribute("type", "hidden");
            tb.MergeAttribute("name", name);
            tb.MergeAttribute("value", value);

            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text)
        {
            return helper.SubmitButton(text, null);
        }

        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text, Dictionary<string, object> attrs)
        {
            var tb = new TagBuilder("input");

            if (attrs != null)
                tb.MergeAttributes(attrs);

            tb.MergeAttribute("value", text);
            tb.MergeAttribute("type", "submit");

            return new MvcHtmlString(tb.ToString());
        }

        public static MvcHtmlString JavaScriptVariable(this HtmlHelper helper, string varName, object varValue)
        {
            if (varValue is string)
                return new MvcHtmlString(string.Format("var {0} = '{1}';", varName, varValue));

            return new MvcHtmlString(string.Format("var {0} = {1};", varName, varValue));
        }

        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText)
        {
            var tb = new TagBuilder("img");
            tb.MergeAttribute("src", src);
            tb.MergeAttribute("alt", altText);

            return new MvcHtmlString(tb.ToString());
        }
    }
}
