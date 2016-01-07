using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.SysDefine;
using Framework.Libs;
using MachineSystem.form.UserSystem;

namespace MachineSystem.TabPage
{
    public partial class frmWorkOverTimeSummaryReport : Framework.Abstract.frmSearchBasic2	
    {

        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;
        private DateTime dtBegin;
        private DateTime dtEnd;

        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmWorkOverTimeSummaryReport()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.bandedGridView1;
            m_ParenSlctColName = "SlctValue";
            ////////////////
            EditButtonVisibility = false;
            DeleteButtonVisibility = false;
            SelectAllButtonVisibility = false;
            SelectOffButtonVisibility = false;

            DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
            dateOperDate1.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
            dateOperDate2.EditValue = string.Format("{0:yyyy-MM-dd}", LastDay);
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                 isLoad = false;

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面按钮功能处理方法
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="frmBaseToolXC"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            if ((dateOperDate1.EditValue == null || string.IsNullOrEmpty(dateOperDate1.EditValue.ToString())))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate1, "请选择开始时间!");
                return;
            }
            if ((dateOperDate2.EditValue == null || string.IsNullOrEmpty(dateOperDate2.EditValue.ToString())))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "请选择结束时间!");
                return;
            }

            dtBegin = DateTime.Parse(dateOperDate1.EditValue.ToString());
            dtEnd = DateTime.Parse(dateOperDate2.EditValue.ToString());
            if (dtEnd < dtBegin)
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "结束时间须大于开始时间!");
                return;
            }
            base.SetSearchProc(frmBaseToolXC);
            //GetDspDataList();
        }

        #endregion

        #region 事件处理方法

      
      
        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {

            string _sql = "select * from V_Attend_OT_Set_Sum  where 1=1 ";
            try
            {
                if ((dateOperDate1.EditValue != null && !string.IsNullOrEmpty(dateOperDate1.EditValue.ToString()))
                    ||(dateOperDate2.EditValue != null && !string.IsNullOrEmpty(dateOperDate2.EditValue.ToString())))
                {
                    _sql += " and (OTStrDate between '" + dateOperDate1.EditValue.ToString() + "' and  '" + dateOperDate2.EditValue.ToString() + "' ) ";
                   //_sql += " or (OTEndDate between '" + dateOperDate1.EditValue.ToString() + "' and  '" + dateOperDate2.EditValue.ToString() + "' ) ";
                     
                }

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    _sql += " and myTeamName='" + Common._myTeamName + "'";
                }
               
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
                gridControl1.DataSource = m_tblDataList;
                if (m_tblDataList.Rows.Count > 0)
                {
                    ExcelButtonEnabled = true;
                }
                else
                {
                    ExcelButtonEnabled = false;
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

       
        #endregion


    }
}
