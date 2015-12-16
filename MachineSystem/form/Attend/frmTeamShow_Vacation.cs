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

/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于请假登记信息的资料查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_Vacation:Framework.Abstract.frmSearchBasic2 
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_Vacation));
        /// 界面初始化标示
        bool isLoad = true;
        #endregion
        

        #region 画面初始化
        public frmTeamShow_Vacation()
        {
            InitializeComponent();

            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            this.TableName = "Attend_Vacation";//操作表名称
            m_ParenSlctColName = "SlctValue";
            CopyAddCaption = "数据上传";            
          
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        ///  
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();
                //初始化日期
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime LastDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                dateBeginDate.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
                dateEndDate.EditValue = string.Format("{0:yyyy-MM-dd}", LastDay);

                //获取检索条件中下拉框数据
                GetSelectLookUpList();
                lookJobFor.Text = Common._JobForId;
                lookmyTeamName.Text = Common._orgTeamName;
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
        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            base.SetSearchProc(frmBaseToolXC);

            GetDspDataList();
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
                frmTeamShow_VacationAdd frm = new frmTeamShow_VacationAdd();
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

                frmTeamShow_VacationAdd frm = new frmTeamShow_VacationAdd();
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
                            m_dicItemData["ID"] = dr["myID"].ToString();
                            m_dicPrimarName["ID"] = dr["myID"].ToString();
                            result = SysParam.m_daoCommon.SetDeleteDataItem("Attend_Vacation", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("请假登记:", "删除", dr["myID"].ToString());
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
        
        //根据用户id，查询用户其他信息
        private void txtUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txtUserID.EditValue = _frm.SelectRowData["UserID"].ToString();
                this.txtUserName.EditValue = _frm.SelectRowData["UserName"].ToString();
            }
        }
        #endregion

        #region 事件处理方法
        /// <summary>
        /// 选择向别别加载该班别下的班别
        /// </summary>
        private void lookJobFor_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookJobFor.EditValue.ToString() != "-1")
            {
                str_where = " and  JobForID ='" + lookJobFor.EditValue.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT orgTeamName From  V_Attend_Vacation_i " + str_where + " Order by GuanweiName");
           
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookmyTeamName.Properties.DataSource = dt_temp.DefaultView;
            lookmyTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookmyTeamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookmyTeamName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookmyTeamName.ItemIndex = 0;
            lookmyTeamName.Properties.BestFit();
        }
        /// <summary>
        /// 选择班别加载该班别下的关位
        /// </summary>
        private void lookmyTeamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = " where 1=1 ";
            if (lookmyTeamName.Text.ToString() != "全部")
            {
                str_where = " and myTeamName ='" + lookmyTeamName.Text.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT GuanweiID,GuanweiName From  V_Attend_Vacation_i   " + str_where + " Order by GuanweiName");

            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookGuanweiName.Properties.DataSource = dt_temp.DefaultView;
            lookGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanweiName.ItemIndex = 0;
            lookGuanweiName.Properties.BestFit();
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
                txtUserID.Enabled = true;
                txtUserID.Properties.ReadOnly = false;
                NewButtonEnabled = true;
                string str_sql = "Select  CAST('0' AS Bit) AS SlctValue,* From V_Attend_Vacation_i where 1=1 ";
                //请假开始时间检查：未输入
                if (string.IsNullOrEmpty(dateBeginDate.EditValue.ToString()))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateBeginDate, "请选择请假开始时间!");
                    return;
                }
                //请假结束时间检查：未输入
                if (string.IsNullOrEmpty(dateEndDate.EditValue.ToString()))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateEndDate, "请选择请假结束时间!");
                    return;
                }
                //开始日期，结束日期
                if ((dateBeginDate.EditValue != null && dateBeginDate.EditValue.ToString() != "")
                    && (dateEndDate.EditValue != null && dateEndDate.EditValue.ToString() != ""))
                {
                    str_sql += " and  (CONVERT(varchar(10),VacationBgnDate,120) between '" + DateTime.Parse(dateBeginDate.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "'  and  '" + DateTime.Parse(dateEndDate.EditValue.ToString()).ToString("yyyy-MM-dd") + "'";
                    str_sql += " OR  CONVERT(varchar(10),VacationEndDate,120) between '" + DateTime.Parse(dateBeginDate.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "'  and  '" + DateTime.Parse(dateEndDate.EditValue.ToString()).ToString("yyyy-MM-dd") + "')";
                    
                }
                else if (dateBeginDate.EditValue != null && dateBeginDate.EditValue.ToString() != "")
                {
                    str_sql += " and  '" + DateTime.Parse(dateBeginDate.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "' <=  CONVERT(varchar(10),VacationBgnDate,120)";
                }
                else if (dateEndDate.EditValue != null && dateEndDate.EditValue.ToString() != "")
                {
                    str_sql += " and  '" + DateTime.Parse(dateEndDate.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "' >=  and  CONVERT(varchar(10),VacationEndDate,120)";
                }

                

                //工号
                if (txtUserID.Text.Trim() != "")
                {
                    str_sql += " and myUserID like '%" + txtUserID.Text.Trim() + "%' ";
                }
                //姓名
                if (txtUserName.Text.Trim() != "")
                {
                    str_sql += " and UserName like '%" + txtUserName.Text.Trim() + "%' ";
                }
                //向别
                if (this.lookJobFor.EditValue.ToString() != "-1")
                {
                    str_sql += " and JobForID= '" + lookJobFor.EditValue.ToString() + "' ";
                }
                //向别-班别
                if (this.lookmyTeamName.Text.ToString() != "全部")
                {
                    str_sql += " and orgTeamName in ('" + this.lookmyTeamName.Text.Trim() + "') ";
                }
                //关位
                if (this.lookGuanweiName.EditValue.ToString() != "-1")
                {
                    str_sql += " and GuanweiID = '" + lookGuanweiName.EditValue.ToString() + "' ";
                }
                //请假类型
                if (this.lookVacationType.EditValue.ToString() != "-1")
                {
                    str_sql += " and VacationType = '" + lookVacationType.EditValue.ToString() + "' ";
                }

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }
                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    SearchButtonEnabled = true;
                    DeleteButtonEnabled = true;
                    EditButtonEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
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
        /// 检查删除数据在【出勤情况】里是否有关联数据
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
                        string str_sql = "Select UserID from V_Attend_Result_Info where 1=1 ";
                        str_sql += " and JobForID= " + dr["JobForID"].ToString();

                        str_sql += " and (AttendDate between '" + dr["VacationBgnDate"].ToString();
                        str_sql += "' and  '" + dr["VacationEndDate"].ToString()+"')";
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                        if (m_tblDataList.Rows.Count > 0)
                        {

                            XtraMsgBox.Show("请假信息已经被统计到出勤情况中，不能删除！",
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
        public void GetSelectLookUpList()
        {
            try
            {
                string strSql = "";
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


                //工程别-班别
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct orgTeamName from V_Attend_Vacation_i WHERE orgTeamName<>''  order by orgTeamName");
                }
                else
                {
                    strSql = string.Format(@"Select distinct orgTeamName From V_Attend_Vacation_i  where  myTeamName in ('{0}') AND orgTeamName<>'' Order by orgTeamName", Common._myTeamName);
                }
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                dr = dt_temp.NewRow();
            
                dr[0] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookmyTeamName.Properties.DataSource = dt_temp.DefaultView;
                lookmyTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookmyTeamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName; ;

                if (dt_temp.Rows.Count > 0)
                {
                    lookmyTeamName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookmyTeamName.ItemIndex = 0;
                lookmyTeamName.Properties.BestFit();

                //关位
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName from V_Attend_Vacation_i WHERE GuanweiID<>0  order by GuanweiName");
                }
                else
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName  from V_Attend_Vacation_i   where  myTeamName in ('{0}') AND   GuanweiID<>0 Order by GuanweiName", Common._myTeamName);
                }
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                dr = dt_temp.NewRow();
                dr[0] = "-1";
                dr[1] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookGuanweiName.Properties.DataSource = dt_temp.DefaultView;
                lookGuanweiName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookGuanweiName.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

                if (dt_temp.Rows.Count > 0)
                {
                    lookGuanweiName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookGuanweiName.ItemIndex = 0;
                lookGuanweiName.Properties.BestFit();

                //请假类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Attend_VacationType, this.lookVacationType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);

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
