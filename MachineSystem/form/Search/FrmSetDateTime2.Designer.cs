namespace MachineSystem.form.Search
{
    partial class FrmSetDateTime2
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
            this.btnEnter = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanel = new DevExpress.XtraEditors.SimpleButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Appearance.Font = new System.Drawing.Font("微软雅黑", 38F);
            this.btnEnter.Appearance.Options.UseFont = true;
            this.btnEnter.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnEnter.Location = new System.Drawing.Point(13, 281);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(232, 81);
            this.btnEnter.TabIndex = 94;
            this.btnEnter.Text = "确定";
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnCanel
            // 
            this.btnCanel.Appearance.Font = new System.Drawing.Font("微软雅黑", 38F);
            this.btnCanel.Appearance.Options.UseFont = true;
            this.btnCanel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.btnCanel.Location = new System.Drawing.Point(320, 281);
            this.btnCanel.Name = "btnCanel";
            this.btnCanel.Size = new System.Drawing.Size(220, 81);
            this.btnCanel.TabIndex = 94;
            this.btnCanel.Text = "取消";
            this.btnCanel.Click += new System.EventHandler(this.btnCanel_Click);
            // 
            // txtHour
            // 
            this.txtHour.EditValue = "";
            this.txtHour.Location = new System.Drawing.Point(78, 113);
            this.txtHour.Name = "txtHour";
            this.txtHour.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.txtHour.Properties.Appearance.Options.UseFont = true;
            this.txtHour.Properties.Mask.EditMask = "##";
            this.txtHour.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
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
            this.btnMinusHour.Location = new System.Drawing.Point(78, 178);
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
            this.btnAddHour.Location = new System.Drawing.Point(78, 23);
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
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(413, 116);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(27, 35);
            this.labelControl3.TabIndex = 95;
            this.labelControl3.Tag = "UserName";
            this.labelControl3.Text = "时";
            // 
            // txtSecond
            // 
            this.txtSecond.EditValue = "";
            this.txtSecond.Location = new System.Drawing.Point(281, 111);
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
            this.btnMinusSecond.Location = new System.Drawing.Point(281, 179);
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
            this.btnAddSecond.Location = new System.Drawing.Point(281, 23);
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
            this.labelControl4.Appearance.Font = new System.Drawing.Font("微软雅黑", 36F);
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Location = new System.Drawing.Point(235, 94);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(21, 62);
            this.labelControl4.TabIndex = 95;
            this.labelControl4.Tag = "UserName";
            this.labelControl4.Text = "-";
            // 
            // FrmSetDateTime2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 391);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.btnAddSecond);
            this.Controls.Add(this.btnAddHour);
            this.Controls.Add(this.btnCanel);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.btnMinusSecond);
            this.Controls.Add(this.btnMinusHour);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtHour);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetDateTime2";
            this.Text = "时间设置";
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnCanel;
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