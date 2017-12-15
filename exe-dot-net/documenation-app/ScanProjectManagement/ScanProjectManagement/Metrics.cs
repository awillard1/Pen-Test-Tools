using ScanProjectManagement.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace ScanProjectManagement
{
    public partial class Metrics : Form
    {
        private const string xmlStyle = "xml-stylesheet";
        private const string xsltFile = "weeklyreport.xslt";
        private const string xmlStyleInstruction = "type='text/xsl' href='weeklyreport.xslt'";

        DayOfWeek dOW { get; set; }

        XDocument rootExport;
        XDocument createDocument()
        {
            XProcessingInstruction instruction = new XProcessingInstruction(xmlStyle, xmlStyleInstruction);
            XDocument document = new XDocument();//instruction);
            document.Declaration = CreateDocumentDeclaration();
            XElement root = new XElement("report");
            document.Add(root);
            return document;
        }
        private static XDeclaration CreateDocumentDeclaration()
        {
            return new XDeclaration("1.1", "UTF-8", "yes");
        }

        public Metrics()
        {
            InitializeComponent();
            dOW = GetFirstDayForCalendar();
            monthCalendar1.FirstDayOfWeek =  GetDay();

        }
        private IList<ProjectPOCO> projects { get; set; }
        string fileLocation { get; set; }
        List<FileInfo> files { get; set; }
        
        private void browseForDir_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            using (FolderBrowserDialog d = new FolderBrowserDialog())
            {
                dr = d.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    fileLocation = d.SelectedPath;
                    directoryText.Text = fileLocation;
                    files = GetFiles(fileLocation);
                    foreach (var x in files)
                        logsToProcess.Items.Add(x.Name);
                }
            }
        }

        private List<FileInfo> GetFiles(string _fileLocation)
        {
            List<FileInfo> files = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(_fileLocation);
            foreach (FileInfo f in di.GetFiles("*." + Logger.LoggerTypes.sshlog.ToString()))
                files.Add(f);
            foreach (FileInfo f in di.GetFiles("*." + Logger.LoggerTypes.nmaplog.ToString()))
                files.Add(f);
            foreach (FileInfo f in di.GetFiles("*." + Logger.LoggerTypes.browserlog.ToString()))
                files.Add(f);

            return files;
        }

        private void Metrics_Load(object sender, EventArgs e)
        {
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (null == projects)
            {   
                MessageBox.Show("Please load the correct file", "Error");
                return;
            }
            if (sender is MonthCalendar)
            {
                MonthCalendar c = sender as MonthCalendar;
                if (null != c)
                {
                    rootExport = createDocument();
                    DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                    Calendar cal = dfi.Calendar;
                    dfi.FirstDayOfWeek = dOW;

                    int week = cal.GetWeekOfYear(e.Start, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                    DateTime day = GetDates(week, e.Start, dfi);
                    SetLabel(day);
                    List<SummaryObjects.VulnerabilitySummary> vulns = ProcessFindings(day);
                    ProcessWeeklyVulns(vulns, day);
                }
            }
        }

        private DayOfWeek GetFirstDayForCalendar()
        {
            return configurationHelper.GetDayOfWeek();
        }

        private void ProcessWeeklyVulns(List<SummaryObjects.VulnerabilitySummary> vulns, DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Vulnerabilities:");
            bool hasVulns = false;
            foreach (var s in vulns)
            {
                if (s.VulnerableItem.Count() > 0)
                {
                    hasVulns = true;
                    int last=1;
                    int count = s.VulnerableItem.Count();
                    foreach (var v in s.VulnerableItem.ToList())
                    {
                        XElement parent = new XElement("vulnerabilities");
                        parent.Add(new XElement("project", new XCData(s.Project)));
                        parent.Add(new XElement("title", new XCData(v.Title)));
                        string dte = string.Empty;
                        if (null!=v.DiscoveredDate)
                            dte = Convert.ToDateTime(v.DiscoveredDate.ToString()).ToShortDateString();
                        parent.Add(new XElement("date", new XCData(dte)));
                        rootExport.Element("report").Add(parent);
                        sb.AppendLine(s.Project + ": " + v.Title);
                        if (last == count)
                            sb.Append(Environment.NewLine);
                        last++;

                    }
                }
            }
            if (!hasVulns)
                sb.AppendLine("None to report" + Environment.NewLine);
            sb.AppendLine("Other Updates:");
            bool hasUpdates = false;
            foreach (var s in vulns)
            {
                if (s.DocumentationItem.Count() > 0)
                {
                    hasUpdates = true;
                    int last = 1;
                    int count = s.DocumentationItem.Count();
                    foreach (var v in s.DocumentationItem.ToList())
                    {
                        XElement parent = new XElement("documentation");
                        parent.Add(new XElement("project", new XCData(s.Project)));
                        parent.Add(new XElement("details", new XCData(v.Details)));
                        string dte = v.DateOfIssue.ToShortDateString();
                        parent.Add(new XElement("date", new XCData(dte)));
                        rootExport.Element("report").Add(parent);
                        sb.AppendLine(s.Project + ": " + v.Details);
                        if (last == count)
                            sb.Append(Environment.NewLine);
                        last++;

                    }
                }
            }
            if (!hasUpdates)
                sb.AppendLine("None to report"+ Environment.NewLine);
            
            string weeklyDetails = GetLoggingForWeekly(dt);
            rootExport.Element("report").Add(new XElement("summary", new XCData(weeklyDetails)));
            SaveWeeklyReport(rootExport);
            sb.Append(weeklyDetails);
            rtfReport.Text = sb.ToString();
        }
        public static string TransformXMLToReport(XDocument d)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            string text = string.Empty;
            using (StreamReader streamReader = new StreamReader("weeklyreport.xslt"))
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

        private void SaveWeeklyReport(XDocument doc)
        {
            DirectoryInfo di = new DirectoryInfo(fileLocation);
            using (StreamWriter sw = new StreamWriter(di.Parent.FullName + "\\" +"weeklyReport.html"))
            {
                string content = TransformXMLToReport(doc);
                sw.Write(content);
            }
        }

        private string GetLoggingForWeekly(DateTime day)
        {
            StringBuilder sb = new StringBuilder();
            DateTime end = day.AddDays(7);
            List<FileInfo> _files = new List<FileInfo>();
            _files = GetFiles(info.DirectoryName);
            List<SSHLogCollection> ssh = sshLogProcessing(_files.Where(x => x.Extension == "." + Logger.LoggerTypes.sshlog.ToString()).ToList(),day,end);
            List<NmapLogCollection> nmap = nmapLogProcessing(_files.Where(x => x.Extension == "." + Logger.LoggerTypes.nmaplog.ToString()).ToList(),day,end);
            List<BrowserLogCollection> browser = browserLogProcessing(_files.Where(x => x.Extension == "." + Logger.LoggerTypes.browserlog.ToString()).ToList(),day,end );

            sb.AppendLine("Recon:");
            long counter = 0;
            foreach (var v in ssh.Where(x => x.successfulLogin > 0).ToList())
                counter += v.successfulLogin;

            sb.AppendLine("SSH Connections: Successful - " + counter.ToString());
            counter = 0;
            foreach (var v in ssh.Where(x => x.failedLogin > 0).ToList())
                counter += v.failedLogin;

            sb.AppendLine("SSH Connections: Failed - " + counter.ToString());
            sb.AppendLine("Total Nmap Scans: " + nmap.Count().ToString());
            sb.AppendLine("Total Sites Tested: " + browser.Count());
            return sb.ToString();
        }
        
        private List<SummaryObjects.VulnerabilitySummary> ProcessFindings(DateTime day)
        {
            List<SummaryObjects.VulnerabilitySummary> v = new List<SummaryObjects.VulnerabilitySummary>();
            if (null == projects)
                return null;
            DateTime end = day.AddDays(7);
            
            foreach (ProjectPOCO p in projects)
            {
                SummaryObjects.VulnerabilitySummary s = new SummaryObjects.VulnerabilitySummary();
                s.VulnerableItem = new List<IVulnerability>();
                s.DocumentationItem = new List<Documentation>();
                if (null != p.Vulnerabilities)
                {
                    IEnumerable<IVulnerability> v1 = p.Vulnerabilities.Where(x => x.isWeeklyReportItem == true
                        && x.DiscoveredDate >= day
                        && x.DiscoveredDate < end);
                    s.Project = p.Name;
                    if (null != v1.ToList())
                    {
                        foreach (IVulnerability a in v1.ToList())
                            s.VulnerableItem.Add(a);
                    }
                }

                if (null != p.DocumentationItems)
                {
                    IEnumerable<Documentation> d1 = p.DocumentationItems.Where(x => x.Category == "Weekly Update"
                        && x.DateOfIssue >= day
                        && x.DateOfIssue < end);
                    s.Project = p.Name;
                    if (null != d1.ToList())
                    {
                        foreach (Documentation a in d1.ToList())
                            s.DocumentationItem.Add(a);
                    }
                }
                v.Add(s);
            }
            return v;
        }

        private void SetLabel(DateTime day)
        {
            weekOfLabel.Text = "Weekly Report - " + day.ToShortDateString() + " - "
                + day.AddDays(6).ToShortDateString();
        }

        private DateTime GetDates(int week, DateTime dateTime, DateTimeFormatInfo dfi)
        {
            DayOfWeek firstDay = dfi.FirstDayOfWeek;
            DateTime firstDayInWeek = dateTime.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (sender is MonthCalendar)
            {
                MonthCalendar c = sender as MonthCalendar;
                if (null != c)
                {
                    
                }
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private FileInfo info { get; set; }
        private void LoadData()
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                DialogResult dr = openFileDialog1.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    string file = openFileDialog1.FileName;
                    fileLocation = file;
                    try
                    {
                        info = new FileInfo(file);
                        projects = projectHelper.GetDataObjects(file);
                    }
                    catch { }
                }
            }
        }

        Day GetDay()
        {
            Day retval = Day.Monday;

                if (Enum.IsDefined(typeof(DayOfWeek), dOW.ToString()))
                {
                    retval = (Day)Enum.Parse(typeof(Day), dOW.ToString(), true);
                }
                else
                {
                    retval = Day.Monday;
                }

            return retval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
            monthCalendar1.SetDate(DateTime.Today);
            monthCalendar1_DateChanged(monthCalendar1, new DateRangeEventArgs(DateTime.Today, DateTime.Today));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null != files)
            {
                List<SSHLogCollection> ssh = sshLogProcessing(files.Where(x => x.Extension == "." + Logger.LoggerTypes.sshlog.ToString()).ToList());
             //   List<NmapLogCollection> nmap = nmapLogProcessing(files.Where(x => x.Extension == "." + Logger.LoggerTypes.nmaplog.ToString()));
                List<BrowserLogCollection> browser = browserLogProcessing(files.Where(x => x.Extension == "." + Logger.LoggerTypes.browserlog.ToString()).ToList());
                ProcessAllSSHToChart(ssh);
                ProcessAllBrowserToChart(browser);
                chart1.DataBind();
                browserChart.DataBind();
            }
        }

        private void ProcessAllBrowserToChart(List<BrowserLogCollection> browser)
        {
            if (null != browser)
                browserChart.DataSource = browser.ToList();
        }

      
        private void ProcessAllSSHToChart(List<SSHLogCollection> ssh)
        {
            if (null != ssh)
            {
                chart1.DataSource = ssh.ToList(); 
            }
        }

        private List<BrowserLog> _getBData(List<FileInfo> p)
        {

            List<BrowserLog> logs = new List<BrowserLog>();
            if (null != p)
            {
                if (p.Count() > 0)
                {
                    foreach (FileInfo f in p.ToList())
                    {
                        if (File.Exists(f.FullName))
                        {
                            XElement data = XElement.Load(f.FullName);
                            try
                            {
                                foreach (XElement x in data.Elements())
                                {
                                    var s = x.Name.ToString();

                                    if (s == "BrowserLog")
                                        logs.Add(x.FromXElement<BrowserLog>());
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            return logs;
        }


        private List<BrowserLogCollection> browserLogProcessing(List<FileInfo> p, DateTime start, DateTime end)
        {
            List<BrowserLog> logs = new List<BrowserLog>();
            logs = _getBData(p);

            if (logs.Count() > 0)
            {
                List<BrowserLogCollection> collection = new List<BrowserLogCollection>();
                foreach (BrowserLog log in logs.Where(z => z.LogDateTime>=start 
                    && z.LogDateTime < end))
                {
                    bool exists = collection.Where(x => x.Url == log.GetUrl()).Count() > 0;
                    if (exists)
                    {
                        BrowserLogCollection item = collection.Where(x => x.Url == log.GetUrl()).First();
                        item.AccessedCount++;
                    }
                    else
                    {
                        BrowserLogCollection item = new BrowserLogCollection();
                        item.Url = log.GetUrl();
                        item.AccessedCount++;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            return new List<BrowserLogCollection>();
        }


        private List<BrowserLogCollection> browserLogProcessing(List<FileInfo> p)
        {

            List<BrowserLog> logs = new List<BrowserLog>();
            logs = _getBData(p);

            if (logs.Count() > 0)
            {
                List<BrowserLogCollection> collection = new List<BrowserLogCollection>();
                foreach (BrowserLog log in logs.Where(z => z.LogDateTime.Month == dte1.Value.Month
                    && z.LogDateTime.Year == dte1.Value.Year))
                {
                    bool exists = collection.Where(x => x.Url == log.GetUrl()).Count() > 0;
                    if (exists)
                    {
                        BrowserLogCollection item = collection.Where(x => x.Url == log.GetUrl()).First();
                        item.AccessedCount++;
                    }
                    else
                    {
                        BrowserLogCollection item = new BrowserLogCollection();
                        item.Url = log.GetUrl();
                        item.AccessedCount++;
                        collection.Add(item);
                    }
                }
                return collection;
            }

            return new List<BrowserLogCollection>();
        }

        private List<NmapLog> getNLogs(List<FileInfo> p)
        {
            List<NmapLog> nmaplogs = new List<NmapLog>();
            if (null != p)
            {                
                if (p.Count() > 0)
                {
                    foreach (FileInfo f in p)
                    {
                        if (File.Exists(f.FullName))
                        {
                            XElement data = XElement.Load(f.FullName);
                            try
                            {
                                foreach (XElement x in data.Elements())
                                {
                                    var s = x.Name.ToString();

                                    if (s == "NmapLog")
                                        nmaplogs.Add(x.FromXElement<NmapLog>());
                                }
                            }
                            catch { }
                        }
                    }
                }
            }   
            return nmaplogs;
        }

         private List<NmapLogCollection> nmapLogProcessing(List<FileInfo> p, DateTime start, DateTime end)
        {
            List<NmapLog> nmaplogs = new List<NmapLog>();
            nmaplogs = getNLogs(p);
                if (nmaplogs.Count() > 0)
                {
                    List<NmapLogCollection> collection = new List<NmapLogCollection>();
                    foreach (NmapLog log in nmaplogs.Where(z => z.LogDateTime>=start
                        && z.LogDateTime < end))
                    {
                       
                       
                            NmapLogCollection item = new NmapLogCollection();
                            item.Count = 1;
                            collection.Add(item);
                       
                    }
                    return collection;
                }
            return new List<NmapLogCollection>();
         }

         private List<NmapLogCollection> nmapLogProcessing(List<FileInfo> p)
         {
             List<NmapLog> nmaplogs = new List<NmapLog>();
             if (nmaplogs.Count() > 0)
             {
                 List<NmapLogCollection> collection = new List<NmapLogCollection>();
                 foreach (NmapLog log in nmaplogs.Where(z => z.LogDateTime.Month == dte1.Value.Month
                     && z.LogDateTime.Year == dte1.Value.Year))
                 {


                     NmapLogCollection item = new NmapLogCollection();
                     item.Count = 1;
                     collection.Add(item);

                 }
                 return collection;
             }
             return new List<NmapLogCollection>();
         }

         private List<SSHLog> getSLogs(List<FileInfo> p)
         {
             List<SSHLog> sshlogs = new List<SSHLog>();
             if (null != p)
             {

                 if (p.Count() > 0)
                 {
                     foreach (FileInfo f in p)
                     {
                         if (File.Exists(f.FullName))
                         {
                             XElement data = XElement.Load(f.FullName);
                             try
                             {
                                 foreach (XElement x in data.Elements())
                                 {
                                     var s = x.Name.ToString();

                                     if (s == "SSHLog")
                                         sshlogs.Add(x.FromXElement<SSHLog>());
                                 }
                             }
                             catch { }
                         }
                     }
                 }
             }
             return sshlogs;
         }

        private List<SSHLogCollection> sshLogProcessing(List<FileInfo> p, DateTime start, DateTime end)
        {
            List<SSHLog> sshlogs = new List<SSHLog>();
            sshlogs = getSLogs(p);
            if (sshlogs.Count() > 0)
            {
                List<SSHLogCollection> collection = new List<SSHLogCollection>();
                foreach (SSHLog log in sshlogs.Where(z => z.LogDate >= start
                    && z.LogDate < end))
                {
                    bool exists = collection.Where(x => x.IPHostname == log.IPHostname).Count() > 0;
                    if (exists)
                    {
                        SSHLogCollection item = collection.Where(x => x.IPHostname == log.IPHostname).First();
                        if (log.wasSuccessful)
                            item.successfulLogin++;
                        else
                            item.failedLogin++;
                    }
                    else
                    {
                        SSHLogCollection item = new SSHLogCollection();
                        item.IPHostname = log.IPHostname;
                        if (log.wasSuccessful)
                            item.successfulLogin++;
                        else
                            item.failedLogin++;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            return new List<SSHLogCollection>();
        }


        private List<SSHLogCollection> sshLogProcessing(List<FileInfo> p)
        {
            List<SSHLog> sshlogs = new List<SSHLog>();
            sshlogs = getSLogs(p);
            if (sshlogs.Count() > 0)
            {
                List<SSHLogCollection> collection = new List<SSHLogCollection>();
                foreach (SSHLog log in sshlogs.Where(z => z.LogDate.Month == dte1.Value.Month
                    && z.LogDate.Year == dte1.Value.Year))
                {
                    bool exists = collection.Where(x => x.IPHostname == log.IPHostname).Count() > 0;
                    if (exists)
                    {
                        SSHLogCollection item = collection.Where(x => x.IPHostname == log.IPHostname).First();
                        if (log.wasSuccessful)
                            item.successfulLogin++;
                        else
                            item.failedLogin++;
                    }
                    else
                    {
                        SSHLogCollection item = new SSHLogCollection();
                        item.IPHostname = log.IPHostname;
                        if (log.wasSuccessful)
                            item.successfulLogin++;
                        else
                            item.failedLogin++;
                        collection.Add(item);
                    }
                }
                return collection;
            }
            return new List<SSHLogCollection>();
        }

        private void CheckExists(SSHLog log)
        {
            throw new NotImplementedException();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rtfReport_MouseUp(object sender, MouseEventArgs e)
        {
            if (System.Windows.Forms.MouseButtons.Right == e.Button)
            {
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Select All and Copy");
                menuItem.Click += new EventHandler(CopyText);
                menuItem.BarBreak = false;
                contextMenu.MenuItems.Add(menuItem);
                rtfReport.ContextMenu = contextMenu;
            }
        }
        void CopyText(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtfReport.Text))
            {
                rtfReport.SelectAll();
                Clipboard.SetText(rtfReport.SelectedText);
                rtfReport.DeselectAll();
            }
        }

        private void exportWeekly_Click(object sender, EventArgs e)
        {

        }
    }
}
