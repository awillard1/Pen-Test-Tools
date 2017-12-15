using System;
namespace ScanProjectManagement.Business
{
    public interface IBrowserLog
    {
        string GetUrl();
        string IpHostname { get; set; }
        DateTime LogDateTime { get; set; }
        string Port { get; set; }
        string Protocol { get; set; }
        string PenTesterIP { get; set; }
        string Project { get; set; }
    }
}
