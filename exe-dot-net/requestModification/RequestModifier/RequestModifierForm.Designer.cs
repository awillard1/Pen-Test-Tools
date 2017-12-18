namespace RequestModifier
{
    partial class RequestModifierForm
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
            this.htmlOuputBox = new System.Windows.Forms.GroupBox();
            this.requestValue = new System.Windows.Forms.TextBox();
            this.saveHtml = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.requestddl = new System.Windows.Forms.ComboBox();
            this.htmlOuputBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // htmlOuputBox
            // 
            this.htmlOuputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlOuputBox.Controls.Add(this.requestValue);
            this.htmlOuputBox.Controls.Add(this.saveHtml);
            this.htmlOuputBox.Controls.Add(this.webBrowser1);
            this.htmlOuputBox.Location = new System.Drawing.Point(3, 39);
            this.htmlOuputBox.Name = "htmlOuputBox";
            this.htmlOuputBox.Size = new System.Drawing.Size(804, 546);
            this.htmlOuputBox.TabIndex = 1;
            this.htmlOuputBox.TabStop = false;
            this.htmlOuputBox.Text = "Html Form/Link";
            // 
            // requestValue
            // 
            this.requestValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.requestValue.Location = new System.Drawing.Point(407, 43);
            this.requestValue.Multiline = true;
            this.requestValue.Name = "requestValue";
            this.requestValue.Size = new System.Drawing.Size(391, 497);
            this.requestValue.TabIndex = 2;
            // 
            // saveHtml
            // 
            this.saveHtml.Location = new System.Drawing.Point(7, 14);
            this.saveHtml.Name = "saveHtml";
            this.saveHtml.Size = new System.Drawing.Size(49, 23);
            this.saveHtml.TabIndex = 1;
            this.saveHtml.Text = "Save";
            this.saveHtml.UseVisualStyleBackColor = true;
            this.saveHtml.Click += new System.EventHandler(this.saveHtml_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.Location = new System.Drawing.Point(7, 43);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(393, 497);
            this.webBrowser1.TabIndex = 0;
            // 
            // requestddl
            // 
            this.requestddl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.requestddl.FormattingEnabled = true;
            this.requestddl.Items.AddRange(new object[] {
            "Select Type to Convert",
            "GET to POST",
            "POST to GET"});
            this.requestddl.Location = new System.Drawing.Point(12, 12);
            this.requestddl.Name = "requestddl";
            this.requestddl.Size = new System.Drawing.Size(190, 21);
            this.requestddl.TabIndex = 2;
            this.requestddl.SelectedIndexChanged += new System.EventHandler(this.requestddl_SelectedIndexChanged);
            // 
            // RequestModifierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 589);
            this.Controls.Add(this.requestddl);
            this.Controls.Add(this.htmlOuputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RequestModifierForm";
            this.Text = "Request Modifier";
            this.htmlOuputBox.ResumeLayout(false);
            this.htmlOuputBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox htmlOuputBox;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button saveHtml;
        private System.Windows.Forms.ComboBox requestddl;
        private System.Windows.Forms.TextBox requestValue;
    }
}

