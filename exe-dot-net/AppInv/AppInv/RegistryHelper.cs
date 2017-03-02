using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppInv
{
    [DataContract]
    public class WirelessNetwork
    {
        [DataMember]
        public string WirelessName { get; set; }
        [DataMember]
        public string MacAddress { get; set; }
    }
    public static class RegistryHelper
    {
        private const string wirelessNetworkKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkList\Signatures\Unmanaged";
        public static List<WirelessNetwork> GetWirelessNetworks()
        {
            List<WirelessNetwork> n = new List<WirelessNetwork>();
            try
            {
                RegistryKey key = RegistryHelper.GetRegistryKey(wirelessNetworkKey);

                foreach (string k in key.GetSubKeyNames())
                {
                    RegistryKey item = RegistryHelper.GetRegistryKey(wirelessNetworkKey + "\\" + k);
                    WirelessNetwork w = new WirelessNetwork();
                    StringBuilder sb = new StringBuilder();
                    if (null != item)
                    {
                        object _data = item.GetValue("DefaultGatewayMac");
                        if (null != _data)
                        {
                            byte[] data = (byte[])_data;
                            foreach (byte b in data)
                            {
                                sb.AppendFormat("{0:x2} ", b);
                            }
                            string binary = sb.ToString();
                            w.MacAddress = binary.Trim().Replace(" ", ":");
                            if (null != item.GetValue("FirstNetwork"))
                                w.WirelessName = item.GetValue("FirstNetwork").ToString() + " (" + item.GetValue("DnsSuffix").ToString() + ")";
                            n.Add(w);
                        }
                    }
                }
            }
            catch { }
            return n;
        }

        public static RegistryKey GetRegistryKey()
        {
            return GetRegistryKey(null);
        }

        public static RegistryKey GetRegistryKey(string keyPath)
        {
            RegistryKey localMachineRegistry
                = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                          Environment.Is64BitOperatingSystem
                                              ? RegistryView.Registry64
                                              : RegistryView.Registry32);

            return string.IsNullOrEmpty(keyPath)
                ? localMachineRegistry
                : localMachineRegistry.OpenSubKey(keyPath);
        }

        public static object GetRegistryValue(string keyPath, string keyName)
        {
            RegistryKey registry = GetRegistryKey(keyPath);
            return registry.GetValue(keyName);
        }
    }
}