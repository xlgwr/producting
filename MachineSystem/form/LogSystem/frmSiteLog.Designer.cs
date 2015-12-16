namespace MachineSystem.TabPage
{
    partial class frmSiteLog
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtOperName = new DevExpress.XtraEditors.TextEdit();
            this.txtOperNo = new DevExpress.XtraEditors.TextEdit();
            this.dateOperDate2 = new DevExpress.XtraEditors.DateEdit();
            this.dateOperDate1 = new DevExpress.XtraEditors.DateEdit();
            this.lookType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.ListData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.myTeamName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.moduleName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.functName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OperType = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelControl11);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.labelControl10);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.labelControl6);
            this.panel1.Controls.Add(this.txtOperName);
            this.panel1.Controls.Add(this.txtOperNo);
            this.panel1.Controls.Add(this.dateOperDate2);
            this.panel1.Controls.Add(this.dateOperDate1);
            this.panel1.Controls.Add(this.lookType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 40);
            this.panel1.TabIndex = 4;
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Options.UseTextOptions = true;
            this.labelControl11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl11.Location = new System.Drawing.Point(653, 12);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(60, 14);
            this.labelControl11.TabIndex = 85;
            this.labelControl11.Text = "操作类型：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(505, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(9, 14);
            this.labelControl1.TabIndex = 84;
            this.labelControl1.Text = "~";
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Options.UseTextOptions = true;
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl10.Location = new System.Drawing.Point(316, 12);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 14);
            this.labelControl10.TabIndex = 84;
            this.labelControl10.Text = "操作日期：";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(177, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 83;
            this.labelControl3.Tag = "UserName";
            this.labelControl3.Text = "姓名:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(21, 12);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 14);
            this.labelControl6.TabIndex = 82;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "操作员号:";
            // 
            // txtOperName
            // 
            this.txtOperName.EditValue = "";
            this.txtOperName.EnterMoveNextControl = true;
            this.txtOperName.Location = new System.Drawing.Point(211, 9);
            this.txtOperName.Name = "txtOperName";
            this.txtOperName.Properties.MaxLength = 30;
            this.txtOperName.Size = new System.Drawing.Size(91, 21);
            this.txtOperName.TabIndex = 79;
            this.txtOperName.Tag = "OperName";
            // 
            // txtOperNo
            // 
            this.txtOperNo.EditValue = "";
            this.txtOperNo.EnterMoveNextControl = true;
            this.txtOperNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtOperNo.Location = new System.Drawing.Point(79, 9);
            this.txtOperNo.Name = "txtOperNo";
            this.txtOperNo.Properties.MaxLength = 20;
            this.txtOperNo.Size = new System.Drawing.Size(92, 21);
            this.txtOperNo.TabIndex = 78;
            this.txtOperNo.Tag = "OperNo";
            // 
            // dateOperDate2
            // 
            this.dateOperDate2.EditValue = "";
            this.dateOperDate2.Location = new System.Drawing.Point(535, 9);
            this.dateOperDate2.Name = "dateOperDate2";
            this.dateOperDate2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateOperDate2.Properties.Mask.EditMask = "";
            this.dateOperDate2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateOperDate2.Properties.MaxLength = 30;
            this.dateOperDate2.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateOperDate2.Size = new System.Drawing.Size(111, 21);
            this.dateOperDate2.TabIndex = 79;
            this.dateOperDate2.TabStop = false;
            this.dateOperDate2.Tag = "OperDate";
            // 
            // dateOperDate1
            // 
            this.dateOperDate1.EditValue = "";
            this.dateOperDate1.Location = new System.Drawing.Point(382, 9);
            this.dateOperDate1.Name = "dateOperDate1";
            this.dateOperDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateOperDate1.Properties.Mask.EditMask = "";
            this.dateOperDate1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateOperDate1.Properties.MaxLength = 30;
            this.dateOperDate1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateOperDate1.Size = new System.Drawing.Size(111, 21);
            this.dateOperDate1.TabIndex = 79;
            this.dateOperDate1.TabStop = false;
            this.dateOperDate1.Tag = "OperDate";
            // 
            // lookType
            // 
            this.lookType.EditValue = "";
            this.lookType.Location = new System.Drawing.Point(712, 9);
            this.lookType.Name = "lookType";
            this.lookType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookType.Properties.Items.AddRange(new object[] {
            "新增",
            "修改",
            "删除"});
            this.lookType.Properties.MaxLength = 30;
            this.lookType.Size = new System.Drawing.Size(118, 21);
            this.lookType.TabIndex = 79;
            this.lookType.TabStop = false;
            this.lookType.Tag = "OperName";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 66);
            this.gridControl1.MainView = this.ListData;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(1224, 454);
            this.gridControl1.TabIndex = 113;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ListData});
            // 
            // ListData
            // 
            this.ListData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.myTeamName,
            this.moduleName,
            this.functName,
            this.gridColumn4,
            this.OperType,
            this.gridColumn5});
            this.ListData.GridControl = this.gridControl1;
            this.ListData.Name = "ListData";
            this.ListData.OptionsPrint.AutoWidth = false;
            this.ListData.OptionsView.ColumnAutoWidth = false;
            this.ListData.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "操作员号";
            this.gridColumn2.FieldName = "OperNo";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "操作员名";
            this.gridColumn3.FieldName = "OperName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "操作日期";
            this.gridColumn4.FieldName = "OperDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "内容";
            this.gridColumn5.FieldName = "Memo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.PasswordChar = '*';
            // 
            // myTeamName
            // 
            this.myTeamName.Caption = "班别";
            this.myTeamName.Name = "myTeamName";
            this.myTeamName.Visible = true;
            this.myTeamName.VisibleIndex = 4;
            // 
            // moduleName
            // 
            this.moduleName.Caption = "模块名称";
            this.moduleName.Name = "moduleName";
            this.moduleName.Visible = true;
            this.moduleName.VisibleIndex = 5;
            // 
            // functName
            // 
            this.functName.Caption = "功能页面";
            this.functName.Name = "functName";
            this.functName.Visible = true;
            this.functName.VisibleIndex = 6;
            // 
            // OperType
            // 
            this.OperType.Caption = "操作类型";
            this.OperType.Name = "OperType";
            this.OperType.Visible = true;
            this.OperType.VisibleIndex = 7;
            // 
            // frmSiteLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonEnabled = false;
            this.ClientSize = new System.Drawing.Size(1224, 520);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmSiteLog";
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SelectAllButtonEnabled = false;
            this.SelectOffButtonEnabled = false;
            this.Text = "操作日志";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtOperName;
        private DevExpress.XtraEditors.TextEdit txtOperNo;
        private DevExpress.XtraEditors.DateEdit dateOperDate1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView ListData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateOperDate2;
        private DevExpress.XtraEditors.ComboBoxEdit lookType;
        private DevExpress.XtraGrid.Columns.GridColumn myTeamName;
        private DevExpress.XtraGrid.Columns.GridColumn moduleName;
        private DevExpress.XtraGrid.Columns.GridColumn functName;
        private DevExpress.XtraGrid.Columns.GridColumn OperType;
    }
}