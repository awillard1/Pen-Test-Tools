using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

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

    [DataContract]
    public class LastVisitedPidlMRU
    {
        [DataMember]
        public string data { get; set; }
    }
    public static class RegistryHelper
    {
        private const string pid = @"Software\Microsoft\Windows\CurrentVersion\Explorer\ComDlg32\LastVisitedPidlMRU";
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

        public static List<LastVisitedPidlMRU> GetPid()
        {
            List<LastVisitedPidlMRU> n = new List<LastVisitedPidlMRU>();
            try
            {
                RegistryKey key = RegistryHelper.GetRegistryKeyCU(pid);

                foreach (string k in key.GetValueNames())
                {
                    object item = key.GetValue(k);
                    LastVisitedPidlMRU w = new LastVisitedPidlMRU();
                    StringBuilder sb = new StringBuilder();
                    if (null != item)
                    {
                        object _data = item;
                        if (null != _data)
                        {
                            byte[] data = (byte[])_data;
                            string binary = System.Text.Encoding.Unicode.GetString(data);
                            binary = Regex.Replace(binary, @"[^\u0020-\u007F]+", string.Empty);
                            w.data = binary;
                            n.Add(w);
                        }
                    }
                }
            }
            catch { }
            return n;
        }

        private static StringBuilder convertToAscii(string hexString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hs = hexString.Substring(i, 2);
                sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
            }
            return sb; 
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

        public static RegistryKey GetRegistryKeyCU(string keyPath)
        {
            RegistryKey cuRegistry
                = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser,
                                          Environment.Is64BitOperatingSystem
                                              ? RegistryView.Registry64
                                              : RegistryView.Registry32);

            return string.IsNullOrEmpty(keyPath)
                ? cuRegistry
                : cuRegistry.OpenSubKey(keyPath);
        }

        public static object GetRegistryValue(string keyPath, string keyName)
        {
            RegistryKey registry = GetRegistryKey(keyPath);
            return registry.GetValue(keyName);
        }
    }
}