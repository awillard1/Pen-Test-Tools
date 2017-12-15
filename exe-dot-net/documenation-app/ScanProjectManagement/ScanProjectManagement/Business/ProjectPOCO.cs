using ScanProjectManagement;
using System;
using System.Collections.Generic;

namespace ScanProjectManagement.Business
{
    public enum CodeScanType
    {
        None = 0,
        Manual = 1,
        Automated = 2,
        Hybrid = 3
    }
    public class ProjectPOCO
    {
        public string Name { get; set; }
        public string ISSO { get; set; }
        public string DevLead { get; set; }
        public bool isCurrentlyScanned { get; set; }
        public CodeScanType ScanConfiguration { get; set; }
        public string Repository { get; set; }
        public List<StaticAnalysisPOCO> StaticAnalysis { get; set; }
        public List<PenetrationTestPOCO> PenetrationTests { get; set; }
        public string ProductionURL { get; set; }
        public List<string> CodeLanguages { get; set; }
        public IList<IVulnerability> Vulnerabilities { get; set; }
        public List<Documentation> DocumentationItems { get; set; }
        public IList<ISSHLog> SshLogs { get; set; }
        public IList<INmapLog> NmapLogs { get; set; }
        public IList<IBrowserLog> BrowserLogs { get; set; }
    }
    public class Documentation
    {
        public string Details { get; set; }
        public DateTime DateOfIssue { get; set; }
        public string Category { get; set; }
    }
    //TODO: Add More Detail for Scan
    public class StaticAnalysisPOCO
    {
        public DateTime AnalysisDate { get; set; }
    }
    //TODO: Possibly Add Testing Type and products
    public class PenetrationTestPOCO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TesterName { get; set; }
    }
}
