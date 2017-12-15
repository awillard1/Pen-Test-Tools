using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanProjectManagement.Business
{
    public interface IUser
    {
        string Name { get; set; }
        string Email { get; set; }
    }

    public class ApplicationUserAccount : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class PenetrationTesterAccount : IUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public static class ApplicationUserData
    {
        private static ApplicationUserAccount _user = null;
        public static ApplicationUserAccount User
        {
            get
            {
                if (null == _user)
                {
                    _user = configurationHelper.getApplicationUserAccount();
                }
                return _user;
            }
        }
    }

    public static class PenetrationTesters
    {
        public static PenetrationTesterAccount getAccount(string name)
        {
            PenetrationTesterAccount account = new PenetrationTesterAccount();
            account = Testers.FirstOrDefault(x => x.Name == name);
            return account;
        }
        public static Object lockObject = new Object();
        private static IList<PenetrationTesterAccount> _users = null;
        public static IList<PenetrationTesterAccount> Testers
        {
            get
            {
                if (null == _users)
                {
                    lock (lockObject)
                    {
                        _users = configurationHelper.loadTesters();
                    }
                }
                return _users;
            }
        }
    }
}