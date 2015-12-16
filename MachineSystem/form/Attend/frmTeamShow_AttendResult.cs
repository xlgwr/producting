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
using Framework.DataAccess;
using Framework.Libs;
using System.Data.SqlClient;
using log4net;

namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_AttendResult : Framework.Abstract.frmSearchBasic2  //Form
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_Vacation));
        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmTeamShow_AttendResult()
        {
            InitializeComponent();
          
            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {   
                //顺序号
                base.SetFormValue();
                //初始化日期
                dateOperDate1.DateTime = DateTime.Now;
                this.EditButtonCaption = "处理"; 
                
                string strSql = string.Empty;
               
                //向别
                if (Common._Administrator == Common._personid)
                {
                   strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User WHERE JobForID<>0 and JobForName<>'' order by JobForName");
                }
                else
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User_Line where  UserID ='{0}' AND  JobForID<>0 order by JobForName", Common._personid);
                }

                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                DataRow dr = dt_temp.NewRow();
                dr[0] = "-1";
                dr[1] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookUpEditJobFor.Properties.DataSource = dt_temp.DefaultView;
                lookUpEditJobFor.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpEditJobFor.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
                if (dt_temp.Rows.Count > 0)
                {
                    lookUpEditJobFor.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookUpEditJobFor.ItemIndex = 0;
                lookUpEditJobFor.Properties.BestFit();

                ////工程别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Project, this.lookUpEditProjectName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //Line别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Line, this.lookUpEditLineName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Team, this.lookUpEditTeamName, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //关位
                SysParam.m_daoCommon.SetLoopUpEditBySql(string.Format(@"select Distinct T.GuanweiName from (
                                        (select p.*,g.pName as GuanweiName from Produce_Guanwei p 
                                            inner join P_Produce_Guanwei g on p.GuanweiID=g.ID) )T
                                            where 1=1 Order by GuanweiName"), this.lookGuanweiName, true, EnumDefine.DefaultPleaseSelect);
                
                //排班,类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_TeamKind, lookTeamID, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //关位类型,直接写定
                DataTable dt_Guanwei = new DataTable();
                dt_Guanwei.Columns.Add("GuanweiID");
                dt_Guanwei.Columns.Add("GuanweiValue");
                DataRow drGuanwei = dt_Guanwei.NewRow();
                drGuanwei["GuanweiID"] = "-1";
                drGuanwei["GuanweiValue"] = "-请选择-";
                dt_Guanwei.Rows.Add(drGuanwei);

                drGuanwei = dt_Guanwei.NewRow();
                drGuanwei["GuanweiID"] = "1";
                drGuanwei["GuanweiValue"] = "直接人员";
                dt_Guanwei.Rows.Add(drGuanwei);

                drGuanwei = dt_Guanwei.NewRow();
                drGuanwei["GuanweiID"] = "2";
                drGuanwei["GuanweiValue"] = "间接人员1";
                dt_Guanwei.Rows.Add(drGuanwei);

                drGuanwei = dt_Guanwei.NewRow();
                drGuanwei["GuanweiID"] = "3";
                drGuanwei["GuanweiValue"] = "间接人员2";
                dt_Guanwei.Rows.Add(drGuanwei);

                LookUpEditGuanweiType.Properties.DataSource = dt_Guanwei;
                LookUpEditGuanweiType.Properties.DropDownRows = dt_Guanwei.Rows.Count;
                LookUpEditGuanweiType.Properties.ValueMember = dt_Guanwei.Columns[0].ColumnName;
                LookUpEditGuanweiType.Properties.DisplayMember = dt_Guanwei.Columns[1].ColumnName;

                if (dt_Guanwei.Rows.Count > 0)
                {
                    LookUpEditGuanweiType.EditValue = dt_Guanwei.Rows[0][1].ToString();
                }
                LookUpEditGuanweiType.ItemIndex = 0;
                LookUpEditGuanweiType.Properties.BestFit();
                //考勤结果,用户状态
                SysParam.m_daoCommon.SetLoopUpEditBySql(string.Format(@"select Distinct AttendResult from V_Attend_Result_Info  "), this.lookUpEditAttendResult, true, EnumDefine.DefaultPleaseSelect);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 画面按钮功能处理方法

        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            GetDspDataList();
        }


         /// <summary>
        /// 修改做处理
        /// </summary>
        public override void SetModifyInit()
        {
            try
            {
                SqlParameter[] paraList = new SqlParameter[7];
                paraList[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
                if (dateOperDate1.EditValue.ToString() != "")
                {
                    DateTime dt =DateTime.Parse ( dateOperDate1.EditValue.ToString());
                    paraList[0].Value = dt.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    paraList[0].Value = "";
                }
                paraList[1] = new SqlParameter("@JobForID", SqlDbType.VarChar, 10);
                if (lookUpEditJobFor.EditValue !=null && lookUpEditJobFor.EditValue.ToString() != "-1")
                {
                    paraList[1].Value = lookUpEditJobFor.EditValue.ToString();
                }
                else
                {
                     paraList[1].Value="0";
                }
                paraList[2] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
                if (lookUpEditProjectName.EditValue !=null && lookUpEditProjectName.EditValue.ToString() != "-1")
                {
                     paraList[2].Value = lookUpEditProjectName.EditValue.ToString();
                }
                else
                {
                    paraList[2].Value = "0";
                }
                paraList[3] = new SqlParameter("@LineID", SqlDbType.VarChar, 10);
                if (lookUpEditLineName.EditValue!=null && lookUpEditLineName.EditValue.ToString() != "-1")
                {
                     paraList[3].Value = lookUpEditLineName.EditValue.ToString();
                }
                else
                {
                    paraList[3].Value = "0";
                }
               
                paraList[4] = new SqlParameter("@TeamID", SqlDbType.VarChar, 10);
                if (lookUpEditTeamName.EditValue!=null && lookUpEditTeamName.EditValue.ToString() != "-1")
                {
                    paraList[4].Value = lookUpEditTeamName.EditValue.ToString();
                }
                else
                {
                   paraList[4].Value = "0";
                }
               
                paraList[5] = new SqlParameter("@OperID", SqlDbType.VarChar, 10);
                if (txtUserID.Text.ToString() != "")
                {
                    paraList[5].Value = txtUserID.Text.ToString();
                }
                else
                {
                    paraList[5].Value = "0";
                }
                //执行存储过程1
                Common.AdoConnect.Connect.SetExecuteSP("PROC_Attend_Total_Result", Common.Choose.OnlyExecSp, paraList);

                SqlParameter[] paraListCardData = new SqlParameter[1];
                paraListCardData[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
                if (dateOperDate1.EditValue.ToString() != "")
                {
                    DateTime dt = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    paraListCardData[0].Value = dt.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    paraListCardData[0].Value = "";
                }
                //执行存储过程2,打卡时间
                Common.AdoConnect.Connect.SetExecuteSP("PROC_CardData_Attend_Result", Common.Choose.OnlyExecSp, paraListCardData);

                XtraMsgBox.Show("执行存储过程成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("执行存储过程失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                //处理按钮可用
                this.EditButtonEnabled = true;

                string str_sql = "Select CAST('0' AS Bit) AS SlctValue, * From V_Attend_Result_Info where 1=1 "; 

                //考勤日期
                 if ((dateOperDate1.EditValue != null && dateOperDate1.EditValue.ToString() != ""))
                {
                    DateTime dtBegin = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    str_sql += " and  AttendDate = '" + dtBegin.ToString("yyyy-MM-dd") + "'";
                }
                if (txtUserID.Text.Trim() != "")
                {
                    str_sql += " and UserID like '%" + txtUserID.Text.Trim() + "%' ";
                }
                if (txtUserName.Text.Trim() != "")
                {
                    str_sql += " and UserNM like '%" + txtUserName.Text.Trim() + "%' ";
                }
                //向别
                if (lookUpEditJobFor.EditValue != null && lookUpEditJobFor.EditValue.ToString() != "-1")
                {
                    str_sql += " and JobForID= " + lookUpEditJobFor.EditValue.ToString().Trim() + " ";
                }
                //工程别
                if (lookUpEditProjectName.EditValue != null && lookUpEditProjectName.EditValue.ToString() != "-1")
                {
                    str_sql += " and ProjectID= " + lookUpEditProjectName.EditValue.ToString().Trim() + " ";
                }
                //Line别
                if (lookUpEditLineName.EditValue != null && lookUpEditLineName.EditValue.ToString() != "-1")
                {
                    str_sql += " and LineID= " + lookUpEditLineName.EditValue.ToString().Trim() + " ";
                }
                //班别Team
                if (lookUpEditTeamName.EditValue.ToString() != null && lookUpEditTeamName.EditValue.ToString() != "-1")
                {
                    str_sql += " and  TeamID = " + lookUpEditTeamName.EditValue.ToString().Trim() + " ";
                }
                //排班班种：白班、晚班
                if (lookTeamID.EditValue.ToString() != null && lookTeamID.EditValue.ToString() != "-1")
                {
                    str_sql += " and   TeamSetNM= '" + lookTeamID.Text.ToString().Trim() + "' ";
                }
                //关位
                if (lookGuanweiName.Text != ""&& lookGuanweiName.Text.Trim() != "-请选择-")
                {
                    str_sql += " and  GuanweiNM = '" + lookGuanweiName.Text.ToString().Trim() + "' ";
                }
                //考勤（结果）
                if (lookUpEditAttendResult.Text != "" && lookUpEditAttendResult.Text.Trim() != "-请选择-")
                {
                    str_sql += " and   AttendResult= '" + lookUpEditAttendResult.Text.ToString().Trim() + "' ";
                }

                //关位类型
                if (LookUpEditGuanweiType.Text != "" && LookUpEditGuanweiType.Text.Trim() != "-请选择-")
                {
                    str_sql += " and   GuanweiTypeNM= '" + LookUpEditGuanweiType.Text.ToString().Trim() + "' ";
                }

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }

                //排序
                str_sql += " Order by  JobForID,ProjectID,LineID,TeamID ,GuanweiNM ";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    SearchButtonEnabled = true;
                    ExcelButtonEnabled = true;
                    PrintButtonEnabled = true;
                }
                else
                {
                    ExcelButtonEnabled = false;
                    PrintButtonEnabled = false;
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

       
        #endregion
    }
}
