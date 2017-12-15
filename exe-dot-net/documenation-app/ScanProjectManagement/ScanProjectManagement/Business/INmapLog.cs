using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanProjectManagement.Business
{
    public interface INmapLog
    {
        string NmapCommand { get; set; }
        DateTime LogDateTime { get; set; }
        string PenTesterIP { get; set; }
        string Project { get; set; }
    }
}
