namespace MachineSystem.form.Pad
{
    partial class frmForLeave
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lookVacationType = new DevExpress.XtraEditors.LookUpEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCanel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMemo = new DevExpress.XtraEditors.MemoEdit();
            this.dtStartTime = new DevExpress.XtraEditors.TextEdit();
            this.dtEndTime = new DevExpress.XtraEditors.TextEdit();
            this.panelContent = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookVacationType.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(16, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 35);
            this.label2.TabIndex = 124;
            this.label2.Text = "开始日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label3.Location = new System.Drawing.Point(439, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 35);
            this.label3.TabIndex = 126;
            this.label3.Text = "~";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 35);
            this.label1.TabIndex = 125;
            this.label1.Text = "请假类型:";
            // 
            // lookVacationType
            // 
            this.lookVacationType.EditValue = "";
            this.lookVacationType.Location = new System.Drawing.Point(152, 12);
            this.lookVacationType.Name = "lookVacationType";
            this.lookVacationType.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.lookVacationType.Properties.Appearance.Options.UseFont = true;
            this.lookVacationType.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.lookVacationType.Properties.AppearanceDropDown.Options.UseFont = true;
            this.lookVacationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookVacationType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pName", "名称")});
            this.lookVacationType.Properties.MaxLength = 30;
            this.lookVacationType.Properties.NullText = "";
            this.lookVacationType.Size = new System.Drawing.Size(281, 42);
            this.lookVacationType.TabIndex = 120;
            this.lookVacationType.TabStop = false;
            this.lookVacationType.Tag = "OperDate";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.btnCanel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lookVacationType);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMemo);
            this.panel1.Controls.Add(this.dtStartTime);
            this.panel1.Controls.Add(this.dtEndTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 186);
            this.panel1.TabIndex = 127;
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnCanel.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.Appearance.Options.UseForeColor = true;
            this.btnCanel.Location = new System.Drawing.Point(793, 3);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(210, 51);
            this.btnCanel.TabIndex = 128;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(571, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(210, 51);
            this.btnSave.TabIndex = 127;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(16, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 35);
            this.label4.TabIndex = 124;
            this.label4.Text = "请假事由:";
            // 
            // txtMemo
            // 
            this.txtMemo.EditValue = "";
            this.txtMemo.Location = new System.Drawing.Point(152, 106);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.txtMemo.Properties.Appearance.Options.UseFont = true;
            this.txtMemo.Properties.MaxLength = 30;
            this.txtMemo.Size = new System.Drawing.Size(602, 74);
            this.txtMemo.TabIndex = 120;
            this.txtMemo.TabStop = false;
            this.txtMemo.Tag = "OperDate";
            // 
            // dtStartTime
            // 
            this.dtStartTime.EditValue = "yyyy-MM-dd  hh:mm";
            this.dtStartTime.Location = new System.Drawing.Point(152, 58);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.dtStartTime.Properties.Appearance.Options.UseFont = true;
            this.dtStartTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd  hh:mm";
            this.dtStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartTime.Properties.EditFormat.FormatString = "yyyy-MM-dd  hh:mm";
            this.dtStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartTime.Properties.MaxLength = 30;
            this.dtStartTime.Size = new System.Drawing.Size(281, 42);
            this.dtStartTime.TabIndex = 122;
            this.dtStartTime.TabStop = false;
            this.dtStartTime.Tag = "OperDate";
            this.dtStartTime.Click += new System.EventHandler(this.dtStartTime_Click);
            // 
            // dtEndTime
            // 
            this.dtEndTime.EditValue = "yyyy-MM-dd  hh:mm";
            this.dtEndTime.Location = new System.Drawing.Point(480, 58);
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.dtEndTime.Properties.Appearance.Options.UseFont = true;
            this.dtEndTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd  hh:mm";
            this.dtEndTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEndTime.Properties.EditFormat.FormatString = "yyyy-MM-dd  hh:mm";
            this.dtEndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtEndTime.Properties.MaxLength = 30;
            this.dtEndTime.Size = new System.Drawing.Size(284, 42);
            this.dtEndTime.TabIndex = 121;
            this.dtEndTime.TabStop = false;
            this.dtEndTime.Tag = "OperDate";
            this.dtEndTime.Click += new System.EventHandler(this.dtEndTime_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 186);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1094, 505);
            this.panelContent.TabIndex = 128;
            // 
            // frmForLeave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 691);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmForLeave";
            this.Text = "请假登记";
            this.Load += new System.EventHandler(this.frmForLeave_Load);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookVacationType.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMemo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LookUpEdit lookVacationType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.MemoEdit txtMemo;
        private DevExpress.XtraEditors.TextEdit dtStartTime;
        private DevExpress.XtraEditors.TextEdit dtEndTime;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel panelContent;
    }
}