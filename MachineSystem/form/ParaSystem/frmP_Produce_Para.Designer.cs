namespace MachineSystem.TabPage
{
    partial class frmP_Produce_Para
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.grid1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.dateStr = new DevExpress.XtraEditors.DateEdit();
            this.lookUpEditGuanweiName = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditLineName = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditTeamName = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEditJobFor = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEditProjectName = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateStr.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditGuanweiName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLineName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTeamName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditJobFor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditProjectName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 124);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemComboBox1,
            this.repositoryItemCheckEdit1});
            this.gridControl1.Size = new System.Drawing.Size(996, 603);
            this.gridControl1.TabIndex = 122;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.grid1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn4,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn13,
            this.gridColumn12,
            this.gridColumn11,
            this.gridColumn14,
            this.gridColumn10,
            this.gridColumn9,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsPrint.AutoWidth = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "选择";
            this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn4.FieldName = "SlctValue";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn4.OptionsFilter.AllowFilter = false;
            this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 0;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "向别";
            this.gridColumn1.FieldName = "JobForName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "工程别";
            this.gridColumn2.FieldName = "ProjectName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 125;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Line别";
            this.gridColumn13.FieldName = "LineName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.OptionsColumn.ReadOnly = true;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 3;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "班别";
            this.gridColumn12.FieldName = "TeamName";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.OptionsColumn.ReadOnly = true;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 4;
            this.gridColumn12.Width = 150;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "关位";
            this.gridColumn11.FieldName = "GuanweiName";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            this.gridColumn11.OptionsColumn.ReadOnly = true;
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 5;
            this.gridColumn11.Width = 100;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "关位顺序";
            this.gridColumn14.FieldName = "RowID";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "关位类型";
            this.gridColumn10.FieldName = "GuanweiType";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "标配人数";
            this.gridColumn9.FieldName = "SetNum";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.OptionsColumn.ReadOnly = true;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 8;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "实际人数";
            this.gridColumn3.FieldName = "realityCount";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 9;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.PasswordChar = '*';
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // grid1
            // 
            this.grid1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.grid1.GridControl = this.gridControl1;
            this.grid1.Name = "grid1";
            this.grid1.OptionsPrint.AutoWidth = false;
            this.grid1.OptionsView.ColumnAutoWidth = false;
            this.grid1.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "全选";
            this.gridColumn5.ColumnEdit = this.repositoryItemComboBox1;
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "ID";
            this.gridColumn6.FieldName = "ID";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 1;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "操作员号";
            this.gridColumn7.FieldName = "pName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 2;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "名称";
            this.gridColumn8.FieldName = "pMark";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelControl10);
            this.panel1.Controls.Add(this.dateStr);
            this.panel1.Controls.Add(this.lookUpEditGuanweiName);
            this.panel1.Controls.Add(this.labelControl3);
            this.panel1.Controls.Add(this.lookUpEditLineName);
            this.panel1.Controls.Add(this.lookUpEditTeamName);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.labelControl5);
            this.panel1.Controls.Add(this.lookUpEditJobFor);
            this.panel1.Controls.Add(this.lookUpEditProjectName);
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Controls.Add(this.labelControl6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(996, 98);
            this.panel1.TabIndex = 121;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Options.UseTextOptions = true;
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl10.Location = new System.Drawing.Point(507, 50);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(60, 14);
            this.labelControl10.TabIndex = 106;
            this.labelControl10.Text = "查询日期：";
            // 
            // dateStr
            // 
            this.dateStr.EditValue = "";
            this.dateStr.Location = new System.Drawing.Point(580, 47);
            this.dateStr.Name = "dateStr";
            this.dateStr.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateStr.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateStr.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateStr.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateStr.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateStr.Properties.Mask.EditMask = "";
            this.dateStr.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateStr.Properties.MaxLength = 30;
            this.dateStr.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateStr.Size = new System.Drawing.Size(105, 21);
            this.dateStr.TabIndex = 105;
            this.dateStr.TabStop = false;
            this.dateStr.Tag = "OperDate";
            // 
            // lookUpEditGuanweiName
            // 
            this.lookUpEditGuanweiName.Location = new System.Drawing.Point(332, 47);
            this.lookUpEditGuanweiName.Name = "lookUpEditGuanweiName";
            this.lookUpEditGuanweiName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditGuanweiName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuanweiID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GuanweiName", "关位")});
            this.lookUpEditGuanweiName.Properties.DropDownRows = 8;
            this.lookUpEditGuanweiName.Properties.NullText = "";
            this.lookUpEditGuanweiName.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
            this.lookUpEditGuanweiName.Size = new System.Drawing.Size(169, 21);
            this.lookUpEditGuanweiName.TabIndex = 104;
            this.lookUpEditGuanweiName.Tag = "AuthorNo";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl3.Location = new System.Drawing.Point(287, 54);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(28, 14);
            this.labelControl3.TabIndex = 103;
            this.labelControl3.Tag = "UserNo";
            this.labelControl3.Text = "关位:";
            // 
            // lookUpEditLineName
            // 
            this.lookUpEditLineName.Location = new System.Drawing.Point(580, 12);
            this.lookUpEditLineName.Name = "lookUpEditLineName";
            this.lookUpEditLineName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditLineName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LineID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("LineName", "Line别")});
            this.lookUpEditLineName.Properties.DropDownRows = 8;
            this.lookUpEditLineName.Properties.NullText = "";
            this.lookUpEditLineName.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
            this.lookUpEditLineName.Size = new System.Drawing.Size(180, 21);
            this.lookUpEditLineName.TabIndex = 102;
            this.lookUpEditLineName.Tag = "AuthorNo";
            this.lookUpEditLineName.EditValueChanged += new System.EventHandler(this.lookUpEditLineName_EditValueChanged);
            // 
            // lookUpEditTeamName
            // 
            this.lookUpEditTeamName.Location = new System.Drawing.Point(75, 51);
            this.lookUpEditTeamName.Name = "lookUpEditTeamName";
            this.lookUpEditTeamName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditTeamName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TeamID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TeamName", "班别")});
            this.lookUpEditTeamName.Properties.DropDownRows = 8;
            this.lookUpEditTeamName.Properties.NullText = "";
            this.lookUpEditTeamName.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
            this.lookUpEditTeamName.Size = new System.Drawing.Size(176, 21);
            this.lookUpEditTeamName.TabIndex = 101;
            this.lookUpEditTeamName.Tag = "AuthorNo";
            this.lookUpEditTeamName.EditValueChanged += new System.EventHandler(this.lookUpEditTeamName_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl4.Location = new System.Drawing.Point(22, 54);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(28, 14);
            this.labelControl4.TabIndex = 100;
            this.labelControl4.Tag = "UserNo";
            this.labelControl4.Text = "班别:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl5.Location = new System.Drawing.Point(526, 12);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(38, 14);
            this.labelControl5.TabIndex = 99;
            this.labelControl5.Tag = "UserNo";
            this.labelControl5.Text = "Line别:";
            // 
            // lookUpEditJobFor
            // 
            this.lookUpEditJobFor.Location = new System.Drawing.Point(75, 12);
            this.lookUpEditJobFor.Name = "lookUpEditJobFor";
            this.lookUpEditJobFor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditJobFor.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("JobForID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("JobForName", "向别")});
            this.lookUpEditJobFor.Properties.DropDownRows = 8;
            this.lookUpEditJobFor.Properties.NullText = "";
            this.lookUpEditJobFor.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
            this.lookUpEditJobFor.Size = new System.Drawing.Size(180, 21);
            this.lookUpEditJobFor.TabIndex = 96;
            this.lookUpEditJobFor.Tag = "AuthorNo";
            this.lookUpEditJobFor.EditValueChanged += new System.EventHandler(this.lookUpEditJobFor_EditValueChanged);
            // 
            // lookUpEditProjectName
            // 
            this.lookUpEditProjectName.EditValue = "-请选择-";
            this.lookUpEditProjectName.Location = new System.Drawing.Point(332, 12);
            this.lookUpEditProjectName.Name = "lookUpEditProjectName";
            this.lookUpEditProjectName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEditProjectName.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProjectID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProjectName", "工程别")});
            this.lookUpEditProjectName.Properties.DropDownRows = 8;
            this.lookUpEditProjectName.Properties.NullText = "";
            this.lookUpEditProjectName.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
            this.lookUpEditProjectName.Size = new System.Drawing.Size(176, 21);
            this.lookUpEditProjectName.TabIndex = 95;
            this.lookUpEditProjectName.Tag = "AuthorNo";
            this.lookUpEditProjectName.EditValueChanged += new System.EventHandler(this.lookUpEditProjectName_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl1.Location = new System.Drawing.Point(275, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 14);
            this.labelControl1.TabIndex = 85;
            this.labelControl1.Tag = "UserNo";
            this.labelControl1.Text = "工程别:";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControl6.Location = new System.Drawing.Point(21, 12);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(28, 14);
            this.labelControl6.TabIndex = 82;
            this.labelControl6.Tag = "UserNo";
            this.labelControl6.Text = "向别:";
            // 
            // frmP_Produce_Para
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonEnabled = false;
            this.ClientSize = new System.Drawing.Size(996, 727);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.CopyAddEnabled = false;
            this.DeleteButtonEnabled = false;
            this.EditButtonEnabled = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmP_Produce_Para";
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SelectAllButtonEnabled = false;
            this.SelectOffButtonEnabled = false;
            this.Text = "关位参数设置";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.gridControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateStr.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateStr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditGuanweiName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditLineName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditTeamName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditJobFor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEditProjectName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraGrid.Views.Grid.GridView grid1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditProjectName;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditJobFor;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditGuanweiName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditLineName;
        private DevExpress.XtraEditors.LookUpEdit lookUpEditTeamName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.DateEdit dateStr;
    }
}