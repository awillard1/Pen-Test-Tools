using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace AppInv
{
    [DataContract]
    public class ProcessData
    {
        [DataMember]
        public string ProcessName { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public int SessionId { get; set; }
        [DataMember]
        public string StartInfoUserName { get; set; }
        [DataMember]
        public string StartInfoArguments { get; set; }
    }
    public class AppProcesses
    {
        public static string GetProcs()
        {
            List<ProcessData> data = new List<ProcessData>();
            foreach (Process p in Process.GetProcesses())
            {
                ProcessData d = new ProcessData();
                d.Id = p.Id;
                d.MachineName = p.MachineName;
                d.ProcessName = p.ProcessName;
                d.SessionId = p.SessionId;
                d.StartInfoUserName = p.StartInfo.UserName;
                d.StartInfoArguments = p.StartInfo.Arguments.ToString();
                data.Add(d);

            }
            return data.JSONSerialize();
        }
    }
}
