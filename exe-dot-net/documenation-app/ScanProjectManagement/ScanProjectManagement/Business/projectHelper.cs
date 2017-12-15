using ScanProjectManagement;
using ScanProjectManagement.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace ScanProjectManagement.Business
{
public enum ModTypes
{
    Add, Remove, Update
}
    public static class projectHelper
    {
        const string dateFormat = "MM/dd/yyyy hh:mm tt";
        internal static void SaveSSHData(string path, SSHLog log)
        {
            SaveLogFile(path, Logger.LoggerTypes.sshlog.ToString(),log);

        }
        internal static void SaveNmapData(string path, NmapLog log)
        {
            SaveLogFile(path, Logger.LoggerTypes.nmaplog.ToString(), log);
        }

        internal static void SaveBrowserData(string path, BrowserLog log)
        {
            SaveLogFile(path, Logger.LoggerTypes.browserlog.ToString(), log);
        }

        private static void SaveLogFile<T>(string path, string type,T obj )
        {
            if (!File.Exists(path))
            {
                XProcessingInstruction instruction = new XProcessingInstruction(xmlStyle, xmlStyleInstruction);
                XDocument document = new XDocument();//instruction);
                document.Declaration = CreateDocumentDeclaration();
                XElement root = new XElement(type);
                document.Add(root);
                document.Save(path);
            }
            
            XElement x = obj.ToXElement<T>();
            XDocument d = XDocument.Load(path);
            d.Element(type).Add(x);
            
            d.Save(path);
        }
        
        internal static Tuple<string,XDocument> SaveData()
        {
            XProcessingInstruction instruction = new XProcessingInstruction(xmlStyle, xmlStyleInstruction);
            XDocument document = new XDocument(instruction);
            document.Declaration = CreateDocumentDeclaration();
            XElement root = new XElement("projects");
            List<ProjectPOCO> sorted = _data.ToList();
            sorted.Sort((x, y) => String.Compare(x.Name, y.Name));
            _data = sorted;
            foreach (ProjectPOCO project in _data.ToList())
            {
                XElement main = new XElement("project");
                main.Add(new XElement("name", new XCData(project.Name)));
                main.Add(new XElement("productionURL", new XCData(project.ProductionURL)));
                XElement contacts = new XElement("contacts");
                contacts.Add(new XElement("isso", new XCData(project.ISSO)));
                contacts.Add(new XElement("devLead", new XCData(project.DevLead)));
                main.Add(contacts);
                
                XElement codeDetail = new XElement("codeDetail");
                XElement languages = new XElement("languages");
                if (null != project.CodeLanguages)
                {
                    foreach (string l in project.CodeLanguages)
                    {
                        XElement language = new XElement("language", new XCData(l));
                        languages.Add(language);
                    }
                }
                codeDetail.Add(languages);
                codeDetail.Add(new XAttribute("isCurrentlyScanned", project.isCurrentlyScanned.ToString()),
                    new XAttribute("scanConfiguration", project.ScanConfiguration.ToString()));
                codeDetail.Add(new XElement("repository", new XCData(string.IsNullOrEmpty(project.Repository) ? string.Empty : project.Repository)));
                
                main.Add(codeDetail);

                XElement codeAnalysis = new XElement("codeAnalysis");
                if (null != project.StaticAnalysis)
                {
                    project.StaticAnalysis.Sort((a,b)=> DateTime.Compare(a.AnalysisDate,b.AnalysisDate));
                    foreach (StaticAnalysisPOCO sa in project.StaticAnalysis.ToList())
                    {
                        XElement x = new XElement("scanDate", new XCData(sa.AnalysisDate.ToShortDateString()));
                        codeAnalysis.Add(x);
                    }
                }
                main.Add(codeAnalysis);
                XElement pts = new XElement("penetrationTests");
                if (null != project.PenetrationTests)
                {
                    project.PenetrationTests.Sort((a, b) => DateTime.Compare(a.StartDate, b.StartDate));
                    foreach (PenetrationTestPOCO pt in project.PenetrationTests.ToList())
                    {
                        XElement scan = new XElement("scan");
                        scan.Add(new XElement("startDate", new XCData(pt.StartDate.ToString(dateFormat))));
                        scan.Add(new XElement("endDate", new XCData(pt.EndDate.ToString(dateFormat))));
                        scan.Add(new XElement("tester", new XCData(pt.TesterName)));
                        pts.Add(scan);
                    }
                }
                main.Add(pts);
                //ADD VULNS
                XElement vulns = new XElement("vulnerabilities");
                try
                {
                    foreach (IVulnerability v in project.Vulnerabilities)
                    {
                        XElement d = new XElement("item",
                            new XElement("title", new XCData(string.IsNullOrEmpty(v.Title) ? string.Empty : v.Title)),
                            new XElement("vulnerability", new XCData(string.IsNullOrEmpty(v.Details) ? string.Empty : HttpUtility.HtmlEncode(v.Details))),
                            new XElement("status", new XCData(string.IsNullOrEmpty(v.Status) ? string.Empty : v.Status)),
                            new XElement("discoveredDate", new XCData(null == v.DiscoveredDate ? string.Empty : v.DiscoveredDate.Value.ToShortDateString())),
                            new XElement("completedDate", new XCData(null == v.CompletedDate ? string.Empty : v.CompletedDate.Value.ToShortDateString())),
                            new XElement("risk", new XCData(string.IsNullOrEmpty(v.Risk) ? string.Empty : v.Risk)),
                            new XElement("tester", new XCData(string.IsNullOrEmpty(v.Tester) ? string.Empty : v.Tester)),
                            new XElement("identifier", new XCData(v.Identifier.ToString())),
                            new XElement("type", new XCData(v.VulnTypeReported.ToString())),
                            new XElement("flagged", new XCData(v.isWeeklyReportItem.ToString())),
                            new XElement("cvss2", new XCData(v.CVSS.ToString())));
                        vulns.Add(d);
                    }
                }
                catch
                { //TODO: TEMPORARY Need to check count - testing save first
                }
                main.Add(vulns);
                //END ADD
                XElement documentation = new XElement("documentation");
                if (null != project.DocumentationItems)
                {
                    IEnumerable<Documentation> xf = project.DocumentationItems.OrderBy(a => a.DateOfIssue).ThenBy(a => a.Category);
                    foreach (Documentation doco in xf.ToList())
                    {
                        XElement item = new XElement("item");
                        item.Add(new XElement("dateOfIssue", new XCData(doco.DateOfIssue.ToShortDateString())));
                        item.Add(new XElement("category", new XCData(doco.Category)));
                        item.Add(new XElement("details", new XCData(doco.Details)));
                        documentation.Add(item);
                    }
                }
                main.Add(documentation);
                root.Add(main);
            }
            document.Add(root);
            return new Tuple<string,XDocument>(document.ToString(),document);
        }

        private static Object _lockObject = new Object();
        private static IList<ProjectPOCO> _data = null;
        public static IList<ProjectPOCO> Data
        {
            get
            {
                if (null == _data)
                {
                    _data = new List<ProjectPOCO>();
                }
                return _data;
            }
        }

        internal static bool checkExists(string p)
        {
            ProjectPOCO project = Data.FirstOrDefault(x => x.Name == p);
            return project == null ? false : true;
        }

        internal static void Load(string file)
        {
            if (File.Exists(file))
            {
                CreateObjects(file);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public static string TransformXMLToHtmlPenTest(XDocument d)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            string text = string.Empty;
            using (StreamReader streamReader = new StreamReader("pentesttransform.xslt"))
                text = streamReader.ReadToEnd();

            using (XmlReader reader = XmlReader.Create(new StringReader(text)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(d.ToString())))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        public static string TransformXMLToHtml(XDocument d)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            string text = string.Empty;
            using (StreamReader streamReader = new StreamReader("transform.xslt"))
                text = streamReader.ReadToEnd();

            using (XmlReader reader = XmlReader.Create(new StringReader(text)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(d.ToString())))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        private static XDeclaration CreateDocumentDeclaration()
        {
            return new XDeclaration("1.1", "UTF-8", "yes");
        }
        private const string xmlStyle = "xml-stylesheet";
        private const string xsltFile = "transform.xslt";
        private const string xmlStyleInstruction = "type='text/xsl' href='transform.xslt'";
        public static void CopyReportFiles(string _filename)
        {
            string toCopyXslt = string.Empty;
            string newFileXslt = string.Empty;
            
            DirectoryInfo infoCopy = new DirectoryInfo(System.Windows.Forms.Application.ExecutablePath);
            toCopyXslt = Path.Combine(infoCopy.Parent.FullName, xsltFile);
            DirectoryInfo infoNew = new DirectoryInfo(_filename);
            newFileXslt = Path.Combine(infoNew.Parent.FullName, xsltFile);
            if (!File.Exists(newFileXslt))
                File.Copy(toCopyXslt, newFileXslt, true);
        }

        private static void CreateObjects(string file)
        {
            IList<ProjectPOCO> projects = GetDataObjects(file);
            _data = projects;
        }

        public static IList<ProjectPOCO> GetDataObjects(string file)
        {
            IList<ProjectPOCO> projects = new List<ProjectPOCO>();
            //TODO fix the crash on junk files.

            XElement projectData = XElement.Load(file);
            foreach (XElement x in projectData.Elements("project"))
            {
                ProjectPOCO p = new ProjectPOCO();
                p.Name = x.Element("name").Value;
                XElement contacts = x.Element("contacts");
                p.ISSO = contacts.Element("isso").Value;
                p.DevLead = contacts.Element("devLead").Value;
                p.ProductionURL = null == x.Element("productionURL") ? string.Empty : x.Element("productionURL").Value;
                XElement codeDetail = x.Element("codeDetail");
                XElement vulnerabilities = x.Element("vulnerabilities");
                IEnumerable<XElement> codeLanguages = null == codeDetail.Element("languages") ? new List<XElement>() : codeDetail.Element("languages").Elements("language");
                if (codeLanguages.Count() > 0)
                {
                    foreach (XElement item in codeLanguages.ToList())
                    {
                        if (null == p.CodeLanguages)
                            p.CodeLanguages = new List<string>();
                        p.CodeLanguages.Add(item.Value);
                    }
                }
                else
                {
                    p.CodeLanguages = new List<string>();
                }

                bool isScanned = false;
                if (bool.TryParse(codeDetail.Attribute("isCurrentlyScanned").Value, out isScanned))
                {
                    p.isCurrentlyScanned = isScanned;
                }
                else
                {
                    p.isCurrentlyScanned = false;
                }
                //need to implement extension method for parsing enums
                p.ScanConfiguration = (CodeScanType)Enum.Parse(typeof(CodeScanType), codeDetail.Attribute("scanConfiguration").Value);
                p.Repository = codeDetail.Element("repository").Value;
                IEnumerable<XElement> scanData = x.Element("codeAnalysis").Elements("scanDate");
                if (scanData.Count() > 0)
                {
                    foreach (XElement ele in scanData.ToList())
                    {
                        DateTime outDate = new DateTime();
                        if (DateTime.TryParse(ele.Value, out outDate))
                        {
                            if (null == p.StaticAnalysis)
                            {
                                p.StaticAnalysis = new List<StaticAnalysisPOCO>();
                            }
                            StaticAnalysisPOCO sa = new StaticAnalysisPOCO();
                            sa.AnalysisDate = outDate;
                            p.StaticAnalysis.Add(sa);
                        }
                    }
                }
                XElement penTests = x.Element("penetrationTests");
                IEnumerable<XElement> scans = penTests.Elements("scan");
                if (scans.Count() > 0)
                {
                    foreach (XElement e1 in scans.ToList())
                    {
                        DateTime outBegin = new DateTime();
                        DateTime outEnd = new DateTime();
                        PenetrationTestPOCO t = new PenetrationTestPOCO();
                        if (DateTime.TryParse(e1.Element("startDate").Value, out outBegin))
                        {
                            t.StartDate = outBegin;
                        }
                        else
                        {
                            continue;
                        }
                        if (DateTime.TryParse(e1.Element("endDate").Value, out outEnd))
                        {
                            t.EndDate = outEnd;
                        }
                        else
                        {
                            continue;
                        }
                        t.TesterName = e1.Element("tester").Value;
                        if (null == p.PenetrationTests)
                        {
                            p.PenetrationTests = new List<PenetrationTestPOCO>();
                        }
                        p.PenetrationTests.Add(t);
                    }
                }
                XElement dx = x.Element("documentation");
                if (null != dx)
                {
                    IEnumerable<XElement> doco = dx.Elements("item");
                    if (doco.Count() > 0)
                    {
                        if (null == p.DocumentationItems)
                            p.DocumentationItems = new List<Documentation>();
                        foreach (XElement d in doco.ToList())
                        {
                            Documentation dxi = new Documentation();
                            dxi.Details = string.IsNullOrEmpty(d.Element("details").Value) ? string.Empty : d.Element("details").Value;
                            DateTime outDoI = new DateTime();
                            if (DateTime.TryParse(d.Element("dateOfIssue").Value, out outDoI))
                            {
                                dxi.DateOfIssue = outDoI;
                            }
                            else
                            {
                                continue;
                            }
                            dxi.Category = string.IsNullOrEmpty(d.Element("category").Value) ? string.Empty : d.Element("category").Value;
                            p.DocumentationItems.Add(dxi);
                        }
                    }
                }
                else
                {
                    p.DocumentationItems = new List<Documentation>();
                }
                try //LEGACY HELPER TRY
                {
                    IEnumerable<XElement> items = vulnerabilities.Elements("item");
                    IList<IVulnerability> list = new List<IVulnerability>();
                    foreach (var item in items)
                    {
                        IVulnerability v = new Vulnerability();
                        string date1 = item.Element("discoveredDate").Value;
                        string date2 = item.Element("completedDate").Value;
                        Nullable<DateTime> nulled = null;
                        if (null != item.Element("title"))
                            v.Title = item.Element("title").Value;
                        v.DiscoveredDate = string.IsNullOrEmpty(date1) ? nulled : Convert.ToDateTime(date1);
                        v.CompletedDate = string.IsNullOrEmpty(date2) ? nulled : Convert.ToDateTime(date2);
                        v.Risk = item.Element("risk").Value;
                        v.Status = item.Element("status").Value;
                        v.Details = HttpUtility.HtmlDecode(item.Element("vulnerability").Value);
                        v.Tester = item.Element("tester").Value;
                        v.Identifier = Guid.Parse(item.Element("identifier").Value);
                        v.VulnTypeReported = item.Element("type").Value;
                        if (null != item.Element("flagged"))
                        {
                            v.isWeeklyReportItem = bool.Parse(item.Element("flagged").Value);
                        }
                        else
                        {
                            v.isWeeklyReportItem = false;
                        }
                        if (null != item.Element("cvss2"))
                        {
                            decimal dec = new decimal();
                            if (decimal.TryParse(item.Element("cvss2").Value, out dec))
                                v.CVSS = dec;
                            else
                                v.CVSS = 0;
                        }
                        list.Add(v);
                    }
                    if (list.Count() > 0)
                        p.Vulnerabilities = list;
                }
                catch { }

                projects.Add(p);
            }
            return projects;
        }

        public static ProjectPOCO GetCurrentProject(string p)
        {
            if (null == _data)
                return null;
            else
                return projectHelper._data.First(x => x.Name == p);
        }

        internal static void AddProject(string project)
        {
            lock (_lockObject)
            {
                if (null == _data)
                {
                    _data = new List<ProjectPOCO>();
                }
                ProjectPOCO p = new ProjectPOCO();
                p.Name = project;
                p.PenetrationTests = new List<PenetrationTestPOCO>();
                p.StaticAnalysis = new List<StaticAnalysisPOCO>();
                p.isCurrentlyScanned = false;
                p.ScanConfiguration = CodeScanType.None;
                _data.Add(p);
            }
        }

        internal static void ModifyDocumenation(ModTypes mod, string currentProject, Documentation data)
        {
            ProjectPOCO p = GetCurrentProject(currentProject);
            if (ModTypes.Add == mod)
            {
                if (!checkExists(p, data))
                {
                    p.DocumentationItems.Add(data);
                }
            }
            else if (ModTypes.Remove == mod)
            {
                if (checkExists(p, data))
                {
                    Documentation c = p.DocumentationItems.FirstOrDefault(x => x.Category == data.Category
                        && x.DateOfIssue == data.DateOfIssue && x.Details == data.Details);
                    if (null != c)
                        p.DocumentationItems.Remove(c);
                }
            }
        }

        private static bool checkExists(ProjectPOCO p, Documentation data)
        {
            if (null == p.DocumentationItems)
                p.DocumentationItems = new List<Documentation>();
            IEnumerable<Documentation> d = p.DocumentationItems.Where(x => x.Details == data.Details
                && x.DateOfIssue == data.DateOfIssue
                && x.Category == data.Category);
            if (0 == d.Count())
                return false;
            else
                return true;
        }


        internal static void ModifyStaticAnalysis(ModTypes _mod, string currentProject, StaticAnalysisPOCO details)
        {
            ProjectPOCO p = GetCurrentProject(currentProject);
            if (ModTypes.Add == _mod)
            {
                if (!checkExists(p, details))
                {
                    p.StaticAnalysis.Add(details);
                }
            }
            else if (ModTypes.Remove == _mod)
            {
                if (checkExists(p, details))
                {
                    StaticAnalysisPOCO c = p.StaticAnalysis.FirstOrDefault(x => x.AnalysisDate == details.AnalysisDate);
                    if (null != c)
                        p.StaticAnalysis.Remove(c);
                }
            }
        }

        private static bool checkExists(ProjectPOCO p, StaticAnalysisPOCO details)
        {
            if (null == p.StaticAnalysis)
                p.StaticAnalysis = new List<StaticAnalysisPOCO>();
            IEnumerable<StaticAnalysisPOCO> d = p.StaticAnalysis.Where(x => x.AnalysisDate == details.AnalysisDate);
            if (0 == d.Count())
                return false;
            else
                return true;
        }

        internal static void ModifyPenTestAnalysis(ModTypes _mod, string currentProject, PenetrationTestPOCO details)
        {
            ProjectPOCO p = GetCurrentProject(currentProject);
            if (ModTypes.Add == _mod)
            {
                if (!checkExists(p, details))
                {
                    p.PenetrationTests.Add(details);
                }
            }
            else if (ModTypes.Remove == _mod)
            {
                if (checkExists(p, details))
                {
                    PenetrationTestPOCO c = p.PenetrationTests.FirstOrDefault(x => x.EndDate == details.EndDate
                        && x.StartDate == details.StartDate && x.TesterName == details.TesterName);
                    if (null != c)
                        p.PenetrationTests.Remove(c);
                }
            }
        }

        internal static void ModifyVulnerabilityAnalysis(ModTypes _mod, string currentProject, IVulnerability details)
        {
            ProjectPOCO p = GetCurrentProject(currentProject);
            if (ModTypes.Add == _mod)
            {
                if (!checkExists(p, details,false))
                {
                    p.Vulnerabilities.Add(details);
                }
            }
            else if (ModTypes.Remove == _mod)
            {
                if (checkExists(p, details,false))
                {
                    IVulnerability c = p.Vulnerabilities.FirstOrDefault(x => x.Identifier == details.Identifier);
                    if (null != c)
                        p.Vulnerabilities.Remove(c);
                }
            }
            else if (ModTypes.Update == _mod)
            {
                if (checkExists(p, details, true))
                {
                    IVulnerability c = p.Vulnerabilities.FirstOrDefault(x => x.Identifier == details.Identifier);
                    if (null != c)
                        p.Vulnerabilities.Remove(c);
                    p.Vulnerabilities.Add(details);
                }
            }
        }


        private static bool checkExists(ProjectPOCO p, PenetrationTestPOCO details)
        {
            if (null == p.PenetrationTests)
                p.PenetrationTests = new List<PenetrationTestPOCO>();
            IEnumerable<PenetrationTestPOCO> d = p.PenetrationTests.Where(x => x.StartDate == details.StartDate
                && x.EndDate == details.EndDate
                && x.TesterName == details.TesterName);
            if (0 == d.Count())
                return false;
            else
                return true;
        }

        private static bool checkExists(ProjectPOCO p, IVulnerability details, bool isUpdate)
        {
            if (null == p.Vulnerabilities)
                p.Vulnerabilities = new List<IVulnerability>();
            if (!isUpdate)
            {
                IEnumerable<IVulnerability> d = p.Vulnerabilities.Where(x => x.Details == details.Details
                    && x.CompletedDate == details.CompletedDate
                    && x.CVSS == details.CVSS
                    && x.DiscoveredDate == details.DiscoveredDate
                    && x.Risk == details.Risk
                    && x.Status == details.Status
                    && x.Title == details.Title);
                if (0 == d.Count())
                    return false;
                else
                    return true;
            }
            else
            {
                IEnumerable<IVulnerability> d = p.Vulnerabilities.Where(x => x.Identifier == details.Identifier);
                if (0 == d.Count())
                    return false;
                else
                    return true;
            }
        }

        internal static bool SaveData(string fileLocation)
        {
            Tuple<string, XDocument> data = projectHelper.SaveData();
            CopyReportFiles(fileLocation);
            using (StreamWriter sw = new StreamWriter(fileLocation, false))
            {
                sw.Write(data.Item1);
            }
            string filenameHtml = fileLocation.Replace(".xml", ".html");
            string filenamePenTest = fileLocation.Replace(".xml", "pentest.html");
            using (StreamWriter sw = new StreamWriter(filenameHtml))
            {
                string content = TransformXMLToHtml(data.Item2);
                sw.Write(content);
            }
            using (StreamWriter sw = new StreamWriter(filenamePenTest))
            {
                string content = TransformXMLToHtmlPenTest(data.Item2);
                sw.Write(content);
            }
            return true;
        }

        internal static void UpdateDetails(string projectName, string isso, string devLead, bool isScanned, CodeScanType codeScanType, string productionURL, IList<string> languages, string repository)
        {
            ProjectPOCO p = projectHelper.GetCurrentProject(projectName);
            p.ISSO = isso;
            p.DevLead = devLead;
            p.isCurrentlyScanned = isScanned;
            p.ScanConfiguration = codeScanType;
            p.ProductionURL = productionURL;
            p.CodeLanguages = languages.ToList();
            p.Repository = repository;
        }

        internal static Tuple<bool, IList<string>> CanSave()
        {   
            IList<string> errors = new List<string>();
            foreach (ProjectPOCO project in Data)
            {
                if (string.IsNullOrEmpty(project.DevLead))
                    errors.Add("Dev Lead cannot be blank for " + project.Name);
                if (string.IsNullOrEmpty(project.ISSO))
                    errors.Add("ISSO/IAM cannot be blank for " + project.Name);
            }
            return new Tuple<bool, IList<string>>(errors.Count() > 0 ? false : true, errors);
        }

        internal static void Clear()
        {
            if (null != _data)
            {
                _data.Clear();
            }
        }

      
    }
}
