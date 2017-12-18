using System;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;

namespace RequestModifier
{
    public partial class GetToPost : Form
    {
        Form p;
        WebBrowser webBrowser1;
        TextBox tb;
        private string html { get; set; }
        public GetToPost(Form parent)
        {
            InitializeComponent();
            SetBrowser(parent);
        }
        
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
                    else { webBrowser1 = new WebBrowser();}
                    ctrls = p.Controls.Find("requestValue",true);
                    if (ctrls.Length == 1)
                    {
                        tb = ctrls[0] as TextBox;
                    }
                    else { tb = new TextBox(); }
                }
            }
        }

        private void processUrl_Click(object sender, EventArgs e)
        {
            Uri u = null;
            try
            {
                u = new Uri(urlToProcess.Text);
                ProcessGetToPost(u, RequestMod.RequestType.Url);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Incorrect URL format");
                return;
            }
        }
        
        private void GetHtmlFromUrl(Uri u)
        {
            html = RequestMod.GetDetails(u);
            webBrowser1.DocumentText = html;
        }
        private void ProcessGetToPost(Uri u, RequestMod.RequestType r)
        {
            string d = String.Empty;
            if (r.Equals(RequestMod.RequestType.Url))
            {
                GetHtmlFromUrl(u);
                string rawrequest = string.Format(RequestMod.requestFormatFirst, "POST", u.Scheme + "://" + u.Host + u.AbsolutePath);
                string hostdata = string.Format(RequestMod.requestFormatHost, u.Host);
                tb.Text = rawrequest + Environment.NewLine + hostdata;
                StringBuilder sb = new StringBuilder();
                NameValueCollection c = u.Query.ConvertToPair();
                foreach (string item in c)
                {
                    sb.Append(item + "=" + c[item] + "&");
                }
                tb.Text += Environment.NewLine + "Content-Length:" + sb.Length + Environment.NewLine + Environment.NewLine + sb.ToString();
            }
        }
    }
}