namespace MachineSystem.form.Pad
{
    partial class frmProduce_TeamAttend
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnUserTotalShow = new DevExpress.XtraEditors.SimpleButton();
            this.chkAll = new DevExpress.XtraEditors.CheckEdit();
            this.btnStatus = new DevExpress.XtraEditors.SimpleButton();
            this.btnScheduling = new DevExpress.XtraEditors.SimpleButton();
            this.Turnover = new DevExpress.XtraEditors.SimpleButton();
            this.btnLessFrequently = new DevExpress.XtraEditors.SimpleButton();
            this.btnOvertime = new DevExpress.XtraEditors.SimpleButton();
            this.btnForLeave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dateOperDate1 = new DevExpress.XtraEditors.TextEdit();
            this.lookmyteamName = new DevExpress.XtraEditors.TextEdit();
            this.panelContent = new System.Windows.Forms.FlowLayoutPanel();
            this.xtraScrollableControl1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookmyteamName.Properties)).BeginInit();
            this.xtraScrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.button1);
            this.panelTop.Controls.Add(this.simpleButton1);
            this.panelTop.Controls.Add(this.btnUserTotalShow);
            this.panelTop.Controls.Add(this.chkAll);
            this.panelTop.Controls.Add(this.btnStatus);
            this.panelTop.Controls.Add(this.btnScheduling);
            this.panelTop.Controls.Add(this.Turnover);
            this.panelTop.Controls.Add(this.btnLessFrequently);
            this.panelTop.Controls.Add(this.btnOvertime);
            this.panelTop.Controls.Add(this.btnForLeave);
            this.panelTop.Controls.Add(this.labelControl1);
            this.panelTop.Controls.Add(this.labelControl10);
            this.panelTop.Controls.Add(this.dateOperDate1);
            this.panelTop.Controls.Add(this.lookmyteamName);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1360, 141);
            this.panelTop.TabIndex = 10;
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Location = new System.Drawing.Point(1275, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 63);
            this.btnClose.TabIndex = 108;
            this.btnClose.Text = "退出";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.button1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.button1.Appearance.Options.UseFont = true;
            this.button1.Appearance.Options.UseForeColor = true;
            this.button1.Location = new System.Drawing.Point(1113, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 63);
            this.button1.TabIndex = 108;
            this.button1.Text = "人员调动";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Location = new System.Drawing.Point(774, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 63);
            this.simpleButton1.TabIndex = 107;
            this.simpleButton1.Text = "刷新";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnUserTotalShow
            // 
            this.btnUserTotalShow.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.btnUserTotalShow.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnUserTotalShow.Appearance.Options.UseFont = true;
            this.btnUserTotalShow.Appearance.Options.UseForeColor = true;
            this.btnUserTotalShow.Location = new System.Drawing.Point(939, 3);
            this.btnUserTotalShow.Name = "btnUserTotalShow";
            this.btnUserTotalShow.Size = new System.Drawing.Size(156, 63);
            this.btnUserTotalShow.TabIndex = 107;
            this.btnUserTotalShow.Text = "人员揭示";
            this.btnUserTotalShow.Click += new System.EventHandler(this.btnUserTotalShow_Click);
            // 
            // chkAll
            // 
            this.chkAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chkAll.Location = new System.Drawing.Point(861, 12);
            this.chkAll.Name = "chkAll";
            this.chkAll.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.chkAll.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.chkAll.Properties.Appearance.Options.UseFont = true;
            this.chkAll.Properties.Appearance.Options.UseForeColor = true;
            this.chkAll.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkAll.Properties.Caption = "全选";
            this.chkAll.Size = new System.Drawing.Size(82, 40);
            this.chkAll.TabIndex = 106;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // btnStatus
            // 
            this.btnStatus.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.btnStatus.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnStatus.Appearance.Options.UseFont = true;
            this.btnStatus.Appearance.Options.UseForeColor = true;
            this.btnStatus.Location = new System.Drawing.Point(1139, 72);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(214, 64);
            this.btnStatus.TabIndex = 93;
            this.btnStatus.Text = "人员状态";
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnScheduling
            // 
            this.btnScheduling.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.btnScheduling.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnScheduling.Appearance.Options.UseFont = true;
            this.btnScheduling.Appearance.Options.UseForeColor = true;
            this.btnScheduling.Location = new System.Drawing.Point(909, 72);
            this.btnScheduling.Name = "btnScheduling";
            this.btnScheduling.Size = new System.Drawing.Size(214, 64);
            this.btnScheduling.TabIndex = 93;
            this.btnScheduling.Text = "排班登记";
            this.btnScheduling.Click += new System.EventHandler(this.btnScheduling_Click);
            // 
            // Turnover
            // 
            this.Turnover.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.Turnover.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.Turnover.Appearance.Options.UseFont = true;
            this.Turnover.Appearance.Options.UseForeColor = true;
            this.Turnover.Location = new System.Drawing.Point(681, 72);
            this.Turnover.Name = "Turnover";
            this.Turnover.Size = new System.Drawing.Size(214, 64);
            this.Turnover.TabIndex = 93;
            this.Turnover.Text = "离职登记";
            this.Turnover.Click += new System.EventHandler(this.btnTurnover_Click);
            // 
            // btnLessFrequently
            // 
            this.btnLessFrequently.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.btnLessFrequently.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnLessFrequently.Appearance.Options.UseFont = true;
            this.btnLessFrequently.Appearance.Options.UseForeColor = true;
            this.btnLessFrequently.Location = new System.Drawing.Point(231, 72);
            this.btnLessFrequently.Name = "btnLessFrequently";
            this.btnLessFrequently.Size = new System.Drawing.Size(214, 64);
            this.btnLessFrequently.TabIndex = 93;
            this.btnLessFrequently.Text = "欠勤登记";
            this.btnLessFrequently.Click += new System.EventHandler(this.btnLessFrequently_Click);
            // 
            // btnOvertime
            // 
            this.btnOvertime.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.btnOvertime.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnOvertime.Appearance.Options.UseFont = true;
            this.btnOvertime.Appearance.Options.UseForeColor = true;
            this.btnOvertime.Location = new System.Drawing.Point(455, 72);
            this.btnOvertime.Name = "btnOvertime";
            this.btnOvertime.Size = new System.Drawing.Size(214, 64);
            this.btnOvertime.TabIndex = 92;
            this.btnOvertime.Text = "加班登记";
            this.btnOvertime.Click += new System.EventHandler(this.btnOvertime_Click);
            // 
            // btnForLeave
            // 
            this.btnForLeave.Appearance.Font = new System.Drawing.Font("微软雅黑", 32F);
            this.btnForLeave.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnForLeave.Appearance.Options.UseFont = true;
            this.btnForLeave.Appearance.Options.UseForeColor = true;
            this.btnForLeave.Location = new System.Drawing.Point(6, 72);
            this.btnForLeave.Name = "btnForLeave";
            this.btnForLeave.Size = new System.Drawing.Size(214, 64);
            this.btnForLeave.TabIndex = 92;
            this.btnForLeave.Text = "请假登记";
            this.btnForLeave.Click += new System.EventHandler(this.btnForLeave_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(505, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 21);
            this.labelControl1.TabIndex = 87;
            this.labelControl1.Text = "日期:";
            this.labelControl1.Visible = false;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl10.Location = new System.Drawing.Point(14, 25);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(36, 21);
            this.labelControl10.TabIndex = 85;
            this.labelControl10.Tag = "UserName";
            this.labelControl10.Text = "向别:";
            this.labelControl10.Visible = false;
            // 
            // dateOperDate1
            // 
            this.dateOperDate1.EditValue = "";
            this.dateOperDate1.Location = new System.Drawing.Point(502, 15);
            this.dateOperDate1.Name = "dateOperDate1";
            this.dateOperDate1.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.dateOperDate1.Properties.Appearance.Options.UseFont = true;
            this.dateOperDate1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateOperDate1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateOperDate1.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm";
            this.dateOperDate1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateOperDate1.Properties.MaxLength = 30;
            this.dateOperDate1.Size = new System.Drawing.Size(269, 38);
            this.dateOperDate1.TabIndex = 86;
            this.dateOperDate1.TabStop = false;
            this.dateOperDate1.Tag = "OperDate";
            this.dateOperDate1.EditValueChanged += new System.EventHandler(this.dateOperDate1_EditValueChanged);
            this.dateOperDate1.Click += new System.EventHandler(this.dateOperDate1_Click);
            // 
            // lookmyteamName
            // 
            this.lookmyteamName.EditValue = "";
            this.lookmyteamName.Location = new System.Drawing.Point(11, 15);
            this.lookmyteamName.Name = "lookmyteamName";
            this.lookmyteamName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.lookmyteamName.Properties.Appearance.Options.UseFont = true;
            this.lookmyteamName.Properties.MaxLength = 30;
            this.lookmyteamName.Size = new System.Drawing.Size(485, 38);
            this.lookmyteamName.TabIndex = 84;
            this.lookmyteamName.TabStop = false;
            this.lookmyteamName.Tag = "pMark";
            this.lookmyteamName.EditValueChanged += new System.EventHandler(this.lookmyteamName_EditValueChanged);
            this.lookmyteamName.Click += new System.EventHandler(this.lookmyteamName_Click);
            // 
            // panelContent
            // 
            this.panelContent.Location = new System.Drawing.Point(0, 2);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(2);
            this.panelContent.Size = new System.Drawing.Size(1340, 536);
            this.panelContent.TabIndex = 11;
            // 
            // xtraScrollableControl1
            // 
            this.xtraScrollableControl1.AllowTouchScroll = true;
            this.xtraScrollableControl1.Controls.Add(this.panelContent);
            this.xtraScrollableControl1.Location = new System.Drawing.Point(0, 140);
            this.xtraScrollableControl1.Name = "xtraScrollableControl1";
            this.xtraScrollableControl1.Size = new System.Drawing.Size(1360, 536);
            this.xtraScrollableControl1.TabIndex = 12;
            // 
            // frmProduce_TeamAttend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 691);
            this.Controls.Add(this.xtraScrollableControl1);
            this.Controls.Add(this.panelTop);
            this.MinimizeBox = false;
            this.Name = "frmProduce_TeamAttend";
            this.Text = "人员考勤";
            this.Deactivate += new System.EventHandler(this.frmProduce_TeamAttend_Deactivate);
            this.Load += new System.EventHandler(this.frmProduce_TeamAttend_Load);
            this.Leave += new System.EventHandler(this.frmProduce_TeamAttend_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAll.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookmyteamName.Properties)).EndInit();
            this.xtraScrollableControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton btnLessFrequently;
        private DevExpress.XtraEditors.SimpleButton btnForLeave;
        private DevExpress.XtraEditors.SimpleButton Turnover;
        private DevExpress.XtraEditors.SimpleButton btnOvertime;
        private DevExpress.XtraEditors.SimpleButton btnScheduling;
        private DevExpress.XtraEditors.TextEdit dateOperDate1;
        private DevExpress.XtraEditors.TextEdit lookmyteamName;
        private DevExpress.XtraEditors.CheckEdit chkAll;
        private DevExpress.XtraEditors.SimpleButton btnUserTotalShow;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton btnStatus;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.FlowLayoutPanel panelContent;
        private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl1;
    }
}