namespace MachineSystem.form.Pad
{
    partial class frmV_Attend_Line
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnNight = new DevExpress.XtraEditors.SimpleButton();
            this.btnDay = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblShowDate = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblSelfLineCnt = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblSupportLineCnt = new DevExpress.XtraEditors.LabelControl();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblAbnormalLineCnt = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAll);
            this.panel1.Controls.Add(this.btnNight);
            this.panel1.Controls.Add(this.btnDay);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblShowDate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 206);
            this.panel1.TabIndex = 1;
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(610, 23);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(105, 41);
            this.btnAll.TabIndex = 94;
            this.btnAll.Text = "全部";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnNight
            // 
            this.btnNight.Location = new System.Drawing.Point(839, 23);
            this.btnNight.Name = "btnNight";
            this.btnNight.Size = new System.Drawing.Size(105, 41);
            this.btnNight.TabIndex = 93;
            this.btnNight.Text = "晚班";
            this.btnNight.Click += new System.EventHandler(this.btnNight_Click);
            // 
            // btnDay
            // 
            this.btnDay.Location = new System.Drawing.Point(725, 23);
            this.btnDay.Name = "btnDay";
            this.btnDay.Size = new System.Drawing.Size(105, 41);
            this.btnDay.TabIndex = 92;
            this.btnDay.Text = "白班";
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("黑体", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(87, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(763, 80);
            this.label1.TabIndex = 1;
            this.label1.Text = "欠勤替关者对应一览";
            // 
            // lblShowDate
            // 
            this.lblShowDate.Appearance.Font = new System.Drawing.Font("黑体", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShowDate.Appearance.Options.UseFont = true;
            this.lblShowDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblShowDate.Location = new System.Drawing.Point(79, 23);
            this.lblShowDate.Name = "lblShowDate";
            this.lblShowDate.Size = new System.Drawing.Size(525, 37);
            this.lblShowDate.TabIndex = 1;
            this.lblShowDate.Text = "日期";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(64, 276);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(247, 48);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "自Line对应";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.Lime;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(27, 414);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(296, 48);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "其他Line支援";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(101, 549);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(198, 48);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "异常Line";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGreen;
            this.panel3.Controls.Add(this.lblSelfLineCnt);
            this.panel3.Location = new System.Drawing.Point(376, 237);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(288, 99);
            this.panel3.TabIndex = 7;
            // 
            // lblSelfLineCnt
            // 
            this.lblSelfLineCnt.Appearance.Font = new System.Drawing.Font("黑体", 56.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSelfLineCnt.Appearance.Options.UseFont = true;
            this.lblSelfLineCnt.Location = new System.Drawing.Point(94, 14);
            this.lblSelfLineCnt.Name = "lblSelfLineCnt";
            this.lblSelfLineCnt.Size = new System.Drawing.Size(0, 75);
            this.lblSelfLineCnt.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGreen;
            this.panel4.Controls.Add(this.lblSupportLineCnt);
            this.panel4.Location = new System.Drawing.Point(376, 374);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(288, 99);
            this.panel4.TabIndex = 8;
            // 
            // lblSupportLineCnt
            // 
            this.lblSupportLineCnt.Appearance.Font = new System.Drawing.Font("黑体", 56.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupportLineCnt.Appearance.Options.UseFont = true;
            this.lblSupportLineCnt.Location = new System.Drawing.Point(94, 13);
            this.lblSupportLineCnt.Name = "lblSupportLineCnt";
            this.lblSupportLineCnt.Size = new System.Drawing.Size(0, 75);
            this.lblSupportLineCnt.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightGreen;
            this.panel5.Controls.Add(this.lblAbnormalLineCnt);
            this.panel5.Location = new System.Drawing.Point(376, 509);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(288, 99);
            this.panel5.TabIndex = 9;
            // 
            // lblAbnormalLineCnt
            // 
            this.lblAbnormalLineCnt.Appearance.Font = new System.Drawing.Font("黑体", 56.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbnormalLineCnt.Appearance.Options.UseFont = true;
            this.lblAbnormalLineCnt.Location = new System.Drawing.Point(94, 17);
            this.lblAbnormalLineCnt.Name = "lblAbnormalLineCnt";
            this.lblAbnormalLineCnt.Size = new System.Drawing.Size(0, 75);
            this.lblAbnormalLineCnt.TabIndex = 0;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(696, 288);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(100, 48);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "Line";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(696, 426);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(100, 48);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Line";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("黑体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(696, 561);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(100, 48);
            this.labelControl6.TabIndex = 12;
            this.labelControl6.Text = "Line";
            // 
            // frmV_Attend_Line
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 675);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panel1);
            this.Name = "frmV_Attend_Line";
            this.Text = "欠勤者替关对应一览";
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl lblSelfLineCnt;
        private DevExpress.XtraEditors.LabelControl lblSupportLineCnt;
        private DevExpress.XtraEditors.LabelControl lblAbnormalLineCnt;
        private DevExpress.XtraEditors.LabelControl lblShowDate;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnNight;
        private DevExpress.XtraEditors.SimpleButton btnDay;
        private DevExpress.XtraEditors.SimpleButton btnAll;
    }
}