namespace MachineSystem.form.Pad
{
    partial class frmOvertimeHours
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
            this.txtHour = new DevExpress.XtraEditors.TextEdit();
            this.btnAddHour = new DevExpress.XtraEditors.SimpleButton();
            this.btnMinusHour = new DevExpress.XtraEditors.SimpleButton();
            this.txtHour2 = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusHour2 = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddHour2 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.txtSecond = new DevExpress.XtraEditors.TextEdit();
            this.btnMinusSecond = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddSecond = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanel = new DevExpress.XtraEditors.SimpleButton();
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHour
            // 
            this.txtHour.EditValue = "0";
            this.txtHour.Location = new System.Drawing.Point(151, 131);
            this.txtHour.Name = "txtHour";
            this.txtHour.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtHour.Properties.Appearance.Options.UseFont = true;
            this.txtHour.Properties.Mask.EditMask = "d";
            this.txtHour.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHour.Properties.MaxLength = 2;
            this.txtHour.Properties.ReadOnly = true;
            this.txtHour.Size = new System.Drawing.Size(126, 42);
            this.txtHour.TabIndex = 93;
            this.txtHour.TabStop = false;
            this.txtHour.EditValueChanged += new System.EventHandler(this.txtHour_EditValueChanged);
            // 
            // btnAddHour
            // 
            this.btnAddHour.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddHour.Appearance.Options.UseFont = true;
            this.btnAddHour.Location = new System.Drawing.Point(138, 39);
            this.btnAddHour.Name = "btnAddHour";
            this.btnAddHour.Size = new System.Drawing.Size(153, 71);
            this.btnAddHour.TabIndex = 96;
            this.btnAddHour.Tag = "Year";
            this.btnAddHour.Text = "+";
            this.btnAddHour.Click += new System.EventHandler(this.btnAddHour_Click);
            // 
            // btnMinusHour
            // 
            this.btnMinusHour.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusHour.Appearance.Options.UseFont = true;
            this.btnMinusHour.Location = new System.Drawing.Point(138, 194);
            this.btnMinusHour.Name = "btnMinusHour";
            this.btnMinusHour.Size = new System.Drawing.Size(153, 71);
            this.btnMinusHour.TabIndex = 95;
            this.btnMinusHour.Tag = "Year";
            this.btnMinusHour.Text = "-";
            this.btnMinusHour.Click += new System.EventHandler(this.btnMinusHour_Click);
            // 
            // txtHour2
            // 
            this.txtHour2.EditValue = "0";
            this.txtHour2.Location = new System.Drawing.Point(369, 131);
            this.txtHour2.Name = "txtHour2";
            this.txtHour2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtHour2.Properties.Appearance.Options.UseFont = true;
            this.txtHour2.Properties.Mask.EditMask = "d";
            this.txtHour2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtHour2.Properties.MaxLength = 2;
            this.txtHour2.Properties.ReadOnly = true;
            this.txtHour2.Size = new System.Drawing.Size(126, 42);
            this.txtHour2.TabIndex = 93;
            this.txtHour2.TabStop = false;
            this.txtHour2.Tag = "pMark";
            // 
            // btnMinusHour2
            // 
            this.btnMinusHour2.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusHour2.Appearance.Options.UseFont = true;
            this.btnMinusHour2.Location = new System.Drawing.Point(356, 194);
            this.btnMinusHour2.Name = "btnMinusHour2";
            this.btnMinusHour2.Size = new System.Drawing.Size(153, 71);
            this.btnMinusHour2.TabIndex = 95;
            this.btnMinusHour2.Tag = "Year";
            this.btnMinusHour2.Text = "-";
            this.btnMinusHour2.Click += new System.EventHandler(this.btnMinusHour2_Click);
            // 
            // btnAddHour2
            // 
            this.btnAddHour2.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddHour2.Appearance.Options.UseFont = true;
            this.btnAddHour2.Location = new System.Drawing.Point(356, 39);
            this.btnAddHour2.Name = "btnAddHour2";
            this.btnAddHour2.Size = new System.Drawing.Size(153, 71);
            this.btnAddHour2.TabIndex = 96;
            this.btnAddHour2.Tag = "Year";
            this.btnAddHour2.Text = "+";
            this.btnAddHour2.Click += new System.EventHandler(this.btnAddHour2_Click);
            // 
            // textEdit2
            // 
            this.textEdit2.EditValue = ".";
            this.textEdit2.Location = new System.Drawing.Point(549, 131);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.textEdit2.Properties.Appearance.Options.UseFont = true;
            this.textEdit2.Properties.MaxLength = 2;
            this.textEdit2.Properties.ReadOnly = true;
            this.textEdit2.Size = new System.Drawing.Size(46, 42);
            this.textEdit2.TabIndex = 93;
            this.textEdit2.TabStop = false;
            this.textEdit2.Tag = "pMark";
            // 
            // txtSecond
            // 
            this.txtSecond.EditValue = "0";
            this.txtSecond.Location = new System.Drawing.Point(634, 131);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtSecond.Properties.Appearance.Options.UseFont = true;
            this.txtSecond.Properties.MaxLength = 2;
            this.txtSecond.Properties.ReadOnly = true;
            this.txtSecond.Size = new System.Drawing.Size(126, 42);
            this.txtSecond.TabIndex = 93;
            this.txtSecond.TabStop = false;
            this.txtSecond.Tag = "pMark";
            // 
            // btnMinusSecond
            // 
            this.btnMinusSecond.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMinusSecond.Appearance.Options.UseFont = true;
            this.btnMinusSecond.Location = new System.Drawing.Point(621, 194);
            this.btnMinusSecond.Name = "btnMinusSecond";
            this.btnMinusSecond.Size = new System.Drawing.Size(153, 71);
            this.btnMinusSecond.TabIndex = 95;
            this.btnMinusSecond.Tag = "Year";
            this.btnMinusSecond.Text = "-";
            this.btnMinusSecond.Click += new System.EventHandler(this.btnMinusSecond_Click);
            // 
            // btnAddSecond
            // 
            this.btnAddSecond.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddSecond.Appearance.Options.UseFont = true;
            this.btnAddSecond.Location = new System.Drawing.Point(621, 39);
            this.btnAddSecond.Name = "btnAddSecond";
            this.btnAddSecond.Size = new System.Drawing.Size(153, 71);
            this.btnAddSecond.TabIndex = 96;
            this.btnAddSecond.Tag = "Year";
            this.btnAddSecond.Text = "+";
            this.btnAddSecond.Click += new System.EventHandler(this.btnAddSecond_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnCanel.Location = new System.Drawing.Point(534, 310);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(264, 95);
            this.btnCanel.TabIndex = 98;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Appearance.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnEnter.Location = new System.Drawing.Point(103, 310);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(276, 95);
            this.btnEnter.TabIndex = 97;
            this.btnEnter.Text = "确定";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // frmOvertimeHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 430);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnAddSecond);
            this.Controls.Add(this.btnMinusSecond);
            this.Controls.Add(this.btnAddHour2);
            this.Controls.Add(this.btnMinusHour2);
            this.Controls.Add(this.btnAddHour);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.txtHour2);
            this.Controls.Add(this.btnMinusHour);
            this.Controls.Add(this.txtHour);
            this.Name = "frmOvertimeHours";
            this.Text = "加班时数设置";
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtHour;
        private DevExpress.XtraEditors.SimpleButton btnAddHour;
        private DevExpress.XtraEditors.SimpleButton btnMinusHour;
        private DevExpress.XtraEditors.TextEdit txtHour2;
        private DevExpress.XtraEditors.SimpleButton btnMinusHour2;
        private DevExpress.XtraEditors.SimpleButton btnAddHour2;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.TextEdit txtSecond;
        private DevExpress.XtraEditors.SimpleButton btnMinusSecond;
        private DevExpress.XtraEditors.SimpleButton btnAddSecond;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
    }
}