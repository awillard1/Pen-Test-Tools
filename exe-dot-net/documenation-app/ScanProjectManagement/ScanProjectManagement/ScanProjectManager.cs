using ScanProjectManagement.Business;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanProjectManagement
{
    public partial class ScanProjectManager : Form
    {
        private string fileLocation { get; set; }
        private bool firstLoad { get; set; }
        public ScanProjectManager()
        {
            InitializeComponent();
            
            port.TextChanged += port_TextChanged;
            foreach (string v in configurationHelper.documentationCategories.ToList())
                issueType.Items.Add(v);

            inviteLocations.Text = "[Location] - " + new TemporaryDirectory().DirectoryPath;

            foreach (string vuln in VulnerabilityTypes.VulnerabilityType.ToList())
            {
                vulnTypeItems.Items.Add(vuln);
            }

            foreach (PenetrationTesterAccount account in PenetrationTesters.Testers)
            {
                penTesterUserList.Items.Add(account.Name);
                penTesterDiscover.Items.Add(account.Name);
            }
            dataGridView1.AutoGenerateColumns = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearVulnForm();
            populateUI(true);
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddNewProjectForm form = new AddNewProjectForm())
                form.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                DialogResult dr = openFileDialog1.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    string file = openFileDialog1.FileName;
                    fileLocation = file;
                    projectHelper.Load(file);
                    populateUI(false);
                }
            }
        }

        public void AddProjects()
        {
            string current = ddlProjects.Text;
            ResetProjects();
            ddlProjects.Text = current;
        }

        private void populateUI(bool hasChanged)
        {
            if (null != projectHelper.Data)
            {
                if (projectHelper.Data.Count > 0)
                {
                    if (!hasChanged)
                        ResetProjects();
                }
                BindProjectData();
            }
        }

        private void BindProjectData()
        {
            ProjectPOCO p = projectHelper.Data.First(x => x.Name == ddlProjects.Text);
            if (null != p)
            {
                //Clear Note
                ResetDocumentationInput();
                isso.Text = p.ISSO;
                developmentLead.Text = p.DevLead;
                codeScanned.Checked = p.isCurrentlyScanned;
                repository.Text = p.Repository;
                productionUrlText.Text = p.ProductionURL;
                BindDocumentation(p);
                BindLanguages(p);
                BindPenTest(p);
                BindStaticAnalysis(p);
                BindCodeScanType(p);
                BindVulnerabilities(p);
            }
        }

        private void ResetDocumentationInput()
        {
            documentationDetail.Text = string.Empty;
            issueType.SelectedIndex = 0;
            issueDatePicker.Value = DateTime.Now;
        }

        private void ResetProjects()
        {
            ClearVulnForm();
            ddlProjects.SelectedIndexChanged -= ddlProjects_SelectedIndexChanged;
            GenerateDropListProjects();
            ddlProjects.SelectedIndexChanged += ddlProjects_SelectedIndexChanged;
        }

        private void BindLanguages(ProjectPOCO p)
        {
            ClearLanguages();
            if (null != p.CodeLanguages)
            {
                foreach (string s in p.CodeLanguages)
                {
                    int index = codeLanguages.Items.IndexOf(s);

                    if (index > 0)
                    {
                        codeLanguages.SetItemChecked(index, true);
                    }
                }
            }
        }

        private void ClearLanguages()
        {
            for (int i = 0; i < codeLanguages.Items.Count; i++)
                codeLanguages.SetItemCheckState(i, CheckState.Unchecked);
        }

        private void BindCodeScanType(ProjectPOCO p)
        {
            if (CodeScanType.Automated == p.ScanConfiguration)
                scanTypeAutomated.Checked = true;
            else if (CodeScanType.Hybrid == p.ScanConfiguration)
                scanTypeAutoMan.Checked = true;
            else if (CodeScanType.Manual == p.ScanConfiguration)
                scanTypeManual.Checked = true;
            else if (CodeScanType.None == p.ScanConfiguration)
                scanTypeNone.Checked = true;
        }

        private void BindVulnerabilities(ProjectPOCO p)
        {
            if (null != p.Vulnerabilities)
                dataGridView1.DataSource = p.Vulnerabilities.ToList().OrderBy(x => x.Risk).ToList();
            else
                dataGridView1.DataSource = new List<IVulnerability>();
            dataGridView1.Columns["VulnerabilityID"].Visible = false;
        }       

        private void BindPenTest(ProjectPOCO p)
        {
            SetPenTestDataviewProperties();
            if (null != p.PenetrationTests)
                penTesterDataView.DataSource = p.PenetrationTests.ToList();
            else
                penTesterDataView.DataSource = new List<PenetrationTestPOCO>();
        }

        private void SetPenTestDataviewProperties()
        {
            penTesterDataView.Columns[0].DataPropertyName = "StartDate";
            penTesterDataView.Columns[1].DataPropertyName = "EndDate";
            penTesterDataView.Columns[2].DataPropertyName = "TesterName";
        }

        private void BindDocumentation(ProjectPOCO project)
        {
            if (null != project.DocumentationItems)
                notesDataView.DataSource = project.DocumentationItems.ToList();
            else
                notesDataView.DataSource = new List<Documentation>();
        }

        private void BindStaticAnalysis(ProjectPOCO p)
        {
            SetStaticAnalysisDataviewProperties();
            if (null != p.StaticAnalysis)
                staticCodeAnalysisData.DataSource = p.StaticAnalysis.ToList();
            else
                staticCodeAnalysisData.DataSource = new List<StaticAnalysisPOCO>();
        }

        private void SetStaticAnalysisDataviewProperties()
        {
            staticCodeAnalysisData.Columns[0].DataPropertyName = "AnalysisDate";
        }

        private void GenerateDropListProjects()
        {
            SetProjectDataProperties();
            BindingSource source = new BindingSource();
            source.DataSource = projectHelper.Data;
            ddlProjects.DataSource = source;
        }

        private void SetProjectDataProperties()
        {
            ddlProjects.DisplayMember = "Name";
        }

        private void addCodeAnalysisDate_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
                ShowError("Unable to find selected project. Please make sure you have selected a project");
            else
            {
                StaticAnalysisPOCO data = new StaticAnalysisPOCO();
                data.AnalysisDate = staticScanCalendar.SelectionStart;
                projectHelper.ModifyStaticAnalysis(ModTypes.Add, ddlProjects.Text, data);
                BindStaticAnalysis(project);
            }
        }

        private void addPenTestDate_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (string.IsNullOrEmpty(penTesterUserList.Text))
            {
                ShowError("Unable to add test." + Environment.NewLine + "Please add tester name.");
                return;
            }
            if (null == project)
            {
                ShowError("Unable to find selected project. Please make sure you have selected a project");
            }
            else
            {
                PenetrationTestPOCO data = new PenetrationTestPOCO();
                data.TesterName = penTesterUserList.Text;
                data.EndDate = penTestTimePickerEnd.Value;
                data.StartDate = penTestTimePickerStart.Value;
                if (data.StartDate >= data.EndDate)
                {
                    ShowError("Start Date must be less than End Date");
                    return;
                }
                else
                {
                    projectHelper.ModifyPenTestAnalysis(ModTypes.Add, ddlProjects.Text, data);
                    BindPenTest(project);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tuple<bool, IList<string>> resultForSave = projectHelper.CanSave();
            if (!resultForSave.Item1)
            {
                StringBuilder s = new StringBuilder();
                foreach (string err in resultForSave.Item2)
                {
                    s.Append(err + Environment.NewLine);
                }
                ShowError(s.ToString());
                return;
            }

            if (string.IsNullOrEmpty(fileLocation))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xml files (*.xml)|*.xml";
                DialogResult result = sfd.ShowDialog();
                if (DialogResult.OK == result)
                {
                    bool isSaved = projectHelper.SaveData(sfd.FileName);
                    fileLocation = sfd.FileName;
                }
            }
            else
            {
                bool result = projectHelper.SaveData(fileLocation);
                if (!result)
                    ShowError("Unable to save; file not found");
                else
                    ShowMessage("File Saved", "Save Successful");
            }
        }

        private void saveDetails_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("Please select a project before adding project details.");
                return;
            }
            CodeScanType codeScanType = CodeScanType.None;
            if (scanTypeAutoMan.Checked)
                codeScanType = CodeScanType.Hybrid;
            else if (scanTypeAutomated.Checked)
                codeScanType = CodeScanType.Automated;
            else if (scanTypeManual.Checked)
                codeScanType = CodeScanType.Manual;
            else
                codeScanType = CodeScanType.None;

            IList<string> selectedLanguages = new List<string>();
            foreach (var c in codeLanguages.CheckedItems)
            {
                selectedLanguages.Add(c.ToString());
            }

            projectHelper.UpdateDetails(project.Name, isso.Text, developmentLead.Text, codeScanned.Checked, codeScanType, productionUrlText.Text, selectedLanguages, repository.Text);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.Exit();
        }

        private void newMenuItem1_Click(object sender, EventArgs e)
        {
            //TODO These methods won't work for clearing the data. Doh.
            projectHelper.Clear();
            staticCodeAnalysisData.DataSource = null;
            notesDataView.DataSource = null;
            penTesterDataView.DataSource = null;
            ddlProjects.Items.Clear();
            isso.Text = string.Empty;
            developmentLead.Text = string.Empty;
            productionUrlText.Text = string.Empty;
            SetTimePickers();
            codeScanned.Checked = false;
            scanTypeNone.Checked = true;
            repository.Text = string.Empty;
            ClearLanguages();
            Vulnerabilities.Clear();
        }

        private void SetTimePickers()
        {
            DateTime t = new DateTime();
            t = DateTime.Now;
            penTestTimePickerStart.Value = t;
            penTestTimePickerEnd.Value = t;
            staticScanCalendar.SelectionStart = t;
            staticScanCalendar.SelectionEnd = t;
        }

        private void ScanProjectManager_Load(object sender, EventArgs e)
        {
            SetTimePickers();
            SetProgrammingLanguages();
            DialogResult dr = MessageBox.Show("Attempt to obtain public ip address?", "Request for data", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == dr)
                IPAddress.AllowAccessForIP(true);
            else
                IPAddress.AllowAccessForIP(false);
            BindTesterIP();
        }

        private void BindTesterIP()
        {
            List<string> ipdata = IPAddress.GetIPAddresses();
            nmapTestersIP.DataSource = ipdata;
            sshTestingIP.DataSource = ipdata;
            browserTestingIP.DataSource = ipdata;
        }

        private void SetProgrammingLanguages()
        {
            foreach (string s in configurationHelper.programmingLanguages.ToList())
                codeLanguages.Items.Add(s);
        }

        private void addNote_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (string.IsNullOrEmpty(documentationDetail.Text))
            {
                ShowError("Unable to add documenation." + Environment.NewLine + "Please add details about the issue.");
                return;
            }
            else if (string.IsNullOrEmpty(issueType.Text))
            {
                ShowError("Unable to add documentation" + Environment.NewLine + "Please select a category.");
                return;
            }
            if (null == project)
            {
                ShowError("Unable to find selected project. Please make sure you have selected a project");
            }
            else
            {
                Documentation data = new Documentation();
                data.Category = issueType.Text;
                DateTime d = Convert.ToDateTime(issueDatePicker.Value.ToShortDateString());
                data.DateOfIssue = d;
                data.Details = documentationDetail.Text;

                projectHelper.ModifyDocumenation(ModTypes.Add, ddlProjects.Text, data);
                BindDocumentation(project);
            }
        }

        private void meetingCreation_Click(object sender, EventArgs e)
        {
            ProjectPOCO p = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == p)
                ShowError("Unable to generate invites, please select a project");
            else
            {
                if (null != p.PenetrationTests)
                {
                    foreach (PenetrationTestPOCO d in p.PenetrationTests.ToList())
                    {
                        MailAddressCollection mac = new MailAddressCollection();
                        mac.Add(new MailAddress(PenetrationTesters.getAccount(d.TesterName).Email));
                        MailMessage m = MeetingInvite.CreateMeetingRequest(d.StartDate,
                            d.EndDate,
                            "Penetration Test for " + p.Name,
                            "Meeting Request For Penetration Test",
                            "Remote",
                            ApplicationUserData.User.Name,
                            ApplicationUserData.User.Email,
                            mac);
                        m.RawMessage();
                    }
                }
            }
        }

        private void deleteTest_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            if (penTesterDataView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection collection = penTesterDataView.SelectedRows;
                foreach (DataGridViewRow gvr in collection)
                {
                    DateTime start = (DateTime)gvr.Cells[0].Value;
                    DateTime end = (DateTime)gvr.Cells[1].Value;
                    string tester = gvr.Cells[2].Value.ToString();
                    PenetrationTestPOCO obj = new Business.PenetrationTestPOCO();
                    obj.TesterName = tester;
                    obj.StartDate = start;
                    obj.EndDate = end;
                    projectHelper.ModifyPenTestAnalysis(Business.ModTypes.Remove, project.Name, obj);
                }
                BindPenTest(project);
            }
            else
                ShowError("No tests were selected to remove");
        }

        private void deleteAnalysis_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            if (staticCodeAnalysisData.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection collection = staticCodeAnalysisData.SelectedRows;
                foreach (DataGridViewRow gvr in collection)
                {
                    DateTime date = (DateTime)gvr.Cells[0].Value;
                    StaticAnalysisPOCO obj = new StaticAnalysisPOCO();
                    obj.AnalysisDate = date;
                    projectHelper.ModifyStaticAnalysis(ModTypes.Remove, project.Name, obj);
                }
                BindStaticAnalysis(project);
            }
            else
                ShowError("No scans were selected to remove");
        }

        private void deleteDocumentation_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            if (notesDataView.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection collection = notesDataView.SelectedRows;
                foreach (DataGridViewRow gvr in collection)
                {
                    DateTime date = (DateTime)gvr.Cells[1].Value;
                    Documentation obj = new Documentation();
                    obj.DateOfIssue = date;
                    obj.Category = gvr.Cells[2].Value.ToString();
                    obj.Details = gvr.Cells[0].Value.ToString();
                    projectHelper.ModifyDocumenation(ModTypes.Remove, project.Name, obj);
                }
                BindDocumentation(project);
            }
            else
                ShowError("No documentation was selected to remove");
        }

        private void ShowError(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowMessage(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void aboutScanManagementMetadateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutProduct form = new AboutProduct())
            {
                form.ShowDialog();
            }
        }

        //This is the Activity Section
        private void protocolItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (protocolItems.SelectedItem.ToString() == "https")
                port.Text = "443";
            else if (protocolItems.SelectedItem.ToString() == "http")
                port.Text = "80";
            else if (protocolItems.SelectedItem.ToString() == "ftp")
                port.Text = "21";
        }
        void port_TextChanged(object sender, EventArgs e)
        {
            if (sender is NumericPortTextBox)
            {
                NumericPortTextBox p = sender as NumericPortTextBox;
                if (null != p)
                {
                    if (p.TestRange())
                    {
                        MessageBox.Show("Value must be between 1 and 65535", "Error");
                        p.Text = p.previousValue;

                    }
                }
            }
        }

        List<string> errorsForBrowser = new List<string>();
        List<string> errorsForSSH = new List<string>();
        List<string> errorsForNmap = new List<string>();
        private bool HasErrorsForSSH()
        {
            string hostmsg = ipHostname.TestTextBox("IP/Hostname");
            string usernamemsg = sshUsername.TestTextBox("SSH Username");
            string passwordmsg = sshUsername.TestTextBox("SSH Password");
            string usersIPmsg = sshTestingIP.TestComboBox("Tester's IP");
            if (!string.IsNullOrEmpty(hostmsg))
                errorsForSSH.Add(hostmsg);
            if (!string.IsNullOrEmpty(usernamemsg))
                errorsForSSH.Add(usernamemsg);
            if (!string.IsNullOrEmpty(passwordmsg))
                errorsForSSH.Add(passwordmsg);
            if (!string.IsNullOrEmpty(usersIPmsg))
                errorsForSSH.Add(usersIPmsg);
            return errorsForSSH.Count > 0 ? true : false;
        }

        private bool HasErrorsForBrowser()
        {
            string hostmsg = ipHostname.TestTextBox("IP/Hostname");
            string portmsg = port.TestTextBox("Port");
            string protocolmsg = protocolItems.TestComboBox("Protocol"); ;
            string usersIPmsg = browserTestingIP.TestComboBox("Tester's IP"); ;
            if (!string.IsNullOrEmpty(hostmsg))
                errorsForBrowser.Add(hostmsg);
            if (!string.IsNullOrEmpty(portmsg))
                errorsForBrowser.Add(portmsg);
            if (!string.IsNullOrEmpty(protocolmsg))
                errorsForBrowser.Add(protocolmsg);
            if (!string.IsNullOrEmpty(usersIPmsg))
                errorsForBrowser.Add(usersIPmsg);
            return errorsForBrowser.Count > 0 ? true : false;
        }

        private bool HasErrorsForNmap()
        {
            string nmapCommandmsg = nmapCommand.TestTextBox("Nmap Command");
            
            string nmapTestIpMsg = nmapTestersIP.TestComboBox("Tester's IP");
            if (!string.IsNullOrEmpty(nmapCommandmsg))
            {
                errorsForNmap.Add(nmapCommandmsg);
            }
            else
            {
                if (!nmapCommand.Text.StartsWith("nmap "))
                    errorsForNmap.Add("Nmap Command");
            }
            if (!string.IsNullOrEmpty(nmapTestIpMsg))
                errorsForNmap.Add(nmapTestIpMsg);
            return errorsForNmap.Count > 0 ? true : false;
        }

        private void AlertMessages(ref List<string> list)
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (string s in list.ToList())
            {
                if (list.Count == i)
                    sb.Append(s);
                else
                    sb.Append(s + Environment.NewLine);
                i++;
            }
            MessageBox.Show(sb.ToString(), "Error");
            list = new List<string>();
        }

        private void browser_Click(object sender, EventArgs e)
        {
            SaveBrowserActivity(true);
        }

        private void SaveBrowserActivity(bool openBrowser)
        {
            bool isInvalid = HasErrorsForBrowser();
            if (isInvalid)
            {
                AlertMessages(ref errorsForBrowser);
                return;
            }

            if (!String.IsNullOrEmpty(ipHostname.Text) &&
                 !String.IsNullOrEmpty(port.Text))
            {
                BrowserLog l = new BrowserLog();
                l.IpHostname = ipHostname.Text;
                l.Port = port.Text;
                l.Protocol = protocolItems.SelectedItem.ToString();
                l.Project = ddlProjects.Text;
                l.LogDateTime = DateTime.Now;
                l.PenTesterIP = browserTestingIP.Text;
                string f = CreateFileName(Logger.LoggerTypes.browserlog.ToString(), ddlProjects.Text);
                projectHelper.SaveBrowserData(f, l);
                if (openBrowser)
                    CmdHelper.LaunchInternetExplorer(l);
            }
        }

        private string CreateFileName(string type, string p)
        {
            return fileLocation.Replace(".xml", p + "." + type);
        }

        private void ipHostname_TextChanged(object sender, EventArgs e)
        {
            hostLabel.Text = ipHostname.Text;
            hostLabel2.Text = ipHostname.Text;
        }

        //DETAILED VULNERABILITIES
        private void deleteVuln_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DataGridViewSelectedRowCollection collection = dataGridView1.SelectedRows;
                foreach (DataGridViewRow gvr in collection)
                {
                    object obj = gvr.DataBoundItem;
                    if (null != obj)
                    {
                        if (obj is Vulnerability)
                        {
                            Vulnerability v = obj as Vulnerability;
                            if (null != v)
                            {
                                Vulnerabilities.RemoveItem(v);
                                bindGrid(null);
                            }
                        }
                    }
                }
            }
        }

        private void addIssueButton_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            bool isUpdate = false;
            ModTypes t = ModTypes.Add;
           
            DateTime outCompleteDate;
            DateTime outDiscoveredDate;
            Decimal cvss = 0;
            IVulnerability vuln = new Vulnerability();
            vuln.Identifier = Guid.NewGuid();
            if (sender is Button)
            {
                Button localB = sender as Button;
                if (null != localB)
                {
                    isUpdate = localB.Text == "Save" ? true : false;
                    if (isUpdate)
                    {
                        t = ModTypes.Update;
                        vuln.Identifier = EditGuid;
                    }
                    else
                        t = ModTypes.Add;
                    
                    
                }
            }
            if (Decimal.TryParse(cvssDataText.Text, out cvss))
            {
                vuln.CVSS = cvss;
            }
            else
            {
                MessageBox.Show("Value was not a decimal.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (DateTime.TryParse(completeDateInput.Text, out outCompleteDate))
                vuln.CompletedDate = outCompleteDate;
            else
                vuln.CompletedDate = null;

            if (DateTime.TryParse(discoveredDate.Text, out outDiscoveredDate))
                vuln.DiscoveredDate = outDiscoveredDate;
            else
                vuln.DiscoveredDate = null;

            string title = vulnTitle.Text;
            string status = statusInput.Text;
            string risklevel = riskLevelInput.Text;
            string details = vulnerabilityInput.Text;
            if (status != "Not An Issue" || status != "Fixed")
                vuln.CompletedDate = null;
            vuln.Title = title;
            vuln.VulnTypeReported = vulnTypeItems.SelectedItem.ToString();
            vuln.Status = status;
            vuln.Risk = risklevel;
            vuln.Details = details;
            vuln.Tester = penTesterDiscover.SelectedItem.ToString();
            
            
            vuln.isWeeklyReportItem = isForUpdate.Checked;
            Vulnerabilities.AddObject(vuln);

            projectHelper.ModifyVulnerabilityAnalysis(t, ddlProjects.Text, vuln);
            if (ModTypes.Update == t)
                dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            bindGrid(project);
            ClearVulnForm();
        }

        private void ClearVulnForm()
        {
            vulnTypeItems.SelectedIndex = 0;
            vulnTitle.Text = string.Empty;
            vulnerabilityInput.Text = string.Empty;
            penTesterDiscover.SelectedIndex = 0;
            riskLevelInput.SelectedIndex = 0;
            statusInput.SelectedIndex = 0;
            cvssDataText.Value = Convert.ToDecimal("0.0");
            discoveredDate.Value = DateTime.Now;
            completeDateInput.Value = DateTime.Now;
            isForUpdate.Checked = false;
            addIssueButton.Text = "Add";
        }

        private void bindGrid(ProjectPOCO p)
        {
            dataGridView1.DataSource = p.Vulnerabilities.OrderBy(x => x.Risk).ToList();
            dataGridView1.Update();
            dataGridView1.Columns["VulnerabilityID"].Visible = false;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string error = string.Empty;
            switch (e.ColumnIndex)
            {
                case 0:
                    error = processError("Title");
                    break;
                case 1:
                    error = processError("Vulnerability");
                    break;
                case 2:
                    error = processError("Risk Level");
                    break;
                case 3:
                    error = processError("Due Date");
                    break;
                case 4:
                    error = processError("Status");
                    break;
                case 5:
                    error = processError("Date Fixed");
                    break;
                case 6:
                    error = processError("CVSS");
                    break;
                default:
                    break;
            }
            MessageBox.Show(error);
            e.Cancel = true;
        }

        private const string invalidData = "Invalid data in the {0} column";
        private static string processError(string error)
        {
            if (string.IsNullOrEmpty(error))
                return "Unknown Exception";
            else
                return string.Format(invalidData, error);
        }

        private void cvssCalc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://nvd.nist.gov/cvss.cfm?calculator&version=2");
        }

        async private void sshConnect_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            bool isInvalid = HasErrorsForSSH();
            if (isInvalid)
            {
                AlertMessages(ref errorsForSSH);
                return;
            }
            SSHLog log = new SSHLog();            
            
            string f = CreateFileName(Logger.LoggerTypes.sshlog.ToString(), ddlProjects.Text);
            log.IPHostname = ipHostname.Text;
            log.Project = ddlProjects.Text;
            log.LogDate = DateTime.Now;
            log.Username = sshUsername.Text;
            SecureString pw = new SecureString();
            foreach (char o in sshPassword.Text)
            {
                pw.AppendChar(o);
            }
            log.PenTesterIP = sshTestingIP.Text;
            string h = await LaunchPutty(log, pw);
            DialogResult d = MessageBox.Show("Please wait while the application starts..." + Environment.NewLine+ Environment.NewLine+"Was the connection successful?", "Confirm", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == d)
            {
                log.wasSuccessful = true;
                projectHelper.SaveSSHData(f, log);
            }
            else
            {
                log.wasSuccessful = false;
                projectHelper.SaveSSHData(f, log);
            }
        }


        async private Task<string> LaunchPutty(SSHLog v, SecureString s)
        {
            return await Task.Run<string>(()=> CmdHelper.LoadPutty(v,s));
        }

        private void nmapExecute_Click(object sender, EventArgs e)
        {
            ProjectPOCO project = projectHelper.GetCurrentProject(ddlProjects.Text);
            if (null == project)
            {
                ShowError("No project found.");
                return;
            }
            bool isInvalid = HasErrorsForNmap();
            if (isInvalid)
            {
                AlertMessages(ref errorsForNmap);
                return;
            }
            if (!string.IsNullOrEmpty(nmapCommand.Text))
            {
                NmapLog log = new NmapLog();
                log.LogDateTime = DateTime.Now;
                log.NmapCommand = nmapCommand.Text;
                log.Project = ddlProjects.Text;
                log.PenTesterIP = nmapTestersIP.SelectedItem.ToString();
                CmdHelper.LaunchNmap(log);
                string f = CreateFileName(Logger.LoggerTypes.nmaplog.ToString(), ddlProjects.Text);
                projectHelper.SaveNmapData(f, log);
            }
            else
            {
                MessageBox.Show("Unable to Execute nmap command","Error");
            }
        }

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateIPAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPAddress.ClearIP();
            DialogResult dr = MessageBox.Show("Attempt to obtain public ip address?", "Request for data", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == dr)
                IPAddress.AllowAccessForIP(true);
            else
                IPAddress.AllowAccessForIP(false);
            BindTesterIP();
        }

        private void auditViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AuditViewer form = new AuditViewer())
            {
                form.ShowDialog();
            }
        }
        private Guid EditGuid { get; set; }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (-1 == e.RowIndex)
                return;
            DataGridViewRow gvr = dataGridView1.Rows[e.RowIndex];
            if (gvr.DataBoundItem is Vulnerability)
            {
                Vulnerability v = gvr.DataBoundItem as Vulnerability;
                if (null != v)
                {
                    if (EditGuid == v.Identifier)
                        return;
                    try
                    {
                        CancelSave.Visible = true;
                        vulnTypeItems.SelectedIndex = vulnTypeItems.Items.IndexOf(v.VulnTypeReported);
                        vulnTitle.Text = v.Title;
                        vulnerabilityInput.Text = v.Details;
                        penTesterDiscover.SelectedIndex = penTesterDiscover.Items.IndexOf(v.Tester);
                        riskLevelInput.SelectedIndex = riskLevelInput.Items.IndexOf(v.Risk);
                        statusInput.SelectedIndex = statusInput.Items.IndexOf(v.Status);
                        cvssDataText.Value = Convert.ToDecimal(v.CVSS);
                        discoveredDate.Value = (DateTime)v.DiscoveredDate;
                        EditGuid = v.Identifier;
                        completeDateInput.Value = null == v.CompletedDate ? DateTime.Now : (DateTime)v.CompletedDate;
                        isForUpdate.Checked = v.isWeeklyReportItem;
                        addIssueButton.Text = "Save";
                    }
                    catch { }
                }
            }
        }

        private void metricsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Metrics form = new Metrics())
            {
                form.ShowDialog();
            }
        }

        private void CancelSave_Click(object sender, EventArgs e)
        {
            EditGuid = Guid.NewGuid();
            ClearVulnForm();
            CancelSave.Visible = false;
        }

        private void saveBrowserActivity_Click(object sender, EventArgs e)
        {
            SaveBrowserActivity(false);
        }

        private void base64FileDataItem_Click(object sender, EventArgs e)
        {
            using (ImageData idata = new ImageData())
            {
                idata.ShowDialog();
            }
        }
    }
}