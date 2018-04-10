using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace URLTester.Business
{
    public static class StringUtilities
    {
        public static string HashString<T>(this string src) where T : HashAlgorithm, new()
        {
            byte[] b = UTF8Encoding.UTF8.GetBytes(src);
            using (T obj = new T())
            {
                return Convert.ToBase64String(obj.ComputeHash(b));
            }
        }

        public static bool Contains(this string src, string value, bool ignorecase)
        {
            if (!ignorecase)
                return src.Contains(value);
            else
                return src.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
        
        public static int ContainsCount(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return 0;
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i, StringComparison.InvariantCultureIgnoreCase)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        public static MatchCollection GetMatch(this string input, string regex)
        {
            return Regex.Matches(input, regex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        public static bool IsEmpty(this string[] values)
        {
            foreach (string value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetDateTimeAsLongString()
        {
            DateTime d = DateTime.Now;
            return d.Year.ToString() + d.Month.LeadingZeroCheck() + d.Day.LeadingZeroCheck() + d.Hour.LeadingZeroCheck() + d.Minute.LeadingZeroCheck() + d.Second.LeadingZeroCheck();
        }

        private static string LeadingZeroCheck(this int value)
        {
            if (10 > value)
                return "0" + value.ToString();
            else
                return value.ToString();
        }
    }
}