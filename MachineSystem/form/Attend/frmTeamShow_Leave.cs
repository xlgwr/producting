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
using MachineSystem.SettingTable;
using DevExpress.XtraEditors.Controls;

/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：libing
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于【 离职登记 】信息的资料查询、删除、批量删除操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_Leave :Framework.Abstract.frmSearchBasic2
    {
        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_Leave));
        /// 界面初始化标示
        bool isLoad = true;
        #endregion

        #region 画面初始化
        public frmTeamShow_Leave()
        {
            InitializeComponent();
            this.m_GridViewUtil.GridControlList = this.gridControl1;
            this.m_GridViewUtil.ParentGridView = this.gridView1;
            m_ParenSlctColName = "SlctValue";
            this.TableName = "V_Attend_Leave";//操作表名称
            CopyAddCaption = "数据上传";
            
        }

     

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            string strSql = string.Empty;
            DataTable dt_temp = new DataTable();
            
            try
            {
                base.SetFormValue();
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
               DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);
                dateStr.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
                dateEnd.EditValue = string.Format("{0:yyyy-MM-dd}", LastDay);

                //获取检索条件中下拉框数据
                GetSelectLookUpList();
                isLoad = false;
                if (Common._myTeamName != "")
                {
                    lookUpEditmyTeamName.Text = Common._myTeamName;
                }
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

            //GetDspDataList();
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
                frmTeamShow_LeaveAdd frm = new frmTeamShow_LeaveAdd();
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

                frmTeamShow_LeaveAdd frm = new frmTeamShow_LeaveAdd();
                frm.ScanMode = Common.DataModifyMode.upd;
                //选择所有选择的数据
                frm.dr = this.GetSelectList()[0];
                if (frm.dr["Flag"].ToString() == "1")
                {
                    XtraMsgBox.Show("用户离职登记信息已经上传，不能修改！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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
                            result = SysParam.m_daoCommon.SetDeleteDataItem("Attend_Leave", m_dicItemData, m_dicPrimarName);
                            if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("离职登记:", "删除", dr["myID"].ToString());
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

        /// <summary>
        /// 离职数据上传
        /// </summary>
        protected override void SetCopyAddInit()
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
               
                if (XtraMsgBox.Show("是否上传离职数据？", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                      if (drs.Length > 0)
                    {
                        for (int i = 0; i < drs.Length; i++)
                        {
                            m_dicPrimarName.Clear();
                            m_dicPrimarName["UserID"] = drs[i]["myUserID"].ToString();
                            m_dicItemData = new System.Collections.Specialized.StringDictionary();
                            m_dicItemData["UserID"] = drs[i]["myUserID"].ToString();
                            m_dicItemData["Flag"] = "1";
                            m_dicItemData["OperID"] = Common._personid;
                            m_dicItemData["OperDate"] = System.DateTime.Now.ToString();

                            result = SysParam.m_daoCommon.SetModifyDataItem("Attend_Leave", m_dicItemData, m_dicPrimarName);

                           if (result > 0)
                            {
                                //日志
                                SysParam.m_daoCommon.WriteLog("离职登记:", "修改", drs[i]["myID"].ToString());
                            }
                        }
                    }
                    if (result > 0)
                    {
                        XtraMsgBox.Show("上传数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GetDspDataList();
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("上传数据失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region 事件处理方法

        /// <summary>
        /// 选择班别加载该班别下的关位
        /// </summary>
        private void lookmyTeamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = "";
            if (lookUpEditmyTeamName.Text.ToString() != "全部")
            {
                str_where = " myTeamName ='" + lookUpEditmyTeamName.Text.ToString() + "'";
            }
            string str_sql = string.Format(@"Select DISTINCT GuanweiID,GuanweiName From  V_Produce_Para where 1=1  " + str_where + " Order by GuanweiName");

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
                string str_sql = "Select CAST('0' AS Bit) AS SlctValue, * " +
                                 " from V_Attend_Leave " + 
                                 " where 1=1  ";

                if (txtUserID.Text.Trim() != "")
                {
                    str_sql += " and myUserID like '%" + txtUserID.Text.Trim() + "%' ";
                }
                if (txtUserName.Text.Trim() != "")
                {
                    str_sql += " and  UserName like '%" + txtUserName.Text.Trim() + "%' ";
                }
                //向别-班别
                if (lookUpEditmyTeamName.Text.Trim() != "全部")
                {
                    str_sql += " and myTeamName ='" + lookUpEditmyTeamName.Text.Trim() + "' ";
                }
                //关位
                if (lookGuanweiName.EditValue.ToString() != "-1")
                {
                    str_sql += " and  GuanweiName = '" + lookGuanweiName.Text.ToString().Trim() + "' ";
                }
                //最后上班日
                if ((dateStr.EditValue != null && dateStr.EditValue.ToString() != "") &&
                (dateEnd.EditValue != null && dateEnd.EditValue.ToString() != ""))
                {
                    DateTime dtBegin = DateTime.Parse(dateStr.EditValue.ToString());
                    DateTime dtEnd = DateTime.Parse(dateEnd.EditValue.ToString());

                    str_sql += " and  SetDate BETWEEN '" + dtBegin.ToString("yyyy-MM-dd") + "' AND '" + dtEnd.ToString("yyyy-MM-dd") + "'  ";
                }
                //上传情况
                if (lookUpFlag.EditValue.ToString() != "-1")
                {
                    str_sql += " and  FlagName= '" + lookUpFlag.Text.ToString().Trim() + "' ";
                }
                //离职类型
                if (lookUpLeaveType.EditValue.ToString() != "-1")
                {
                    str_sql += " and  LeaveTypeName= '" + lookUpLeaveType.Text.ToString().Trim() + "' ";
                }
                //离职原因
                if (lookUpReason.EditValue.ToString() != "-1")
                {
                    str_sql += " and  LeaveReasonName= '" + lookUpReason.Text.ToString().Trim() + "' ";
                }
                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    str_sql += " and myTeamName='" + Common._myTeamName + "'";
                }

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                for (int i = 0; i < m_tblDataList.Rows.Count;i++ )
                {
                    //取得组长
                    str_sql = "select top 1 myTeamName , UserID,UserName AS LeaderUserName from v_Produce_User  where GuanweiName='组长'  and myTeamName ='"
                        + m_tblDataList.Rows[i]["myTeamName"].ToString() + "' ";
                    DataTable tem_1 = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if(tem_1.Rows.Count==1){
                        m_tblDataList.Rows[i]["LeaderNm"] = tem_1.Rows[0]["LeaderUserName"].ToString();
                    }
                    //取得班长
                    str_sql = "select top 1 myTeamName , UserID,UserName AS ClassNm from v_Produce_User  where GuanweiName='班长'  and myTeamName ='"
                        + m_tblDataList.Rows[i]["myTeamName"].ToString() + "' ";
                    tem_1 = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (tem_1.Rows.Count == 1)
                    {
                        m_tblDataList.Rows[i]["ClassNm"] = tem_1.Rows[0]["ClassNm"].ToString();
                    }
                }
                if (m_tblDataList.Rows.Count > 0)
                {
                    SearchButtonEnabled = true;
                    DeleteButtonEnabled = true;
                    SelectAllButtonEnabled = true;
                    SelectOffButtonEnabled = true;
                    EditButtonEnabled = true;
                    ExcelButtonEnabled = true;
                    CopyAddEnabled = true;
                }
                else
                {
                    DeleteButtonEnabled = false;
                    SelectAllButtonEnabled = false;
                    SelectOffButtonEnabled = false;
                    EditButtonEnabled = false;
                    ExcelButtonEnabled = false;
                    CopyAddEnabled = false;
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
                        if (dr["Flag"].ToString() == "1")
                        {
                            XtraMsgBox.Show("用户离职登记信息已经上传，不能删除！", 
                                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        
                        string str_sql = "Select UserID from V_Attend_Result_Info where 1=1 ";
                        str_sql += " and UserID= '" + dr["UserID"].ToString();
                        str_sql += "' and LastDate= '" + dr["SetDate"].ToString()+"'";//最后上班日
                        
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
                //离职类型
                SysParam.m_daoCommon.SetLoopUpEdit("Attend_LeaveType", this.lookUpLeaveType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //离职原因
                SysParam.m_daoCommon.SetLoopUpEdit("Attend_LeaveReason", this.lookUpReason, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
               
                //工程别-班别
                if (Common._Administrator == Common._personid)
                {
                    strSql = string.Format(@"select distinct myTeamName from V_Produce_User_Line WHERE myTeamName<>'' ");
                }
                else
                {
                    strSql = string.Format(@"select distinct myTeamName from V_Produce_User_Line where  UserID ='{0}' AND  myTeamName<>'' ", Common._personid);
                }
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                DataRow dr = dt_temp.NewRow();

                dr[0] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookUpEditmyTeamName.Properties.DataSource = dt_temp.DefaultView;
                lookUpEditmyTeamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpEditmyTeamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName; ;

                if (dt_temp.Rows.Count > 0)
                {
                    lookUpEditmyTeamName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookUpEditmyTeamName.ItemIndex = 0;
                lookUpEditmyTeamName.Properties.BestFit();

                //关位
                if (Common._myTeamName == "" || Common._myTeamName == null)
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName from V_Produce_Para WHERE GuanweiID<>0  order by GuanweiName");
                }
                else
                {
                    strSql = string.Format(@"select distinct GuanweiID,GuanweiName  from V_Produce_Para   where  myTeamName ='{0}' AND   GuanweiID<>0 Order by GuanweiName", Common._myTeamName);
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

                //上传情况
                dt_temp=new DataTable();
                dt_temp.Columns.Add("ID");
                dt_temp.Columns.Add("pName");
                DataRow dr1;
                dr1=dt_temp.NewRow();
                dr1["ID"] = "-1";
                dr1["pName"] = "全部";
                dt_temp.Rows.Add(dr1);
                lookUpFlag.Properties.DataSource = dt_temp.DefaultView;
                lookUpFlag.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpFlag.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

                dr1 = dt_temp.NewRow();
                dr1["ID"] = "0";
                dr1["pName"] = "未上传";
                dt_temp.Rows.Add(dr1);
                lookUpFlag.Properties.DataSource = dt_temp.DefaultView;
                lookUpFlag.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpFlag.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

                dr1 = dt_temp.NewRow();
                dr1["ID"] = "1";
                dr1["pName"] = "已上传";
                dt_temp.Rows.Add(dr1);
                lookUpFlag.Properties.DataSource = dt_temp.DefaultView;
                lookUpFlag.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpFlag.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

                lookUpFlag.EditValue = dt_temp.Rows[0][0].ToString();
                lookUpFlag.ItemIndex = 0;
                lookUpFlag.Properties.BestFit();
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
