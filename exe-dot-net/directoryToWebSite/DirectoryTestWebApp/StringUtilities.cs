using System;

namespace DirectoryTestWebApp
{
    public static class StringUtilities
    {
        public static bool Contains(this string src, string value, bool ignorecase)
        {
            if (!ignorecase)
                return src.Contains(value);
            else
                return src.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
