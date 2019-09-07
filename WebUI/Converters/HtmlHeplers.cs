using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Converters
{
    public static class HtmlHeplers
    {
        public static MvcHtmlString HtmlLink(this HtmlHelper html, string url, string text, object htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml = text;
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tb.MergeAttribute("href", url);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Index(this HtmlHelper html)
        {
            var prefix = html.ViewData.TemplateInfo.HtmlFieldPrefix;
            var m = Regex.Match(prefix, @".+\[(\d+)\]");
            if (m.Success && m.Groups.Count == 2)
                return MvcHtmlString.Create(m.Groups[1].Value);
            return null;
        }
    }
}
