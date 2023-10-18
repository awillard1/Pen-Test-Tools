using System;
using System.Text;

namespace DynamicCode
{
    internal static class stringhelper
    {
        internal static bool Contains(this string src, string value, bool ignorecase)
        {
            if (!ignorecase)
                return src.Contains(value);
            else
                return src.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        internal static string Base64Decode(this string src) {
            string base64Text = string.Empty;
            try {
                var b = Convert.FromBase64String(src);
                base64Text = Encoding.UTF8.GetString(b);
            }
            catch {
                base64Text = string.Empty;
            }
            return base64Text;
        }
    }
}