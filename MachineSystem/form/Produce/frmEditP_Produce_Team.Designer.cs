namespace MachineSystem.TabPage
{
    partial class frmEditP_Produce_Team
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
            this.txtMark = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMark
            // 
            this.txtMark.Location = new System.Drawing.Point(192, 134);
            this.txtMark.Name = "txtMark";
            this.txtMark.Size = new System.Drawing.Size(180, 21);
            this.txtMark.TabIndex = 9;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(117, 136);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "备注:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(192, 89);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(180, 21);
            this.txtName.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(117, 91);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "名称:";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(192, 49);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(180, 21);
            this.txtID.TabIndex = 11;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(129, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "ID:";
            // 
            // frmEditP_Produce_Team
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 189);
            this.Controls.Add(this.txtMark);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.labelControl1);
            this.DeleteButtonVisibility = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmEditP_Produce_Team";
            this.NewButtonVisibility = false;
            this.PrintButtonVisibility = false;
            this.RefreshButtonVisibility = false;
            this.Text = "frmProdTeamManagers";
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.txtMark, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtMark;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}