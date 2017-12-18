using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace RequestModifier
{
    public partial class RequestModifierForm : Form
    {
        public RequestModifierForm()
        {
            InitializeComponent();
        }

        private void saveHtml_Click(object sender, EventArgs e)
        {
            if (string.Empty == webBrowser1.DocumentText)
            {
                MessageBox.Show("Unable to save blank document.");
                return;
            }
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "HTML | *.htm";
                dlg.Title = "Save html file - useful to test csrf";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Filter != string.Empty)
                    {
                        using (FileStream fs = (FileStream)dlg.OpenFile())
                        {
                            using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                            {
                                sw.Write(webBrowser1.DocumentText);
                            }
                        }
                    }
                }
            }
        }

        private void requestddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Form parent = this.FindForm();
            if (sender is ComboBox)
            {
                var b = sender as ComboBox;
                var value = b.Items[b.SelectedIndex];
                if (1 == b.SelectedIndex)
                {
                    using (GetToPost frm = new RequestModifier.GetToPost(parent))
                    {
                        frm.ShowDialog();
                    }
                }
                else if (2 == b.SelectedIndex)
                {
                    using (PostsToGet frm = new RequestModifier.PostsToGet(parent))
                    {
                        frm.ShowDialog();
                    }
                }
            }
        }
    }
}