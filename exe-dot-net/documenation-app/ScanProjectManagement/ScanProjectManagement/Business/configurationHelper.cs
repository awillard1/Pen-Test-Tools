using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ScanProjectManagement.Business
{
    public static class configurationHelper
    {
        private static Object lockObject = new Object();
        private static IList<string> _pl = null;
        private static IList<string> _dc = null;

        public static IList<string> documentationCategories
        {
            get
            {
                if (null == _dc)
                {
                    lock (lockObject)
                    {
                        _dc = new List<string>();
                        _dc = LoadDocoCategories();
                    }
                }
                return _dc;
            }
        }

        private static IList<string> LoadDocoCategories()
        {
            XElement main = configuration.Element("documentation");
            XElement data = main.Element("category");
            IList<string> details = new List<string>();
            foreach (XElement x in data.Elements("item").ToList())
            {
                details.Add(x.Value);
            }
            return details;
        }

        public static IList<string> programmingLanguages
        {
            get
            {
                if (null == _pl)
                {
                    lock (lockObject)
                    {
                        _pl = new List<string>();
                        _pl = LoadLanguages();
                    }
                }
                return _pl;
            }
        }

        private static XElement baseConfigElements = null;
        private static XElement configuration
        {
            get
            {
                if (null == baseConfigElements)
                {
                    baseConfigElements = XElement.Load("config.xml");
                }
                return baseConfigElements;
            }
        }

        private static IList<string> LoadLanguages()
        {
            XElement data = configuration.Element("programmingLanguages");
            IList<string> details = new List<string>();
            foreach (XElement x in data.Elements("item").ToList())
            {
                details.Add(x.Value);
            }
            return details;
        }

        internal static string getMeetingInviteStorage()
        {
            XElement e = configuration.Element("meetingInviteStorage");
            return e.Value;
        }

        internal static ApplicationUserAccount getApplicationUserAccount()
        {
            ApplicationUserAccount a = new ApplicationUserAccount();
            a.Name = string.Empty;
            a.Email = string.Empty;
            XElement userElement = configuration.Element("userDetails");
            XElement user = userElement.Element("user");
            if (null != user)
            {
                a.Name = user.Element("name").Value;
                a.Email = user.Element("email").Value;
            }
            return a;
        }

        internal static IList<PenetrationTesterAccount> loadTesters()
        {
            IList<PenetrationTesterAccount> testers = new List<PenetrationTesterAccount>();
            XElement data = configuration.Element("penetrationTesters");
            if (null != data)
            {
                foreach (XElement x in data.Elements("user").ToList())
                {
                    string name = x.Element("name").Value;
                    string email = x.Element("email").Value;
                    PenetrationTesterAccount a = new PenetrationTesterAccount();
                    a.Name = name;
                    a.Email = email;
                    testers.Add(a);
                }
            }
            return testers;
        }

        internal static DayOfWeek GetDayOfWeek()
        {
            XElement data = configuration.Element("calendar");
            DayOfWeek retval = DayOfWeek.Monday;
            if (null != data)
            {
                var d = data.Elements("firstDayOfWeek").FirstOrDefault();
                if (Enum.IsDefined(typeof(DayOfWeek), d.Value.ToString()))
                {
                    retval = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), d.Value.ToString(), true);
                }
                else
                {
                    retval = DayOfWeek.Monday;
                }
            }
            return retval;
        }

        internal static IList<string> loadVulnerabilities()
        {
            IList<string> vuln = new List<string>();
            XElement data = configuration.Element("vulnerability");
            if (null != data)
            {
                foreach (XElement x in data.Elements("item").ToList())
                {
                    vuln.Add(x.Value);
                }
            }
            return vuln;
        }
    }
}
