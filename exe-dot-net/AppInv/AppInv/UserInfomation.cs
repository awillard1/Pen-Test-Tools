using System.Runtime.Serialization;
namespace AppInv
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public string UserName { get; set; }
    }
    public static class UserInformation
    {
        public static string GetUserName()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return userInfo.JSONSerialize();
        }
    }
}