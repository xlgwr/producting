namespace MachineSystem.TabPage
{
    partial class frmEditP_LeaveReason
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtpName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(16, 60);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 14);
            this.labelControl1.TabIndex = 99;
            this.labelControl1.Tag = "ID";
            this.labelControl1.Text = "ID:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(221, 60);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 100;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "名称:";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.EnterMoveNextControl = true;
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID.Location = new System.Drawing.Point(42, 56);
            this.txtID.Name = "txtID";
            this.txtID.Properties.MaxLength = 20;
            this.txtID.Size = new System.Drawing.Size(138, 21);
            this.txtID.TabIndex = 2;
            this.txtID.Tag = "ID";
            // 
            // txtpName
            // 
            this.txtpName.Location = new System.Drawing.Point(266, 57);
            this.txtpName.Name = "txtpName";
            this.txtpName.Size = new System.Drawing.Size(252, 21);
            this.txtpName.TabIndex = 1;
            // 
            // frmEditP_OTType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 104);
            this.Controls.Add(this.txtpName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtID);
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = false;
            this.ExitButtonVisibility = false;
            this.ImportButtonEnabled = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmEditP_OTType";
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.RefreshButtonEnabled = false;
            this.RefreshButtonVisibility = false;
            this.Text = "frmEditP_LeaveReason";
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.txtpName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.TextEdit txtpName;
    }
}