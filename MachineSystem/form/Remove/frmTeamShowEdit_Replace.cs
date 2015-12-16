using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.form.ParaLicense;
using Framework.Libs;
using MachineSystem.SysDefine;
using MachineSystem.TabPage;
using log4net;
using System.Collections.Specialized;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-26
** 修改内容：代码规范化

** 描述：
**    主要用于调动登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.form.Remove
{
    public partial class frmTeamShowEdit_Replace :Framework.Abstract.frmBaseToolEntryXC
    {
        #region 变量定义

        /// <summary>
        /// 选中行信息
        /// </summary>
        public DataRow dr;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShowEdit_Replace));
        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;

        #endregion

        #region 画面初始化

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmTeamShowEdit_Replace()
        {
            InitializeComponent();
            this.TableName = "Attend_Move";
        }

       

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                isLoad = false;
                //向别-班别
                string strSql = string.Format(@"Select distinct myteamName from [V_Produce_Para] where myteamName is not null order by myteamName");
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
                lookmyteamName.Properties.DataSource = dt_temp.DefaultView;
                lookmyteamName.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookmyteamName.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
                //默认选中
                DataRow row = dt_temp.NewRow();
                row[0] = "全部";
                dt_temp.Rows.InsertAt(row, 0);

                if (dt_temp.Rows.Count > 0)
                {
                    lookmyteamName.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookmyteamName.ItemIndex = 0;
                lookmyteamName.Properties.BestFit();

                dtpStartDate.EditValue = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                dtpEndDate.EditValue = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
              
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增调动记录";
                }
                else
                {
                    this.Text = "修改记录";
                    txtUserID.Enabled = false;
                    //取得更新数据
                    GetDataRowValue(dr);
                }

                if (lookType.Text == "替关登记" || lookType.Text == "支援调出")
                {
                    lookmyteamName.Enabled = false;//向别-班别
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
        /// 保存
        /// </summary>
        /// <param name="frmbase"></param>
        protected override void SetSaveDataProc(frmBaseToolEntryXC frmbase)
        {
            try
            {
                base.SetSaveDataProc(frmbase);
                m_dicItemData.Clear();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("新增失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="isClear"></param>
        protected override void SetCancelInit(bool isClear)
        {
            this.Close();
        }
       
        #endregion


        #region 事件处理方法
        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {
                Common.AdoConnect.Connect.CreateSqlTransaction();
                string str_sql = string.Empty;
                DataTable dt_temp = new DataTable();

                //根据向别-班别查出JobForID，ProjectID，LineID，TeamID
                str_sql = string.Format(@"select top 1 JobForID,ProjectID,LineID,TeamID,GuanweiID from V_Produce_Para where myTeamName='{0}' Order by id", lookmyteamName.EditValue.ToString());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    int result = 0;
                    m_dicItemData["userID"] = txtUserID.Text.Trim();
                    m_dicItemData["StrDate"] = dtpStartDate.DateTime.ToString();
                    m_dicItemData["EndDate"] = dtpEndDate.DateTime.ToString();
                    m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                    m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                    m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                    m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                    m_dicItemData["GuanweiID"] = lookGuanwei.EditValue.ToString();
                    m_dicItemData["GuanweiSite"] = lookUpEditSite.EditValue.ToString();//关位位置
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    if (lookType.SelectedIndex == 0)
                    {//替关调整
                        m_dicItemData["PFlag"] = pFlag.pflag6.GetHashCode().ToString();
                        m_dicItemData["GuanweiID"] = lookGuanwei.EditValue.ToString();//替关的关位
                        str_sql = string.Format(@"update Attend_Move set EndDate='{0}' 
                                                where UserID='{1}'  
                                                and EndDate='4000-01-01' ", 
                          m_dicItemData["StrDate"], txtID.EditValue.ToString());
                        result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());
                    }
                    else if (lookType.SelectedIndex == 1)
                    {//支援调出
                        m_dicItemData["PFlag"] = pFlag.pflag4.GetHashCode().ToString();
                        
                   }
                    else if (lookType.SelectedIndex == 2)
                    {//支援调入
                        m_dicItemData["PFlag"] = pFlag.pflag5.GetHashCode().ToString();
                        m_dicItemData["GuanweiID"] = lookGuanwei.EditValue.ToString();//替关的关位
                    }
                    

                    result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                       XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //日志
                       SysParam.m_daoCommon.WriteLog("调动登记", "新增", txtUserID.Text.Trim());
                       DialogResult = DialogResult.OK;
                    }
                }
               
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="RtnValue"></param>
        public override void SetModifyProc(ref int RtnValue)
        {
            base.SetModifyProc(ref RtnValue);
            try
            {
                Common.AdoConnect.Connect.CreateSqlTransaction();
                m_dicPrimarName.Clear();
                m_dicPrimarName["ID"] = txtID.Text.Trim();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();

                string str_sql = string.Empty;
                DataTable dt_temp = new DataTable();

                //根据向别-班别查出JobForID，ProjectID，LineID，TeamID
                str_sql = string.Format(@"select top 1 JobForID,ProjectID,LineID,TeamID,GuanweiID from V_Produce_Para where myTeamName='{0}' Order by id", lookmyteamName.EditValue.ToString());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    m_dicItemData["userID"] = txtUserID.Text.Trim();
                    m_dicItemData["StrDate"] = dtpStartDate.DateTime.ToString();
                    m_dicItemData["EndDate"] = dtpEndDate.DateTime.ToString();
                    m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                    m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                    m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                    m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                    m_dicItemData["GuanweiID"] = lookGuanwei.EditValue.ToString();
                    m_dicItemData["GuanweiSite"] = lookUpEditSite.EditValue.ToString();//关位位置
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();
                    int result = 0;
                    if (lookType.SelectedIndex == 0)
                    {//替关调整
                        m_dicItemData["PFlag"] = pFlag.pflag6.GetHashCode().ToString();
                        str_sql = string.Format(@"update Attend_Move set EndDate='{0}' where UserID='{1}'  and EndDate='4000-01-01' ", m_dicItemData["StrDate"], txtID.EditValue.ToString());
                        result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());
                    }
                    else if (lookType.SelectedIndex == 1)
                    {//支援调出
                        m_dicItemData["PFlag"] = pFlag.pflag4.GetHashCode().ToString();
                    }
                    else if (lookType.SelectedIndex == 2)
                    {//支援调入
                        m_dicItemData["PFlag"] = pFlag.pflag5.GetHashCode().ToString();
                        
                    }

                    result = SysParam.m_daoCommon.SetModifyDataIdentityColumn(this.TableName, m_dicItemData, m_dicPrimarName);
                    if (result > 0)
                    {
                        Common.AdoConnect.Connect.TransactionCommit();
                        XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //日志
                        SysParam.m_daoCommon.WriteLog("调动登记", "修改", txtUserID.Text.Trim());
                        DialogResult = DialogResult.OK;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
                throw ex;
            }
        }


        /// <summary>
        /// 选择人员信息
        /// </summary>
        private void txtID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch frm = new frmUserInfoSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtUserID.Text = frm.SelectRowData["UserID"].ToString();
                txtName.Text = frm.SelectRowData["UserName"].ToString();
                CbxSex.Text = frm.SelectRowData["Sex"].ToString();
                lookDuty.Text = frm.SelectRowData["DutyName"].ToString();
                lookStatus.Text = frm.SelectRowData["user_status"].ToString();
                textmyteamName.Text = frm.SelectRowData["myTeamName"].ToString();
                textGuanwei.Text = frm.SelectRowData["GuanweiName"].ToString();
                textSite.Text = frm.SelectRowData["GuanweiSite"].ToString();
                
            }
        }

        /// <summary>
        /// 根据选择的调用类型，设置页面可用不可用项目
        /// </summary>
        private void lookType_SelectedValueChanged(object sender, EventArgs e)
        {
            groupControl1.Visible = true;
            lookmyteamName.Properties.ReadOnly = false;
            lookGuanwei.Properties.ReadOnly = false;
            lookmyteamName.Text = Common._myTeamName;
            switch (lookType.SelectedIndex)
            {
                case 0://替关登记
                    lookmyteamName.Properties.ReadOnly = true;
                     break;
                case 1://支援调出
                     lookmyteamName.Properties.ReadOnly = true;
                     lookGuanwei.Properties.ReadOnly = true;
                     lookUpEditSite.Properties.ReadOnly = true;
                     break;
                case 2://支援调入
                    break;
               

            }
        }

        /// <summary>
        /// 选择向别-班别加载该向别下的关位
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {
            string str_sql = "";
            if (lookmyteamName.EditValue.ToString() != "-1")
            {
                str_sql = string.Format(@"Select GuanWeiID as id, GuanweiName From V_Produce_Para where myTeamName='{0}' Order by id", lookmyteamName.EditValue.ToString());
            }
            else {
                str_sql = string.Format(@"Select distinct GuanWeiID as id, GuanweiName From V_Produce_Para Order by id");
            }
            
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookGuanwei.Properties.DataSource = dt_temp.DefaultView;
            lookGuanwei.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookGuanwei.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookGuanwei.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookGuanwei.ItemIndex = 0;
            lookGuanwei.Properties.BestFit();
        }
        /// <summary>
        /// 选择关位变更该关位的关位位置
        /// </summary>
        private void lookGuanwei_EditValueChanged(object sender, EventArgs e)
        {
            string str_sql = "";
            if (lookGuanwei.EditValue.ToString() != "-1")
            {
                str_sql = string.Format(@"Select SetNum as ID,SetNum as GuanweiSite  From V_Produce_Para where  myTeamName='{0}' and  GuanWeiID='{1}' Order by GuanWeiID",
                    lookmyteamName.EditValue.ToString(), lookGuanwei.EditValue.ToString());

            }
           
       
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            int SetNum = 0;
            if (dt_temp.Rows.Count > 0)
            {
                SetNum = int.Parse(dt_temp.Rows[0][0].ToString());
            }
            for (int i = 1; i < SetNum; i++)
            {
                DataRow dr = dt_temp.NewRow();
                dr[0] = i;
                dr[1] = i;
                dt_temp.Rows.InsertAt(dr,i-1);
            }
            if (SetNum>0) { 
                DataRow dr = dt_temp.NewRow();
                dr[0] = "99";
                dr[1] = "99";
                dt_temp.Rows.InsertAt(dr, 0);

                lookUpEditSite.Properties.DataSource = dt_temp.DefaultView;
                lookUpEditSite.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookUpEditSite.Properties.DisplayMember = dt_temp.Columns[0].ColumnName;
                lookUpEditSite.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookUpEditSite.ItemIndex = 0;
            lookUpEditSite.Properties.BestFit();
        }


        #endregion

        #region 共同方法

        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        protected override void GetInputCheck(ref bool isSucces)
        {
            base.GetInputCheck(ref isSucces);
            isSucces = false;
            try
            {
                isSucces = false;
                //用户编号检查：未输入
                if (string.IsNullOrEmpty(this.txtUserID.Text))
                {
                   DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户编号不能为空!");
                   return;
               }

                //开始时间：未输入
                if (string.IsNullOrEmpty(dtpStartDate.EditValue.ToString()))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dtpStartDate, "请选择开始日期!");
                    return;
                }
                //结束日期：未输入
                if (string.IsNullOrEmpty(dtpEndDate.EditValue.ToString()))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dtpEndDate, "请选择结束日期!");
                    return;
                }
                //开始时间>结束时间
                if (DateTime.Parse(dtpEndDate.EditValue.ToString()) < DateTime.Parse(dtpStartDate.EditValue.ToString()))
                {
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.dtpEndDate, "结束日期大于或者等于开始日期!");
                    return;
                }

                //重复性检查
                if (this.ScanMode == Common.DataModifyMode.add 
                    && (lookType.Text == "替关调整" || lookType.Text == "支援调出"))
                {
                    string str_sql = "Select  *   from V_Attend_Move_i   where 1=1 and UserID = '"
                        + txtUserID.Text.Trim() + "' ";
                    str_sql += " and (CONVERT(char(10),StrDate,120) between  '" + dtpStartDate.DateTime.ToString("yyyy-MM-dd")
                        + "' and  '" + dtpEndDate.DateTime.ToString("yyyy-MM-dd") + "' ";
                    str_sql += " or CONVERT(char(10),EndDate,120) between  '" + dtpStartDate.DateTime.ToString("yyyy-MM-dd")
                        + "' and  '" + dtpEndDate.DateTime.ToString("yyyy-MM-dd") + "') ";
    

                    DataTable m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (m_tblDataList.Rows.Count > 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户在指定时间段内已经有调动登记信息，人员不能在同一时间段内重复调动!");
                        return;
                    }
                }
                if(lookType.Text=="支援调入")
                {
                     //支援调入时，查看是否有支援调出数据,
                    string str_sql = string.Format(@"select *  FROM V_Attend_Move_i where   UserID='{0}'
                                and MoveStatus ='支援调出' and ('{1}' between StrDate and EndDate
                                or '{2}' between StrDate and EndDate) ",
                                txtUserID.Text.Trim(), dtpStartDate.DateTime.ToString("yyyy-MM-dd"),
                                dtpEndDate.DateTime.ToString("yyyy-MM-dd"));
                    DataTable m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                    //
                    if (m_tblDataList.Rows.Count <= 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "该人员还没有支援调出，不能进行支援调入操作!");
                        return;
                    }
                    else if(this.ScanMode == Common.DataModifyMode.add ) 
                    {
                        //查看是否有支援调入数据
                        str_sql = string.Format(@"select *  FROM V_Attend_Move_i where   UserID='{0}'
                                and MoveStatus ='支援调入' and ('{1}' between StrDate and EndDate
                                or '{2}' between StrDate and EndDate) ",
                                    txtUserID.Text.Trim(), dtpStartDate.DateTime.ToString("yyyy-MM-dd"), dtpEndDate.DateTime.ToString("yyyy-MM-dd"));
                        m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                         if (m_tblDataList.Rows.Count > 0)
                         {
                             DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户在指定时间段内已经有支援调入信息，人员不能在同一时间段内重复调动!");
                             return;
                         }
                    }
                }
                
                isSucces = true;

            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        
        /// <summary>
        /// 获取选中行数据
        /// </summary>
        /// <param name="dr"></param>
        public void GetDataRowValue(DataRow dr)
        {
            txtID.Text = dr["ID"].ToString();
            txtUserID.Text = dr["UserID"].ToString();
            txtName.Text = dr["UserName"].ToString();
            CbxSex.Text = dr["Sex"].ToString();
            lookDuty.Text = dr["DutyName"].ToString();
            lookStatus.Text = dr["StatusNames"].ToString();
            lookType.Text = dr["MoveStatus"].ToString();
            lookmyteamName.EditValue = dr["myteamName"].ToString();
            lookGuanwei.Text = dr["GuanweiName"].ToString();
            dtpStartDate.Text = dr["strDate"].ToString();
            dtpEndDate.Text = dr["EndDate"].ToString();
            
        }
        #endregion

        

       

       


    }
}
