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
using MachineSystem.TabPage;
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于请假登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem
{
    public partial class frmTeamShow_VacationAdd  :  Framework.Abstract.frmBaseToolEntryXC
    {

        #region 变量定义
        public DataRow dr;
        private DateTime dtBegin;
        private DateTime dtEnd;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_VacationAdd));
        #endregion


        #region 画面初始化

        public frmTeamShow_VacationAdd()
        {
            InitializeComponent();
            this.TableName = "Attend_Vacation";//操作表名称
        }
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //请假类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Attend_VacationType, this.lookVacationType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);

                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增请假记录";
                }
                else
                {
                    this.Text = "修改请假记录";
                    //取得更新数据
                    GetDataRowValue(dr);
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
        /// 根据工号，查询用户其他信息
        /// </summary>
        private void txtUserID_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            frmUserInfoSearch _frm = new frmUserInfoSearch();
            if (_frm.ShowDialog() == DialogResult.OK)
            {
                this.txtUserID.EditValue = _frm.SelectRowData["UserID"].ToString();//用户编号
                this.txtUserName.EditValue = _frm.SelectRowData["UserName"].ToString();//用户姓名
                this.txtSex.EditValue = _frm.SelectRowData["Sex"].ToString();//用户性别
                this.txtDutyName.EditValue = _frm.SelectRowData["DutyName"].ToString();//用户职等
                this.txtStatus.EditValue = _frm.SelectRowData["User_Status"].ToString();//用户状态
                this.txtGuanwei.EditValue = _frm.SelectRowData["GuanweiName"].ToString();//用户关位
                this.txtMyTeamName.EditValue = _frm.SelectRowData["myTeamName"].ToString();//向别-班别
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        protected override void SetInsertProc(ref int RtnValue)
        {
            base.SetInsertProc(ref RtnValue);
            try
            {
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
           
                m_dicItemData["UserID"] = txtUserID.Text.Trim();
                m_dicItemData["VacationType"] = this.lookVacationType.EditValue.ToString();
                m_dicItemData["Memo"] = textVacationMemo.Text.Trim();// 请假事由
                m_dicItemData["BgnDate"] = DateTime.Parse(dateVacationBgnDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                m_dicItemData["EndDate"] = DateTime.Parse(dateVacationEndDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();
                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("请假登记", "新增", txtUserID.Text.Trim() + "：" + dateVacationBgnDate.EditValue.ToString() );
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
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
            DateTime dtBegin = DateTime.Now;
            DateTime dtEnd = DateTime.Now;

            base.SetModifyProc(ref RtnValue);
            try
            {
                m_dicPrimarName.Clear();
                m_dicPrimarName["myID"] = txtID.Text.Trim();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["UserID"] = txtUserID.Text.Trim();
                m_dicItemData["VacationType"] = this.lookVacationType.EditValue.ToString();
                m_dicItemData["Memo"] = textVacationMemo.Text.Trim();// 请假事由
                m_dicItemData["BgnDate"] = DateTime.Parse(dateVacationBgnDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                m_dicItemData["EndDate"] = DateTime.Parse(dateVacationEndDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();

                int result = SysParam.m_daoCommon.SetModifyDataItem(this.TableName, m_dicItemData, m_dicPrimarName);
                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("请假登记", "修改", txtUserID.Text.Trim() + " 在" + dateVacationBgnDate.EditValue.ToString() + "记录");
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
        }

        #endregion

       
        #region 共同方法
        

       
        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        protected override void GetInputCheck(ref bool isSucces)
        {
            try
            {
                base.GetInputCheck(ref isSucces);
                if (isSucces)
                {
                    isSucces = false;
                    //用户编号检查：未输入
                    if (string.IsNullOrEmpty(this.txtUserID.Text))
                    {
                        
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户编号不能为空!");

                    }
                   
                    //请假类型检查：未输入
                    if (lookVacationType.EditValue.ToString().Equals("-1"))
                    {

                        DataValid.ShowErrorInfo(this.ErrorInfo, this.lookVacationType, "请假类型必须选择!");
                        return;
                    }
                    //请假开始时间检查：未输入
                    if (string.IsNullOrEmpty(dateVacationBgnDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationBgnDate, "请选择请假开始时间!");
                        return;
                    }
                    //请假结束时间检查：未输入
                    if (string.IsNullOrEmpty(dateVacationEndDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationEndDate, "请选择请假结束时间!");
                        return;
                    }

                    dtBegin = DateTime.Parse(dateVacationBgnDate.EditValue.ToString());
                    dtEnd = DateTime.Parse(dateVacationEndDate.EditValue.ToString());
                    //请假结束时间>请假开始时间
                    if (dtEnd < dtBegin)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationEndDate, "结束时间须大于开始时间!");
                        return;
                    }
                    //已经存在这个人的请假信息
                    string str_sql = "select * from V_Attend_Vacation_i as a  where 1=1 ";
                    str_sql += " AND myUserID='" + txtUserID.Text.Trim() + "' ";//用户编号
                    str_sql += " AND ('" + DateTime.Parse(dateVacationBgnDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "' between VacationBgnDate and VacationEndDate ";//请假开始时间
                    str_sql += " OR '" + DateTime.Parse(dateVacationEndDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'  between VacationBgnDate and VacationEndDate)";//请假结束时间
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0 && this.ScanMode == Common.DataModifyMode.add)
                    {
                         DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationEndDate, "已经存在该人员的请假信息！");
                        return;
                    }
                   
                
                    //请假时间不在排班的上班范围内
                    str_sql = "select * from V_Attend_TeamSet_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + txtMyTeamName.Text.Trim() + "' ";//向别-班别
                    str_sql += " AND ('" + DateTime.Parse(dateVacationBgnDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "' between BgnDate and EndDate ";//请假开始时间
                    str_sql += " OR '" + DateTime.Parse(dateVacationEndDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "'  between BgnDate and EndDate)";//请假结束时间
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count <= 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationEndDate, "该人员在此时间段内不上班，不需要请假！");
                        return;
                    }
                    //已经存在了欠勤信息
                    str_sql = "select * from V_Attend_NoAttend_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + txtMyTeamName.Text.Trim() + "' ";//向别-班别
                    str_sql += " AND (NoAttendDate between  '" + DateTime.Parse(dateVacationBgnDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "' and  ";//请假开始时间
                    str_sql += "  '" + DateTime.Parse(dateVacationEndDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "')";//请假结束时间
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateVacationEndDate, "该人员在此时间段内有欠勤记录！");
                        return;
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
        //修改数据取得
        public void GetDataRowValue(DataRow dr)
        {
            //id
            txtID.EditValue = this.dr["myID"].ToString();
            //工号
            txtUserID.EditValue = this.dr["myUserID"].ToString();
            //姓名
            txtUserName.EditValue = this.dr["UserName"].ToString();
            ////性别
            txtSex.EditValue = "";
            ////状态
            txtStatus.EditValue = "";
            //班别
            txtMyTeamName.EditValue = this.dr["orgTeamName"].ToString();
            //职等
            txtDutyName.EditValue = this.dr["DutyName"].ToString();
            //关位
            txtGuanwei.EditValue = this.dr["GuanweiName"].ToString();
            //请假类型
            lookVacationType.Text = this.dr["VacationName"].ToString();

            //请假事由
            textVacationMemo.EditValue = this.dr["VacationMemo"].ToString();
            //开始时间
            dateVacationBgnDate.EditValue = this.dr["VacationBgnDate"].ToString();
            //结束时间
            dateVacationEndDate.EditValue = this.dr["VacationEndDate"].ToString();

            try
            {
                if (this.dr != null)
                {
                    string _sql = "select * from V_Produce_User as a  where a.UserID='";
                    _sql += this.dr["myUserID"].ToString() + "'";
                    DataTable _dt = Common.AdoConnect.Connect.GetDataSet(_sql);
                    if (_dt.Rows.Count > 0)
                    {
                        ////性别
                        txtSex.EditValue = _dt.Rows[0]["Sex"].ToString();
                        ////状态
                        txtStatus.EditValue = _dt.Rows[0]["User_Status"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

        }


        #endregion

    }
}
