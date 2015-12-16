namespace MachineSystem.form.Search
{
    partial class FrmSetDateTime
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
            this.txtYear = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusYear = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddYear = new DevExpress.XtraEditors.SimpleButton();
            this.txtMonth = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusMonth = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddMonth = new DevExpress.XtraEditors.SimpleButton();
            this.txtDay = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusDay = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddDay = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanel = new DevExpress.XtraEditors.SimpleButton();
            this.lblnoLicense = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtHour = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusHour = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddHour = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSecond = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusSecond = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddSecond = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYear
            // 
            this.txtYear.EditValue = "";
            this.txtYear.Location = new System.Drawing.Point(24, 92);
            this.txtYear.Name = "txtYear";
            this.txtYear.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtYear.Properties.Appearance.Options.UseFont = true;
            this.txtYear.Properties.MaxLength = 4;
            this.txtYear.Size = new System.Drawing.Size(120, 42);
            this.txtYear.TabIndex = 92;
            this.txtYear.TabStop = false;
            this.txtYear.Tag = "pMark";
            // 
            // btnMinusYear
            // 
            this.btnMinusYear.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusYear.Appearance.Options.UseFont = true;
            this.btnMinusYear.Location = new System.Drawing.Point(24, 161);
            this.btnMinusYear.Name = "btnMinusYear";
            this.btnMinusYear.Size = new System.Drawing.Size(153, 71);
            this.btnMinusYear.TabIndex = 94;
            this.btnMinusYear.Tag = "Year";
            this.btnMinusYear.Text = "-";
            this.btnMinusYear.Click += new System.EventHandler(this.btnMinusYear_Click);
            this.btnMinusYear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinusYear_MouseDown);
            this.btnMinusYear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinusYear_MouseUp);
            // 
            // btnAddYear
            // 
            this.btnAddYear.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddYear.Appearance.Options.UseFont = true;
            this.btnAddYear.Location = new System.Drawing.Point(24, 6);
            this.btnAddYear.Name = "btnAddYear";
            this.btnAddYear.Size = new System.Drawing.Size(153, 71);
            this.btnAddYear.TabIndex = 94;
            this.btnAddYear.Tag = "Year";
            this.btnAddYear.Text = "+";
            this.btnAddYear.Click += new System.EventHandler(this.btnAddYear_Click);
            this.btnAddYear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddYear_MouseDown);
            this.btnAddYear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddYear_MouseUp);
            // 
            // txtMonth
            // 
            this.txtMonth.EditValue = "";
            this.txtMonth.Location = new System.Drawing.Point(233, 96);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtMonth.Properties.Appearance.Options.UseFont = true;
            this.txtMonth.Properties.MaxLength = 2;
            this.txtMonth.Size = new System.Drawing.Size(120, 42);
            this.txtMonth.TabIndex = 92;
            this.txtMonth.TabStop = false;
            this.txtMonth.Tag = "pMark";
            // 
            // btnMinusMonth
            // 
            this.btnMinusMonth.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusMonth.Appearance.Options.UseFont = true;
            this.btnMinusMonth.Location = new System.Drawing.Point(233, 161);
            this.btnMinusMonth.Name = "btnMinusMonth";
            this.btnMinusMonth.Size = new System.Drawing.Size(153, 71);
            this.btnMinusMonth.TabIndex = 94;
            this.btnMinusMonth.Tag = "Month";
            this.btnMinusMonth.Text = "-";
            this.btnMinusMonth.Click += new System.EventHandler(this.btnMinusMonth_Click);
            this.btnMinusMonth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinusMonth_MouseDown);
            this.btnMinusMonth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinusMonth_MouseUp);
            // 
            // btnAddMonth
            // 
            this.btnAddMonth.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddMonth.Appearance.Options.UseFont = true;
            this.btnAddMonth.Location = new System.Drawing.Point(233, 6);
            this.btnAddMonth.Name = "btnAddMonth";
            this.btnAddMonth.Size = new System.Drawing.Size(153, 71);
            this.btnAddMonth.TabIndex = 94;
            this.btnAddMonth.Tag = "Month";
            this.btnAddMonth.Text = "+";
            this.btnAddMonth.Click += new System.EventHandler(this.btnAddMonth_Click);
            this.btnAddMonth.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddMonth_MouseDown);
            this.btnAddMonth.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddMonth_MouseUp);
            // 
            // txtDay
            // 
            this.txtDay.EditValue = "";
            this.txtDay.Location = new System.Drawing.Point(425, 98);
            this.txtDay.Name = "txtDay";
            this.txtDay.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtDay.Properties.Appearance.Options.UseFont = true;
            this.txtDay.Properties.MaxLength = 2;
            this.txtDay.Size = new System.Drawing.Size(126, 42);
            this.txtDay.TabIndex = 92;
            this.txtDay.TabStop = false;
            this.txtDay.Tag = "pMark";
            // 
            // btnMinusDay
            // 
            this.btnMinusDay.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusDay.Appearance.Options.UseFont = true;
            this.btnMinusDay.Location = new System.Drawing.Point(425, 161);
            this.btnMinusDay.Name = "btnMinusDay";
            this.btnMinusDay.Size = new System.Drawing.Size(153, 71);
            this.btnMinusDay.TabIndex = 94;
            this.btnMinusDay.Tag = "Day";
            this.btnMinusDay.Text = "-";
            this.btnMinusDay.Click += new System.EventHandler(this.btnMinusDay_Click);
            this.btnMinusDay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinusDay_MouseDown);
            this.btnMinusDay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinusDay_MouseUp);
            // 
            // btnAddDay
            // 
            this.btnAddDay.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddDay.Appearance.Options.UseFont = true;
            this.btnAddDay.Location = new System.Drawing.Point(425, 6);
            this.btnAddDay.Name = "btnAddDay";
            this.btnAddDay.Size = new System.Drawing.Size(153, 71);
            this.btnAddDay.TabIndex = 94;
            this.btnAddDay.Tag = "Day";
            this.btnAddDay.Text = "+";
            this.btnAddDay.Click += new System.EventHandler(this.btnAddDay_Click);
            this.btnAddDay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddDay_MouseDown);
            this.btnAddDay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddDay_MouseUp);
            // 
            // btnEnter
            // 
            this.btnEnter.Appearance.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnEnter.Location = new System.Drawing.Point(110, 311);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(276, 95);
            this.btnEnter.TabIndex = 94;
            this.btnEnter.Text = "确定";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnCanel.Location = new System.Drawing.Point(505, 311);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(264, 95);
            this.btnCanel.TabIndex = 94;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // lblnoLicense
            // 
            this.lblnoLicense.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblnoLicense.Appearance.Options.UseFont = true;
            this.lblnoLicense.Appearance.Options.UseTextOptions = true;
            this.lblnoLicense.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblnoLicense.Location = new System.Drawing.Point(150, 97);
            this.lblnoLicense.Name = "lblnoLicense";
            this.lblnoLicense.Size = new System.Drawing.Size(27, 35);
            this.lblnoLicense.TabIndex = 95;
            this.lblnoLicense.Tag = "UserName";
            this.lblnoLicense.Text = "年";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(359, 99);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 35);
            this.labelControl1.TabIndex = 95;
            this.labelControl1.Tag = "UserName";
            this.labelControl1.Text = "月";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.Location = new System.Drawing.Point(557, 98);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(27, 35);
            this.labelControl2.TabIndex = 95;
            this.labelControl2.Tag = "UserName";
            this.labelControl2.Text = "日";
            // 
            // txtHour
            // 
            this.txtHour.EditValue = "";
            this.txtHour.Location = new System.Drawing.Point(616, 96);
            this.txtHour.Name = "txtHour";
            this.txtHour.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtHour.Properties.Appearance.Options.UseFont = true;
            this.txtHour.Properties.MaxLength = 2;
            this.txtHour.Size = new System.Drawing.Size(126, 42);
            this.txtHour.TabIndex = 92;
            this.txtHour.TabStop = false;
            this.txtHour.Tag = "pMark";
            // 
            // btnMinusHour
            // 
            this.btnMinusHour.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusHour.Appearance.Options.UseFont = true;
            this.btnMinusHour.Location = new System.Drawing.Point(616, 161);
            this.btnMinusHour.Name = "btnMinusHour";
            this.btnMinusHour.Size = new System.Drawing.Size(153, 71);
            this.btnMinusHour.TabIndex = 94;
            this.btnMinusHour.Tag = "Day";
            this.btnMinusHour.Text = "-";
            this.btnMinusHour.Click += new System.EventHandler(this.btnMinusHour_Click);
            this.btnMinusHour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinusHour_MouseDown);
            this.btnMinusHour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinusHour_MouseUp);
            // 
            // btnAddHour
            // 
            this.btnAddHour.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddHour.Appearance.Options.UseFont = true;
            this.btnAddHour.Location = new System.Drawing.Point(616, 6);
            this.btnAddHour.Name = "btnAddHour";
            this.btnAddHour.Size = new System.Drawing.Size(153, 71);
            this.btnAddHour.TabIndex = 94;
            this.btnAddHour.Tag = "Day";
            this.btnAddHour.Text = "+";
            this.btnAddHour.Click += new System.EventHandler(this.btnAddHour_Click);
            this.btnAddHour.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddHour_MouseDown);
            this.btnAddHour.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddHour_MouseUp);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(748, 97);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 35);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Tag = "UserName";
            this.labelControl3.Text = "时";
            // 
            // txtSecond
            // 
            this.txtSecond.EditValue = "";
            this.txtSecond.Location = new System.Drawing.Point(819, 94);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtSecond.Properties.Appearance.Options.UseFont = true;
            this.txtSecond.Properties.Mask.EditMask = "f0";
            this.txtSecond.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtSecond.Properties.MaxLength = 2;
            this.txtSecond.Size = new System.Drawing.Size(126, 42);
            this.txtSecond.TabIndex = 92;
            this.txtSecond.TabStop = false;
            this.txtSecond.Tag = "pMark";
            // 
            // btnMinusSecond
            // 
            this.btnMinusSecond.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusSecond.Appearance.Options.UseFont = true;
            this.btnMinusSecond.Location = new System.Drawing.Point(819, 162);
            this.btnMinusSecond.Name = "btnMinusSecond";
            this.btnMinusSecond.Size = new System.Drawing.Size(153, 71);
            this.btnMinusSecond.TabIndex = 94;
            this.btnMinusSecond.Tag = "Day";
            this.btnMinusSecond.Text = "-";
            this.btnMinusSecond.Click += new System.EventHandler(this.btnMinusSecond_Click);
            this.btnMinusSecond.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMinusSecond_MouseDown);
            this.btnMinusSecond.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMinusSecond_MouseUp);
            // 
            // btnAddSecond
            // 
            this.btnAddSecond.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddSecond.Appearance.Options.UseFont = true;
            this.btnAddSecond.Location = new System.Drawing.Point(819, 6);
            this.btnAddSecond.Name = "btnAddSecond";
            this.btnAddSecond.Size = new System.Drawing.Size(153, 71);
            this.btnAddSecond.TabIndex = 94;
            this.btnAddSecond.Tag = "Day";
            this.btnAddSecond.Text = "+";
            this.btnAddSecond.Click += new System.EventHandler(this.btnAddSecond_Click);
            this.btnAddSecond.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAddSecond_MouseDown);
            this.btnAddSecond.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAddSecond_MouseUp);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Location = new System.Drawing.Point(951, 91);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(27, 35);
            this.labelControl4.TabIndex = 95;
            this.labelControl4.Tag = "UserName";
            this.labelControl4.Text = "分";
            // 
            // FrmSetDateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 430);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblnoLicense);
            this.Controls.Add(this.btnAddSecond);
            this.Controls.Add(this.btnAddHour);
            this.Controls.Add(this.btnAddDay);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnMinusSecond);
            this.Controls.Add(this.btnAddMonth);
            this.Controls.Add(this.btnMinusHour);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.btnAddYear);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.btnMinusDay);
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.btnMinusMonth);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.btnMinusYear);
            this.Controls.Add(this.txtYear);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetDateTime";
            this.Text = "时间设置";
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtYear;
        private DevExpress.XtraEditors.SimpleButton btnMinusYear;
        private DevExpress.XtraEditors.SimpleButton btnAddYear;
        private DevExpress.XtraEditors.TextEdit txtMonth;
        private DevExpress.XtraEditors.SimpleButton btnMinusMonth;
        private DevExpress.XtraEditors.SimpleButton btnAddMonth;
        private DevExpress.XtraEditors.TextEdit txtDay;
        private DevExpress.XtraEditors.SimpleButton btnMinusDay;
        private DevExpress.XtraEditors.SimpleButton btnAddDay;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
        private DevExpress.XtraEditors.LabelControl lblnoLicense;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtHour;
        private DevExpress.XtraEditors.SimpleButton btnMinusHour;
        private DevExpress.XtraEditors.SimpleButton btnAddHour;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSecond;
        private DevExpress.XtraEditors.SimpleButton btnMinusSecond;
        private DevExpress.XtraEditors.SimpleButton btnAddSecond;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}