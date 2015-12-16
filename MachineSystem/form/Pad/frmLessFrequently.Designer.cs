namespace MachineSystem.form.Pad
{
    partial class frmLessFrequently
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dtStartTime = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.dtStartTime);
            this.panel1.Controls.Add(this.btnCanel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 61);
            this.panel1.TabIndex = 128;
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.btnCanel.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.Appearance.Options.UseForeColor = true;
            this.btnCanel.Location = new System.Drawing.Point(812, 3);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(189, 52);
            this.btnCanel.TabIndex = 130;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.label2.Location = new System.Drawing.Point(29, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 35);
            this.label2.TabIndex = 124;
            this.label2.Text = "欠勤日期:";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("微软雅黑", 26F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(552, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(189, 52);
            this.btnSave.TabIndex = 131;
            this.btnSave.Text = "确定";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 61);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1094, 630);
            this.panelContent.TabIndex = 129;
            // 
            // dtStartTime
            // 
            this.dtStartTime.EditValue = null;
            this.dtStartTime.Location = new System.Drawing.Point(165, 11);
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F);
            this.dtStartTime.Properties.Appearance.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceDropDown.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.dtStartTime.Properties.AppearanceDropDown.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceDropDownHeader.Font = new System.Drawing.Font("微软雅黑", 23F);
            this.dtStartTime.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceDropDownHeaderHighlight.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.dtStartTime.Properties.AppearanceDropDownHeaderHighlight.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceDropDownHighlight.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.dtStartTime.Properties.AppearanceDropDownHighlight.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceFocused.Font = new System.Drawing.Font("微软雅黑", 22.5F);
            this.dtStartTime.Properties.AppearanceFocused.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.dtStartTime.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.dtStartTime.Properties.AppearanceWeekNumber.Font = new System.Drawing.Font("微软雅黑", 30F);
            this.dtStartTime.Properties.AppearanceWeekNumber.Options.UseFont = true;
            this.dtStartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartTime.Properties.CalendarTimeProperties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F);
            this.dtStartTime.Properties.CalendarTimeProperties.Appearance.Options.UseFont = true;
            this.dtStartTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtStartTime.Properties.CalendarTimeProperties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F4);
            this.dtStartTime.Properties.CalendarTimeProperties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Default;
            this.dtStartTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dtStartTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartTime.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dtStartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtStartTime.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dtStartTime.Size = new System.Drawing.Size(195, 42);
            this.dtStartTime.TabIndex = 140;
            this.dtStartTime.Tag = "OtDate";
            this.dtStartTime.Click += new System.EventHandler(this.dtStartTime_Click_2);
            // 
            // frmLessFrequently
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 691);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLessFrequently";
            this.Text = "欠勤登记";
            this.Load += new System.EventHandler(this.frmLessFrequently_Load);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel panelContent;
        private DevExpress.XtraEditors.DateEdit dtStartTime;
    }
}