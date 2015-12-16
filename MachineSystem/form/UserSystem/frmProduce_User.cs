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
    public partial class frmProduce_User : Framework.Abstract.frmSearchBasic2	
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
        #endregion


        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduce_User()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            this.TableName = "v_Produce_User";
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                //向别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_JobFor, lookJobFor, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Team, lookProduce_Team, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //职等
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_User_Duty,lookDuty, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                //状态
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_User_Status1, lookStatus, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);


                //获取下拉框数据
                GetComboBox();
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
            base.SetSearchProc(frmBaseToolXC);

            //GetDspDataList();
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        public override void SetModifyInit()
        {
            base.SetModifyInit(); 
            
            try
            {
                frmEditProduce_User frm = new frmEditProduce_User();
                frm.ScanMode = Common.DataModifyMode.upd;
                frm.dr = gridView1.GetFocusedDataRow();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 选择向别加载该向别下的工程别
        /// </summary>
        private void lookJobFor_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where=" where 1=1 ";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where+=" and JobForID='"+lookJobFor.EditValue.ToString()+"'";
            }
            string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM V_Produce_Para " + str_where + " order by ProjectName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookProject.Properties.DataSource = dt_temp.DefaultView;
            lookProject.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProject.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookProject.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProject.ItemIndex = 0;
            lookProject.Properties.BestFit();
        }

        /// <summary>
        /// 选择工程别加载该向别下的Line别
        /// </summary>
        private void lookProject_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where = " JobForID ='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                if (str_where != "") 
                {
                    str_where += " and ";
                }
                str_where = str_where + " ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (str_where != "")
            {
                str_where = " where " + str_where;
            }
            string str_sql = string.Format(@"select DISTINCT LineID,LineName FROM V_Produce_Para " + str_where + " order by LineName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookLine.Properties.DataSource = dt_temp.DefaultView;
            lookLine.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookLine.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookLine.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookLine.ItemIndex = 0;
            lookLine.Properties.BestFit();
        }

        /// <summary>
        /// 选择Line别加载该向别下的班别
        /// </summary>
        private void lookLine_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                str_where += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (lookLine.EditValue.ToString() != "-1")
            {
                str_where += " and LineID='" + lookLine.EditValue.ToString() + "'";
            }
            if (str_where != "")
            {
                str_where = " where 1=1 " + str_where;
            }
            string str_sql = string.Format(@"select DISTINCT TeamID as ID,TeamName as pName FROM V_Produce_Para " + str_where + " order by TeamName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookProduce_Team.Properties.DataSource = dt_temp.DefaultView;
            lookProduce_Team.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProduce_Team.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookProduce_Team.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProduce_Team.ItemIndex = 0;
            lookProduce_Team.Properties.BestFit();
        }

        /// <summary>
        /// 选择班别加载该向别下的关位
        /// </summary>
        private void lookProduce_Team_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString() != "-1")
            {
                str_where += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                str_where += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (lookLine.EditValue.ToString() != "-1")
            {
                str_where += " and LineID='" + lookLine.EditValue.ToString() + "'";
            }
            if (lookProduce_Team.EditValue!= null)
            {
                if (lookProduce_Team.EditValue.ToString() != "-1")
                {
                    str_where += " and TeamID='" + lookProduce_Team.EditValue.ToString() + "'";
                }
            }
            if (str_where != "")
            {
                str_where = " where 1=1 " + str_where;
            }
            //string str_sql = string.Format(@"Select DISTINCT GuanweiNames From Produce_Guanwei " + str_where + " Order by GuanweiNames");
            string str_sql = string.Format(@"select Distinct T.GuanweiName from (
                                                    (select p.*,g.pName as GuanweiName from Produce_Guanwei p inner 
                                                        join P_Produce_Guanwei g on p.GuanweiID=g.ID) )T" + str_where + " Order by GuanweiName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            //dr[0] = "-1";
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookGuanwei.Properties.DataSource = dt_temp.DefaultView;
            lookGuanwei.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanwei.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanwei.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanwei.ItemIndex = 0;
            lookGuanwei.Properties.BestFit();
        }

        /// <summary>
        /// 选择向别-班别加载该向别下的关位
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            //if (lookmyteamName.EditValue.ToString() != "-1")
            //{
            //    str_where += " and myteamName='" + lookmyteamName.EditValue.ToString() + "'";
            //}

            string str_sql = string.Format(@"Select DISTINCT GuanweiName From V_Produce_Para " + str_where + " Order by GuanweiName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            //dr[0] = "-1";
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookGuanwei.Properties.DataSource = dt_temp.DefaultView;
            lookGuanwei.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanwei.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanwei.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanwei.ItemIndex = 0;
            lookGuanwei.Properties.BestFit();
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
                string str_where=" WHERE 1=1 ";
                string str_sql = string.Format(@"Select  CAST('0' AS Bit) AS SlctValue,(CASE WHEN AttendUnit = 1 THEN '是' ELSE '否' END) AS AttendUnitName,* From v_Produce_User");

                if (!string.IsNullOrEmpty(txtoperNo.Text.Trim())) 
                {
                    str_where += " and UserID Like '%" + txtoperNo.Text.Trim() + "%'";
                }

                if (!string.IsNullOrEmpty(txtoperName.Text.Trim()))
                {
                    str_where += " and UserName Like '%" + txtoperName.Text.Trim() + "%'";
                }
                if (lookDuty.EditValue.ToString()!="-1") 
                {
                    str_where += " and DutyName like '%" + lookDuty.Text.ToString() + "%' ";
                }
                if (lookJobFor.EditValue.ToString() != "-1")
                {
                    str_where += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
                }
                if (lookProject.EditValue.ToString() != "-1")
                {
                    str_where += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
                }
                if (lookLine.EditValue.ToString() != "-1")
                {
                    str_where += " and LineID='" + lookLine.EditValue.ToString() + "'";
                }
                if (lookProduce_Team.EditValue.ToString() != "-1")
                {
                    str_where += " and TeamID='" + lookProduce_Team.EditValue.ToString() + "'";
                }
                if (lookGuanwei.EditValue.ToString() != "-1" && lookGuanwei.Text.ToString() != "全部") 
                {
                    str_where += " and GuanweiName like '%" + lookGuanwei.EditValue.ToString() + "%'";
                }
               
                if (lookStatus.EditValue.ToString() != "-1")
                {
                    str_where += " and StatusNames like '%" + lookStatus.Text.ToString() + "%'";
                }
                str_sql += str_where;
                str_sql += " order by JobForID";
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                gridControl1.DataSource = m_tblDataList;
                if (m_tblDataList.Rows.Count > 0) 
                {
                    EditButtonEnabled = true;
                    DeleteButtonEnabled = true;
                    ExcelButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                    ExcelButtonEnabled = false;
                }
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }


        public void GetComboBox() 
        {
            DataTable dt_temp = new DataTable();
            //工程别
            string strSql = string.Format(@"Select DISTINCT ProjectID,ProjectName From V_Produce_Para Order by ProjectName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            lookProject.Properties.DataSource = dt_temp.DefaultView;
            lookProject.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProject.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            if (dt_temp.Rows.Count > 0)
            {
                lookProject.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProject.ItemIndex = 0;
            lookProject.Properties.BestFit();
            
            //Line别
            strSql = string.Format(@"Select DISTINCT LineID,LineName FROM V_Produce_Para order by LineName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            lookLine.Properties.DataSource = dt_temp.DefaultView;
            lookLine.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookLine.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            if (dt_temp.Rows.Count > 0)
            {
                lookLine.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookLine.ItemIndex = 0;
            lookLine.Properties.BestFit();

            ////向别-班别
            //strSql = string.Format(@"Select distinct myteamName from [V_Produce_Para] where myteamName is not null order by myteamName");
            //dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            //lookmyteamName.Properties.DataSource = dt_temp.DefaultView;
            //lookmyteamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            //lookmyteamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;

            ////默认选中
            //dr = dt_temp.NewRow();
            //dr[0] = "全部";
            //dt_temp.Rows.InsertAt(dr, 0);

            //if (dt_temp.Rows.Count > 0)
            //{
            //    lookmyteamName.EditValue = dt_temp.Rows[0][0].ToString();
            //}
            //lookmyteamName.ItemIndex = 0;
            //lookmyteamName.Properties.BestFit();

            //关位
            strSql = string.Format(@"select Distinct T.GuanweiName from (
                                        (select p.*,g.pName as GuanweiName from Produce_Guanwei p 
                                            inner join P_Produce_Guanwei g on p.GuanweiID=g.ID) )T
                                            where 1=1 Order by GuanweiName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            lookGuanwei.Properties.DataSource = dt_temp.DefaultView;
            lookGuanwei.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanwei.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            if (dt_temp.Rows.Count > 0)
            {
                lookGuanwei.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanwei.ItemIndex = 0;
            lookGuanwei.Properties.BestFit();
        }
        #endregion


    }
}
