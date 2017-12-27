using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace DirectoryTestWebApp.Business
{
    public class RobotParse
    {
        public List<string> ParsedList(HttpWebResponse data)
        {
            List<string> list = new List<string>();
            using (Stream st = data.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(st))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string s = sr.ReadLine();
                            if (!string.IsNullOrEmpty(s.Trim()))
                            {
                                if (s.StartsWith("Disallow:", StringComparison.InvariantCultureIgnoreCase) ||
                                    s.StartsWith("Allow:", StringComparison.InvariantCultureIgnoreCase) ||
                                    s.StartsWith("sitemap:", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    int item = s.IndexOf(':') + 1;
                                    list.Add(s.Substring(item, s.Length - item).Trim());
                                }
                            }
                        }
                        catch (System.IO.IOException)
                        {
                            //TODO: Handle; maybe log
                        }
                        catch (System.OutOfMemoryException)
                        {
                            //TODO: Handle; maybe log
                        }
                    }
                }
            }
            return list;
        }
    }
}