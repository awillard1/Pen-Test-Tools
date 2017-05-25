using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace AppInv
{
    public static class JSONHelper
    {
        public static string JSONSerialize<T>(this T src)
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(src.GetType());
                ser.WriteObject(stream1, src);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                string r = sr.ReadToEnd();
                Console.WriteLine(r);
                return r;
            }
        }
    }
}
