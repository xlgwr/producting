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
using log4net;
/********************************************************************************
** 作者： xxx
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-7-9
** 修改内容：代码规范化

** 描述：
**    主要用于欠勤登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_NoAttendAdd :Framework.Abstract.frmBaseToolEntryXC
    { 
        #region 变量定义
        public DataRow dr;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_NoAttendAdd));
        #endregion

         #region 画面初始化

        public frmTeamShow_NoAttendAdd()
        {
            InitializeComponent();
            this.TableName = "Attend_NoAttend";//操作表名称
        }
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
              if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增欠勤记录";
                }
                else
                {
                    this.Text = "修改欠勤记录";
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
       
        #region 事件处理方法
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
                m_dicItemData["SetDate"] = dateSetDate.EditValue.ToString();
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();
                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("欠勤登记", "新增", txtUserID.Text.Trim() + " :" + dateSetDate.EditValue.ToString());
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
                m_dicPrimarName["ID"] = txtID.Text.Trim();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["SetDate"] = dateSetDate.EditValue.ToString();
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();


                int result = SysParam.m_daoCommon.SetModifyDataIdentityColumn(this.TableName, m_dicItemData, m_dicPrimarName);
                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog( "欠勤登记", "修改", txtUserID.Text.Trim() + " 在" + dateSetDate.EditValue.ToString() + "记录");
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
                    if (string.IsNullOrEmpty(this.txtUserID.Text) || this.txtUserID.Text.Trim()=="")
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户编号不能为空!");
                        return;
                    }

                    //日期检查：未输入
                    if (string.IsNullOrEmpty(dateSetDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateSetDate, "请选择欠勤日期!");
                        return;
                    }
                    

                    //已经存在这个人的欠勤信息
                    string str_sql = "select * from V_Attend_NoAttend_i as a  where 1=1 ";
                    str_sql += " AND myUserID='" + txtUserID.Text.Trim() + "' ";//用户编号
                    str_sql += " AND '" + DateTime.Parse(dateSetDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm") + "'=  CONVERT(varchar(10),NoAttendDate,120)  ";//欠勤日期
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateSetDate, "已经存在该人员" + dateSetDate.EditValue.ToString() + "的 欠勤信息！");
                        return;
                    }

                    //请假时间不在排班的上班范围内
                    str_sql = "select * from V_Attend_TeamSet_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + txtMyTeamName.Text.Trim() + "' ";//向别-班别
                    str_sql += " AND '" + DateTime.Parse(dateSetDate.EditValue.ToString()).ToString("yyyy-MM-dd hh:mm") 
                        + "' between CONVERT(varchar(10),BgnDate,120) and CONVERT(varchar(10),EndDate,120) ";//开始时间
                   
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count <= 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateSetDate, "该人员在此时间段内不上班，不需要登记欠勤信息！");
                        return;
                    }
                    //已经存在了请假信息
                    str_sql = "select * from V_Attend_Vacation_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + txtMyTeamName.Text.Trim() + "' ";//向别-班别
                    str_sql += " AND myUserID='" + txtUserID.Text.Trim() + "' ";//用户编号
                    str_sql += " AND '" + DateTime.Parse(dateSetDate.EditValue.ToString()).ToString("yyyy-MM-dd hh")
                        + "'  between CONVERT(varchar(10),VacationBgnDate,120) and CONVERT(varchar(10),VacationEndDate,120) ";//开始时间
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateSetDate, "该人员在此时间段内有请假记录！");
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
            txtMyTeamName.EditValue = this.dr["myTeamName"].ToString();
            //职等
            txtDutyName.EditValue = this.dr["DutyName"].ToString();
            //关位
            txtGuanwei.EditValue = this.dr["GuanweiName"].ToString();
 
            //时间
            dateSetDate.EditValue = this.dr["NoAttendDate"].ToString();
            

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
