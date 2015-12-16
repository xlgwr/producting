namespace MachineSystem.TabPage
{
    partial class frmEditP_Produce_Guanwei
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
            this.comboBoxGuanweiType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtpName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGuanweiType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(50, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(16, 14);
            this.labelControl1.TabIndex = 103;
            this.labelControl1.Tag = "ID";
            this.labelControl1.Text = "ID:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(36, 99);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 104;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "名称:";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.EnterMoveNextControl = true;
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID.Location = new System.Drawing.Point(76, 56);
            this.txtID.Name = "txtID";
            this.txtID.Properties.MaxLength = 20;
            this.txtID.Properties.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(253, 21);
            this.txtID.TabIndex = 3;
            this.txtID.Tag = "ID";
            // 
            // comboBoxGuanweiType
            // 
            this.comboBoxGuanweiType.EditValue = "-请选择-";
            this.comboBoxGuanweiType.Location = new System.Drawing.Point(76, 141);
            this.comboBoxGuanweiType.Name = "comboBoxGuanweiType";
            this.comboBoxGuanweiType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxGuanweiType.Properties.Items.AddRange(new object[] {
            "直接人员",
            "间接人员1",
            "间接人员2"});
            this.comboBoxGuanweiType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxGuanweiType.Size = new System.Drawing.Size(253, 21);
            this.comboBoxGuanweiType.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.Location = new System.Drawing.Point(36, 145);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 122;
            this.labelControl2.Tag = "UserNo";
            this.labelControl2.Text = "类型:";
            // 
            // txtpName
            // 
            this.txtpName.Location = new System.Drawing.Point(76, 94);
            this.txtpName.Name = "txtpName";
            this.txtpName.Size = new System.Drawing.Size(253, 21);
            this.txtpName.TabIndex = 123;
            // 
            // frmEditP_Produce_Guanwei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 188);
            this.Controls.Add(this.txtpName);
            this.Controls.Add(this.comboBoxGuanweiType);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtID);
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmEditP_Produce_Guanwei";
            this.NewButtonVisibility = false;
            this.PrintButtonVisibility = false;
            this.RefreshButtonVisibility = false;
            this.Text = "关位名称设置";
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.comboBoxGuanweiType, 0);
            this.Controls.SetChildIndex(this.txtpName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxGuanweiType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxGuanweiType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtpName;
    }
}