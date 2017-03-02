using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace AppInv
{
    [DataContract]
    public class DriveDetails
    {
        [DataMember]
        public string DriveType { get; set; }
        [DataMember]
        public string DriveFormat { get; set; }
        [DataMember]
        public string VolumneLabel { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
    public class DriveMappings
    {
        public static string GetDriveMappings()
        {
            List<DriveDetails> drives = new List<DriveDetails>();
            int i = 65;
            while (i <= 90)
            {
                char a = (char)i;
                DriveInfo d = new DriveInfo(a.ToString());
                if (d.DriveType != DriveType.NoRootDirectory)
                {
                    DriveDetails e = new DriveDetails();
                    e.Name = d.Name;
                    e.DriveType = d.DriveType.ToString();
                    if (d.IsReady)
                    {
                        e.DriveFormat = d.DriveFormat;
                        e.VolumneLabel = d.VolumeLabel;
                    }
                    drives.Add(e);
                }
                i++;
            }
            return drives.JSONSerialize();
        }
    }
}