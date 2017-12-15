namespace ScanProjectManagement
{
    partial class Metrics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.metricsBox = new System.Windows.Forms.GroupBox();
            this.logsToProcess = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dte1 = new System.Windows.Forms.DateTimePicker();
            this.directoryText = new System.Windows.Forms.TextBox();
            this.browseForDir = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.browserChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtfReport = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.weekOfLabel = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metricsBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metricsBox
            // 
            this.metricsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metricsBox.Controls.Add(this.logsToProcess);
            this.metricsBox.Controls.Add(this.button2);
            this.metricsBox.Controls.Add(this.dte1);
            this.metricsBox.Controls.Add(this.directoryText);
            this.metricsBox.Controls.Add(this.browseForDir);
            this.metricsBox.Location = new System.Drawing.Point(6, 6);
            this.metricsBox.Name = "metricsBox";
            this.metricsBox.Size = new System.Drawing.Size(270, 400);
            this.metricsBox.TabIndex = 4;
            this.metricsBox.TabStop = false;
            this.metricsBox.Text = "Metrics";
            // 
            // logsToProcess
            // 
            this.logsToProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsToProcess.FormattingEnabled = true;
            this.logsToProcess.Location = new System.Drawing.Point(0, 123);
            this.logsToProcess.Name = "logsToProcess";
            this.logsToProcess.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.logsToProcess.Size = new System.Drawing.Size(264, 264);
            this.logsToProcess.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(210, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Process";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dte1
            // 
            this.dte1.CustomFormat = "MMMM yyyy";
            this.dte1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dte1.Location = new System.Drawing.Point(6, 19);
            this.dte1.Name = "dte1";
            this.dte1.Size = new System.Drawing.Size(132, 20);
            this.dte1.TabIndex = 6;
            // 
            // directoryText
            // 
            this.directoryText.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.directoryText.Location = new System.Drawing.Point(6, 45);
            this.directoryText.Name = "directoryText";
            this.directoryText.ReadOnly = true;
            this.directoryText.Size = new System.Drawing.Size(201, 20);
            this.directoryText.TabIndex = 5;
            // 
            // browseForDir
            // 
            this.browseForDir.Location = new System.Drawing.Point(210, 43);
            this.browseForDir.Name = "browseForDir";
            this.browseForDir.Size = new System.Drawing.Size(57, 23);
            this.browseForDir.TabIndex = 4;
            this.browseForDir.Text = "Browse";
            this.browseForDir.UseVisualStyleBackColor = true;
            this.browseForDir.Click += new System.EventHandler(this.browseForDir_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(942, 435);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.browserChart);
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Controls.Add(this.metricsBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(934, 409);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Metrics";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // browserChart
            // 
            this.browserChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.browserChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.browserChart.Legends.Add(legend1);
            this.browserChart.Location = new System.Drawing.Point(282, 128);
            this.browserChart.Name = "browserChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Accessed";
            series1.XValueMember = "Url";
            series1.YValueMembers = "AccessedCount";
            this.browserChart.Series.Add(series1);
            this.browserChart.Size = new System.Drawing.Size(646, 270);
            this.browserChart.TabIndex = 6;
            this.browserChart.Text = "Pen Testing via Web Browser";
            title1.Name = "IP Address";
            title1.Text = "Pen Testing via Web Browser";
            this.browserChart.Titles.Add(title1);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(282, 5);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Failed";
            series2.XValueMember = "IPHostname";
            series2.YValueMembers = "failedLogin";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Sucessful";
            series3.XValueMember = "IPHostname";
            series3.YValueMembers = "successfulLogin";
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(646, 118);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "SSH Login Attempts By Server";
            title2.Name = "IP Address";
            title2.Text = "SSH Login Attempts By Server";
            this.chart1.Titles.Add(title2);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtfReport);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.weekOfLabel);
            this.tabPage2.Controls.Add(this.monthCalendar1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(934, 409);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Weekly Report";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtfReport
            // 
            this.rtfReport.Location = new System.Drawing.Point(278, 40);
            this.rtfReport.Name = "rtfReport";
            this.rtfReport.Size = new System.Drawing.Size(653, 348);
            this.rtfReport.TabIndex = 3;
            this.rtfReport.Text = "";
            this.rtfReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rtfReport_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Project";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // weekOfLabel
            // 
            this.weekOfLabel.AutoSize = true;
            this.weekOfLabel.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weekOfLabel.Location = new System.Drawing.Point(272, 3);
            this.weekOfLabel.Name = "weekOfLabel";
            this.weekOfLabel.Size = new System.Drawing.Size(273, 33);
            this.weekOfLabel.TabIndex = 1;
            this.weekOfLabel.Text = "[Date Range for Report]";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.FirstDayOfWeek = System.Windows.Forms.Day.Friday;
            this.monthCalendar1.Location = new System.Drawing.Point(9, 40);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowWeekNumbers = true;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.openProjectToolStripMenuItem.Text = "&Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // Metrics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 465);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Metrics";
            this.Text = "Metrics";
            this.Load += new System.EventHandler(this.Metrics_Load);
            this.metricsBox.ResumeLayout(false);
            this.metricsBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox metricsBox;
        private System.Windows.Forms.Button browseForDir;
        private System.Windows.Forms.TextBox directoryText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label weekOfLabel;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtfReport;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dte1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ListBox logsToProcess;
        private System.Windows.Forms.DataVisualization.Charting.Chart browserChart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}