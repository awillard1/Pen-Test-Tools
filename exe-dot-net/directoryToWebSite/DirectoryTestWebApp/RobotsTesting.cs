using DirectoryTestWebApp.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace DirectoryTestWebApp
{
    public partial class RobotsTesting : Form
    {
        public RobotsTesting()
        {
            InitializeComponent();
        }

        private void processRobots_Click(object sender, EventArgs e)
        {
            Uri uri = null;
            List<string> itemsToAdd = new List<string>();
            string testurl = url.Text.Trim();
            if (!string.IsNullOrEmpty(testurl))
            {
                try
                {
                    uri = new Uri(testurl);
                    if (!testurl.Contains(".xml", true))
                    {
                        ExtractRobotsDotTxt(uri, itemsToAdd);
                    }
                    else
                    {
                        ExtractSiteMap(uri, itemsToAdd);
                    }
                }
                catch
                {
                    ShowProcessError();
                }
            }
            else
            {
                ShowProcessError();
            }
        }

        private void ExtractSiteMap(Uri uri, List<string> itemsToAdd)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (null != response)
            {
                if (HttpStatusCode.OK == response.StatusCode)
                {
                    if (response.ContentType == "text/xml")
                    {
                        List<string> urls = new SitemapHelper().Urls(response.GetResponseStream());
                        ProcessList(urls);
                    }
                }
            }
        }

        private void ExtractRobotsDotTxt(Uri uri, List<string> itemsToAdd)
        {
            Uri rbt = new Uri(uri.ToString() + "/robots.txt");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(rbt);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (null != response)
            {
                if (HttpStatusCode.OK == response.StatusCode)
                {
                    Business.RobotParse Parser = new Business.RobotParse();
                    List<string> list = Parser.ParsedList(response);
                    foreach (string s in list)
                    {
                        if (s.Contains("http:") || s.Contains("https:") || s.Contains(':'))
                        {
                            itemsToAdd.Add(s);
                        }
                        else
                        {
                            if (s.StartsWith("/"))
                            {
                                itemsToAdd.Add(CreateBaseUrl(uri, true) + s);
                            }
                            else
                            {
                                itemsToAdd.Add(CreateBaseUrl(uri, false) + "/" + s);
                            }
                        }
                    }
                    ProcessList(itemsToAdd);
                }
                else
                {
                    ShowProcessError();
                }
            }
        }

        private string CreateBaseUrl(Uri uri, bool itemStartsWithSlash)
        {

            string value = string.Empty;
            if (useFullUrl.Checked)
            {
                value = uri.OriginalString.Trim();
                if (value.EndsWith("/") && itemStartsWithSlash)
                {
                    value = value.Substring(0, value.Length -1);
                }
            }
            else
            {
                value = uri.Scheme + "://" + uri.Host;
            }
            return value;
        }

        private void ProcessList(List<string> itemsToAdd)
        {
            files.Items.Clear();
            foreach (string s in itemsToAdd)
            {
                files.Items.Add(s);
            }
        }

        private static void ShowProcessError()
        {
            MessageBox.Show("Unable to process url", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void files_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (null != files.SelectedItem)
            {
                string url = files.SelectedItem.ToString();
                loadedUrl.Text = url;
                browser.Navigate(url);
            }
        }

        private void RobotsTesting_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RobotsTesting_FormClosing(null, null);
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
    }
}
