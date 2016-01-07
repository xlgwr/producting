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
    public partial class frmWorkOverTimeDetailReport : Framework.Abstract.frmSearchBasic2	
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
        public frmWorkOverTimeDetailReport()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;

            this.m_GridViewUtil.ParentGridView = this.gridView1;
            //m_ParenSlctColName = "SlctValue";
            this.TableName = "v_Produce_User";
            /////////////////
            EditButtonVisibility = false;
            DeleteButtonVisibility = false;
            SelectAllButtonVisibility = false;
            SelectOffButtonVisibility = false;
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                dateOperDate1.EditValue = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                dateOperDate2.EditValue = DateTime.Now.ToString("yyyy-MM-dd");

                base.SetFormValue();
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
        /// 选择向别-班别加载该向别下的关位
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookmyteamName.EditValue.ToString() != "-1")
            {
                str_where += " and myteamName='" + lookmyteamName.EditValue.ToString() + "'";
            }

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
                //testc();
                //return;
                string _sql = "select * from V_Attend_OT_Set as a where 1=1 ";
                if (!string.IsNullOrEmpty(txtoperNo.Text.Trim()))
                {
                    _sql += " and a.myUserID Like '%" + txtoperNo.Text.Trim() + "%'";
                }
                if (!string.IsNullOrEmpty(txtoperName.Text.Trim()))
                {
                    _sql += " and a.UserName Like '%" + txtoperName.Text.Trim() + "%'";
                }
                if (lookmyteamName.EditValue.ToString() != "-1" && lookmyteamName.Text.ToString() != "全部")
                {
                    _sql += " and a.myTeamName='" + lookmyteamName.EditValue.ToString() + "'";
                }
                if (lookGuanwei.EditValue.ToString() != "-1" && lookGuanwei.Text.ToString() != "全部")
                {
                    _sql+= " and a.GuanweiName like '%" + lookGuanwei.EditValue.ToString() + "%'";
                }
                if (dateOperDate1.EditValue != null && !string.IsNullOrEmpty(dateOperDate1.EditValue.ToString()) &&
                   dateOperDate2.EditValue != null && !string.IsNullOrEmpty(dateOperDate2.EditValue.ToString()))
                {

                    _sql += " and CONVERT(varchar(10),a.OtDate ,120)  >= CONVERT(varchar(10),'" + DateTime.Parse(dateOperDate1.EditValue.ToString()).ToString("yyyy-MM-dd") + "' ,120)" +
                        " and CONVERT(varchar(10),a.OtDate ,120)  <= CONVERT(varchar(10),'" + DateTime.Parse(dateOperDate2.EditValue.ToString()).ToString("yyyy-MM-dd") + "' ,120)";
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
                    EditButtonEnabled = false;
                }
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        private void testc()
        {
            DataColumn _col = new DataColumn("OTStrDate", typeof(string));
            DataColumn _col1 = new DataColumn("OTEndTime", typeof(string));

            m_tblDataList.Columns.Add(_col);
            m_tblDataList.Columns.Add(_col1);
            DataRow _dr = m_tblDataList.NewRow();
            _dr["OTStrDate"] = DateTime.Now.ToString();
            _dr["OTEndTime"] = DateTime.Now.AddDays(1).ToString();
            m_tblDataList.Rows.Add(_dr);
            gridControl1.DataSource = m_tblDataList;
        }
        

        public void GetComboBox() 
        {
            DataTable dt_temp = new DataTable();
            DataRow dr;
            string strSql = "";

            //工程别-班别
            if (Common._Administrator == Common._personid)
            {
                strSql = string.Format(@"select distinct myTeamName from V_Produce_User_Line WHERE myTeamName<>'' ");
            }
            else
            {
                strSql = string.Format(@"select distinct myTeamName from V_Produce_User_Line where  UserID ='{0}' AND  myTeamName<>'' ", Common._personid);
            }
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

            //默认选中
            dr = dt_temp.NewRow();

            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
            lookmyteamName.Properties.DataSource = dt_temp.DefaultView;
            lookmyteamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookmyteamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName; ;

            if (dt_temp.Rows.Count > 0)
            {
                lookmyteamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookmyteamName.ItemIndex = 0;
            lookmyteamName.Properties.BestFit();

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

        private void dateOperDate2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {

        }


    }
}
