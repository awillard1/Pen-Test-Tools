using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScanProjectManagement.Business
{
    public static class SecureHelper
    {
        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
                throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
    public class CmdHelper
    {
        public static void LaunchInternetExplorer(BrowserLog v)
        {
            Process.Start("IExplore.exe", v.GetUrl());
        }

        public static void LaunchNmap(NmapLog v)
        {
            Process.Start("nmap", v.NmapCommand.Replace("nmap ", "") + " oX " + v.Project +"_" + DateTime.UtcNow.ToString() + ".xml");
        }


        public static string LoadPutty(SSHLog v, SecureString s)
        {
            try
            {
                Process.Start("C:\\Program Files (x86)\\PuTTY\\putty.exe", "-ssh " + v.Username + "@" + v.IPHostname + " -pw \"" + SecureHelper.ConvertToUnsecureString(s).Replace(@"""", @"\"""));
            }
            catch { }
            return string.Empty;
        }
    }
}
