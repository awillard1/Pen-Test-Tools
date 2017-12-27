using DirectoryTestWebApp.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DirectoryTestWebApp
{
    public partial class DirectoryTest : Form
    {
        public enum SaveType
        {
            None,
            SiteMap,
            SiteMapFiltered,
            FullDirectory
        }
        private SaveType saveType { get; set; }
        private string browserPath { get; set; }
        public DirectoryTest()
        {
            InitializeComponent();
            saveType = SaveType.None;
        }

        private void directorySelector_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (DialogResult.OK == result)
            {
                browserPath = folderBrowserDialog1.SelectedPath;
                ListDirectory(treeView1, browserPath);
            }
        }

        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            try
            {
                treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to process request." + Environment.NewLine + ex.Message);
                return;
            }
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (startingUrl.Text.Contains("<site>", true))
            {
                startingUrl.Focus();
                MessageBox.Show("Please input the correct url to test");
                return;
            }
            if (!startingUrl.Text.EndsWith("/"))
            {
                startingUrl.Text += "/";
                MessageBox.Show("Trailing \"/\" was added to the Uri.");
            }
            string url = startingUrl.Text + e.Node.FullPath.Replace("\\", "/").Replace(treeView1.Nodes[0].Text + "/", "") ;
            loadedUrl.Text = url;
            webBrowser1.Navigate(url);
        }

        private void loadedUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (loadedUrl.Text.StartsWith("http"))
            {
                Uri url = null;
                try
                {
                    url = new Uri(loadedUrl.Text);
                }
                catch
                {
                    MessageBox.Show("Unable to load url");
                }
                finally
                {
                    if (null != url)
                    {
                        System.Diagnostics.Process.Start(url.ToString());
                    }
                }
            }
        }

        private void viewFile_Click(object sender, EventArgs e)
        {
            if (null != sender)
            {
                Control c = GetSourceControl(sender, null);
                if (c is TreeView)
                {
                    TreeView t = c as TreeView;
                    if (null != t)
                    {
                        try
                        {
                            string path = browserPath + t.SelectedNode.FullPath.Replace(t.Nodes[0].Text, "");
                            System.Diagnostics.Process.Start("notepad.exe", path);
                        }
                        catch
                        {
                            MessageBox.Show("Please select the item first before attempting to view");
                        }
                    }
                }
            }
        }

        private Control GetSourceControl(object sender, Control control)
        {
            ToolStripDropDownItem menuItem = sender as ToolStripDropDownItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                    control = owner.SourceControl;
            }
            return control;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        const string unableToSave = "Unable to save file";
        private void ShowError(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveFileListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveType = SaveType.SiteMap;
            SaveXMLFile();

        }
        private void SaveXMLFile()
        {
            TreeView treeview = treeView1;
            if (null != treeview)
            {
                XDocument data = CallTreeRecursive(treeview);
                SaveData(data);
            }
            else
            {
                ShowError(unableToSave);
            }
}

        private void SaveData(XDocument data)
        {
            if (null != data)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "XML files (*.xml)|*.xml";
                    sfd.FilterIndex = 0;
                    sfd.RestoreDirectory = false;
                    sfd.CreatePrompt = true;
                    sfd.Title = "Save path of the file to be exported";
                    DialogResult result = sfd.ShowDialog();
                    if (DialogResult.OK == result)
                    {
                        data.Save(sfd.FileName);
                    }
                }
            }
            else
            {
                ShowError(unableToSave);
            }
        }

        private void TreeRecursive(TreeNode treeNode, TreeNode parent = null)
        {
            doc.Root.Add(GetNodeData(treeNode, parent));
            foreach (TreeNode tn in treeNode.Nodes)
            {
                TreeRecursive(tn,parent);
            }
        }
        XDocument doc = null;
        static XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private XDocument CallTreeRecursive(TreeView treeView)
        {
            doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement rootItem = new XElement(ns + "urlset");
            doc.Add(rootItem);
            TreeNodeCollection nodes = treeView.Nodes;
            foreach (TreeNode n in nodes)
            {
                TreeRecursive(n);
            }
            return doc;
        }

        private XElement GetNodeData(TreeNode treeNode, TreeNode parent = null)
        {
            XElement url = new XElement(ns + "url");
            string urldata = string.Empty;
            if (SaveType.SiteMap == saveType)
            {
                urldata = FixUrl(startingUrl.Text) + treeNode.FullPath.Replace("\\", "/").Replace(treeView1.Nodes[0].Text, "");
                url.Add(new XElement(ns + "loc", urldata));
            }
            else if (SaveType.FullDirectory == saveType)
            {
                urldata = treeNode.FullPath.Replace("\\", "/");
                url.Add(new XElement(ns + "loc", urldata));
            }
            else if (SaveType.SiteMapFiltered == saveType)
            {
                string path = string.Empty;
                if (null != parent)
                {
                    if (null != parent.Parent)
                    {
                        path = parent.Parent.Text;
                        if (FilterOptionData.IgnoreParent)
                        {
                            string dir = treeNode.FullPath.Replace("\\", "/");
                            int location = dir.IndexOf(path) + path.Length + 1;
                            dir = dir.Substring(location, dir.Length - location);
                            //Probably Run into an issue here eventually - will keep an eye on it - may need 
                            //something more explicit than just a replace. But working for now.
                            //If you downloaded this code, you may want to look into this.
                            if (!string.IsNullOrEmpty(dir))
                                dir = dir.Replace(FilterOptionData.DirectoryFilter, "");
                            urldata = FixUrl(startingUrl.Text) + path + dir;
                        }
                        url.Add(new XElement(ns + "loc", urldata));
                    }
                }
            }
            return url;
        }

        private string FixUrl(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                int len = p.Length;
                int lastIndex = p.LastIndexOf("/");
                if (len == lastIndex)
                    return p.Substring(0, len - 1);
                else
                    return p;
            }
            else
            {
                return string.Empty;
            }
        }

        private void saveFullListing_Click(object sender, EventArgs e)
        {
            saveType = SaveType.FullDirectory;
            SaveXMLFile();
        }

        public FilterOption FilterOptionData { get; set; }

        private void clearFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterOptionData = null;
        }

        private void setFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BulkFilterSaveOptions b = new BulkFilterSaveOptions())
            {
                b.ShowDialog();
            }
        }

        private void saveFilteredSitemap_Click(object sender, EventArgs e)
        {
            if (null == FilterOptionData)
            {
                ShowError("Unable to process. Filter was not set");
                return;
            }
            else if (string.IsNullOrEmpty(FilterOptionData.DirectoryFilter))
            {
                ShowError("Unable to process. Directory was not set for Filter");
                return;
            }
            ProcessFilter();
            SaveData(doc);
        }
        private void ProcessFilter()
        {
            saveType = SaveType.SiteMapFiltered;
            CallFilterTreeRecursive(treeView1);
        }

        List<TreeNode> _treeNode = null;

        private void FilterTreeRecursive(TreeNode treeNode)
        {
            if (treeNode.Text == FilterOptionData.DirectoryFilter)
            {
                _treeNode.Add(treeNode);
            }
            foreach (TreeNode tn in treeNode.Nodes)
            {
                FilterTreeRecursive(tn);
            }
        }

        private XDocument CallFilterTreeRecursive(TreeView treeView)
        {
            doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement rootItem = new XElement(ns + "urlset");
            doc.Add(rootItem);
            TreeNodeCollection nodes = treeView.Nodes;
            _treeNode = new List<TreeNode>();
            //Find the nodes
            foreach (TreeNode n in nodes)
            {
                FilterTreeRecursive(n);
            }
            //Then process the nodes
            foreach (TreeNode t in _treeNode)
            {
                TreeRecursive(t, t);
            }
            return doc;
        }

        private void testRobotstxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RobotsTesting t = new RobotsTesting();
            t.Show();
        }
    }
}