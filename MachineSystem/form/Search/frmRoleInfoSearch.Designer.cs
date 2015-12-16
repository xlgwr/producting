namespace MachineSystem.TabPage
{
    partial class frmRoleInfoSearch
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
            this.grpInfo = new DevExpress.XtraEditors.GroupControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRoleName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtRoleD = new DevExpress.XtraEditors.TextEdit();
            this.GridMain = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SlctValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).BeginInit();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.labelControl3);
            this.grpInfo.Controls.Add(this.txtRoleName);
            this.grpInfo.Controls.Add(this.labelControl6);
            this.grpInfo.Controls.Add(this.txtRoleD);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Location = new System.Drawing.Point(0, 26);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(822, 61);
            this.grpInfo.TabIndex = 6;
            this.grpInfo.Text = "信息维护";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(281, 33);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(76, 16);
            this.labelControl3.TabIndex = 215;
            this.labelControl3.Tag = "UserNo";
            this.labelControl3.Text = "角色名称:";
            // 
            // txtRoleName
            // 
            this.txtRoleName.EditValue = "";
            this.txtRoleName.EnterMoveNextControl = true;
            this.txtRoleName.Location = new System.Drawing.Point(364, 30);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Properties.MaxLength = 20;
            this.txtRoleName.Size = new System.Drawing.Size(161, 21);
            this.txtRoleName.TabIndex = 212;
            this.txtRoleName.Tag = "RoleName";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(15, 33);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(77, 16);
            this.labelControl6.TabIndex = 214;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "角色编号:";
            // 
            // txtRoleD
            // 
            this.txtRoleD.EditValue = "";
            this.txtRoleD.EnterMoveNextControl = true;
            this.txtRoleD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtRoleD.Location = new System.Drawing.Point(101, 30);
            this.txtRoleD.Name = "txtRoleD";
            this.txtRoleD.Properties.MaxLength = 20;
            this.txtRoleD.Size = new System.Drawing.Size(161, 21);
            this.txtRoleD.TabIndex = 211;
            this.txtRoleD.Tag = "RoleID";
            // 
            // GridMain
            // 
            this.GridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridMain.Location = new System.Drawing.Point(0, 87);
            this.GridMain.MainView = this.gridView1;
            this.GridMain.Name = "GridMain";
            this.GridMain.Size = new System.Drawing.Size(822, 598);
            this.GridMain.TabIndex = 125;
            this.GridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SlctValue,
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.GridMain;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // SlctValue
            // 
            this.SlctValue.Caption = "选择";
            this.SlctValue.FieldName = "SlctValue";
            this.SlctValue.Name = "SlctValue";
            this.SlctValue.Visible = true;
            this.SlctValue.VisibleIndex = 0;
            this.SlctValue.Width = 64;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "角色编号";
            this.gridColumn1.FieldName = "RoleID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "角色名称";
            this.gridColumn2.FieldName = "RoleName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // frmRoleInfoSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 685);
            this.Controls.Add(this.GridMain);
            this.Controls.Add(this.grpInfo);
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.MaximizeBox = false;
            this.Name = "frmRoleInfoSearch";
            this.SaveButtonVisibility = true;
            this.Text = "角色搜索";
            this.Controls.SetChildIndex(this.grpInfo, 0);
            this.Controls.SetChildIndex(this.GridMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).EndInit();
            this.grpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoleD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpInfo;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtRoleName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtRoleD;
        private DevExpress.XtraGrid.GridControl GridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn SlctValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}