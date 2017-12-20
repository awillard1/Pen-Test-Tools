using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace RequestModifier
{
    public partial class PostsToGet : Form
    {
        Form p;
        WebBrowser webBrowser1;

        public PostsToGet(Form parent)
        {
            InitializeComponent();
            SetBrowser(parent);
             HelpLabel.Text = "Ensure your request includes the http/https and any port that may need to be associated in the POST requests first line." + Environment.NewLine +
            "Example: POST https://localhost:9443/somepage.jsp HTTP/1.1";
        }

        private enum RequestType
        {
            Url, Raw
        };

        public void SetBrowser(Form parent)
        {
            if (parent is RequestModifierForm)
            {
                p = parent as RequestModifierForm;
                if (null != p)
                {
                    Control[] ctrls = p.Controls.Find("webBrowser1", true);
                    if (ctrls.Length == 1)
                    {
                        webBrowser1 = ctrls[0] as WebBrowser;
                    }
                }
            }
        }

        private string html { get; set; }
        public Regex ItemMatch { get => itemMatch; set => itemMatch = value; }

        private Regex itemMatch = new Regex(@"[^?]+(?:\?foo=([^&]+).*)?");
        private void processRaw_Click(object sender, EventArgs e)
        {
            string[] items = requestValue.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            bool hasMethod = false;
            bool hasHost = false;
            bool hasData = false;
            int newLineCount = 0;
            string methodData = string.Empty;
            string hostData = string.Empty;
            string requestData = string.Empty;
            foreach (var value in items)
            {
                string item = value.Trim();
                if (item.StartsWith("POST ", StringComparison.InvariantCultureIgnoreCase))
                {
                    int firstSpace = item.IndexOf(" ");
                    methodData = item.Substring(firstSpace, item.Length - (8 + firstSpace)).Trim();
                    hasMethod = true;
                }
                if (item.StartsWith("HOST:", StringComparison.InvariantCultureIgnoreCase))
                {
                    hasHost = true;
                    hostData = item.Substring(5).Trim();
                }
                if (string.IsNullOrEmpty(item.Trim()))
                {
                    newLineCount++;
                }
                if (newLineCount == 1)
                {
                    if (ItemMatch.IsMatch(item))
                    {
                        hasData = true;
                        StringBuilder sb = new StringBuilder();
                        NameValueCollection c = item.ConvertToPair();
                        foreach (string t in c)
                        {
                            if (!string.IsNullOrEmpty(t))
                                sb.Append(HttpUtility.UrlEncode(t) + "=" + HttpUtility.UrlEncode(c[t]) + "&");
                        }
                        requestData = sb.ToString();
                    }
                }
            }

            if (hasData && hasHost && hasMethod)
            {
                Uri url = null;
                try
                {
                    url = new Uri(methodData);
                }
                catch
                {
                    MessageBox.Show("Please ensure that the first line follows Protocol://hostname/Path structure.");
                    return;
                }
                if (null != url)
                {
                    html = string.Format(RequestMod.myUrlFormat, methodData, requestData);
                    Uri u = new Uri(methodData + "?" + requestData);

                    webBrowser1.DocumentText = RequestMod.GetDetails(u,html);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to convert POST to GET");
            }
        }
    }
}
