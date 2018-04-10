using System;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;

namespace URLTester.Business
{
    public enum RequestType
    {
        GET,
        POST,
        HEAD,
        OPTIONS
    }
    public class WebSiteJob
    {
        private string _url;
        private string _protocol;
        private CookieContainer _cookies;
        private WorkItem workItem;
        private RequestType type = RequestType.GET;
        public WorkItem Result
        {
            get { return workItem; }
            set { workItem = value; }
        }
        public WebSiteJob(string url, string protocol, CookieContainer cookies = null,
            RequestType requestType = RequestType.GET)
        {
            _url = url;
            _protocol = protocol;
            type = requestType;
            _cookies = cookies;
        }
        public Task<WorkItem> DoWork()
        {
            return Task.Factory.StartNew<WorkItem>(this.PerformWork);
        }
        public WorkItem DoNoThreadedWork()
        {
            return this.PerformWork();
        }

        public static bool IsValidUrl(string url)
        {
            return string.IsNullOrEmpty(url) ? false : url.StartsWith(Constants.http, StringComparison.InvariantCultureIgnoreCase) ||
                                url.StartsWith(Constants.https, StringComparison.InvariantCultureIgnoreCase);
        }

        private WorkItem PerformWork()
        {
            WorkItem w = new WorkItem();
            if (!_url.Contains("mailto:"))
            {
                if (IsValidUrl(_url))
                {
                    w = new WorkItem(new WebRequestHelper().GetWebRequest(_url, _protocol, _cookies, type));
                    if (null == w.Response)
                        w.Message = w.Error;
                    else
                        w.Message = "Completed Analysis";
                }
                else
                {
                    w.Message = "Request Failed";
                }
            }
            return w;
        }
    }
    public class WebRequestHelper
    {
        public HttpWebRequest GetWebRequest(string url, string protocolType, CookieContainer cookies, RequestType type = RequestType.GET)
        {
            SecurityProtocolType protocol = SecurityProtocolType.Tls;
            if (string.IsNullOrEmpty(protocolType))
            {
                protocol = SecurityProtocolType.Ssl3;
            }
            else if ("ssl3".Equals(protocolType, StringComparison.InvariantCultureIgnoreCase))
            {
                protocol = SecurityProtocolType.Ssl3;
            }
            else if ("tls".Equals(protocolType, StringComparison.InvariantCultureIgnoreCase))
            {
                protocol = SecurityProtocolType.Tls;
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 5 * 1000;//5 seconds
            request.CookieContainer = new CookieContainer();
            if (null != cookies)
            {
                if (null == request.CookieContainer)
                { }
                try
                {
                    Uri u = new Uri(url);
                    request.CookieContainer = cookies;
                }
                catch { }

            }
            request.Method = type.ToString();
            request.AllowAutoRedirect = true;
            request.AllowWriteStreamBuffering = true;
            request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(
                delegate { return true; });
            return request;
        }
    }
}
