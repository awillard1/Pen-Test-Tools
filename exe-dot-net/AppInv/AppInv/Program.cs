using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;

namespace AppInv
{
    class Program
    {
        static void Main(string[] args)
        {
            string d = DriveMappings.GetDriveMappings().JSONSerialize();
            string nsJSON = NetStat.NetStatJSON();
            string w = RegistryHelper.GetWirelessNetworks().JSONSerialize();
            string p = AppProcesses.GetProcs();
            string i = NetworkInterfaces.GetInterfaces();
            string username = UserInformation.GetUserName().JSONSerialize();
            StringBuilder x = new StringBuilder();
            x.Append("---DriveMappings---" + Environment.NewLine + d + Environment.NewLine + Environment.NewLine);
            x.Append("---NetStat---" + Environment.NewLine + nsJSON + Environment.NewLine + Environment.NewLine);
            x.Append("---Wifi Locations - if running as admin---" + Environment.NewLine + w + Environment.NewLine + Environment.NewLine);
            x.Append("---Processes---" + Environment.NewLine + p + Environment.NewLine + Environment.NewLine);
            x.Append("---NetworkInterfaces---" + Environment.NewLine + i + Environment.NewLine + Environment.NewLine);
            x.Append("---UserName---" + Environment.NewLine + username + Environment.NewLine + Environment.NewLine);
            using (TextWriter writer = File.CreateText("data.txt"))
            {
                writer.Write(x.ToString());
            }
        }
    }
}