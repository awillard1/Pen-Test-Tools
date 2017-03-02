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

        internal static string Base64Decode(this string src, Encoding v)
        {
            string base64Text = string.Empty;
            try
            {
                Base64Encoding.MemoryEncoding.MemoryBase64Decoder h = 
                    new Base64Encoding.MemoryEncoding.MemoryBase64Decoder(src);
                byte[] a = h.Decode();
                base64Text = v.GetString(a);
            }
            catch
            {
                base64Text = string.Empty;
            }
            return base64Text;
        }
    }
}