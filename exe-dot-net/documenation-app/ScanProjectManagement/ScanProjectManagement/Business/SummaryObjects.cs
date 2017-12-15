using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanProjectManagement.Business
{
    public class SummaryObjects
    {
        public class VulnerabilitySummary
        {
            public string Project { get; set; }
            public List<IVulnerability> VulnerableItem { get; set; }
            public List<Documentation> DocumentationItem { get; set; }
        }
    }
}
