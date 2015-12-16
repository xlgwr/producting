namespace MachineSystem.TabPage
{
    partial class frmEditP_OTApplay
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtIndexNum = new DevExpress.XtraEditors.TextEdit();
            this.cboOTKind = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.memoRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtpName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndexNum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOTKind.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(38, 171);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 93;
            this.labelControl3.Tag = "Remark";
            this.labelControl3.Text = "备注:";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(38, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 14);
            this.labelControl1.TabIndex = 91;
            this.labelControl1.Tag = "排序";
            this.labelControl1.Text = "排序:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(38, 96);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 92;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "名称:";
            // 
            // txtIndexNum
            // 
            this.txtIndexNum.EditValue = "";
            this.txtIndexNum.EnterMoveNextControl = true;
            this.txtIndexNum.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtIndexNum.Location = new System.Drawing.Point(73, 57);
            this.txtIndexNum.Name = "txtIndexNum";
            this.txtIndexNum.Properties.MaxLength = 20;
            this.txtIndexNum.Size = new System.Drawing.Size(337, 21);
            this.txtIndexNum.TabIndex = 1;
            this.txtIndexNum.Tag = "ID";
            // 
            // cboOTKind
            // 
            this.cboOTKind.EditValue = "计划内";
            this.cboOTKind.Location = new System.Drawing.Point(73, 131);
            this.cboOTKind.Name = "cboOTKind";
            this.cboOTKind.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOTKind.Properties.Items.AddRange(new object[] {
            "计划内",
            "计划外"});
            this.cboOTKind.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboOTKind.Size = new System.Drawing.Size(337, 21);
            this.cboOTKind.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.Location = new System.Drawing.Point(38, 134);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 95;
            this.labelControl2.Tag = "UserNo";
            this.labelControl2.Text = "类型:";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.EnterMoveNextControl = true;
            this.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID.Location = new System.Drawing.Point(73, 30);
            this.txtID.Name = "txtID";
            this.txtID.Properties.MaxLength = 20;
            this.txtID.Size = new System.Drawing.Size(31, 21);
            this.txtID.TabIndex = 8;
            this.txtID.Tag = "ID";
            this.txtID.Visible = false;
            // 
            // memoRemark
            // 
            this.memoRemark.Location = new System.Drawing.Point(73, 171);
            this.memoRemark.Name = "memoRemark";
            this.memoRemark.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.memoRemark.Size = new System.Drawing.Size(337, 64);
            this.memoRemark.TabIndex = 4;
            // 
            // txtpName
            // 
            this.txtpName.Location = new System.Drawing.Point(73, 96);
            this.txtpName.Name = "txtpName";
            this.txtpName.Size = new System.Drawing.Size(337, 21);
            this.txtpName.TabIndex = 2;
            // 
            // frmEditP_OTApplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 248);
            this.Controls.Add(this.txtpName);
            this.Controls.Add(this.memoRemark);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.cboOTKind);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtIndexNum);
            this.DeleteButtonVisibility = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmEditP_OTApplay";
            this.NewButtonVisibility = false;
            this.PrintButtonVisibility = false;
            this.RefreshButtonVisibility = false;
            this.Text = "新增加班事由";
            this.Controls.SetChildIndex(this.txtIndexNum, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.cboOTKind, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.txtID, 0);
            this.Controls.SetChildIndex(this.memoRemark, 0);
            this.Controls.SetChildIndex(this.txtpName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndexNum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOTKind.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtIndexNum;
        private DevExpress.XtraEditors.ComboBoxEdit cboOTKind;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraEditors.MemoEdit memoRemark;
        private DevExpress.XtraEditors.TextEdit txtpName;
    }
}