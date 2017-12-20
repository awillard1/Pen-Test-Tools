using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace RequestModifier
{
    public static class ConverterHelper
    {
        public static NameValueCollection ConvertToPair(this string u)
        {
            return HttpUtility.ParseQueryString(u);
        }
    }
    

    public class RequestMod
    {
        public enum RequestType
        {
            Url, Raw
        };
        public const string requestFormatFirst = "{0} {1} HTTP/1.1";
        public const string requestFormatHost = "Host: {0}";
        public const string myUrlFormat = "<a href=\"{0}?{1}\">Converted Url</a>";
        public const string myGetHtml = "<h1>Test URL</h2><p>";
        public static string myHtml = "<html><head><style>body { font-family:Arial;}" + Environment.NewLine + " h1 {display:table;border-bottom:1px solid black;padding-right:40%;}" + Environment.NewLine + " td {font-size:.8em;}</style></head><body>";

        public static string GetDetails(Uri u, string html = "")
        {
            StringBuilder h = new StringBuilder(myHtml);
            if (!String.IsNullOrEmpty(html) && null != u)
            {
                h.Append(myGetHtml + html + "<br />" + HttpUtility.HtmlEncode(u.ToString()) + "</p>");
            }
                
            if (null != u)
            {
                h.Append("<h1>Test Form</h1><form method=POST action=\"" + u.Scheme + "://" + u.Host + u.AbsolutePath + "\"><table>");
                NameValueCollection items = u.Query.ConvertToPair();
                foreach (string item in items)
                {
                    h.Append(string.Format("<tr><td>" + HttpUtility.HtmlEncode(item) + ":</td><td><input type=\"text\" width=150 name={0} value=\"{1}\"></td></tr>", HttpUtility.HtmlEncode(item), HttpUtility.HtmlEncode(items[item])));
                }
            }
            h.Append("</table><input type='submit' value='Test Post'></form></body></html>");
            return h.ToString();
        }
    }
}
