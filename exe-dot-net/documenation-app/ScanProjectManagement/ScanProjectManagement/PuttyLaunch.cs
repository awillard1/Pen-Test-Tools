using ScanProjectManagement.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanProjectManagement
{
    public partial class PuttyLaunch : Form
    {
        private SSHLog l { get; set; }
        private string f { get; set; }
        public PuttyLaunch(SSHLog log, string _f)
        {
            InitializeComponent();
            l = log;
            f = _f;
        }

        private void yesBtn_Click(object sender, EventArgs e)
        {
            l.wasSuccessful = true;
            projectHelper.SaveSSHData(f, l);
            this.Dispose();
        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            l.wasSuccessful = false;
            projectHelper.SaveSSHData(f, l);
            this.Dispose();
        }

    }
}
