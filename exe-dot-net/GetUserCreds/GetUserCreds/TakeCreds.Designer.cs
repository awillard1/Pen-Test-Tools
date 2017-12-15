namespace GetUserCreds
{
    partial class TakeCreds
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
            this.source = new System.Windows.Forms.TextBox();
            this.getSource = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.destination = new System.Windows.Forms.TextBox();
            this.getDest = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.specFolder = new System.Windows.Forms.Label();
            this.specialfolder = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.source);
            this.groupBox1.Controls.Add(this.getSource);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source File Location";
            // 
            // source
            // 
            this.source.Location = new System.Drawing.Point(7, 22);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(471, 20);
            this.source.TabIndex = 1;
            // 
            // getSource
            // 
            this.getSource.Location = new System.Drawing.Point(484, 19);
            this.getSource.Name = "getSource";
            this.getSource.Size = new System.Drawing.Size(75, 23);
            this.getSource.TabIndex = 0;
            this.getSource.Text = "Browse";
            this.getSource.UseVisualStyleBackColor = true;
            this.getSource.Click += new System.EventHandler(this.GetSource_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.destination);
            this.groupBox2.Controls.Add(this.getDest);
            this.groupBox2.Location = new System.Drawing.Point(13, 81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(565, 59);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // destination
            // 
            this.destination.Location = new System.Drawing.Point(7, 22);
            this.destination.Name = "destination";
            this.destination.Size = new System.Drawing.Size(471, 20);
            this.destination.TabIndex = 3;
            // 
            // getDest
            // 
            this.getDest.Location = new System.Drawing.Point(484, 19);
            this.getDest.Name = "getDest";
            this.getDest.Size = new System.Drawing.Size(75, 23);
            this.getDest.TabIndex = 2;
            this.getDest.Text = "Browse";
            this.getDest.UseVisualStyleBackColor = true;
            this.getDest.Click += new System.EventHandler(this.GetDest_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Simple - dir/user/AppData",
            "Complex - dir/user/UPM_Profile/AppData"});
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(553, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.specFolder);
            this.groupBox3.Controls.Add(this.specialfolder);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 146);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(565, 75);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Folder Layout";
            // 
            // specFolder
            // 
            this.specFolder.AutoSize = true;
            this.specFolder.Location = new System.Drawing.Point(141, 50);
            this.specFolder.Name = "specFolder";
            this.specFolder.Size = new System.Drawing.Size(156, 13);
            this.specFolder.TabIndex = 4;
            this.specFolder.Text = "Special Folder - ex UPM_Profile";
            // 
            // specialfolder
            // 
            this.specialfolder.Enabled = false;
            this.specialfolder.Location = new System.Drawing.Point(7, 47);
            this.specialfolder.Name = "specialfolder";
            this.specialfolder.Size = new System.Drawing.Size(128, 20);
            this.specialfolder.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Take Credentials";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TakeCreds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 259);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TakeCreds";
            this.Text = "aswsec - Get Firefox Credentials";
            this.Load += new System.EventHandler(this.TakeCreds_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.Button getSource;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox destination;
        private System.Windows.Forms.Button getDest;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label specFolder;
        private System.Windows.Forms.TextBox specialfolder;
        private System.Windows.Forms.Button button1;
    }
}

