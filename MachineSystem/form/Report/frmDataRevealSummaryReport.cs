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
    public partial class frmDataRevealSummaryReport : Framework.Abstract.frmSearchBasic2	
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
        public frmDataRevealSummaryReport()
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
            ///////////
            dateOperDate1.EditValue = DateTime.Now;
            dateOperDate2.EditValue = DateTime.Now;
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                string strSql = string.Empty;
                //向别 
                if (Common._Administrator==Common._personid)
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
                lookJobFor.Properties.DataSource = dt_temp.DefaultView;
                lookJobFor.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookJobFor.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
                if (dt_temp.Rows.Count > 0)
                {
                    lookJobFor.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookJobFor.ItemIndex = 0;
                lookJobFor.Properties.BestFit();

                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Team, lookProduce_Team, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
 
               
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
            
            //base.SetSearchProc(frmBaseToolXC);

            //GetDspDataList();



            GetDspDataListV2();
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

            
        }

        /// <summary>
        /// 选择向别-班别加载该向别下的关位
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            

            string str_sql = string.Format(@"Select DISTINCT GuanweiName From V_Produce_Para " + str_where + " Order by GuanweiName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            //dr[0] = "-1";
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
             
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
                
                //string str_sql = string.Format(@"Select  CAST('0' AS Bit) AS SlctValue, * From v_Produce_User");
 
                //m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //gridControl1.DataSource = m_tblDataList;
                //if (m_tblDataList.Rows.Count > 0) 
                //{
                //    EditButtonEnabled = true;
                //    DeleteButtonEnabled = true;
                //    ExcelButtonEnabled = true;
                //    SelectAllButtonEnabled = true;
                //    SelectOffButtonEnabled = true;
                //}
                //else
                //{
                    
                //    SelectAllButtonEnabled = false;
                //    SelectOffButtonEnabled = false;
                //}
                //gridView1.BestFitColumns();
                //gridView1.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected  void GetDspDataListV2()
        {
            string _sql = "SELECT * FROM V_Produce_TeamShow_Group_i as a where 1=1 ";
            string _start;
            string _end;
            try
            {

                //string str_sql = string.Format(@"Select  CAST('0' AS Bit) AS SlctValue, * From v_Produce_User");
                //m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dateOperDate1.EditValue != null&&!string.IsNullOrEmpty(dateOperDate1.EditValue.ToString())&&
                    dateOperDate2.EditValue != null&&!string.IsNullOrEmpty(dateOperDate2.EditValue.ToString()))
                {
                    _start = dateOperDate1.DateTime.ToString("yyyy-MM-dd");
                    _end = dateOperDate2.DateTime.ToString("yyyy-MM-dd");

                    _sql += " and AttendDate>='" + _start + "'" +
                         " and AttendDate<='" + _end + "'";
                }
                //向别
                if (lookJobFor.EditValue.ToString() != "-1")
                {
                   _sql+= " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
                }
                //工程别
                if (lookProject.EditValue.ToString() != "-1")
                {
                   _sql+=" and ProjectID='" + lookProject.EditValue.ToString() + "'";
                }
                //班别
                if (lookProduce_Team.EditValue != null)
                {
                    if (lookProduce_Team.EditValue.ToString() != "-1")
                    {
                        _sql += " and TeamID='" + lookProduce_Team.EditValue.ToString() + "'";
                    }
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
                gridView1.BestFitColumns();
                gridView1.OptionsBehavior.Editable = false;
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
          
        }
        #endregion


    }
}
