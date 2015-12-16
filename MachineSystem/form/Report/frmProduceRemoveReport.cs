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
using MachineSystem.form.Report;
using log4net;

/********************************************************************************
** 作者： xuensheng
** 创始时间：2015-7-18

** 修改人：xuensheng
** 修改时间：2015-7-19
** 修改内容：代码规范化

** 描述：
**    调动报表的查询、导出操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmProduceRemoveReport :Framework.Abstract.frmSearchBasic2
    {

        #region 变量定义

        /// <summary>
        /// 表格数据
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmProduceRemoveReport));

        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduceRemoveReport()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;
             this.TableName = "V_Attend_Move_i";
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();
                //初始化日期
                dtpStartDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpEndDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                ////取得下拉框数据
                GetLookUpList();
            }
            catch (Exception ex)
            {
                log.Error(ex);
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
            base.SetSearchProc(frmBaseToolXC);

            GetDspDataList();
        }

        //根据用户id，查询用户其他信息
        private void txtUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txUserID.EditValue = _frm.SelectRowData["UserID"].ToString();
                this.txUserName.EditValue = _frm.SelectRowData["UserName"].ToString();
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
            
            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " and JobForID='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM  " + this.TableName + "  " + str_where + " order by ProjectName");
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
            
            string str_where = " where 1=1 ";
            if (lookUpEditJobFor.EditValue.ToString() != "-1")
            {
                str_where += " AND JobForID ='" + lookUpEditJobFor.EditValue.ToString() + "'";
            }
            if (lookUpEditProjectName.EditValue.ToString() != "-1")
            {
                str_where += " AND  ProjectID='" + lookUpEditProjectName.EditValue.ToString() + "'";
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
                str_where += " AND LineID='" + lookUpEditLineName.EditValue.ToString() + "'";

            }
            string str_sql = string.Format(@"select DISTINCT TeamID ,TeamName FROM  " + this.TableName + "  " + str_where + " order by TeamName");
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
                str_where += " AND LineID='" + lookUpEditLineName.EditValue.ToString() + "'";

            }
            if (lookUpEditTeamName.EditValue.ToString() != "-1")
            {
                str_where += " AND TeamID='" + lookUpEditTeamName.EditValue.ToString() + "'";
            }

            string str_sql = string.Format(@"Select DISTINCT GuanweiID,GuanweiName From  " + this.TableName + "  " + str_where + " Order by GuanweiName");

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
                
                txUserName.Enabled = true;
                string str_sql = "select  * from V_Attend_Move_i ";
                string str_where = " where 1=1 ";
                if (!string.IsNullOrEmpty(txUserID.Text.Trim())) //用户编号
                {
                    str_where += " and UserID='" + txUserID.Text.Trim() + "'";
                }
                if (!string.IsNullOrEmpty(txUserName.Text.Trim())) //用户名称
                {
                    str_where += " and UserName='" + txUserName.Text.Trim() + "'";
                }
                if (lookUpEditJobFor.EditValue != null && lookUpEditJobFor.EditValue.ToString() != "-1")
                {
                    str_where += " and JobForID= " + lookUpEditJobFor.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditProjectName.EditValue != null && lookUpEditProjectName.EditValue.ToString() != "-1")
                {
                    str_where += " and ProjectID= " + lookUpEditProjectName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditLineName.EditValue != null && lookUpEditLineName.EditValue.ToString() != "-1")
                {
                    str_where += " and LineID= " + lookUpEditLineName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditTeamName.EditValue != null && lookUpEditTeamName.EditValue.ToString() != "-1")
                {
                    str_where += " and TeamID= " + lookUpEditTeamName.EditValue.ToString().Trim() + " ";
                }
                if (lookUpEditGuanweiName.EditValue != null && lookUpEditGuanweiName.EditValue.ToString() != "-1")
                {
                    str_where += " and GuanweiID= " + lookUpEditGuanweiName.EditValue.ToString().Trim() + " ";
                }

                if (lookType.SelectedIndex > 0)//调动类型
                {
                    
                    int index=0;
                    if (lookType.SelectedIndex == 1) //1：人员调入
                    {
                        index = 1;
                    }
                    else if (lookType.SelectedIndex == 2)//2：人员调出
                    {
                        index = 2;
                    }
                    else if (lookType.SelectedIndex == 3)//3：关位调整
                    {
                        index = 3;
                    }
                    else if (lookType.SelectedIndex == 4)//4：支援调出
                    {
                        index = 4;
                    }
                    else if (lookType.SelectedIndex == 5)//5：支援调入
                    {
                        index = 5;
                    }
                    else if (lookType.SelectedIndex == 6)////6：替关调动
                    {
                        index = 6;
                    }
                    str_where += " and pFlag='" + index + "'";
                }
                str_where += " and (OperDate between '" + dtpStartDate.DateTime.ToString("yyyy-MM-dd") + "'  and  '" + dtpEndDate.DateTime.ToString("yyyy-MM-dd") + "') ";
                str_sql += str_where;

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                
                if (m_tblDataList.Rows.Count > 0)
                {
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



        /// 获取下拉框数据
        /// </summary>
        public void GetLookUpList()
        {
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


            //工程别
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct ProjectID,ProjectName from V_Produce_Para order by ProjectName");
            }
            else
            {
                strSql = string.Format(@"Select distinct ProjectID,ProjectName From V_Produce_Para  where  myTeamName in ('{0}') Order by ProjectName", Common._myTeamName);
            }
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
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct LineID,LineName from V_Produce_Para order by LineName");
            }
            else
            {
                strSql = string.Format(@"Select distinct LineID,LineName FROM V_Produce_Para  where  myTeamName in ('{0}') order by LineName", Common._myTeamName);
            }
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
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct TeamID,TeamName from V_Produce_Para order by TeamName");
            }
            else
            {
                strSql = string.Format(@"Select distinct TeamID,TeamName FROM V_Produce_Para  where  myTeamName in ('{0}') order by TeamName", Common._myTeamName);
            }
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
            if (Common._myTeamName == "" || Common._myTeamName == null)
            {
                strSql = string.Format(@"select distinct GuanweiID,GuanweiName from V_Produce_Para order by GuanweiName");
            }
            else
            {
                strSql = string.Format(@"select distinct GuanweiID,GuanweiName  from V_Produce_Para  Order by GuanweiName");
            }
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
