using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanProjectManagement.Business
{
    public interface ISSHLog
    {
        string IPHostname { get; set; }
        string Username { get; set; }
        bool wasSuccessful { get; set; }
        DateTime LogDate { get; set; }
        string PenTesterIP { get; set; }
        string Project { get; set; }
    }
}
