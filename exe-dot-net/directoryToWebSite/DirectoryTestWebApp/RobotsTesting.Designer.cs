namespace DirectoryTestWebApp
{
    partial class RobotsTesting
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.useFullUrl = new System.Windows.Forms.CheckBox();
            this.processRobots = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.url = new System.Windows.Forms.TextBox();
            this.files = new System.Windows.Forms.ListBox();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.loadedUrl = new System.Windows.Forms.LinkLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.useFullUrl);
            this.groupBox1.Controls.Add(this.processRobots);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.url);
            this.groupBox1.Controls.Add(this.files);
            this.groupBox1.Location = new System.Drawing.Point(13, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 393);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options (Enter URL for Sitemap or Base Url for Robots)";
            // 
            // useFullUrl
            // 
            this.useFullUrl.AutoSize = true;
            this.useFullUrl.Location = new System.Drawing.Point(10, 71);
            this.useFullUrl.Name = "useFullUrl";
            this.useFullUrl.Size = new System.Drawing.Size(96, 17);
            this.useFullUrl.TabIndex = 4;
            this.useFullUrl.Text = "Use Url as root";
            this.useFullUrl.UseVisualStyleBackColor = true;
            // 
            // processRobots
            // 
            this.processRobots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processRobots.Location = new System.Drawing.Point(203, 66);
            this.processRobots.Name = "processRobots";
            this.processRobots.Size = new System.Drawing.Size(75, 23);
            this.processRobots.TabIndex = 3;
            this.processRobots.Text = "Process";
            this.processRobots.UseVisualStyleBackColor = true;
            this.processRobots.Click += new System.EventHandler(this.processRobots_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Host";
            // 
            // url
            // 
            this.url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.url.Location = new System.Drawing.Point(7, 41);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(271, 20);
            this.url.TabIndex = 1;
            this.url.Text = "https://<site>/";
            // 
            // files
            // 
            this.files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.files.FormattingEnabled = true;
            this.files.HorizontalScrollbar = true;
            this.files.Location = new System.Drawing.Point(7, 100);
            this.files.Name = "files";
            this.files.Size = new System.Drawing.Size(271, 264);
            this.files.TabIndex = 0;
            this.files.SelectedIndexChanged += new System.EventHandler(this.files_SelectedIndexChanged);
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(306, 54);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(474, 369);
            this.browser.TabIndex = 1;
            // 
            // loadedUrl
            // 
            this.loadedUrl.AutoSize = true;
            this.loadedUrl.Location = new System.Drawing.Point(310, 35);
            this.loadedUrl.Name = "loadedUrl";
            this.loadedUrl.Size = new System.Drawing.Size(0, 13);
            this.loadedUrl.TabIndex = 5;
            this.loadedUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loadedUrl_LinkClicked);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(783, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // RobotsTesting
            // 
            this.AcceptButton = this.processRobots;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 435);
            this.Controls.Add(this.loadedUrl);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RobotsTesting";
            this.Text = "Robots/SiteMap Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RobotsTesting_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button processRobots;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.ListBox files;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.CheckBox useFullUrl;
        private System.Windows.Forms.LinkLabel loadedUrl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}