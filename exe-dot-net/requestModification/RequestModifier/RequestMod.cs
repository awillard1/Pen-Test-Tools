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

        public static string GetDetails(Uri u)
        {
            StringBuilder h = new StringBuilder("<h1>Test Form</h1><form method=POST action=\"" + u.Scheme + "://" + u.Host + u.AbsolutePath + "\"><table>");
            if (null != u)
            {
                NameValueCollection items = u.Query.ConvertToPair();
                foreach (string item in items)
                {
                    h.Append(string.Format("<tr><td><strong>" + HttpUtility.HtmlEncode(item) + ":</strong></td><td><input type=\"text\" width=150 name={0} value=\"{1}\"></td></tr>", HttpUtility.HtmlEncode(item), HttpUtility.HtmlEncode(items[item])));
                }
            }
            h.Append("</table><input type='submit' value='Test Post'></form>");
            return h.ToString();
        }
    }
}
