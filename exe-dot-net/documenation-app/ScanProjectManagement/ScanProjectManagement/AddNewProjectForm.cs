using ScanProjectManagement.Business;
using System;
using System.Windows.Forms;

namespace ScanProjectManagement
{
    public partial class AddNewProjectForm : Form
    {
        public AddNewProjectForm()
        {
            InitializeComponent();
        }

        private void addProject_Click(object sender, EventArgs e)
        {
            if (projectHelper.checkExists(projectName.Text.Trim()))
            {
                MessageBox.Show("Project already exists in this file");
            }
            else
            {
                projectHelper.AddProject(projectName.Text.Trim());
                MessageBox.Show("Project Added");
                ScanProjectManager x = Application.OpenForms["ScanProjectManager"] as ScanProjectManager;
                if (null!=x)
                    x.AddProjects();
                this.Dispose();
            }
        }
    }
}
