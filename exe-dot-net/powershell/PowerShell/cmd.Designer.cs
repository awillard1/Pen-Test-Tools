namespace PowerShell
{
    partial class cmd
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
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.execScript = new System.Windows.Forms.Button();
            this.pwscript = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.gb1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb1.Controls.Add(this.execScript);
            this.gb1.Controls.Add(this.pwscript);
            this.gb1.Location = new System.Drawing.Point(13, 13);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(560, 227);
            this.gb1.TabIndex = 0;
            this.gb1.TabStop = false;
            this.gb1.Text = "PowerShell";
            // 
            // execScript
            // 
            this.execScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.execScript.Location = new System.Drawing.Point(511, 197);
            this.execScript.Name = "execScript";
            this.execScript.Size = new System.Drawing.Size(43, 23);
            this.execScript.TabIndex = 1;
            this.execScript.Text = "run";
            this.execScript.UseVisualStyleBackColor = true;
            this.execScript.Click += new System.EventHandler(this.execScript_Click);
            // 
            // pwscript
            // 
            this.pwscript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pwscript.Location = new System.Drawing.Point(7, 13);
            this.pwscript.Multiline = true;
            this.pwscript.Name = "pwscript";
            this.pwscript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.pwscript.Size = new System.Drawing.Size(547, 178);
            this.pwscript.TabIndex = 0;
            this.pwscript.WordWrap = false;
            // 
            // output
            // 
            this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output.Location = new System.Drawing.Point(13, 245);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(560, 230);
            this.output.TabIndex = 1;
            this.output.WordWrap = false;
            // 
            // cmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 487);
            this.Controls.Add(this.output);
            this.Controls.Add(this.gb1);
            this.Name = "cmd";
            this.Text = "Execute Script";
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb1;
        private System.Windows.Forms.Button execScript;
        private System.Windows.Forms.TextBox pwscript;
        private System.Windows.Forms.TextBox output;
    }
}

