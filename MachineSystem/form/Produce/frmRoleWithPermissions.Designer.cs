namespace MachineSystem.TabPage
{
    partial class frmRoleWithPermissions
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
            this.labelDeptName = new DevExpress.XtraEditors.LabelControl();
            this.txtDeptName = new DevExpress.XtraEditors.TextEdit();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.myGrid = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.funcsTreeList = new DevExpress.XtraTreeList.TreeList();
            this.pPageNameCol = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.myGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.funcsTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labelDeptName);
            this.panel1.Controls.Add(this.txtDeptName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1224, 40);
            this.panel1.TabIndex = 119;
            // 
            // labelDeptName
            // 
            this.labelDeptName.Appearance.Options.UseTextOptions = true;
            this.labelDeptName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelDeptName.Location = new System.Drawing.Point(11, 12);
            this.labelDeptName.Name = "labelDeptName";
            this.labelDeptName.Size = new System.Drawing.Size(52, 14);
            this.labelDeptName.TabIndex = 83;
            this.labelDeptName.Tag = "UserName";
            this.labelDeptName.Text = "角色名称:";
            // 
            // txtDeptName
            // 
            this.txtDeptName.EditValue = "";
            this.txtDeptName.EnterMoveNextControl = true;
            this.txtDeptName.Location = new System.Drawing.Point(70, 12);
            this.txtDeptName.Name = "txtDeptName";
            this.txtDeptName.Properties.MaxLength = 30;
            this.txtDeptName.Size = new System.Drawing.Size(216, 21);
            this.txtDeptName.TabIndex = 79;
            this.txtDeptName.Tag = "DeptName";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 66);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.myGrid);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.funcsTreeList);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1224, 454);
            this.splitContainerControl1.SplitterPosition = 849;
            this.splitContainerControl1.TabIndex = 120;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // myGrid
            // 
            this.myGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myGrid.Location = new System.Drawing.Point(0, 0);
            this.myGrid.MainView = this.gridView1;
            this.myGrid.Name = "myGrid";
            this.myGrid.Size = new System.Drawing.Size(849, 454);
            this.myGrid.TabIndex = 119;
            this.myGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.myGrid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsPrint.AutoWidth = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "RoleID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "RoleName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // funcsTreeList
            // 
            this.funcsTreeList.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.funcsTreeList.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Gray;
            this.funcsTreeList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Gray;
            this.funcsTreeList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.funcsTreeList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.funcsTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.pPageNameCol,
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4});
            this.funcsTreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.funcsTreeList.KeyFieldName = "id";
            this.funcsTreeList.Location = new System.Drawing.Point(0, 0);
            this.funcsTreeList.Name = "funcsTreeList";
            this.funcsTreeList.OptionsView.ShowCheckBoxes = true;
            this.funcsTreeList.ParentFieldName = "fatherid";
            this.funcsTreeList.PreviewFieldName = "pName";
            this.funcsTreeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.funcsTreeList.RootValue = "AA";
            this.funcsTreeList.Size = new System.Drawing.Size(369, 454);
            this.funcsTreeList.TabIndex = 5;
            this.funcsTreeList.BeforeCheckNode += new DevExpress.XtraTreeList.CheckNodeEventHandler(this.funcsTreeList_BeforeCheckNode);
            this.funcsTreeList.AfterCheckNode += new DevExpress.XtraTreeList.NodeEventHandler(this.funcsTreeList_AfterCheckNode);
            this.funcsTreeList.Leave += new System.EventHandler(this.funcsTreeList_Leave);
            // 
            // pPageNameCol
            // 
            this.pPageNameCol.Caption = "权限";
            this.pPageNameCol.FieldName = "pPageName";
            this.pPageNameCol.Name = "pPageNameCol";
            this.pPageNameCol.Visible = true;
            this.pPageNameCol.VisibleIndex = 0;
            this.pPageNameCol.Width = 186;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "父";
            this.treeListColumn1.FieldName = "fatherid";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "id";
            this.treeListColumn2.FieldName = "id";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Width = 79;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "窗体";
            this.treeListColumn3.FieldName = "pFromName";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.Width = 79;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "是否功能节点";
            this.treeListColumn4.FieldName = "isleaf";
            this.treeListColumn4.Name = "treeListColumn4";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // frmRoleWithPermissions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonEnabled = false;
            this.ClientSize = new System.Drawing.Size(1224, 520);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panel1);
            this.CopyAddEnabled = false;
            this.DeleteButtonCaption = "删除角色(F3)";
            this.DeleteButtonEnabled = false;
            this.EditButtonCaption = "修改角色(F2)";
            this.EditButtonEnabled = false;
            this.ExcelButtonEnabled = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmRoleWithPermissions";
            this.NewButtonCaption = "新增角色(F1)";
            this.PrintButtonEnabled = false;
            this.SaveButtonCaption = "保存角色权限(F5)";
            this.SaveButtonEnabled = false;
            this.SearchButtonCaption = "检索角色(F4)";
            this.SelectAllButtonEnabled = false;
            this.SelectOffButtonEnabled = false;
            this.Text = "角色权限";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.splitContainerControl1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDeptName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.myGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.funcsTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelDeptName;
        private DevExpress.XtraEditors.TextEdit txtDeptName;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl myGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraTreeList.TreeList funcsTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn pPageNameCol;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
    }
}