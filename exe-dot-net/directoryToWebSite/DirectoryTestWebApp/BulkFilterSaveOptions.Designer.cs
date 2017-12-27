namespace DirectoryTestWebApp
{
    partial class BulkFilterSaveOptions
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
            this.directoryFilter = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.processFilter = new System.Windows.Forms.Button();
            this.ignoreDirectory = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // directoryFilter
            // 
            this.directoryFilter.Location = new System.Drawing.Point(88, 19);
            this.directoryFilter.Name = "directoryFilter";
            this.directoryFilter.Size = new System.Drawing.Size(239, 20);
            this.directoryFilter.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.processFilter);
            this.groupBox1.Controls.Add(this.ignoreDirectory);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.directoryFilter);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 99);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // processFilter
            // 
            this.processFilter.Location = new System.Drawing.Point(250, 68);
            this.processFilter.Name = "processFilter";
            this.processFilter.Size = new System.Drawing.Size(75, 23);
            this.processFilter.TabIndex = 3;
            this.processFilter.Text = "Process";
            this.processFilter.UseVisualStyleBackColor = true;
            this.processFilter.Click += new System.EventHandler(this.processFilter_Click);
            // 
            // ignoreDirectory
            // 
            this.ignoreDirectory.AutoSize = true;
            this.ignoreDirectory.Location = new System.Drawing.Point(88, 45);
            this.ignoreDirectory.Name = "ignoreDirectory";
            this.ignoreDirectory.Size = new System.Drawing.Size(160, 17);
            this.ignoreDirectory.TabIndex = 2;
            this.ignoreDirectory.Text = "Ignore Directory (use parent)";
            this.ignoreDirectory.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Directory Filter:";
            // 
            // BulkFilterSaveOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 107);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BulkFilterSaveOptions";
            this.Text = "Bulk Filter Save Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox directoryFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button processFilter;
        private System.Windows.Forms.CheckBox ignoreDirectory;
        private System.Windows.Forms.Label label1;
    }
}