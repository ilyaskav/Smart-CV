using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace SmartCV.WebUI.Converters
{
    public static class HtmlHeplers
    {
        public static IHtmlContent HtmlLink(this IHtmlHelper html, string url, string text, object htmlAttributes)
        {
            TagBuilder tb = new TagBuilder("a");
            tb.InnerHtml.Append(text);
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tb.MergeAttribute("href", url);

            return tb;
        }

        public static IHtmlContent Index(this IHtmlHelper html)
        {
            var prefix = html.ViewData.TemplateInfo.HtmlFieldPrefix;
            var m = Regex.Match(prefix, @".+\[(\d+)\]");
            if (m.Success && m.Groups.Count == 2)
                return new HtmlString( m.Groups[1].Value);

            return null;
        }
    }
}
