using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Framework.Libs;
using Framework.DataAccess;
using System.Collections.Specialized;
using log4net;

namespace Framework.Abstract
{
    public partial class frmBaseXC : DevExpress.XtraEditors.XtraForm
    {

        #region 变量定义
        
        /// <summary>
        /// 数据项控件
        /// </summary>
        protected DevExpress.XtraEditors.GroupControl m_GrpDataItem;

        /// <summary>
        /// 画面编辑模式
        /// </summary>
        public Common.DataModifyMode ScanMode = Common.DataModifyMode.dsp;
        /// <summary>
        /// 记录日志数据
        /// </summary>
        private  static readonly ILog log = LogManager.GetLogger(typeof(frmBaseXC));

        #region 画面数据变量定义

        /// <summary>
        /// 画面ID
        /// </summary>
        protected XtraForm m_strFormID;
        /// <summary>
        /// 画面ID
        /// </summary>
        [Bindable(true), Category("业务控件绑定"), Description("画面唯ID")]
        public XtraForm FormID
        {
            get { return m_strFormID; }
            set
            {
                m_strFormID = value;
            }
        }

        /// <summary>
        /// 多语言编号
        /// </summary>
        protected string m_strLangKey = "";
        /// <summary>
        /// 多语言编号
        /// </summary>
        [Bindable(true), Category("业务控件绑定"), Description("多语言编号")]
        public string LangKey
        {
            get { return m_strLangKey; }
            set
            {
                m_strLangKey = value;
            }
        }

        /// <summary>
        /// 画面描述
        /// </summary>
        [Bindable(true), Category("业务数据绑定"), Description("窗体描述信息")]
        public string Caption
        {
            get;
            set;
        }
        /// <summary>
        /// 画面操作表名称
        /// </summary>
        protected string TableName = "";

        /// <summary>
        /// 数据排序字符串
        /// </summary>
        protected string m_OrderBy = "";
        
        /// <summary>
        /// 画面表格绑定数据
        /// </summary>
        protected DataTable TableGrid ;

        /// <summary>
        /// 画面查询表名称
        /// </summary>
        protected string ViewTableName="";

        /// <summary>
        /// 重名检测列名
        /// </summary>
        protected String m_RepFiledName = "";

        /// <summary>
        /// 精确查询条件列名
        /// </summary>
        protected StringDictionary m_dicConds = new StringDictionary();

        /// <summary>
        /// 时间段查询列名
        /// </summary>
        protected StringDictionary m_dicBetweenConds = new StringDictionary();

        /// <summary>
        /// 模糊查询条件列名
        /// </summary>
        protected StringDictionary m_dicLikeConds = new StringDictionary();

        /// <summary>
        /// 数据唯一主键列名
        /// </summary>
        protected StringDictionary m_dicPrimarName = new StringDictionary();

        /// <summary>
        /// 操作员信息列名
        /// </summary>
        protected StringDictionary m_dicUserColum = new StringDictionary();

        /// <summary>
        /// 画面可输入数据对象
        /// </summary>
        protected StringDictionary m_dicItemData = new StringDictionary();

        #endregion

        #endregion

        public frmBaseXC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public virtual void SetFormValue()
        {
        }

        #region 共通处理方法


        /// <summary>
        /// 获取表格一览信息
        /// </summary>
        protected virtual void GetDspDataList()
        {
            
        }


        /// <summary>
        /// 获取需要编辑数据信息
        /// </summary>
        /// <param name="dr"></param>
        protected virtual void GetGrpDataItem()
        {

        }

        /// <summary>
        /// 获取选择行数据显示
        /// </summary>
        /// <param name="dr"></param>
        protected virtual void SetGridRowData(DataRow dr)
        {

        }

        /// <summary>
        /// 设定验证条件处理
        /// </summary>
        protected virtual void SetValidCondition()
        {

        }

        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        /// <param name="isSucces">检查成功标识</param>
        protected virtual void GetInputCheck(ref  bool isSucces)
        {

            isSucces = true;
        }

        /// <summary>
        /// 清空画面数据
        /// </summary>
        protected virtual void ClearMainText()
        {

        }

        /// <summary>
        /// 数据存在检查(主键重复)
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetExistDataItem()
        {
            return false;
        }

        /// <summary>
        /// 数据存在检查(主键重复)
        /// </summary>
        /// <returns></returns>
        protected virtual bool GetRepNameCheck()
        {
           return false;
        }

        /// <summary>
        /// 初始化数据表格样式
        /// </summary>
        /// <param name="ReadOnly"></param>
        protected virtual void SetIniGridStyle(Common.enumGridStyle style)
        {
 
        }

        /// <summary>
        /// 表格列只读设置
        /// </summary>
        /// <param name="ReadOnly"></param>
        protected virtual void SetGridColumnReadOnly(bool ReadOnly)
        {

        }

        #endregion

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {

        //        CreateParams cp = base.CreateParams;

        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
        //        this.Opacity = 1;

        //        //if (this.IsXpOr2003 == true)
        //        //{
        //        //    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED
        //        //    this.Opacity = 1;
        //        //}

        //        return cp;

        //    }

        //}  //防止闪烁

        //private Boolean IsXpOr2003
        //{
        //    get
        //    {
        //        OperatingSystem os = Environment.OSVersion;
        //        Version vs = os.Version;

        //        if (os.Platform == PlatformID.Win32NT)
        //            if ((vs.Major == 5) && (vs.Minor != 0))
        //                return true;
        //            else
        //                return false;
        //        else
        //            return false;
        //    }
        //}


    }
}