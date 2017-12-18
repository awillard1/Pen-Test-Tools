namespace RequestModifier
{
    public partial class GetToPost
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.processUrl = new System.Windows.Forms.Button();
            this.urlToProcess = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.processUrl);
            this.groupBox2.Controls.Add(this.urlToProcess);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(691, 250);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GET to POST Request";
            // 
            // processUrl
            // 
            this.processUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.processUrl.Location = new System.Drawing.Point(623, 220);
            this.processUrl.Name = "processUrl";
            this.processUrl.Size = new System.Drawing.Size(60, 23);
            this.processUrl.TabIndex = 4;
            this.processUrl.Text = "Process";
            this.processUrl.UseVisualStyleBackColor = true;
            this.processUrl.Click += new System.EventHandler(this.processUrl_Click);
            // 
            // urlToProcess
            // 
            this.urlToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.urlToProcess.Location = new System.Drawing.Point(6, 18);
            this.urlToProcess.Multiline = true;
            this.urlToProcess.Name = "urlToProcess";
            this.urlToProcess.Size = new System.Drawing.Size(677, 196);
            this.urlToProcess.TabIndex = 2;
            // 
            // GetToPost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 261);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GetToPost";
            this.Text = "GetToPost";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button processUrl;
        private System.Windows.Forms.TextBox urlToProcess;
    }
}