using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;

namespace AppInv
{
    public static class NetworkInterfaces
    {
        public static List<NetworkInfo> data { get; set; }
        public static string GetInterfaces()
        {
            data = new List<NetworkInfo>();
            foreach( NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                NetworkInfo o = new NetworkInfo();
                o.Description = n.Description;
                o.Id = n.Id;
                o.IsReceiveOnly = n.IsReceiveOnly;
                o.Name = n.Name;
                IPInterfaceProperties ip = n.GetIPProperties();
                    o.DnsAddresses = new List<string>();
                    foreach (var a in ip.DnsAddresses)
                    {
                        o.DnsAddresses.Add(a.ToString());
                    }

                data.Add(o);
            }
                return data.JSONSerialize();
        }
    }

    [DataContract]
    public class NetworkInfo
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public bool IsReceiveOnly { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<string> DnsAddresses { get; set; }
    }
}