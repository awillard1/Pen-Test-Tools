using System;
using System.Collections.Generic;

namespace ScanProjectManagement.Business
{
    [Serializable]
    public class SSHLog : ISSHLog
    {
        public string IPHostname
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public bool wasSuccessful
        {
            get;
            set;
        }

        public DateTime LogDate
        {
            get;
            set;
        }
        public string PenTesterIP { get; set; }

        public string Project { get; set; }
    }

    [Serializable]
    public class SSHLogData
    {
        
    }

    [Serializable]
    public class SSHLogCollection
    {
        public string IPHostname { get; set; }
        public long failedLogin { get; set; }
        public long successfulLogin { get; set; }
    }
}
