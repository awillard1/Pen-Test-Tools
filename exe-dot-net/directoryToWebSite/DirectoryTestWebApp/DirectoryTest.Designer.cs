namespace DirectoryTestWebApp
{
    partial class DirectoryTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectorySelector = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.startingUrl = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.loadedUrl = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFullListing = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFilteredSitemap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.testRobotstxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.DirectorySelector);
            this.groupBox1.Location = new System.Drawing.Point(4, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File List";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Location = new System.Drawing.Point(7, 49);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(282, 295);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewFile});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(162, 26);
            // 
            // viewFile
            // 
            this.viewFile.Name = "viewFile";
            this.viewFile.Size = new System.Drawing.Size(161, 22);
            this.viewFile.Text = "View in NotePad";
            this.viewFile.Click += new System.EventHandler(this.viewFile_Click);
            // 
            // DirectorySelector
            // 
            this.DirectorySelector.Location = new System.Drawing.Point(213, 13);
            this.DirectorySelector.Name = "DirectorySelector";
            this.DirectorySelector.Size = new System.Drawing.Size(75, 23);
            this.DirectorySelector.TabIndex = 0;
            this.DirectorySelector.Text = "Directory";
            this.DirectorySelector.UseVisualStyleBackColor = true;
            this.DirectorySelector.Click += new System.EventHandler(this.directorySelector_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URI:";
            // 
            // startingUrl
            // 
            this.startingUrl.Location = new System.Drawing.Point(34, 26);
            this.startingUrl.Name = "startingUrl";
            this.startingUrl.Size = new System.Drawing.Size(274, 20);
            this.startingUrl.TabIndex = 2;
            this.startingUrl.Text = "https://<site>/";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(315, 84);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(598, 318);
            this.webBrowser1.TabIndex = 3;
            // 
            // loadedUrl
            // 
            this.loadedUrl.AutoSize = true;
            this.loadedUrl.Location = new System.Drawing.Point(314, 65);
            this.loadedUrl.Name = "loadedUrl";
            this.loadedUrl.Size = new System.Drawing.Size(0, 13);
            this.loadedUrl.TabIndex = 4;
            this.loadedUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loadedUrl_LinkClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(925, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveFileListToolStripMenuItem,
            this.saveFullListing,
            this.saveFilteredSitemap,
            this.toolStripSeparator2,
            this.testRobotstxtToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(198, 6);
            // 
            // saveFileListToolStripMenuItem
            // 
            this.saveFileListToolStripMenuItem.Name = "saveFileListToolStripMenuItem";
            this.saveFileListToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.saveFileListToolStripMenuItem.Text = "Save File List as Sitemap";
            this.saveFileListToolStripMenuItem.Click += new System.EventHandler(this.saveFileListToolStripMenuItem_Click);
            // 
            // saveFullListing
            // 
            this.saveFullListing.Name = "saveFullListing";
            this.saveFullListing.Size = new System.Drawing.Size(201, 22);
            this.saveFullListing.Text = "Save Listing of All Files";
            this.saveFullListing.Click += new System.EventHandler(this.saveFullListing_Click);
            // 
            // saveFilteredSitemap
            // 
            this.saveFilteredSitemap.Name = "saveFilteredSitemap";
            this.saveFilteredSitemap.Size = new System.Drawing.Size(201, 22);
            this.saveFilteredSitemap.Text = "Save Filtered Sitemap";
            this.saveFilteredSitemap.Click += new System.EventHandler(this.saveFilteredSitemap_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // testRobotstxtToolStripMenuItem
            // 
            this.testRobotstxtToolStripMenuItem.Name = "testRobotstxtToolStripMenuItem";
            this.testRobotstxtToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.testRobotstxtToolStripMenuItem.Text = "&Robots.txt and Sitemaps";
            this.testRobotstxtToolStripMenuItem.Click += new System.EventHandler(this.testRobotstxtToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFilterToolStripMenuItem,
            this.clearFilterToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setFilterToolStripMenuItem
            // 
            this.setFilterToolStripMenuItem.Name = "setFilterToolStripMenuItem";
            this.setFilterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.setFilterToolStripMenuItem.Text = "Set Filter";
            this.setFilterToolStripMenuItem.Click += new System.EventHandler(this.setFilterToolStripMenuItem_Click);
            // 
            // clearFilterToolStripMenuItem
            // 
            this.clearFilterToolStripMenuItem.Name = "clearFilterToolStripMenuItem";
            this.clearFilterToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.clearFilterToolStripMenuItem.Text = "Clear Filter";
            this.clearFilterToolStripMenuItem.Click += new System.EventHandler(this.clearFilterToolStripMenuItem_Click);
            // 
            // DirectoryTest
            // 
            this.AcceptButton = this.DirectorySelector;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 414);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.loadedUrl);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.startingUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DirectoryTest";
            this.Text = "Directory to Site Test Tool ";
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button DirectorySelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox startingUrl;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.LinkLabel loadedUrl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem saveFullListing;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFilteredSitemap;
        private System.Windows.Forms.ToolStripMenuItem testRobotstxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

