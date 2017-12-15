using ScanProjectManagement.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ScanProjectManagement.Business
{ 
    /*
  * XElement element = myClass.ToXElement<MyClass>();
var newMyClass = element.FromXElement<MyClass>();*/
    public class Logger
    {
        public enum LoggerTypes
        {
            sshlog,
            nmaplog,
            browserlog
        }
        internal static XElement Log(IBrowserLog l)
        {
            return l.ToXElement<BrowserLog>();
        }
        internal static XElement Log(INmapLog l)
        {
            return l.ToXElement<NmapLog>();
        }
        internal static XElement Log(ISSHLog l)
        {
            return l.ToXElement<SSHLog>();
        }
    }
}
