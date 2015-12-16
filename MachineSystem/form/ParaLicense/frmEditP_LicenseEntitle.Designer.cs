namespace MachineSystem.form.ParaLicense
{
    partial class frmEditP_LicenseEntitle
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtpMark = new DevExpress.XtraEditors.TextEdit();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtpName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(209, 91);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 87;
            this.labelControl3.Tag = "UserName";
            this.labelControl3.Text = "简称:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(24, 91);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 86;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "名称:";
            // 
            // txtpMark
            // 
            this.txtpMark.EditValue = "";
            this.txtpMark.EnterMoveNextControl = true;
            this.txtpMark.Location = new System.Drawing.Point(243, 88);
            this.txtpMark.Name = "txtpMark";
            this.txtpMark.Properties.MaxLength = 30;
            this.txtpMark.Size = new System.Drawing.Size(118, 21);
            this.txtpMark.TabIndex = 86;
            this.txtpMark.Tag = "pMark";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.EnterMoveNextControl = true;
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID.Location = new System.Drawing.Point(58, 49);
            this.txtID.Name = "txtID";
            this.txtID.Properties.MaxLength = 20;
            this.txtID.Size = new System.Drawing.Size(118, 21);
            this.txtID.TabIndex = 87;
            this.txtID.Tag = "ID";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(36, 52);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 14);
            this.labelControl1.TabIndex = 86;
            this.labelControl1.Tag = "ID";
            this.labelControl1.Text = "ID:";
            // 
            // txtpName
            // 
            this.txtpName.EditValue = "";
            this.txtpName.EnterMoveNextControl = true;
            this.txtpName.Location = new System.Drawing.Point(58, 88);
            this.txtpName.Name = "txtpName";
            this.txtpName.Properties.MaxLength = 30;
            this.txtpName.Size = new System.Drawing.Size(118, 21);
            this.txtpName.TabIndex = 85;
            this.txtpName.Tag = "pName";
            // 
            // frmEditP_LicenseEntitle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 161);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtpName);
            this.Controls.Add(this.txtpMark);
            this.Controls.Add(this.txtID);
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonVisibility = false;
            this.ImportButtonEnabled = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.MaximizeBox = false;
            this.Name = "frmEditP_LicenseEntitle";
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.RefreshButtonVisibility = false;
            this.Text = "新增资格设置";
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.txtpMark, 0);
            this.Controls.SetChildIndex(this.txtpName, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtpMark;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtpName;
    }
}