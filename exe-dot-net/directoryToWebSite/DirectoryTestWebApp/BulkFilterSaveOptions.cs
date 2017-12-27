using DirectoryTestWebApp.Business;
using System;
using System.Windows.Forms;

namespace DirectoryTestWebApp
{
    public partial class BulkFilterSaveOptions : Form
    {
        public BulkFilterSaveOptions()
        {
            InitializeComponent();
        }

        private void processFilter_Click(object sender, EventArgs e)
        {
            FilterOption fo = new FilterOption();
            bool ignoreDir = ignoreDirectory.Checked;
            string dir = directoryFilter.Text;
            if (string.IsNullOrEmpty(dir))
            {
                MessageBox.Show("Unable to process request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Control c = Application.OpenForms["DirectoryTest"];
            if (c is DirectoryTest)
            {
                DirectoryTest d = c as DirectoryTest;
                if (null != d)
                {
                    fo.DirectoryFilter = dir;
                    fo.IgnoreParent = ignoreDir;
                    d.FilterOptionData = fo;
                }
            }
            this.Close();
        }
    }
}
