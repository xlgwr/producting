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

namespace Framework.Abstract
{
    public partial class frmSearchBasic2 : frmBaseXC
    {
        #region 变量定义

        /// <summary>
        /// 记录日志数据
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(frmBaseToolXC));

        #endregion

        #region 属性设置

        #region 画面业务设置

        //表格控件对象
        protected GridViewUtil m_GridViewUtil = new GridViewUtil();

        #region 画面数据变量定义

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

        #endregion

        #region 画面属性设置

        /// <summary>
        /// 右键菜单对象
        /// </summary>
        public DevExpress.XtraBars.BarManager barmanager
        {
            get
            {
                return this.barManager;
            }
            set
            {
                if (this.barManager == value)
                    return;
                this.barManager = value;
            }
        }

        #endregion

        #endregion

        #region 按钮标签设定

        //各按钮标签设定
        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定导入按钮描述")]
        public string ImportButtonCaption
        {
            get { return this.cmdImport.Caption; }
            set { this.cmdImport.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定退出按钮标签")]
        public object ExitButtonTag
        {
            get { return this.cmdExit.Tag; }
            set { this.cmdExit.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定保存按钮标签")]
        public object SaveButtonTag
        {
            get { return this.cmdSave.Tag; }
            set { this.cmdSave.Tag = value; }
        }

        //[Bindable(true), Category("工具栏按钮标签设定"), Description("设定导出按钮标签")]
        //public object ExportButtonTag
        //{
        //    get { return this.cmdExport.Tag; }
        //    set { this.cmdExport.Tag = value ; }
        //}

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定取消按钮标签")]
        public object CancelButtonTag
        {
            get { return this.cmdCancel.Tag; }
            set { this.cmdCancel.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定导出excel按钮标签")]
        public object ExcelButtonTag
        {
            get { return this.cmdExcel.Tag; }
            set { this.cmdExcel.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定打印按钮标签")]
        public object PrintButtonTag
        {
            get { return this.cmdPrint.Tag; }
            set { this.cmdPrint.Tag = value; }
        }
        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定新增按钮标签")]
        public object NewButtonTag
        {
            get { return this.cmdNew.Tag; }
            set { this.cmdNew.Tag = value; }
        }
        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定修改按钮标签")]
        public object EditButtonTag
        {
            get { return this.cmdEdit.Tag; }
            set { this.cmdEdit.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定删除按钮标签")]
        public object DeleteButtonTag
        {
            get { return this.cmdDelete.Tag; }
            set { this.cmdDelete.Tag = value; }
        }
        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定查找按钮标签")]
        public object SearchButtonTag
        {
            get { return this.cmdSearch.Tag; }
            set { this.cmdSearch.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定查找按钮标签")]
        public object SelectAllButtonTag
        {
            get { return this.cmdSelectAll.Tag; }
            set { this.cmdSelectAll.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定查找按钮标签")]
        public object SelectOffButtonTag
        {
            get { return this.cmdSelectOff.Tag; }
            set { this.cmdSelectOff.Tag = value; }
        }

        [Bindable(true), Category("工具栏按钮标签设定"), Description("设定复制并新增按钮标签")]
        public object CopyAddTag
        {
            get { return this.cmdCopyAdd.Tag; }
            set { this.cmdCopyAdd.Tag = value; }
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

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定保存按钮描述")]
        public string SaveButtonCaption
        {
            get { return this.cmdSave.Caption; }
            set { this.cmdSave.Caption = value; }
        }

        //[Bindable(true), Category("工具栏按钮描述设定"), Description("设定导出按钮描述")]
        //public string ExportButtonCaption
        //{
        //    get { return this.cmdExport.Caption; }
        //    set { this.cmdExport.Caption = value ; }
        //}

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定取消按钮描述")]
        public string CancelButtonCaption
        {
            get { return this.cmdCancel.Caption; }
            set { this.cmdCancel.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定导出excel按钮描述")]
        public string ExcelButtonCaption
        {
            get { return this.cmdExcel.Caption; }
            set { this.cmdExcel.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定打印按钮描述")]
        public string PrintButtonCaption
        {
            get { return this.cmdPrint.Caption; }
            set { this.cmdPrint.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定查找按钮描述")]
        public string SearchButtonCaption
        {
            get { return this.cmdSearch.Caption; }
            set { this.cmdSearch.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定查找按钮描述")]
        public string SelectAllButtonCaption
        {
            get { return this.cmdSelectAll.Caption; }
            set { this.cmdSelectAll.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定查找按钮描述")]
        public string SelectOffButtonCaption
        {
            get { return this.cmdSelectOff.Caption; }
            set { this.cmdSelectOff.Caption = value; }
        }
        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定新增按钮描述")]
        public string NewButtonCaption
        {
            get { return this.cmdNew.Caption; }
            set { this.cmdNew.Caption = value; }
        }
        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定修改按钮描述")]
        public string EditButtonCaption
        {
            get { return this.cmdEdit.Caption; }
            set { this.cmdEdit.Caption = value; }
        }

        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定删除按钮描述")]
        public string DeleteButtonCaption
        {
            get { return this.cmdDelete.Caption; }
            set { this.cmdDelete.Caption = value; }
        }
        [Bindable(true), Category("工具栏按钮描述设定"), Description("设定复制并新增按钮描述")]
        public string CopyAddCaption
        {
            get { return this.cmdCopyAdd.Caption; }
            set { this.cmdCopyAdd.Caption = value; }
        }
        #endregion

        #region 按钮可视及有效性设定

        //各按钮的可视性
        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定导入按钮的可视化")]
        public bool ImportButtonVisibility
        {
            get { return this.cmdImport.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdImport.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定退出按钮的可视化")]
        public bool ExitButtonVisibility
        {
            get { return this.cmdExit.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdExit.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定保存按钮的可视化")]
        public bool SaveButtonVisibility
        {
            get { return this.cmdSave.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdSave.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        //[Bindable(true), Category("工具栏按钮可视化设定"), Description("设定导出按钮的可视化")]
        //public bool ExportButtonVisibility
        //{
        //    get { return this.cmdExport.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
        //    set { this.cmdExport.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        //}

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定取消按钮的可视化")]
        public bool CancelButtonVisibility
        {
            get { return this.cmdCancel.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdCancel.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定导出excel按钮的可视化")]
        public bool ExcelButtonVisibility
        {
            get { return this.cmdExcel.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdExcel.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定打印按钮的可视化")]
        public bool PrintButtonVisibility
        {
            get { return this.cmdPrint.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdPrint.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定查找按钮的可视化")]
        public bool SearchButtonVisibility
        {
            get { return this.cmdSearch.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdSearch.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定打印按钮的可视化")]
        public bool SelectAllButtonVisibility
        {
            get { return this.cmdSelectAll.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdSelectAll.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定查找按钮的可视化")]
        public bool SelectOffButtonVisibility
        {
            get { return this.cmdSelectOff.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdSelectOff.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }


        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定新增按钮的可视化")]
        public bool NewButtonVisibility
        {
            get { return this.cmdNew.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdNew.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定修改按钮的可视化")]
        public bool EditButtonVisibility
        {
            get { return this.cmdEdit.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdEdit.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定删除按钮的可视化")]
        public bool DeleteButtonVisibility
        {
            get { return this.cmdDelete.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdDelete.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }

        [Bindable(true), Category("工具栏按钮可视化设定"), Description("设定复制并新增按钮的可视化")]
        public bool CopyAddVisibility
        {
            get { return this.cmdCopyAdd.Visibility == DevExpress.XtraBars.BarItemVisibility.Always ? true : false; }
            set { this.cmdCopyAdd.Visibility = (value == true ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never); }
        }
        #endregion

        #region 各按钮的有效性

        //各按钮的有效性

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定导入按钮的有效性")]
        public bool ImportButtonEnabled
        {
            get { return this.cmdImport.Enabled; }
            set { this.cmdImport.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定退出按钮的有效性")]
        public bool ExitButtonEnabled
        {
            get { return this.cmdExit.Enabled; }
            set { this.cmdExit.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定保存按钮的有效性")]
        public bool SaveButtonEnabled
        {
            get { return this.cmdSave.Enabled; }
            set { this.cmdSave.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定取消按钮的有效性")]
        public bool CancelButtonEnabled
        {
            get { return this.cmdCancel.Enabled; }
            set { this.cmdCancel.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定导出excel按钮的有效性")]
        public bool ExcelButtonEnabled
        {
            get { return this.cmdExcel.Enabled; }
            set { this.cmdExcel.Enabled = value; }
        }


        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定打印按钮的有效性")]
        public bool PrintButtonEnabled
        {
            get { return this.cmdPrint.Enabled; }
            set { this.cmdPrint.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定查找按钮的有效性")]
        public bool SearchButtonEnabled
        {
            get { return this.cmdSearch.Enabled; }
            set { this.cmdSearch.Enabled = value; }         
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定打印按钮的有效性")]
        public bool SelectAllButtonEnabled
        {
            get { return this.cmdSelectAll.Enabled; }
            set { this.cmdSelectAll.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定打印按钮的有效性")]
        public bool SelectOffButtonEnabled
        {
            get { return this.cmdSelectOff.Enabled; }
            set { this.cmdSelectOff.Enabled = value; }
        }


        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定新增按钮的有效性")]
        public bool NewButtonEnabled
        {
            get { return this.cmdNew.Enabled; }
            set { this.cmdNew.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定修改按钮的有效性")]
        public bool EditButtonEnabled
        {
            get { return this.cmdEdit.Enabled; }
            set { this.cmdEdit.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定删除按钮的有效性")]
        public bool DeleteButtonEnabled
        {
            get { return this.cmdDelete.Enabled; }
            set { this.cmdDelete.Enabled = value; }
        }

        [Bindable(true), Category("工具栏按钮有效化设定"), Description("设定复制并新增按钮的有效性")]
        public bool CopyAddEnabled
        {
            get { return this.cmdCopyAdd.Enabled; }
            set { this.cmdCopyAdd.Enabled = value; }
        }

        #endregion

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmSearchBasic2()
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

            //表格事件绑定处理
            if (this.TableName != null && m_GridViewUtil.ParentGridView != null)
            {
                this.m_GridViewUtil.ParentGridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(ParentGridView_CustomDrawRowIndicator);
                this.m_GridViewUtil.ParentGridView.BeforeLeaveRow += new DevExpress.XtraGrid.Views.Base.RowAllowEventHandler(ListView_BeforeLeaveRow);
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

                this.ErrorInfo.ClearErrors();

                switch (e.Item.Tag.ToString())
                {
                    case "import":
                        SetImportInit();
                        break;
                    case "save":
                        SetSaveDataProc(this);
                        break;
                    case "search":
                        SetSearchProc(this);
                        break;
                    case "print":

                        //报表打印功能处理
                        GetPrintReportProc();
                        break;

                    case "excel":
                        GetExcelDerivedProc();
                        break;

                    case "selectall":
                        GetSelectAllProc();
                        break;

                    case "selectoff":
                        GetSelectOffProc();
                        break;

                    case "exit":
                        //subSaveFormLayOut();
                        this.Close();
                        break;

                    case "new":
                        SetInsertInit(true);
                        break;
                    case "edit":
                        SetModifyInit();
                        break;
                    case "delete":
                        SetDeleteInit();

                        break;
                    case "copyadd":
                        SetCopyAddInit();
                        break;
                    case "cancel":

                        this.ScanMode = Common.DataModifyMode.dsp;

                        SetCancelInit(true);
                        SetCancelInits();
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
        /// 行离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ListView_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (this.ScanMode != Common.DataModifyMode.dsp)
            {
                e.Allow = false;
            }
        }

        #endregion

        #region 设定工具栏的有效性

        /// <summary>
        /// 设定工具栏的有效性
        /// </summary>
        /// <param name="mode">编辑的状态</param>
        public static void SetCmdControl(frmSearchBasic2 frmbase)
        {

            int RecordCount = 0;

            if (frmbase.m_GridViewUtil != null
                 && frmbase.m_GridViewUtil.ParentGridView != null
                 && frmbase.m_GridViewUtil.ParentGridView.RowCount > 0)
            {
                RecordCount = frmbase.m_GridViewUtil.ParentGridView.RowCount;
            }

            //初始化状态
            //frmbase.SearchButtonEnabled = true;
            //frmbase.CopyAddVisibility = false;
            //frmbase.CopyAddEnabled = false;

            switch (frmbase.ScanMode)
            {

                case Common.DataModifyMode.dsp:

                    //初始化状态
                    frmbase.NewButtonEnabled = true;
                    frmbase.SaveButtonEnabled = false;
                    frmbase.CancelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = true;
                    frmbase.ImportButtonEnabled = true;
                    if (RecordCount > 0)
                    {
                        frmbase.PrintButtonEnabled = true;
                        frmbase.ExcelButtonEnabled = true;
                        frmbase.DeleteButtonEnabled = true;
                        frmbase.EditButtonEnabled = true;
                        frmbase.CopyAddEnabled = true;
                        frmbase.SelectAllButtonEnabled = true;
                        frmbase.SelectOffButtonEnabled = true;
                    }
                    else
                    {
                        frmbase.PrintButtonEnabled = false;
                        frmbase.ExcelButtonEnabled = false;
                        frmbase.DeleteButtonEnabled = false;
                        frmbase.EditButtonEnabled = false;
                        frmbase.CopyAddEnabled = false;
                        frmbase.SelectAllButtonEnabled = false;
                        frmbase.SelectOffButtonEnabled = false;
                    }
                    break;

                case Common.DataModifyMode.add:

                    //添加状态
                    frmbase.NewButtonEnabled = false;
                    frmbase.PrintButtonEnabled = false;
                    frmbase.ImportButtonEnabled = false;
                    frmbase.SaveButtonEnabled = true;
                    frmbase.ExcelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = false;
                    frmbase.EditButtonEnabled = false;
                    frmbase.DeleteButtonEnabled = false;
                    frmbase.CancelButtonEnabled = true;
                    break;

                case Common.DataModifyMode.upd:

                    //修改状态
                    frmbase.NewButtonEnabled = false;
                    frmbase.PrintButtonEnabled = false;
                    frmbase.ImportButtonEnabled = false;
                    frmbase.SaveButtonEnabled = true;
                    frmbase.ExcelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = false;
                    frmbase.EditButtonEnabled = false;
                    frmbase.DeleteButtonEnabled = false;
                    frmbase.CancelButtonEnabled = true;

                    break;

                case Common.DataModifyMode.del:

                    //删除状态
                    frmbase.NewButtonEnabled = false;
                    frmbase.PrintButtonEnabled = false;
                    frmbase.ImportButtonEnabled = false;
                    frmbase.SaveButtonEnabled = true;
                    frmbase.ExcelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = false;
                    frmbase.EditButtonEnabled = false;
                    frmbase.DeleteButtonEnabled = false;
                    frmbase.CopyAddEnabled = false;
                    frmbase.CancelButtonEnabled = true;

                    break;


            }

            if (Common._isHistory)
            {
                frmbase.cmdSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                frmbase.cmdSelectAll .Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                frmbase.cmdSelectOff.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
        }

        #endregion

        #region 画面按钮初始化处理方法

        /// <summary>
        /// 导入初始化处理
        /// </summary>
        protected virtual void SetImportInit()
        {
            this.ScanMode = Common.DataModifyMode.add;

            //设定工具栏的有效性
            SetCmdControl(this);

            //设定窗体控件的可读性
            Common.SetFormReadOnly(this, false);

        }

        /// <summary>
        /// 新增初始化处理
        /// </summary>
        /// <param name="isClear"></param>
        protected virtual void SetInsertInit(bool isClear)
        {
            //this.ScanMode = Common.DataModifyMode.add;

            //if (isClear == true) ClearMainText();

            ////设定工具栏的有效性

            //SetCmdControl(this);

            ////设定窗体控件的可读性

            //Common.SetFormReadOnly(this, false);
            

        }


        /// <summary>
        /// 取消
        /// </summary>
        protected virtual void SetCancelInits()
        {
           
        }

        /// <summary>
        /// 复制并新增
        /// </summary>
        protected virtual void SetCopyAddInit() 
        {
            
        }

        /// <summary>
        /// 修改初始化处理
        /// </summary>
        public virtual void SetModifyInit()
        {
            //this.ScanMode = Common.DataModifyMode.upd;


            ////设定工具栏的有效性

            //SetCmdControl(this);

            ////设定窗体控件的可读性

            //Common.SetFormReadOnly(this, false);

        }

        /// <summary>
        /// 删除初始化处理
        /// </summary>
        protected virtual void SetDeleteInit()
        {
            this.ScanMode = Common.DataModifyMode.del;

            //设定工具栏的有效性

            SetCmdControl(this);

            //设定窗体控件的可读性

            Common.SetFormReadOnly(this, true);
        }

        #endregion

        # region 画面按钮功能处理方法

        /// <summary>
        /// 新增数据功能处理
        /// </summary>
        protected virtual void SetInsertProc(ref int RtnValue)
        {
            RtnValue = -1;

            try
            {

                if (this.TableName != null && this.m_GrpDataItem != null)
                {
                    RtnValue = Common.m_daoCommon.SetInsertDataItem(this.TableName, this.m_dicItemData, this.m_dicUserColum);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改数据功能处理
        /// </summary>
        public virtual void SetModifyProc(ref int RtnValue)
        {

            RtnValue = 0;

            try
            {

                if (this.TableName != null && this.m_GrpDataItem != null)
                {
                    RtnValue = Common.m_daoCommon.SetModifyDataItem(this.TableName, this.m_dicItemData, this.m_dicPrimarName, this.m_dicUserColum);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除数据功能处理
        /// </summary>
        protected virtual void SetDeleteProc(ref int RtnValue)
        {

            RtnValue = 0;

            try
            {
                if (this.TableName != null && this.m_GrpDataItem != null)
                {
                    RtnValue = Common.m_daoCommon.SetDeleteDataItem(this.TableName, this.m_dicItemData, this.m_dicPrimarName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理数据功能处理
        /// </summary>
        protected virtual void SetSaveDataProc(frmSearchBasic2 frmbase)
        {
            bool isSucces = false;
            int RtnValue = -1;

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
            switch (frmbase.ScanMode)
            {

                case Common.DataModifyMode.add:
                    SetInsertProc(ref RtnValue);
                    break;

                case Common.DataModifyMode.upd:
                    SetModifyProc(ref RtnValue);
                    break;

                case Common.DataModifyMode.del:

                    if (XtraMsgBox.Show("确定要删除吗(Y/N)?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    SetDeleteProc(ref RtnValue);

                    break;

                default:
                    this.ScanMode = Common.DataModifyMode.dsp;
                    break;

            }

            //数据刷新功能处理
            if (RtnValue > 0)
            {
                this.ScanMode = Common.DataModifyMode.dsp;

                SetRefreshProc(this);
                SetCancelInit(true);
            }
            //获取表格一览信息
            this.GetDspDataList();

        }

        /// <summary>
        /// 数据刷新功能处理
        /// </summary>
        protected virtual void SetRefreshProc(frmSearchBasic2 frmBaseToolXC)
        {

            try
            {
                //获取表格一览信息
                GetDspDataList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检索初始化处理
        /// </summary>
        /// <param name="isClear"></param>
        protected virtual void SetCancelInit(bool isClear)
        {

            if (isClear == true) ClearMainText();

            //设定工具栏的有效性
            SetCmdControl(this);

            //设定窗体控件的可读性
            Common.SetFormReadOnly(this, true);

            if (m_GridViewUtil.ParentGridView == null) return;

            if (this.ScanMode != Common.DataModifyMode.upd && this.m_GridViewUtil.ParentGridView.RowCount >= 1)
            {
                this.m_GridViewUtil.ParentGridView.Focus();
                DataRow dr = this.m_GridViewUtil.ParentGridView.GetFocusedDataRow();
                if (dr == null)
                {
                    dr = this.m_GridViewUtil.ParentGridView.GetDataRow(0);
                }

                SetGridRowData(dr);

            }
        }

        /// <summary>
        /// 数据查询功能处理
        /// </summary>
        protected virtual void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {

            try
            {
                this.m_dicItemData.Clear ();

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

                if (this.m_GrpDataItem != null)
                {
                    Common.GetGroupDataSearch(this.m_GrpDataItem, ref this.m_dicItemData);
                }

                this.TableGrid = Common.m_daoCommon.GetTableInfo(this.ViewTableName, this.m_dicItemData, this.m_dicConds,this.m_dicBetweenConds, this.m_dicLikeConds, this.m_OrderBy);

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
