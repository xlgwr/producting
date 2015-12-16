using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using Framework.Libs;
using DevExpress.XtraEditors.DXErrorProvider;
using MachineSystem.form.ParaLicense;
using MachineSystem.SysDefine;
using System.Collections.Specialized;
using log4net;
using System.Data.SqlClient;
/********************************************************************************
** 作者： liujinbao
** 创始时间：2015-6-8

** 修改人：libing
** 修改时间：2015-7-22
** 修改内容：代码规范化

** 描述：
**    关位参数设置的查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmP_Produce_Para :Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmP_Produce_Para));
        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;
        #endregion

        #region 画面初始化

        public frmP_Produce_Para()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "V_Produce_Para_i";//操作表名称
            m_ParenSlctColName = "SlctValue";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                 //获取下拉框数据
                GetLookUpList();
                isLoad = false;
                
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
            base.SetSearchProc(frmBaseToolXC);

        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetInsertInit(bool isClear)
        {
            base.SetInsertInit(isClear);
            try
            {
                frmEditP_Produce_Para frm = new frmEditP_Produce_Para();
                frm.ScanMode = Common.DataModifyMode.add;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;

                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        /// <summary>
        /// 复制并新增
        /// </summary>
        protected override void SetCopyAddInit()
        {
            try
            {
                if (this.GetSelectList().Length <= 0)
                {
                    XtraMsgBox.Show("复制新增时，必须选择一条数据，您未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (this.GetSelectList().Length > 1)
                {
                    XtraMsgBox.Show("复制新增时，只能勾选一条数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmEditP_Produce_Para frm = new frmEditP_Produce_Para();
                frm.ScanMode = Common.DataModifyMode.copyadd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GetDspDataList();
                }
                this.ScanMode = Common.DataModifyMode.dsp;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("修改数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        public override void SetModifyInit()
        {
            base.SetModifyInit();
            try
            {
                if (this.GetSelectList().Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (this.GetSelectList().Length > 1)
                {
                    XtraMsgBox.Show("修改时，只能勾选一条数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmEditP_Produce_Para frm = new frmEditP_Produce_Para();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    this.ScanMode = Common.DataModifyMode.dsp;
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("修改数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        protected override void SetDeleteInit()
        {
            try
            {
                int result = 0;
                //选择所有选择的数据
                DataRow[] drs = this.GetSelectList();

                //没有选择任何数据情况
                if (drs.Length <= 0)
                {
                    XtraMsgBox.Show("未勾选任何数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //检查是否有关联数据
                if (CheckDeleteDate(drs))
                {
                    return;
                }

                if (XtraMsgBox.Show("是否删除数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Common.AdoConnect.Connect.CreateSqlTransaction();
                    if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            DataRow dr = drs[i];
                            m_dicItemData = new System.Collections.Specialized.StringDictionary();
                            m_dicItemData["ID"] = dr["ID"].ToString();
                            m_dicPrimarName["ID"] = dr["ID"].ToString();
                            result = SysParam.m_daoCommon.SetDeleteDataItem("Produce_Guanwei", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("关位参数设置:", "删除", dr["ID"].ToString());
                            }
                        }
                    }
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                        XtraMsgBox.Show("删除数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetDspDataList();
                    }
                }
  
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                XtraMsgBox.Show("删除数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 事件处理方法

        /// <summary>
        /// 选择向别加载该向别下的工程别
        /// </summary>
        /// 
        private void lookUpEditJobFor_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " and JobForID='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (Common._myTeamName != "")
            {
                str_where += " and myTeamName='" + Common._myTeamName + "'";
            }
            string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM  " + this.TableName+"  " + str_where + " order by ProjectName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditProjectName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditProjectName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditProjectName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditProjectName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditProjectName.ItemIndex = 0;
            lookUpEditProjectName.Properties.BestFit();
        }

        /// <summary>
        /// 选择工程别加载该向别下的Line别
        /// </summary>

        private void lookUpEditProjectName_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where += " AND  ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
            }
            if (Common._myTeamName != "") 
            {
                str_where += " and myTeamName='" + Common._myTeamName + "'";
            }

            string str_sql = string.Format(@"select DISTINCT LineID,LineName FROM  " + this.TableName + "  " + str_where + " order by LineName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditLineName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditLineName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditLineName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditLineName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditLineName.ItemIndex = 0;
            lookUpEditLineName.Properties.BestFit();
        }

        /// <summary>
        /// 选择Line别加载该LINE别下的班别
        /// </summary>
        /// 
        private void lookUpEditLineName_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where += " AND ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
            }
            if (lookUpEditLineName.EditValue.ToString() != "-1")
            {
                str_where +=" AND LineID='" + lookUpEditLineName.EditValue.ToString() + "'";

            }
            if (Common._myTeamName != "")
            {
                str_where += " and myTeamName='" + Common._myTeamName + "'";
            }
            string str_sql = string.Format(@"select DISTINCT TeamID ,TeamName FROM  " + this.TableName+"  "+ str_where + " order by TeamName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditTeamName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditTeamName.ItemIndex = 0;
            lookUpEditTeamName.Properties.BestFit();
        }

        /// <summary>
        /// 选择班别加载该班别下的关位
        /// </summary>
        private void lookUpEditTeamName_EditValueChanged(object sender, EventArgs e)
        {


            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where +=  " AND ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
            }
            if (lookUpEditLineName.EditValue.ToString() != "-1")
            {
                str_where +=  " AND LineID='" + lookUpEditLineName.EditValue.ToString() + "'";

            }
            if (lookUpEditTeamName.EditValue.ToString() != "-1")
            {
                str_where += " AND TeamID='" + lookUpEditTeamName.EditValue.ToString() + "'";
            }
            if (Common._myTeamName != "")
            {
                str_where += " and myTeamName='" + Common._myTeamName + "'";
            }
 
            string str_sql = string.Format(@"Select DISTINCT GuanweiID,GuanweiName From  " + this.TableName+"  "+ str_where + " Order by GuanweiName");
            
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookUpEditGuanweiName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditGuanweiName.ItemIndex = 0;
            lookUpEditGuanweiName.Properties.BestFit();
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
                if (dateStr.EditValue == null && dateStr.EditValue.ToString() == "-1" && dateStr.EditValue.ToString() != "")
                {                    
                    XtraMsgBox.Show("请输入查询日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //SqlParameter[] paraList = new SqlParameter[7];
                //paraList[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 16);
                //paraList[0].Value = DateTime.Parse(dateStr.EditValue.ToString()).ToString("yyyy-MM-dd HH:mm");
                //m_dicItemData.Clear();

                //m_dicItemData["JobForID"] = "0";
                //m_dicItemData["ProjectID"] = "0";
                //m_dicItemData["LineID"] = "0";
                //m_dicItemData["TeamID"] = "0";
          
                //paraList[1] = new SqlParameter("@JobForID", SqlDbType.VarChar, 10);
                //paraList[1].Value = m_dicItemData["JobForID"].ToString();

                //paraList[2] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
                //paraList[2].Value = m_dicItemData["ProjectID"].ToString();

                //paraList[3] = new SqlParameter("@LineID", SqlDbType.VarChar, 10);
                //paraList[3].Value = m_dicItemData["LineID"].ToString();

                //paraList[4] = new SqlParameter("@TeamID", SqlDbType.VarChar, 10);
                //paraList[4].Value = m_dicItemData["TeamID"].ToString();


                //paraList[5] = new SqlParameter("@OperID", SqlDbType.VarChar, 10);
                //if (Common._personid.ToString() != "")
                //{
                //    paraList[5].Value = Common._personid;
                //}
                //else
                //{
                //    paraList[5].Value = "0";
                //}
                ////执行存储过程1
                //Common.AdoConnect.Connect.SetExecuteSP("PROC_Attend_Total_Result", Common.Choose.OnlyExecSp, paraList);

                //SqlParameter[] paraListCardData = new SqlParameter[1];
                //paraListCardData[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
            
                //paraListCardData[0].Value = DateTime.Parse(dateStr.EditValue.ToString()).ToString("yyyy-MM-dd HH:mm");
                
               
                ////执行存储过程2,打卡时间
                //Common.AdoConnect.Connect.SetExecuteSP("PROC_CardData_Attend_Result", Common.Choose.OnlyExecSp, paraListCardData);


                //增加可用
                NewButtonEnabled = true;
                if (dateStr.EditValue == null && dateStr.EditValue.ToString() == "-1")
                {
                    XtraMsgBox.Show("请输入查询日期！",
                                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string str_sql = string.Empty;
                str_sql = string.Format(@" SELECT  DISTINCT CAST('0' AS Bit) AS SlctValue, a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,ID,
                                                     JobForID, JobForName,   ProjectID, ProjectName,LineID, LineName, TeamID,TeamName, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM   V_Attend_Result_Info AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 
                                                  GROUP BY a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID, a1.GuanweiID,a1.AttendDate
                                            ) AS h 
                                               ON  a.JobForID = h.JobForID
                                               AND a.ProjectID= h.ProjectID 
                                               AND a.LineID= h.LineID 
                                               AND a.TeamID= h.TeamID 
                                               AND a.GuanweiID= h.GuanweiID
                                               AND a.AttendDate= h.AttendDate
                                       where  a.AttendDate=CONVERT(VARCHAR(10),'{1}',120)    ",
                                  dateStr.Text.Trim(), dateStr.Text.Trim());
                
                if (lookUpEditJobFor.EditValue != null && lookUpEditJobFor.EditValue.ToString() !="-1" )
                {
                    str_sql += " and a.JobForID= " + lookUpEditJobFor.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditProjectName.EditValue != null && lookUpEditProjectName.EditValue.ToString() != "-1")
                {
                    str_sql += " and a.ProjectID= " + lookUpEditProjectName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditLineName.EditValue != null && lookUpEditLineName.EditValue.ToString() != "-1")
                {
                    str_sql += " and a.LineID= " + lookUpEditLineName.EditValue.ToString().Trim() + " ";
                }
                  if (lookUpEditTeamName.EditValue != null && lookUpEditTeamName.EditValue.ToString() != "-1")
                {
                    str_sql += " and a.TeamID= " + lookUpEditTeamName.EditValue.ToString().Trim() + " ";
                }
                  if (lookUpEditGuanweiName.EditValue != null && lookUpEditGuanweiName.EditValue.ToString() != "-1")
                {
                    str_sql += " and a.GuanweiID= " + lookUpEditGuanweiName.EditValue.ToString().Trim() + " ";
                }


                  str_sql += " ORDER BY  a.JobForID ,a.ProjectID ,a.LineID,a.TeamID";
                  //str_sql += " ORDER BY  JobForID ASC ";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    this.CopyAddEnabled = true;
                    DeleteButtonEnabled = true;
                    this.SelectAllButtonEnabled = true;
                    this.SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    this.CopyAddEnabled = false;
                    DeleteButtonEnabled = false;
                    this.SelectAllButtonEnabled =false;
                    this.SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                }
                this.m_GridViewUtil.GridControlList.DataSource = m_tblDataList;
  
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 检查删除数据在【人员资料登记】里是否有关联数据
        /// </summary>
        public Boolean CheckDeleteDate(DataRow[] drs)
        {
           try
            {
                if (drs.Length > 0)
                {
                    for (int i = 0; i < drs.Length; i++)
                    {
                        DataRow dr = drs[i];
                        string str_sql = "Select UserID from Produce_User where 1=1 ";
                        str_sql += " and JobForID= " + dr["JobForID"].ToString();
                        str_sql += " and ProjectID= " + dr["ProjectID"].ToString();
                        str_sql += " and LineID= " + dr["LineID"].ToString();
                        str_sql += " and TeamID= " + dr["TeamID"].ToString();
                        str_sql += " and TeamID= " + dr["GuanweiID"].ToString();
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                        if (m_tblDataList.Rows.Count > 0)
                        {

                            XtraMsgBox.Show("因为向别-班别-关位是：" 
                                            + dr["JobForName"].ToString() +"  "
                                            + dr["ProjectName"].ToString() + "  "
                                            + dr["LineName"].ToString() + "  "
                                            + dr["TeamName"].ToString() + "  "
                                            + dr["GuanweiID"].ToString()
                                            +" 已经分配了用户，不能删除！", 
                                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                return false;
                
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("删除检查失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                return true;
            }
        }

        /// <summary>
        /// 获取勾选数据
        /// </summary>
        protected DataRow[] GetSelectList()
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            //获取删除行
            DataTable dt = ((DataView)this.gridView1.DataSource).Table.Copy();
            //选择所有选择的数据
            DataRow[] drs = dt.Select(EnumDefine.SlctValue + "='true'");
            return drs;
        }

        /// <summary>
        /// 获取下拉框数据
        /// </summary>
        public void GetLookUpList()
        {
            dateStr.EditValue = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            //向别
            string strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_para order by JobForName");
            
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


            //工程别
            
             strSql = string.Format(@"select distinct ProjectID,ProjectName from V_Produce_para order by ProjectName");
            
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditProjectName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditProjectName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditProjectName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName; ;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditProjectName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditProjectName.ItemIndex = 0;
            lookUpEditProjectName.Properties.BestFit();

           
            //Line别
            
            strSql = string.Format(@"select distinct LineID,LineName from V_Produce_para order by LineName");
           
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditLineName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditLineName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditLineName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditLineName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditLineName.ItemIndex = 0;
            lookUpEditLineName.Properties.BestFit();

            //班别
            
            strSql = string.Format(@"select distinct TeamID,TeamName from V_Produce_para order by TeamName");
            
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditTeamName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditTeamName.ItemIndex = 0;
            lookUpEditTeamName.Properties.BestFit();

            //关位
            
            strSql = string.Format(@"select distinct GuanweiID,GuanweiName from V_Produce_para order by GuanweiName");
           
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookUpEditGuanweiName.Properties.DataSource = dt_temp.DefaultView;
            lookUpEditGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookUpEditGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            if (dt_temp.Rows.Count > 0)
            {
                lookUpEditGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditGuanweiName.ItemIndex = 0;
            lookUpEditGuanweiName.Properties.BestFit();
           
        }
       
       
        #endregion
    
    }
}
