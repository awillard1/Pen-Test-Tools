using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using ScanProjectManagement.Business;

namespace ScanProjectManagement
{
    public partial class AuditViewer : Form
    {
        private List<SSHLog> sshlogs { get; set; }
        private List<NmapLog> nmaplogs { get; set; }
        private List<BrowserLog> blogs { get; set; }

        public AuditViewer()
        {
            InitializeComponent();
            ClearData();
        }

        private void ClearData()
        {
            ClearSSH();
            ClearBrowser();
            ClearNmap();
            detailLabel.Text = "[Not Set]";
            auditViewerGrid.DataSource = new List<string>();
            SelectFileToLoad();
        }
        string filelocation { get; set; }
        private void SelectFileToLoad()
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                string AllLogs = string.Format("Log Files (*.{0};*.{1};*.{2})|*.{0};*.{1};*.{2}",
                    Logger.LoggerTypes.sshlog.ToString(),
                    Logger.LoggerTypes.nmaplog.ToString(),
                    Logger.LoggerTypes.browserlog.ToString());
                openFileDialog1.Filter = AllLogs;
                DialogResult dr = openFileDialog1.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    string file = openFileDialog1.FileName;
                    filelocation = file;
                    processFile(file);
                }
            }
        }

        private void processFile(string file)
        {
            if (File.Exists(file))
            {
                XElement projectData = XElement.Load(file);
                GetDataFileForReporting(projectData);
            }
            else
            {
                MessageBox.Show("Unable to open file.", "Error");
            }
        }

        private void GetDataFileForReporting(XElement projectData)
        {
            auditViewerGrid.ReadOnly = true;
            auditViewerGrid.AutoGenerateColumns = true;
            if (null != projectData)
            {
                try
                {
                    foreach (XElement x in projectData.Elements())
                    {
                        var s = x.Name.ToString();

                        if (s == "SSHLog")
                            sshlogs.Add(x.FromXElement<SSHLog>());
                        else if (s == "NmapLog")
                            nmaplogs.Add(x.FromXElement<NmapLog>());
                        else if (s == "BrowserLog")
                            blogs.Add(x.FromXElement<BrowserLog>());
                    }
                }
                catch
                {
                }
                if (sshlogs.Count > 0)
                {
                    GenerateSSHColumns();
                    detailLabel.Text = string.Format("SSH Log - {0}", filelocation);
                    auditViewerGrid.DataSource = sshlogs.ToList();
                }
                else if (nmaplogs.Count > 0)
                {
                    GenerateNmapColumns();
                    detailLabel.Text = string.Format("Nmap Log - {0}", filelocation);
                    auditViewerGrid.DataSource = nmaplogs.ToList();
                }
                else if (blogs.Count > 0)
                {
                    GenerateBrowserColumns();
                    detailLabel.Text = string.Format("Browser Log - {0}", filelocation);
                    auditViewerGrid.DataSource = blogs.ToList();
                }
                else
                    MessageBox.Show("No data found", "Alert");
            }
        }
        
        private DataGridViewTextBoxColumn CreateColumn(string datakey,string header, bool editable)
        {
            DataGridViewTextBoxColumn c = new DataGridViewTextBoxColumn();
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            c.DataPropertyName = datakey;
            c.ReadOnly = editable;
            c.HeaderText =header;
            return c;
        }

        private void GenerateBrowserColumns()
        {
            AddColumn(CreateColumn("Project", "Project",false));
            AddColumn(CreateColumn("Protocol", "Protocol", false));
            AddColumn(CreateColumn("IpHostname", "Ip/Host", false));
            AddColumn(CreateColumn("Port", "Port", false));
            AddColumn(CreateColumn("LogDateTime", "Timestamp", false));
            AddColumn(CreateColumn("PenTesterIP", "Tester IP", false));
        }

        private void AddColumn(DataGridViewColumn c)
        {
            auditViewerGrid.Columns.Add(c);
        }

        private void GenerateNmapColumns()
        {
            AddColumn(CreateColumn("Project", "Project",false));
            AddColumn(CreateColumn("NmapCommand","Command",false));
            AddColumn(CreateColumn("LogDateTime", "Timestamp", false));
            AddColumn(CreateColumn("PenTesterIP","Tester IP",false));
        }

        private void GenerateSSHColumns()
        {
            AddColumn(CreateColumn("Project", "Project", false));
            AddColumn(CreateColumn("IPHostname", "IP/Host", false));
            AddColumn(CreateColumn("Username", "Username Attempted", false));
            AddColumn(CreateColumn("wasSuccessful", "Successful", false));
            AddColumn(CreateColumn("LogDate", "Timestamp", false));
            AddColumn(CreateColumn("PenTesterIP", "Tester IP", false));
        }

        void AuditViewer_Load(object sender, EventArgs e)
        {
            
        }

        private void ClearSSH()
        {
            if (null != sshlogs)
            {
                sshlogs.Clear();
            }
            sshlogs = new List<SSHLog>();
        }

        private void ClearNmap()
        {
            if (null != nmaplogs)
            {
                nmaplogs.Clear();
            }
            nmaplogs = new List<NmapLog>();
        }

        private void ClearBrowser()
        {
            if (null != blogs)
            {
                blogs.Clear();
            }
            blogs = new List<BrowserLog>();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
