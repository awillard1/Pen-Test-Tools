using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace URLTester.Business
{
    public class SslObject
    {
        public string CipherAlgorithm { get; set; }
        public string CipherStrength { get; set; }
        public string SslProto { get; set; }
        public string KeyExchangeAlgorithm { get; set; }
        public string KeyExchangeStrength { get; set; }
        public string HashAlgorithm { get; set; }
        public string HashStrength { get; set; }
        public string GetString()
        {
            return null == this ? "No Data Found" :
                this.CipherAlgorithm + " " + this.CipherStrength + " " + this.HashAlgorithm + " " +
                this.HashStrength + " " + this.KeyExchangeAlgorithm + " " + this.SslProto;
        }
    }

    public static class ControlExtensions
    {
        public static TResult SafeInvoke<TResult>(this Control isi, Func<TResult> call)
        {
            return isi.InvokeRequired ? (TResult)isi.Invoke(call, null) : call();
        }
        public static void SafeInvoke(this Control isi, Action action)
        {
            if (isi.InvokeRequired)
                isi.Invoke(action, null);
            else
                action();
        }
    }

    public class AttackHelper
    {
        public static string CreateData(WebHeaderCollection headers)
        {
            if (null == headers)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (string s in headers)
            {
                sb.AppendFormat("{0} : {1}" + Environment.NewLine, s, headers.Get(s));
            }
            return sb.ToString();
        }

        public async Task<FileStatusObject> AttackAsync(string testUrl)
        {
            WorkItem obj = null;
            try
            {
                Uri uri = new Uri(testUrl);
                obj = new WorkItem(new WebRequestHelper().GetWebRequest(uri.ToString(), "tls", null));
            }
            catch { }
            if (null == obj)
            {
                var o = new FileStatusObject()
                {
                    FileName = testUrl,
                    StatusCode = "ERROR",
                    Details = string.Empty
                };
                return await Task.FromResult(o);
            }
            else if (null != obj.Response)
            {
                var o = new FileStatusObject()
                {
                    FileName = testUrl,
                    StatusCode = obj.Status.ToString(),
                    Details = CreateData(obj.Headers)
                };
                return await Task.FromResult(o);
            }
            else
            {
                if (null != obj)
                {
                    var o = new FileStatusObject()
                    {
                        FileName = testUrl,
                        StatusCode = obj.Status.ToString(),
                        Details = CreateData(obj.Headers)
                    };
                    return await Task.FromResult(o);
                }
                else
                {
                    var o = new FileStatusObject()
                    {
                        FileName = testUrl,
                        StatusCode = obj.Status.ToString(),
                        Details = CreateData(obj.Headers)
                    };
                    return await Task.FromResult(o);
                }
            }
        }
    }

    public class NmapUtility
    {
        private IEnumerable<IPPortData> _ipData = null;
        private static ConcurrentBag<FileStatusObject> daBag;
        public NmapUtility(string url)
        {
            daBag = new ConcurrentBag<FileStatusObject>();
            _ipData = new LoadNmapXML().GetNmapData(url);
        }

        public NmapUtility() { }

        public IEnumerable<string> PreLoad()
        {
            if (null != _ipData)
            {
                foreach (var item in _ipData)
                {
                    yield return string.Format("{0}://{1}:{2}", item.Protocol, item.IP, item.Port);
                }
            }
        }
    }

    public class DirectoryEnumerationUtility
    {
        private string _filename = string.Empty;
        public DirectoryEnumerationUtility(string filename)
        {
            _filename = filename;
        }
        public IEnumerable<string> PreLoad()
        {
            if (!File.Exists(_filename))
                yield return null;
            using (StreamReader sr = new StreamReader(_filename))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (!line.StartsWith("#") && !string.IsNullOrEmpty(line))
                        yield return (line);
                }
            }
        }
    }

    public class WorkItem
    {
        private string _html = string.Empty;
        private HttpWebRequest _httpWebRequest;
        private HttpWebResponse _response;
        private HttpStatusCode _status;
        private SslObject _ssl;
        private string _error;
        public WorkItem() { }
        public WorkItem(HttpWebRequest httpWebRequest)
        {
            _httpWebRequest = httpWebRequest;
        }
        public HttpWebRequest Request { get { return _httpWebRequest; } }
        public HttpStatusCode Status { get { return _status; } }
        public WebHeaderCollection ResponseHeaders { get { return _headers; } }
        public string Message { get; set; }
        public string Error { get { return _error; } }
        private WebHeaderCollection _headers = null;
        private CookieCollection _cookies = null;
        public HttpWebResponse Response
        {
            get
            {
                if (null == _response)
                {
                    try
                    {
                        if (null != this.Request && null != this.Request.RequestUri)
                        {
                            if (WebSiteJob.IsValidUrl(this.Request.RequestUri.ToString()))
                            {
                                _ssl = new CipherSuiteRequest().TestSsl(this.Request.RequestUri);
                                _response = (HttpWebResponse)_httpWebRequest.GetResponse();
                                _headers = _response.Headers ?? null;
                            }
                            else
                            {
                                _ssl = null;
                                _response = null;
                            }
                        }
                    }
                    catch (WebException w)
                    {
                        if (w.Message.Contains("timed out", true))
                        {
                            _error = "Unable to connect. Please select a different protocol type from the dropdown list and try again. (ssl3 or tls):" + Environment.NewLine + Request.RequestUri.ToString();
                        }
                        else
                        {
                            if (w.Message.Contains("404"))
                                _status = HttpStatusCode.NotFound;
                            else if (w.Message.Contains("500"))
                                _status = HttpStatusCode.InternalServerError;
                            else if (w.Message.Contains("400"))
                                _status = HttpStatusCode.BadRequest;
                            else if (w.Message.Contains("409"))
                                _status = HttpStatusCode.Conflict;
                            else if (w.Message.Contains("417"))
                                _status = HttpStatusCode.ExpectationFailed;
                            else if (w.Message.Contains("403"))
                                _status = HttpStatusCode.Forbidden;
                            else if (w.Message.Contains("504"))
                                _status = HttpStatusCode.GatewayTimeout;
                            else if (w.Message.Contains("410"))
                                _status = HttpStatusCode.Gone;
                            else if (w.Message.Contains("505"))
                                _status = HttpStatusCode.HttpVersionNotSupported;
                            else if (w.Message.Contains("411"))
                                _status = HttpStatusCode.LengthRequired;
                            else if (w.Message.Contains("405"))
                                _status = HttpStatusCode.MethodNotAllowed;
                            else if (w.Message.Contains("301"))
                                _status = HttpStatusCode.Moved;
                            else if (w.Message.Contains("406"))
                                _status = HttpStatusCode.NotAcceptable;
                            else if (w.Message.Contains("501"))
                                _status = HttpStatusCode.NotImplemented;
                            else if (w.Message.Contains("304"))
                                _status = HttpStatusCode.NotModified;
                            else if (w.Message.Contains("200"))
                                _status = HttpStatusCode.OK;
                            else if (w.Message.Contains("206"))
                                _status = HttpStatusCode.PartialContent;
                            else if (w.Message.Contains("412"))
                                _status = HttpStatusCode.PreconditionFailed;
                            else if (w.Message.Contains("407"))
                                _status = HttpStatusCode.ProxyAuthenticationRequired;
                            else if (w.Message.Contains("302"))
                                _status = HttpStatusCode.Redirect;
                            else if (w.Message.Contains("413"))
                                _status = HttpStatusCode.RequestEntityTooLarge;
                            else if (w.Message.Contains("408"))
                                _status = HttpStatusCode.RequestTimeout;
                            else if (w.Message.Contains("414"))
                                _status = HttpStatusCode.RequestUriTooLong;
                            else if (w.Message.Contains("503"))
                                _status = HttpStatusCode.ServiceUnavailable;
                            else if (w.Message.Contains("401"))
                                _status = HttpStatusCode.Unauthorized;
                            else if (w.Message.Contains("415"))
                                _status = HttpStatusCode.UnsupportedMediaType;
                            _error = w.Message;
                        }
                    }
                    catch (ProtocolViolationException p)
                    {
                        _error = p.Message;
                    }
                    catch (InvalidOperationException i)
                    {
                        _error = i.Message;
                    }
                    catch (NotSupportedException n)
                    {
                        _error = n.Message;
                    }
                    finally
                    {
                        if (null != _response)
                        {
                            _status = _response.StatusCode;
                        }
                    }
                }
                if (!Request.Method.
                    Equals("OPTIONS", StringComparison.InvariantCultureIgnoreCase) &&
                    string.IsNullOrEmpty(_error))
                {
                    GetHtml();
                }
                return _response;
            }
        }

        public SslObject Ssl
        {
            get { return _ssl; }
        }

        public WebHeaderCollection Headers
        {
            get
            {
                if (null == _headers)
                {
                    _headers = null == Response ? null : Response.Headers;
                }
                return _headers;
            }
        }
        public CookieCollection Cookies
        {
            get
            {
                if (null == _cookies)
                {
                    _cookies = null == Response ? null : Response.Cookies;
                }
                return _cookies;
            }
        }

        public string Html
        {
            get
            {
                GetHtml();
                return _html;
            }
        }

        private void GetHtml()
        {
            if (string.IsNullOrEmpty(_html))
            {
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
                        _html = null == data ? string.Empty : UTF8Encoding.UTF8.GetString(data);
                    }
                }
            }
        }

        public string RemoveNewLineFromHtml
        {
            get
            {
                return string.IsNullOrEmpty(_html) ? string.Empty :
                    _html.Replace(Environment.NewLine, string.Empty);
            }
        }
    }

    public class CipherSuiteRequest
    {
        public SslObject TestSsl(Uri uri)
        {
            SslObject o = null;
            if (!"https".Equals(uri.Scheme))
                return o;
            TcpClient client = null;
            try
            {
                client = new TcpClient();
                client.Connect(uri.Host, 443);
                using (SslStream ssl = new SslStream(client.GetStream(), true, new RemoteCertificateValidationCallback(CertificateValidationCallback)))
                {
                    ssl.AuthenticateAsClient(uri.Host, null, SslProtocols.Tls | SslProtocols.Ssl3 | SslProtocols.Ssl2, false);
                    o = new SslObject()
                    {
                        CipherAlgorithm = ssl.CipherAlgorithm.ToString(),
                        CipherStrength = ssl.CipherStrength.ToString(),
                        SslProto = ssl.SslProtocol.ToString(),
                        HashAlgorithm = ssl.HashAlgorithm.ToString(),
                        HashStrength = ssl.HashStrength.ToString(),
                        KeyExchangeAlgorithm = ssl.KeyExchangeAlgorithm.ToString(),
                        KeyExchangeStrength = ssl.KeyExchangeStrength.ToString()
                    };
                }
            }
            catch (Exception)
            { }
            finally
            {
                if (null != client)
                {
                    client.Close();
                }
            }
            return o;
        }

        static bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}