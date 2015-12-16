using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Libs;
using Framework.DataAccess;
using System.Collections.Specialized;
using DevExpress.XtraGrid.Columns;
using log4net;
using System.Collections;

namespace Framework.Abstract
{
    public partial class frmAssistBasic : frmBaseXC
    {

        #region 变量定义

        /// <summary>
        /// 记录日志数据
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(frmBaseToolXC));

        #endregion

        #region 变量定义

        //表格控件对象
        protected GridViewUtil m_GridViewUtil = new GridViewUtil();
        //表格选择按钮对象
        DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repSJ = 
                        new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();

        //多选对象//
        DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repICE = 
                        new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();

        /// <summary>
        /// 是否为多选标志//
        /// </summary>
        public bool blSearch = false;

        /// <summary>
        /// 表格选择列名(父表)
        /// </summary>
        protected String m_ParenSlctColName = "";

        /// <summary>
        /// 表格选择列名(子表)
        /// </summary>
        protected String m_ChildSlctColName = "";

        /// <summary>
        /// 默认数据表格打印标识
        /// </summary>
        protected bool isAutoPrint = true;

        /// <summary>
        /// 选择行数据
        /// </summary>
        private DataRow m_drSelectRow;

        /// <summary>
        /// 选择多行数据
        /// </summary>
        private List<DataRow> m_drSelectRows=new List<DataRow>();
        #endregion

        #region 画面属性设置

        /// <summary>
        /// 选择行数据
        /// </summary>
        public DataRow SelectRowData
        {
            get { return m_drSelectRow; }
            set { this.m_drSelectRow = value; }
        }

        /// <summary>
        /// 选择多行数据
        /// </summary>
        public List<DataRow> SelectRowDatas
        {
            get { return m_drSelectRows; }
            set { this.m_drSelectRows = value; }
        }

        #region 按钮标签设定

        //各按钮标签设定

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定退出按钮标签")]
        public object ExitButtonTag
        {
            get { return this.cmdExit.Tag; }
            set { this.cmdExit.Tag = value; }
        }
        //[Bindable(true), Category("工具栏按钮标签设定"), Description("设定导出按钮标签")]
        //public object ExportButtonTag
        //{
        //    get { return this.cmdExport.Tag; }
        //    set { this.cmdExport.Tag = value ; }
        //}

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定查找按钮标签")]
        public object SearchButtonTag
        {
            get { return this.cmdSearch.Tag; }
            set { this.cmdSearch.Tag = value; }
        }

       
        #endregion

        #region 按钮描述设定

        //各按钮描述设定

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定退出按钮描述")]
        public string ExitButtonCaption
        {
            get { return this.cmdExit.Caption; }
            set { this.cmdExit.Caption = value; }
        }

        //[Bindable(true), Category("工具栏按钮描述设定"), Description("设定导出按钮描述")]
        //public string ExportButtonCaption
        //{
        //    get { return this.cmdExport.Caption; }
        //    set { this.cmdExport.Caption = value ; }
        //}

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定查找按钮描述")]
        public string SearchButtonCaption
        {
            get { return this.cmdSearch.Caption; }
            set { this.cmdSearch.Caption = value; }
        }

        #endregion

        #region 按钮可视及有效性设定

        //各按钮的可视性

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定退出按钮的可视化")]
        public bool ExitButtonVisibility
        {
            get { return this.cmdExit.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdExit.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        //[Bindable(true), Category("工具栏按钮可视化设定"), Description("设定导出按钮的可视化")]
        //public bool ExportButtonVisibility
        //{
        //    get { return this.cmdExport.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
        //    set { this.cmdExport.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        //}

         [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定查找按钮的可视化")]
        public bool SearchButtonVisibility
        {
            get { return this.cmdSearch.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdSearch.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }


         [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定确定按钮的可视化")]
         public bool SaveButtonVisibility
         {
             get { return this.cmdOk.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
             set { this.cmdOk.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
         }


         [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定全选按钮的可视化")]
         public bool SearchallButtonVisibility
         {
             get { return this.cmdSearchAll.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
             set { this.cmdSearchAll.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
         }


         [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定全非选按钮的可视化")]
         public bool SearchoffButtonVisibility
         {
             get { return this.cmdSearchNoAll.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
             set { this.cmdSearchNoAll.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }

         }
        #endregion

        #region 各按钮的有效性

        //各按钮的有效性
        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定退出按钮的有效性")]
        public bool ExitButtonEnabled
        {
            get { return this.cmdExit.Enabled; }
            set { this.cmdExit.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定查找按钮的有效性")]
        public bool SearchButtonEnabled
        {
            get { return this.cmdSearch.Enabled; }
            set { this.cmdSearch.Enabled = value; }         
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定确定按钮的有效性")]
        public bool SaveButtonEnabled
        {
            get { return this.cmdOk.Enabled; }
            set { this.cmdOk.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定全选按钮的有效性")]
        public bool SearchallButtonEnabled
        {
            get { return this.cmdSearchAll.Enabled; }
            set { this.cmdSearchAll.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定全非选按钮的有效性")]
        public bool SearchoffButtonEnabled
        {
            get { return this.cmdSearchNoAll.Enabled; }
            set { this.cmdSearchNoAll.Enabled = value; }
        }

        #endregion

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmAssistBasic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面初始化显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSearchBasic_Load(object sender, EventArgs e)
        {
            try
            {
                //设定验证条件处理
                SetValidCondition();

                SetFormValue();

            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {

            subRestoreFormLayOut();

            //初始化数据表格样式
            SetIniGridStyle(Common.enumGridStyle.ViewStyle);


            //设定工具栏的有效性
            SetCmdControl(this);

            //设定窗体控件的可读性
            Common.SetFormReadOnly(this, false);

            //表格列只读设置
            SetGridColumnReadOnly(true);

            //设定验证条件处理
            SetValidCondition();

            //表格事件绑定处理
            if (this.TableName != null && m_GridViewUtil.ParentGridView != null)
            {
                this.m_GridViewUtil.ParentGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(ParentGridView_CustomDrawRowIndicator);
                this.m_GridViewUtil.ParentGridView.DoubleClick += new System.EventHandler(this.ParentGridView_DoubleClick);
                if (!string.IsNullOrEmpty(m_ParenSlctColName) && blSearch==false)
                {
                    repSJ.Click += new EventHandler(SetSaveDataProc);
                    repSJ.AutoHeight = false;
                    repSJ.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    repSJ.Buttons[0].Caption = "选择";
                    repSJ.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;

                    this.m_GridViewUtil.ParentGridView.Columns[this.m_ParenSlctColName].ColumnEdit = repSJ;
                }
                else if (!string.IsNullOrEmpty(m_ParenSlctColName) && blSearch == true)
                {
                   // repICE.Click += new EventHandler(SetSaveDataProc);
                    repICE.AutoHeight = false;
                    //repICE.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    //repICE.Buttons[0].Caption = "选择";
                    //repICE.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;

                    this.m_GridViewUtil.ParentGridView.Columns[this.m_ParenSlctColName].ColumnEdit = repICE;
                }

            }

            if (Common.m_daoCommon == null)
            {
                Common.m_daoCommon = new daoCommon();
            }

        }

        #endregion

        #region 画面按钮功点击事件

        /// <summary>
        /// 画面按钮功点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            try
            {

                if (e.Item.Tag.ToString() == null) return;

                //清空验证异常信息
                Common.ClearGroupErr(this.validData, this.m_GrpDataItem);

                if (m_GridViewUtil.ParentGridView != null) m_GridViewUtil.ParentGridView.CloseEditor();
                if (m_GridViewUtil.ChildGridView != null) m_GridViewUtil.ChildGridView.CloseEditor();

                switch (e.Item.Tag.ToString())
                {
                    case "search":
                        SetSearchProc(this);
                        break;
                    case "exit":
                        this.Close();
                        break;
                    case "save":
                        SetSaveDataProc(this);
                        break;
                    case "searchall":
                        GetSelectAllProc();
                        break;
                    case "searchoff":
                        GetSelectOffProc();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                //XtraMsgBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex);
            }
        }

        #endregion

        #region 保存画面控件式样处理

        /// <summary>
        /// 设定窗体关闭事件处理
        /// </summary>
        private void frmBaseToolXC_FormClosing(object sender, FormClosingEventArgs e)
        {
            subSaveFormLayOut();
        }


        protected void subSaveFormLayOut()
        {
            if (this.m_GridViewUtil.ParentGridView == null) return;
            Common.SaveLayout(this.m_GridViewUtil.ParentGridView);

            if (this.m_GridViewUtil.ChildGridView == null) return;
            Common.SaveLayout(this.m_GridViewUtil.ChildGridView);
        }

        protected void subRestoreFormLayOut()
        {
            if (this.m_GridViewUtil.ParentGridView == null) return;
            Common.ReStoreLayOut(this.m_GridViewUtil.ParentGridView);

            if (this.m_GridViewUtil.ChildGridView == null) return;
            Common.ReStoreLayOut(this.m_GridViewUtil.ChildGridView);
        }

        #endregion

        #region 数据表格事件

        /// <summary>
        /// 行编号显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ParentGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "组" + e.RowHandle.ToString();
                }
            }
        }

        /// <summary>
        /// 双击选中行//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ParentGridView_DoubleClick(object sender, EventArgs e)
        {
            //SetSaveDataProc(null, null);
        }

        
        #endregion

        #region 设定工具栏的有效性

        /// <summary>
        /// 设定工具栏的有效性
        /// </summary>
        /// <param name="mode">编辑的状态</param>
        public static void SetCmdControl(frmAssistBasic frmbase)
        {

            int RecordCount = 0;

            if (frmbase.m_GridViewUtil != null
                 && frmbase.m_GridViewUtil.ParentGridView != null
                 && frmbase.m_GridViewUtil.ParentGridView.RowCount > 0)
            {
                RecordCount = frmbase.m_GridViewUtil.ParentGridView.RowCount;
            }
        }

        #endregion

        #region 画面按钮初始化处理方法

        #endregion

        #region 画面按钮功能处理方法

        /// <summary>
        /// 选择行处理数据功能处理
        /// </summary>
        protected virtual void SetSaveDataProc(object sender, EventArgs e)
        {
  

            if (this.TableName != null && m_GridViewUtil.ParentGridView != null)
            {
                if (this.blSearch == false)
                {
                    this.m_drSelectRow = m_GridViewUtil.ParentGridView.GetFocusedDataRow();

                }
                else
                {
                    DataRow dr;
                    for (int i = 0; i <= m_GridViewUtil.ParentGridView.RowCount - 1; i++)
                    { 
                        dr=m_GridViewUtil.ParentGridView.GetDataRow(i);

                        if ( dr[0].ToString().ToLower()=="true")
                        {
                            this.m_drSelectRows.Add(dr);
                     }
                    }
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel ;
            }

        }

        /// <summary>
        /// 处理数据功能处理
        /// </summary>
        protected virtual void SetSaveDataProc(frmAssistBasic frmbase)
        {
            SetSaveDataProc(null, null);

        }
        /// <summary>
        /// 数据查询功能处理
        /// </summary>
        protected virtual void SetSearchProc(frmAssistBasic frmBaseToolXC)
        {
            bool isSucces = false;

            try
            {

                //画面数据验证处理
                if (!this.validData.Validate())
                {
                    return;
                }
                
                //获取需要编辑数据信息
                this.GetGrpDataItem();

                //数据检查功能处理
                this.GetInputCheck(ref isSucces);

                if (!isSucces)
                {
                    return;
                }

                //获取表格一览信息
                GetDspDataList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 报表打印功能处理
        /// </summary>
        protected virtual void GetPrintReportProc()
        {
            if (this.isAutoPrint && this.m_GridViewUtil.GridControlList != null)
            {

                FileOperate.FileOperate.PrintView(this.m_GridViewUtil.GridControlList, this.m_GridViewUtil.ParentGridView, this.Text);
            }
        }

        /// <summary>
        /// 文件导出功能处理
        /// </summary>
        protected virtual void GetExcelDerivedProc()
        {
            if (this.isAutoPrint && this.m_GridViewUtil.ParentGridView != null)
            {
                FileOperate.FileOperate.ExportExcel(this.m_GridViewUtil.ParentGridView);
            }
        }


        /// <summary>
        /// 全选功能处理
        /// </summary>
        protected virtual void GetSelectAllProc()
        {
            GridViewUtil.SetGridSelect(this.m_GridViewUtil.ParentGridView,this.m_ParenSlctColName , true);
 
        }

        /// <summary>
        /// 全非选功能处理
        /// </summary>
        protected virtual void GetSelectOffProc()
        {
            GridViewUtil.SetGridSelect(this.m_GridViewUtil.ParentGridView, this.m_ParenSlctColName, false);            
        }

        /// <summary>
        /// 返回按钮处理
        /// </summary>
        protected virtual void SetExitProc()
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 共通处理方法

        /// <summary>
        /// 获取需要编辑数据信息
        /// </summary>
        /// <param name="dr"></param>
        protected override void GetGrpDataItem()
        {

            if (this.TableName != null && this.m_GrpDataItem != null)
            {
                this.m_dicItemData = new StringDictionary();
                Common.GetGroupDataSearch(this.m_GrpDataItem, ref this.m_dicItemData);
            }
        }

        /// <summary>
        /// 获取表格一览信息
        /// </summary>
        protected override void GetDspDataList()
        {

            if (Common.m_daoCommon == null)
            {
                Common.m_daoCommon = new daoCommon();
            }

            if (this.ViewTableName != null && m_GridViewUtil.ParentGridView != null)
            {

                this.TableGrid = Common.m_daoCommon.GetTableInfo(this.ViewTableName, this.m_dicItemData, this.m_dicConds, this.m_dicLikeConds, this.m_OrderBy);

                this.m_GridViewUtil.GridControlList.DataSource = this.TableGrid.DefaultView;

            }

            //设定工具栏的有效性
            SetCmdControl(this);

        }


        /// <summary>
        /// 初始化数据表格样式
        /// </summary>
        /// <param name="ReadOnly"></param>
        protected override void SetIniGridStyle(Common.enumGridStyle style)
        {
            if (m_GridViewUtil.ParentGridView != null)
                GridViewUtil.SetIniGridStyle(m_GridViewUtil.ParentGridView, style);
            if (m_GridViewUtil.ChildGridView != null)
                GridViewUtil.SetIniGridStyle(m_GridViewUtil.ChildGridView, style);

        }

        /// <summary>
        /// 表格列只读设置
        /// </summary>
        /// <param name="ReadOnly"></param>
        protected override void SetGridColumnReadOnly(bool ReadOnly)
        {
            if (m_GridViewUtil.ParentGridView != null)
                GridViewUtil.SetGridColumnReadOnly(m_GridViewUtil.ParentGridView, ReadOnly);

        

            if (m_GridViewUtil.ChildGridView != null)
                GridViewUtil.SetGridColumnReadOnly(m_GridViewUtil.ChildGridView, ReadOnly);


            if (!string.IsNullOrEmpty (this.m_ParenSlctColName))
            {
                this.m_GridViewUtil.ParentGridView.Columns[this.m_ParenSlctColName].OptionsColumn.AllowEdit = true;
                this.m_GridViewUtil.ParentGridView.Columns[this.m_ParenSlctColName].OptionsColumn.AllowFocus = true;
                this.m_GridViewUtil.ParentGridView.Columns[this.m_ParenSlctColName].OptionsColumn.ReadOnly = false;
            }

            
            if (!string.IsNullOrEmpty (this.m_ChildSlctColName))
            {
                this.m_GridViewUtil.ChildGridView.Columns[this.m_ChildSlctColName].OptionsColumn.AllowEdit = true;
                this.m_GridViewUtil.ChildGridView.Columns[this.m_ChildSlctColName].OptionsColumn.AllowFocus = true;
                this.m_GridViewUtil.ChildGridView.Columns[this.m_ChildSlctColName].OptionsColumn.ReadOnly = false;
            }
        }

        #endregion
    }
}
