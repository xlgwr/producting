namespace MachineSystem.TabPage
{
    partial class frmUserDept
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
            this.labelDeptID = new DevExpress.XtraEditors.LabelControl();
            this.labelDeptName = new DevExpress.XtraEditors.LabelControl();
            this.labelDeptNo = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.txtDeptName = new DevExpress.XtraEditors.TextEdit();
            this.txtID2 = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.deptTreeList = new DevExpress.XtraTreeList.TreeList();
            this.deptNameCol = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.gridControlChildren = new DevExpress.XtraGrid.GridControl();
            this.ListData = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.splitterItem1 = new DevExpress.XtraLayout.SplitterItem();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deptTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChildren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelDeptID);
            this.panel1.Controls.Add(this.labelDeptName);
            this.panel1.Controls.Add(this.labelDeptNo);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.txtDeptName);
            this.panel1.Controls.Add(this.txtID2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1219, 40);
            this.panel1.TabIndex = 5;
            // 
            // labelDeptID
            // 
            this.labelDeptID.Appearance.Options.UseTextOptions = true;
            this.labelDeptID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelDeptID.Location = new System.Drawing.Point(438, 12);
            this.labelDeptID.Name = "labelDeptID";
            this.labelDeptID.Size = new System.Drawing.Size(28, 14);
            this.labelDeptID.TabIndex = 83;
            this.labelDeptID.Tag = "DeptID";
            this.labelDeptID.Text = "记号:";
            // 
            // labelDeptName
            // 
            this.labelDeptName.Appearance.Options.UseTextOptions = true;
            this.labelDeptName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelDeptName.Location = new System.Drawing.Point(237, 12);
            this.labelDeptName.Name = "labelDeptName";
            this.labelDeptName.Size = new System.Drawing.Size(28, 14);
            this.labelDeptName.TabIndex = 83;
            this.labelDeptName.Tag = "UserName";
            this.labelDeptName.Text = "名称:";
            // 
            // labelDeptNo
            // 
            this.labelDeptNo.Appearance.Options.UseTextOptions = true;
            this.labelDeptNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelDeptNo.Location = new System.Drawing.Point(21, 12);
            this.labelDeptNo.Name = "labelDeptNo";
            this.labelDeptNo.Size = new System.Drawing.Size(28, 14);
            this.labelDeptNo.TabIndex = 82;
            this.labelDeptNo.Tag = "DeptNo";
            this.labelDeptNo.Text = "编号:";
            // 
            // txtID
            // 
            this.txtID.EditValue = "";
            this.txtID.EnterMoveNextControl = true;
            this.txtID.Location = new System.Drawing.Point(481, 12);
            this.txtID.Name = "txtID";
            this.txtID.Properties.MaxLength = 30;
            this.txtID.Size = new System.Drawing.Size(118, 21);
            this.txtID.TabIndex = 2;
            this.txtID.Tag = "ID";
            // 
            // txtDeptName
            // 
            this.txtDeptName.EditValue = "";
            this.txtDeptName.EnterMoveNextControl = true;
            this.txtDeptName.Location = new System.Drawing.Point(280, 12);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.Properties.MaxLength = 30;
            this.txtDeptName.Size = new System.Drawing.Size(118, 21);
            this.txtDeptName.TabIndex = 1;
            this.txtDeptName.Tag = "DeptName";
            // 
            // txtID2
            // 
            this.txtID2.EditValue = "";
            this.txtID2.EnterMoveNextControl = true;
            this.txtID2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtID2.Location = new System.Drawing.Point(84, 12);
            this.txtID2.Name = "txtID2";
            this.txtID2.Properties.MaxLength = 20;
            this.txtID2.Size = new System.Drawing.Size(118, 21);
            this.txtID2.TabIndex = 0;
            this.txtID2.Tag = "ID2";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 66);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1219, 426);
            this.panelControl1.TabIndex = 6;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.deptTreeList);
            this.layoutControl1.Controls.Add(this.gridControlChildren);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 2);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1215, 422);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // deptTreeList
            // 
            this.deptTreeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deptTreeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.deptTreeList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Gray;
            this.deptTreeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Gray;
            this.deptTreeList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.deptTreeList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.deptTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.deptNameCol,
            this.treeListColumn1});
            this.deptTreeList.KeyFieldName = "id";
            this.deptTreeList.Location = new System.Drawing.Point(12, 12);
            this.deptTreeList.Name = "deptTreeList";
            this.deptTreeList.ParentFieldName = "fatherid";
            this.deptTreeList.PreviewFieldName = "pName";
            this.deptTreeList.RootValue = "AA";
            this.deptTreeList.Size = new System.Drawing.Size(207, 398);
            this.deptTreeList.TabIndex = 4;
            this.deptTreeList.NodeCellStyle += new DevExpress.XtraTreeList.GetCustomNodeCellStyleEventHandler(this.deptTreeList_NodeCellStyle);
            this.deptTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.deptTreeList_FocusedNodeChanged);
            // 
            // deptNameCol
            // 
            this.deptNameCol.Caption = "部门";
            this.deptNameCol.FieldName = "pName";
            this.deptNameCol.Name = "deptNameCol";
            this.deptNameCol.Visible = true;
            this.deptNameCol.VisibleIndex = 0;
            this.deptNameCol.Width = 186;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "父记号";
            this.treeListColumn1.FieldName = "fatherid";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // gridControlChildren
            // 
            this.gridControlChildren.Location = new System.Drawing.Point(229, 12);
            this.gridControlChildren.MainView = this.ListData;
            this.gridControlChildren.Name = "gridControlChildren";
            this.gridControlChildren.Size = new System.Drawing.Size(974, 398);
            this.gridControlChildren.TabIndex = 114;
            this.gridControlChildren.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ListData});
            // 
            // ListData
            // 
            this.ListData.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.ListData.GridControl = this.gridControlChildren;
            this.ListData.Name = "ListData";
            this.ListData.OptionsPrint.AutoWidth = false;
            this.ListData.OptionsView.ColumnAutoWidth = false;
            this.ListData.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "记号";
            this.gridColumn1.FieldName = "id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "父记号";
            this.gridColumn2.FieldName = "fatherid";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "名称";
            this.gridColumn3.FieldName = "pName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "编号";
            this.gridColumn4.FieldName = "id2";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "内容";
            this.gridColumn5.FieldName = "Memo";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.splitterItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1215, 422);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.deptTreeList;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(211, 402);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gridControlChildren;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(217, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(978, 402);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // splitterItem1
            // 
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.CustomizationFormText = "splitterItem1";
            this.splitterItem1.Location = new System.Drawing.Point(211, 0);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new System.Drawing.Size(6, 402);
            // 
            // frmUserDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonEnabled = false;
            this.Caption = "";
            this.ClientSize = new System.Drawing.Size(1219, 492);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panel1);
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmUserDept";
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;
            this.Text = "部门资料";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panelControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deptTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlChildren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ListData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitterItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelDeptName;
        private DevExpress.XtraEditors.LabelControl labelDeptNo;
        private DevExpress.XtraEditors.TextEdit txtDeptName;
        private DevExpress.XtraEditors.TextEdit txtID2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraTreeList.TreeList deptTreeList;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gridControlChildren;
        private DevExpress.XtraGrid.Views.Grid.GridView ListData;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.SplitterItem splitterItem1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn deptNameCol;
        private DevExpress.XtraEditors.LabelControl labelDeptID;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
    }
}