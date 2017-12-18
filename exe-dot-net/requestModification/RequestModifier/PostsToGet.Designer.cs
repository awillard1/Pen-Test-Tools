namespace RequestModifier
{
    partial class PostsToGet
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
            this.HelpLabel = new System.Windows.Forms.Label();
            this.requestValue = new System.Windows.Forms.TextBox();
            this.processRaw = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HelpLabel);
            this.groupBox1.Controls.Add(this.requestValue);
            this.groupBox1.Controls.Add(this.processRaw);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 413);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "POST to GET Request";
            // 
            // HelpLabel
            // 
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.Location = new System.Drawing.Point(5, 20);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Size = new System.Drawing.Size(0, 13);
            this.HelpLabel.TabIndex = 2;
            // 
            // requestValue
            // 
            this.requestValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requestValue.Location = new System.Drawing.Point(6, 58);
            this.requestValue.Multiline = true;
            this.requestValue.Name = "requestValue";
            this.requestValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.requestValue.Size = new System.Drawing.Size(683, 320);
            this.requestValue.TabIndex = 0;
            // 
            // processRaw
            // 
            this.processRaw.Location = new System.Drawing.Point(629, 384);
            this.processRaw.Name = "processRaw";
            this.processRaw.Size = new System.Drawing.Size(60, 23);
            this.processRaw.TabIndex = 1;
            this.processRaw.Text = "Process";
            this.processRaw.UseVisualStyleBackColor = true;
            this.processRaw.Click += new System.EventHandler(this.processRaw_Click);
            // 
            // PostsToGet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 429);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PostsToGet";
            this.Text = "PostsToGet";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox requestValue;
        private System.Windows.Forms.Button processRaw;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label HelpLabel;
    }


}