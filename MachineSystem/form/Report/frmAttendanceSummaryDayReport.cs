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
    public partial class frmAttendanceSummaryDayReport : Framework.Abstract.frmSearchBasic2	
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
        public frmAttendanceSummaryDayReport()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;
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

            try
            {
//                string str_sql = string.Format(@"SELECT 
//	                                                A.JobForName,
////	                                                B.PartName,
//	                                                A.AttendDate,
//	                                                Sum(A.InCount) as zaiji, --在籍人数
//	                                                Sum(A.chuqin) as Attend,
//	                                                Sum(A.qingjia) as qingjia  
//	                                                FROM V_Produce_TeamShow_Group_i  A
////	                                                LEFT JOIN V_Attend_Total_Result B
////	                                                ON A.JobForName=B.JobForNM
//	                                            where 1=1 ");

//                if (dateOperDate1.EditValue != null && !string.IsNullOrEmpty(dateOperDate1.EditValue.ToString()) &&
//                     dateOperDate2.EditValue != null && !string.IsNullOrEmpty(dateOperDate2.EditValue.ToString()))
//                {
//                    str_sql += " and  A.AttendDate between '" + dateOperDate1.DateTime.ToString("yyyy-MM-dd") + "'" +
//                        " and  '" + dateOperDate2.DateTime.ToString("yyyy-MM-dd") + "'";
//                }
//                str_sql += " GROUP BY A.JobForName,A.AttendDate ";
//                //B.PartName,
//                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
//                gridControl1.DataSource = m_tblDataList;
//                if (m_tblDataList.Rows.Count > 0)
//                {
//                    ExcelButtonEnabled = true;
                string str_sql = string.Format(@"SELECT 
	                                                A.JobForName,
	                                                A.AttendDate,
	                                                Sum(A.InCount) as zaiji, --在籍人数
	                                                Sum(A.chuqin) as Attend,
	                                                Sum(A.qingjia) as qingjia  
	                                                FROM V_Produce_TeamShow_Group_i  A
	                                            where 1=1 ");

                if (dateOperDate1.EditValue != null && !string.IsNullOrEmpty(dateOperDate1.EditValue.ToString()) &&
                     dateOperDate2.EditValue != null && !string.IsNullOrEmpty(dateOperDate2.EditValue.ToString()))
                {
                    str_sql += " and  A.AttendDate between '" + dateOperDate1.DateTime.ToString("yyyy-MM-dd") + "'" +
                        " and  '" + dateOperDate2.DateTime.ToString("yyyy-MM-dd") + "'";
                }

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }

                str_sql += " GROUP BY A.JobForName,A.AttendDate ";
                //B.PartName,
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                gridControl1.DataSource = m_tblDataList;
                if (m_tblDataList.Rows.Count > 0)
                {
                    ExcelButtonEnabled = true;

                }
                else
                {
                    ExcelButtonEnabled = false;
                }
                //gridView1.BestFitColumns();
                gridView1.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        
        #endregion
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                this.Opacity = 1;

                //if (this.IsXpOr2003 == true)
                //{
                //    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED
                //    this.Opacity = 1;
                //}

                return cp;

            }

        }  //防止闪烁

    }
}
