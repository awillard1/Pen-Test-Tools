using ScanProjectManagement.Business;
using System;
using System.Collections.Generic;

namespace ScanProjectManagement
{
    public static class VulnerabilityTypes
    {
        public static Object lockObject = new Object();
        private static IList<string> _types = null;
        public static IList<string> VulnerabilityType
        {
            get
            {
                if (null == _types)
                {
                    lock (lockObject)
                    {
                        _types = configurationHelper.loadVulnerabilities();
                    }
                }
                return _types;
            }
        }
    }

    public class Vulnerabilities
    {
        public static Object lockObject = new Object();
        public static IList<IVulnerability> ListofVulnerabilities
        {
            get;
            set;
        }

        public static void AddObject(IVulnerability item)
        {
            lock (lockObject)
            {
                if (null == ListofVulnerabilities)
                    ListofVulnerabilities = new List<IVulnerability>();
                ListofVulnerabilities.Add(item);
            }
        }

        public static void Clear()
        {
            ListofVulnerabilities = new List<IVulnerability>();
            ListofVulnerabilities.Clear();
        }

        public static void RemoveItem(Vulnerability v)
        {
            lock (lockObject)
            {
                if (null != v)
                {
                    ListofVulnerabilities.Remove(v);
                }
            }
        }
    }
}
