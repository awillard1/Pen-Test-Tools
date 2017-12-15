using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ScanProjectManagement.Business
{
    public class IPAddress
    {
        private static List<string> _ipList { get; set; }
        public static List<string> GetIPAddresses()
        {
            if (null != _ipList)
                return _ipList;
            else
                return new List<string>();
        }
        public static void AllowAccessForIP(bool allow)
        {
            if (allow)
            {
                string url = "https://api.ipify.org/";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string ip = string.Empty;
                if (HttpStatusCode.OK == response.StatusCode)
                {
                    ip = GetHtml(response);
                }
                if (!string.IsNullOrEmpty(ip))
                {
                    if (null == _ipList)
                        _ipList = new List<string>();
                    _ipList.Add(ip);
                }
            }
            processCmdLine();
        }

        private static string GetHtml(HttpWebResponse _response)
        {
            string html = string.Empty;
            if (null != _response)
            {
                using (Stream stream = _response.GetResponseStream())
                {
                    byte[] data = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        const int bufferSize = 4096;
                        int count = 0;
                        do
                        {
                            byte[] buf = new byte[bufferSize];
                            count = stream.Read(buf, 0, bufferSize);
                            ms.Write(buf, 0, count);
                        } while (stream.CanRead && count > 0);
                        data = ms.ToArray();
                    }
                    html = null == data ? string.Empty : UTF8Encoding.UTF8.GetString(data);
                }
            }
            return html;
        }
 
        private static void processCmdLine()
        {
            string baseCmd = "/c ipconfig | findstr /R /C:\"IPv4 Address\"";
            string o = string.Empty;
            ProcessStartInfo psi = new ProcessStartInfo("C:\\windows\\system32\\cmd.exe", baseCmd);
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            using (StreamReader sr = p.StandardOutput)
            {
                o = sr.ReadToEnd();
            }
            parseData(o);
        }

        private static void parseData(string s)
        {
            if (null == _ipList)
                _ipList = new List<string>();
            Regex x = new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            MatchCollection m = x.Matches(s);
            if (null != m)
            {
                foreach (Match _m in m)
                {
                    _ipList.Add(_m.Value);
                }
            }
        }

        internal static void ClearIP()
        {
            if (null != _ipList)
                _ipList.Clear();

            _ipList = new List<string>();
        }
    }
}
