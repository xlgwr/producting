namespace MachineSystem.form.Search
{
    partial class FrmMoveDialog
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
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.lblnoLicense = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtType = new DevExpress.XtraEditors.LabelControl();
            this.textEndDate = new DevExpress.XtraEditors.LabelControl();
            this.textStarDate = new DevExpress.XtraEditors.LabelControl();
            this.txtTo = new DevExpress.XtraEditors.LabelControl();
            this.txtFrom = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Appearance.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Appearance.Options.UseFont = true;
            this.btnOK.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnOK.Location = new System.Drawing.Point(110, 333);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(256, 71);
            this.btnOK.TabIndex = 94;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblnoLicense
            // 
            this.lblnoLicense.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblnoLicense.Appearance.Options.UseFont = true;
            this.lblnoLicense.Appearance.Options.UseTextOptions = true;
            this.lblnoLicense.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblnoLicense.Location = new System.Drawing.Point(11, 56);
            this.lblnoLicense.Name = "lblnoLicense";
            this.lblnoLicense.Size = new System.Drawing.Size(135, 35);
            this.lblnoLicense.TabIndex = 95;
            this.lblnoLicense.Tag = "UserName";
            this.lblnoLicense.Text = "调动类型：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(11, 109);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(135, 35);
            this.labelControl1.TabIndex = 95;
            this.labelControl1.Tag = "UserName";
            this.labelControl1.Text = "调出关位：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.Location = new System.Drawing.Point(11, 168);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(135, 35);
            this.labelControl2.TabIndex = 95;
            this.labelControl2.Tag = "UserName";
            this.labelControl2.Text = "调入关位：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(11, 222);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(135, 35);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Tag = "UserName";
            this.labelControl3.Text = "开始时间：";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Location = new System.Drawing.Point(11, 281);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(135, 35);
            this.labelControl4.TabIndex = 97;
            this.labelControl4.Tag = "UserName";
            this.labelControl4.Text = "结束时间：";
            // 
            // txtType
            // 
            this.txtType.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtType.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtType.Appearance.Options.UseFont = true;
            this.txtType.Appearance.Options.UseForeColor = true;
            this.txtType.Appearance.Options.UseTextOptions = true;
            this.txtType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtType.Location = new System.Drawing.Point(152, 61);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(56, 35);
            this.txtType.TabIndex = 98;
            this.txtType.Tag = "UserName";
            this.txtType.Text = "xxxx";
            // 
            // textEndDate
            // 
            this.textEndDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textEndDate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.textEndDate.Appearance.Options.UseFont = true;
            this.textEndDate.Appearance.Options.UseForeColor = true;
            this.textEndDate.Appearance.Options.UseTextOptions = true;
            this.textEndDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEndDate.Location = new System.Drawing.Point(152, 281);
            this.textEndDate.Name = "textEndDate";
            this.textEndDate.Size = new System.Drawing.Size(56, 35);
            this.textEndDate.TabIndex = 99;
            this.textEndDate.Tag = "UserName";
            this.textEndDate.Text = "xxxx";
            // 
            // textStarDate
            // 
            this.textStarDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textStarDate.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.textStarDate.Appearance.Options.UseFont = true;
            this.textStarDate.Appearance.Options.UseForeColor = true;
            this.textStarDate.Appearance.Options.UseTextOptions = true;
            this.textStarDate.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textStarDate.Location = new System.Drawing.Point(152, 222);
            this.textStarDate.Name = "textStarDate";
            this.textStarDate.Size = new System.Drawing.Size(56, 35);
            this.textStarDate.TabIndex = 100;
            this.textStarDate.Tag = "UserName";
            this.textStarDate.Text = "xxxx";
            // 
            // txtTo
            // 
            this.txtTo.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTo.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtTo.Appearance.Options.UseFont = true;
            this.txtTo.Appearance.Options.UseForeColor = true;
            this.txtTo.Appearance.Options.UseTextOptions = true;
            this.txtTo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtTo.Location = new System.Drawing.Point(152, 168);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(56, 35);
            this.txtTo.TabIndex = 101;
            this.txtTo.Tag = "UserName";
            this.txtTo.Text = "xxxx";
            // 
            // txtFrom
            // 
            this.txtFrom.Appearance.Font = new System.Drawing.Font("微软雅黑", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFrom.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFrom.Appearance.Options.UseFont = true;
            this.txtFrom.Appearance.Options.UseForeColor = true;
            this.txtFrom.Appearance.Options.UseTextOptions = true;
            this.txtFrom.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtFrom.Location = new System.Drawing.Point(152, 109);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(56, 35);
            this.txtFrom.TabIndex = 102;
            this.txtFrom.Tag = "UserName";
            this.txtFrom.Text = "xxxx";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("微软雅黑", 25F);
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.Location = new System.Drawing.Point(99, 15);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(297, 43);
            this.labelControl5.TabIndex = 103;
            this.labelControl5.Tag = "UserName";
            this.labelControl5.Text = "人员调动操作成功！";
            // 
            // FrmMoveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 416);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.textStarDate);
            this.Controls.Add(this.textEndDate);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.lblnoLicense);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMoveDialog";
            this.Text = "调动结果信息";
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl lblnoLicense;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl txtType;
        private DevExpress.XtraEditors.LabelControl textEndDate;
        private DevExpress.XtraEditors.LabelControl textStarDate;
        private DevExpress.XtraEditors.LabelControl txtTo;
        private DevExpress.XtraEditors.LabelControl txtFrom;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}