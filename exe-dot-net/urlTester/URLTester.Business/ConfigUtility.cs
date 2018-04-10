using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace URLTester.Business
{
    public class LoadNmapXML
    {
        public IEnumerable<IPPortData> GetNmapData(string location)
        {
            string[] protocol = new string[] { "http", "https" };
            if (File.Exists(location))
            {
                XElement nmapData = XElement.Load(location);
                var data = nmapData.Elements("host");
                foreach (XElement e in data)
                {
                    string theHost = string.Empty;
                    try
                    {
                        var hostname = e.Element("hostnames").Elements("hostname");
                        foreach (var host in hostname)
                        {
                            if (host.Attribute("type").Value.Contains("user", true))
                            {
                                theHost = host.Attribute("name").Value;
                            }
                        }
                    }
                    catch { }
                    string addr = string.IsNullOrEmpty(theHost) ? e.Element("address").Attribute("addr").Value : theHost;
                    foreach (XElement p in e.Element("ports").Elements("port"))
                    {
                        foreach (XElement s in p.Elements("state"))
                        {
                            if (s.Attribute("state").Value.Contains("open", true))
                            {
                                foreach (var prt in protocol)
                                {
                                    int port = 0;
                                    Int32.TryParse(p.Attribute("portid").Value, out port);
                                    yield return new IPPortData() { IP = addr, Port = port, Protocol=prt };
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }
}