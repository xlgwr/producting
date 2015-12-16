namespace MachineSystem.TabPage
{
    partial class frmUserInfoSearch
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
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbxStatus = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lookPart = new DevExpress.XtraEditors.LookUpEdit();
            this.lookDuty = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtUserID = new DevExpress.XtraEditors.TextEdit();
            this.cbxSex = new DevExpress.XtraEditors.ComboBoxEdit();
            this.GridMain = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SlctValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).BeginInit();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDuty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSex.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.labelControl5);
            this.grpInfo.Controls.Add(this.cbxStatus);
            this.grpInfo.Controls.Add(this.labelControl2);
            this.grpInfo.Controls.Add(this.labelControl4);
            this.grpInfo.Controls.Add(this.lookPart);
            this.grpInfo.Controls.Add(this.lookDuty);
            this.grpInfo.Controls.Add(this.labelControl1);
            this.grpInfo.Controls.Add(this.labelControl3);
            this.grpInfo.Controls.Add(this.txtUserName);
            this.grpInfo.Controls.Add(this.labelControl6);
            this.grpInfo.Controls.Add(this.txtUserID);
            this.grpInfo.Controls.Add(this.cbxSex);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Location = new System.Drawing.Point(0, 26);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(1039, 93);
            this.grpInfo.TabIndex = 5;
            this.grpInfo.Text = "信息维护";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.Location = new System.Drawing.Point(420, 64);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(28, 14);
            this.labelControl5.TabIndex = 222;
            this.labelControl5.Tag = "UserName";
            this.labelControl5.Text = "状态:";
            // 
            // cbxStatus
            // 
            this.cbxStatus.EditValue = "在职";
            this.cbxStatus.Location = new System.Drawing.Point(460, 60);
            this.cbxStatus.Name = "cbxStatus";
            this.cbxStatus.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxStatus.Properties.Items.AddRange(new object[] {
            "在职",
            "离职"});
            this.cbxStatus.Properties.MaxLength = 30;
            this.cbxStatus.Properties.PopupSizeable = true;
            this.cbxStatus.Properties.ReadOnly = true;
            this.cbxStatus.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxStatus.Size = new System.Drawing.Size(138, 21);
            this.cbxStatus.TabIndex = 221;
            this.cbxStatus.TabStop = false;
            this.cbxStatus.Tag = "User_Status";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl2.Location = new System.Drawing.Point(219, 63);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(28, 14);
            this.labelControl2.TabIndex = 219;
            this.labelControl2.Tag = "UserName";
            this.labelControl2.Text = "职等:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Location = new System.Drawing.Point(17, 63);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 14);
            this.labelControl4.TabIndex = 220;
            this.labelControl4.Tag = "UserName";
            this.labelControl4.Text = "部门:";
            // 
            // lookPart
            // 
            this.lookPart.EditValue = "";
            this.lookPart.Location = new System.Drawing.Point(53, 60);
            this.lookPart.Name = "lookPart";
            this.lookPart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPart.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pName", "部门")});
            this.lookPart.Properties.MaxLength = 30;
            this.lookPart.Properties.NullText = "";
            this.lookPart.Size = new System.Drawing.Size(138, 21);
            this.lookPart.TabIndex = 217;
            this.lookPart.TabStop = false;
            this.lookPart.Tag = "PartID";
            // 
            // lookDuty
            // 
            this.lookDuty.EditValue = "";
            this.lookDuty.Location = new System.Drawing.Point(253, 60);
            this.lookDuty.Name = "lookDuty";
            this.lookDuty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookDuty.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("pName", "职等")});
            this.lookDuty.Properties.MaxLength = 30;
            this.lookDuty.Properties.NullText = "";
            this.lookDuty.Size = new System.Drawing.Size(138, 21);
            this.lookDuty.TabIndex = 218;
            this.lookDuty.TabStop = false;
            this.lookDuty.Tag = "DutyName";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(418, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 216;
            this.labelControl1.Tag = "UserNo";
            this.labelControl1.Text = "性别:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.Location = new System.Drawing.Point(208, 36);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 14);
            this.labelControl3.TabIndex = 215;
            this.labelControl3.Tag = "UserNo";
            this.labelControl3.Text = "姓名:";
            // 
            // txtUserName
            // 
            this.txtUserName.EditValue = "";
            this.txtUserName.EnterMoveNextControl = true;
            this.txtUserName.Location = new System.Drawing.Point(253, 33);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.MaxLength = 20;
            this.txtUserName.Size = new System.Drawing.Size(138, 21);
            this.txtUserName.TabIndex = 212;
            this.txtUserName.Tag = "UserName";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.Location = new System.Drawing.Point(-21, 36);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(66, 14);
            this.labelControl6.TabIndex = 214;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "工号:";
            // 
            // txtUserID
            // 
            this.txtUserID.EditValue = "";
            this.txtUserID.EnterMoveNextControl = true;
            this.txtUserID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUserID.Location = new System.Drawing.Point(53, 33);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Properties.MaxLength = 20;
            this.txtUserID.Size = new System.Drawing.Size(138, 21);
            this.txtUserID.TabIndex = 211;
            this.txtUserID.Tag = "UserID";
            // 
            // cbxSex
            // 
            this.cbxSex.EditValue = "全部";
            this.cbxSex.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cbxSex.Location = new System.Drawing.Point(460, 33);
            this.cbxSex.Name = "cbxSex";
            this.cbxSex.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxSex.Properties.Items.AddRange(new object[] {
            "全部",
            "男",
            "女"});
            this.cbxSex.Properties.MaxLength = 20;
            this.cbxSex.Size = new System.Drawing.Size(138, 21);
            this.cbxSex.TabIndex = 213;
            this.cbxSex.TabStop = false;
            this.cbxSex.Tag = "Sex";
            // 
            // GridMain
            // 
            this.GridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridMain.Location = new System.Drawing.Point(0, 119);
            this.GridMain.MainView = this.gridView1;
            this.GridMain.Name = "GridMain";
            this.GridMain.Size = new System.Drawing.Size(1039, 478);
            this.GridMain.TabIndex = 124;
            this.GridMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.SlctValue,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn12,
            this.gridColumn11});
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
            this.gridColumn1.Caption = "工号";
            this.gridColumn1.FieldName = "UserID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "姓名";
            this.gridColumn2.FieldName = "UserName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "性别";
            this.gridColumn3.FieldName = "Sex";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "部门";
            this.gridColumn4.FieldName = "UserDept";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "职等";
            this.gridColumn5.FieldName = "DutyName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "状态";
            this.gridColumn6.FieldName = "User_Status";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "向别";
            this.gridColumn7.FieldName = "JobForName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "工程别";
            this.gridColumn8.FieldName = "ProjectName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Line别";
            this.gridColumn9.FieldName = "LineName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 9;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "班别";
            this.gridColumn10.FieldName = "TeamName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 10;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "关位";
            this.gridColumn11.FieldName = "GuanweiName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 11;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "关位位置";
            this.gridColumn12.FieldName = "GuanweiSite";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 12;
            // 
            // frmUserInfoSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 597);
            this.Controls.Add(this.GridMain);
            this.Controls.Add(this.grpInfo);
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmUserInfoSearch";
            this.SaveButtonVisibility = true;
            this.Text = "用户搜索";
            this.Controls.SetChildIndex(this.grpInfo, 0);
            this.Controls.SetChildIndex(this.GridMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpInfo)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookDuty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSex.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpInfo;
        private DevExpress.XtraGrid.GridControl GridMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn SlctValue;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit cbxStatus;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookPart;
        private DevExpress.XtraEditors.LookUpEdit lookDuty;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtUserID;
        private DevExpress.XtraEditors.ComboBoxEdit cbxSex;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    }
}