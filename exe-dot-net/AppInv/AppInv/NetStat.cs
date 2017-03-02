using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;

namespace AppInv
{
    public class NetStat
    {
        [DataContract
        (Name="NetStatANO")]
        public class NetStatANO
        {
            [DataMember]
            public string LAddress { get; set; }
            [DataMember]
            public string FAddress { get; set; }
            [DataMember]
            public string StateOf { get; set; }
            [DataMember]
            public string ProcessId { get; set; }
            [DataMember]
            public string TypeOf { get; set; }
            [DataMember]
            public string Data { get; set; }
        }
        
        public static string NetStatJSON()
        {
            List<AppInv.NetStat.NetStatANO> n = NetStat.GetNetStatANO("c:\\windows\\system32\\cmd.exe");
            return n.JSONSerialize();
        }
        
        private static List<NetStatANO> GetNetStatANO(string cmdExeLocation)
        {
            ProcessStartInfo psi = new ProcessStartInfo(cmdExeLocation, "/c netstat -ano");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            IList<NetStatANO> items = new List<NetStatANO>();
            using (Process process = Process.Start(psi))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    string[] d = result.Split(Environment.NewLine.ToArray());
                    bool hasData = false;

                    foreach (string s in d)
                    {
                        string k = s.Trim();
                        if (hasData)
                        {
                            if (!string.IsNullOrEmpty(k))
                            {
                                int indexOfFirstSpace = k.IndexOf(" ");
                                NetStatANO obj = new NetStatANO();
                                string type = k.Substring(0, indexOfFirstSpace);
                                obj.TypeOf = type;

                                string remainder = k.Substring(indexOfFirstSpace).Trim();
                                int indexOfSpace = remainder.IndexOf(" ");
                                string local = remainder.Substring(0, indexOfSpace);
                                obj.LAddress = local;

                                remainder = remainder.Substring(indexOfSpace).Trim();
                                indexOfSpace = remainder.IndexOf(" ");
                                string fAddress = remainder.Substring(0, indexOfSpace).Trim();
                                obj.FAddress = fAddress;

                                if (obj.FAddress.Contains("*"))
                                {
                                    obj.StateOf = string.Empty;
                                }
                                else
                                {
                                    remainder = remainder.Substring(indexOfSpace).Trim();
                                    indexOfSpace = remainder.IndexOf(" ");
                                    string state = remainder.Substring(0, indexOfSpace);
                                    obj.StateOf = state;
                                }
                                string processId = remainder.Substring(indexOfSpace).Trim();
                                obj.ProcessId = processId;
                                obj.Data = GetProcessInfo(obj.ProcessId);
                                items.Add(obj);
                            }
                        }
                        else
                        {
                            if (k.StartsWith("Proto", StringComparison.InvariantCultureIgnoreCase))
                                hasData = true;
                        }
                    }
                }
                return items.ToList();
            }
        }

        private static string GetProcessInfo(string id)
        {
            string val = string.Empty;
            if (!string.IsNullOrEmpty(id))
            {
                try
                {
                    int pid = Convert.ToInt32(id);
                    Process p = Process.GetProcessById(pid);
                    val = p.ProcessName;
                }
                catch
                {
                    val = "unknown";
                }
            }
            return val;
        }
    }
}
