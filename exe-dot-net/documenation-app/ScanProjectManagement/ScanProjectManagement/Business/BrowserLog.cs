using System;

namespace ScanProjectManagement.Business
{
    [Serializable]
    public class BrowserLog : ScanProjectManagement.Business.IBrowserLog
    {
        public string IpHostname { get; set; }
        public string Port { get; set; }
        public string Protocol { get; set; }
        public string GetUrl()
        {
            return string.Format("{0}://{1}:{2}", this.Protocol, this.IpHostname, this.Port);
        }
        public DateTime LogDateTime
        {
            get;
            set;
        }
        public string PenTesterIP { get; set; }
        public string Project { get; set; }
    }

    public class BrowserLogCollection
    {
        public string Url { get; set; }
        public long AccessedCount { get; set; }
    }

}
