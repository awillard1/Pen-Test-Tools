using System;

namespace ScanProjectManagement.Business
{
    [Serializable]
    public class NmapLog:INmapLog
    {
        public string NmapCommand
        {
            get;
            set;
        }

        public DateTime LogDateTime
        {
            get;
            set;
        }
        public string PenTesterIP { get; set; }
        public string Project { get; set; }
    }

    public class NmapLogCollection
    {
        public long Count { get; set; }
    }
}
