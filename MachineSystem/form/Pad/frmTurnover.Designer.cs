namespace MachineSystem.form.Pad
{
    partial class frmTurnover
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
            this.btnCanel = new DevExpress.XtraEditors.SimpleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.dtSetDate = new DevExpress.XtraEditors.TextEdit();
            this.dtCallDate = new DevExpress.XtraEditors.TextEdit();
            this.cboLeaveType = new DevExpress.XtraEditors.LookUpEdit();
            this.comboBoxEditReason = new DevExpress.XtraEditors.LookUpEdit();
            this.panelContent = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSetDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCallDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeaveType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditReason.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.btnCanel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.dtSetDate);
            this.panel1.Controls.Add(this.dtCallDate);
            this.panel1.Controls.Add(this.cboLeaveType);
            this.panel1.Controls.Add(this.comboBoxEditReason);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 182);
            this.panel1.TabIndex = 128;
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.btnCanel.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.Appearance.Options.UseForeColor = true;
            this.btnCanel.Location = new System.Drawing.Point(908, 3);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(174, 52);
            this.btnCanel.TabIndex = 131;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label5.Location = new System.Drawing.Point(368, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 35);
            this.label5.TabIndex = 127;
            this.label5.Text = "离职原因:";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(704, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 52);
            this.btnSave.TabIndex = 130;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label4.Location = new System.Drawing.Point(26, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 35);
            this.label4.TabIndex = 124;
            this.label4.Text = "离职说明:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label2.Location = new System.Drawing.Point(30, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 35);
            this.label2.TabIndex = 124;
            this.label2.Text = "离职类型:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label3.Location = new System.Drawing.Point(347, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 35);
            this.label3.TabIndex = 125;
            this.label3.Text = "最后上班日:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 35);
            this.label1.TabIndex = 125;
            this.label1.Text = "离职提出日:";
            // 
            // txtMemo
            // 
            this.txtMemo.EditValue = "";
            this.txtMemo.Location = new System.Drawing.Point(167, 113);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtMemo.Properties.Appearance.Options.UseFont = true;
            this.txtMemo.Properties.MaxLength = 30;
            this.txtMemo.Size = new System.Drawing.Size(599, 58);
            this.txtMemo.TabIndex = 120;
            this.txtMemo.Tag = "OperDate";
            // 
            // dtSetDate
            // 
            this.dtSetDate.EditValue = "yyyy-MM-dd";
            this.dtSetDate.Location = new System.Drawing.Point(504, 13);
            this.dtSetDate.Name = "dtSetDate";
            this.dtSetDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.dtSetDate.Properties.Appearance.Options.UseFont = true;
            this.dtSetDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtSetDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtSetDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtSetDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtSetDate.Properties.MaxLength = 30;
            this.dtSetDate.Size = new System.Drawing.Size(176, 42);
            this.dtSetDate.TabIndex = 122;
            this.dtSetDate.TabStop = false;
            this.dtSetDate.Tag = "OperDate";
            this.dtSetDate.Click += new System.EventHandler(this.dtCallDate_Click);
            // 
            // dtCallDate
            // 
            this.dtCallDate.EditValue = "yyyy-MM-dd";
            this.dtCallDate.Location = new System.Drawing.Point(167, 13);
            this.dtCallDate.Name = "dtCallDate";
            this.dtCallDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.dtCallDate.Properties.Appearance.Options.UseFont = true;
            this.dtCallDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtCallDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtCallDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtCallDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtCallDate.Properties.MaxLength = 30;
            this.dtCallDate.Size = new System.Drawing.Size(174, 42);
            this.dtCallDate.TabIndex = 122;
            this.dtCallDate.TabStop = false;
            this.dtCallDate.Tag = "OperDate";
            this.dtCallDate.Click += new System.EventHandler(this.dtSetDate_Click);
            // 
            // cboLeaveType
            // 
            this.cboLeaveType.EditValue = "请选择";
            this.cboLeaveType.Location = new System.Drawing.Point(167, 61);
            this.cboLeaveType.Name = "cboLeaveType";
            this.cboLeaveType.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.cboLeaveType.Properties.Appearance.Options.UseFont = true;
            this.cboLeaveType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.cboLeaveType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.cboLeaveType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboLeaveType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pName", "pName")});
            this.cboLeaveType.Properties.MaxLength = 30;
            this.cboLeaveType.Properties.NullText = "";
            this.cboLeaveType.Size = new System.Drawing.Size(185, 42);
            this.cboLeaveType.TabIndex = 120;
            this.cboLeaveType.TabStop = false;
            this.cboLeaveType.Tag = "OperDate";
            // 
            // comboBoxEditReason
            // 
            this.comboBoxEditReason.EditValue = "请选择";
            this.comboBoxEditReason.Location = new System.Drawing.Point(504, 61);
            this.comboBoxEditReason.Name = "comboBoxEditReason";
            this.comboBoxEditReason.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.comboBoxEditReason.Properties.Appearance.Options.UseFont = true;
            this.comboBoxEditReason.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.comboBoxEditReason.Properties.AppearanceDropDown.Options.UseFont = true;
            this.comboBoxEditReason.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditReason.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pName", "pName")});
            this.comboBoxEditReason.Properties.MaxLength = 30;
            this.comboBoxEditReason.Properties.NullText = "";
            this.comboBoxEditReason.Size = new System.Drawing.Size(262, 42);
            this.comboBoxEditReason.TabIndex = 126;
            this.comboBoxEditReason.TabStop = false;
            this.comboBoxEditReason.Tag = "OperDate";
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 182);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1094, 509);
            this.panelContent.TabIndex = 134;
            // 
            // frmTurnover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 691);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTurnover";
            this.Text = "离职登记";
            this.Load += new System.EventHandler(this.frmTurnover_Load);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSetDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCallDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLeaveType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditReason.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit dtSetDate;
        private DevExpress.XtraEditors.TextEdit dtCallDate;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.LookUpEdit cboLeaveType;
        private DevExpress.XtraEditors.LookUpEdit comboBoxEditReason;
        private System.Windows.Forms.Panel panelContent;
    }
}