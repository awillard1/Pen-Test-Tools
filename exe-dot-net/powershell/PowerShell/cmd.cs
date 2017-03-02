using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace PowerShell
{
    public partial class cmd : Form
    {
        public cmd()
        {
            InitializeComponent();
        }

        private void execScript_Click(object sender, EventArgs e)
        {
            output.Text = execData(pwscript.Text);
        }

        private string execData(string s_cmd)
        {
            StringBuilder data = new StringBuilder();
            using (Runspace r = RunspaceFactory.CreateRunspace())
            {
                r.Open();
                Pipeline p = r.CreatePipeline();
                p.Commands.AddScript(s_cmd);
                p.Commands.Add("Out-String");
                try
                {
                    Collection<PSObject> results = p.Invoke();
                    r.Close();
                    foreach (PSObject obj in results)
                        data.AppendLine(obj.ToString());
                }
                catch (Exception ex)
                {
                    data.AppendLine(ex.Message);
                }
            }
            return data.ToString();
        }
    }
}