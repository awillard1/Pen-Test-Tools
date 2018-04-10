using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using URLTester.Business;

namespace URLTester.URLBruteForcer
{
    public partial class FileBruteForce : Form
    {
        private string SiteUrl { get; set; }
        public FileBruteForce()
        {
            InitializeComponent();
        }

        private void WebFiles_Load(object sender, EventArgs e)
        {
            this.Text = "Web Files - " + SiteUrl;
            if (!string.IsNullOrEmpty(SiteUrl))
            {
                MessageBox.Show("This will perform many url type attacks to determine if the system is vulnerable to certain known attacks. Please close this form if you do not wish for these tests to run.");
            }
        }

        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            webFileGrid.Update();
            this.Update();

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webFileGrid.Dispose();
            Close();
        }

        private void WebFiles_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != this)
            {
                if (null != bw)
                {
                    bw.CancelAsync();
                    bw.Dispose();
                }
                this.Dispose();
            }
        }

        BackgroundWorker bw = new BackgroundWorker();
        private void beginTest()
        {
            bw.RunWorkerAsync();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Job Completed");
            isNmap = false;
        }

        public void pushData(FileStatusObject item)
        {
            if (null != item)
            {
                if (this.InvokeRequired)
                {
                    webFileGrid.Invoke(new Action(() => webFileGrid.Rows.Add(item.FileName, item.StatusCode, item.Details)));
                }
                else
                {
                    try
                    {
                        webFileGrid.Rows.Add(item.FileName, item.StatusCode);
                    }
                    catch { }
                }
            }
        }
        private string _fileLocation { get; set; }
        void bw_DoWorkNmap(object sender, DoWorkEventArgs e)
        {
            IEnumerable<string> items = new NmapUtility(_fileLocation).PreLoad();
            Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (currentItem) =>
            {
                FileStatusObject returnedTaskTResult = await new AttackHelper().AttackAsync(currentItem);
                Thread thread = new Thread(() => pushData(returnedTaskTResult));
                thread.Start();
            });
        }

        void bw_DoWorkDirbust(object sender, DoWorkEventArgs e)
        {
            if (!SiteUrl.EndsWith("/"))
            {
                SiteUrl += "/";
            }
            IEnumerable<string> items = new DirectoryEnumerationUtility(_fileLocation).PreLoad();
            Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 10 }, async (currentItem) =>
            {
                FileStatusObject returnedTaskTResult = await new AttackHelper().AttackAsync(SiteUrl + currentItem);
                Thread thread = new Thread(() => pushData(returnedTaskTResult));
                thread.Start();
            });
        }

        private bool isNmap { get; set; }

        private void startButton_Click(object sender, EventArgs e)
        {
            SiteUrl = site.Text;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            if (isNmap)
            {
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkNmap);
            }
            else
            {
                if (string.IsNullOrEmpty(SiteUrl))
                {
                    MessageBox.Show("You need to provide a base url.", "Error");
                    return;
                }
                else if (SiteUrl.EndsWith(".xml"))
                {
                    MessageBox.Show("Please select the file for directory enumeration.", "Error");
                    return;
                }
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkDirbust);
            }
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            beginTest();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void openNmapXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "(*.xml)|*.xml";
                DialogResult result = d.ShowDialog();
                if (DialogResult.OK == result)
                {
                    if (d.CheckFileExists)
                    {
                        if (d.FileName.Contains("xml"))
                        {
                            _fileLocation = d.FileName;
                            isNmap = true;
                            site.Enabled = false;
                        }
                    }
                }
            }
        }

        private void openDirBusterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog d = new OpenFileDialog())
            {
                d.Filter = "(*.txt)|*.txt";
                DialogResult result = d.ShowDialog();
                if (DialogResult.OK == result)
                {
                    if (d.CheckFileExists)
                    {
                        if (d.FileName.Contains("txt"))
                        {
                            _fileLocation = d.FileName;
                            isNmap = false;
                            site.Enabled = true;
                        }
                    }
                }
            }
        }
    }
}