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
**    主要用于离职登记信息的资料资料新增、修改操作
*********************************************************************************/
namespace MachineSystem.TabPage
{
    public partial class frmTeamShow_LeaveAdd :Framework.Abstract.frmBaseToolEntryXC
    {

        #region 变量定义
        public DataRow dr;
        // 日志
        private static readonly ILog log = LogManager.GetLogger(typeof(frmTeamShow_LeaveAdd));
        #endregion

        #region 画面初始化
        public frmTeamShow_LeaveAdd()
        {
            InitializeComponent();
            this.TableName = "Attend_Leave";//操作表名称
        }
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                //离职类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.Attend_LeaveType, this.lookUpLeaveType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                //离职原因
                SysParam.m_daoCommon.SetLoopUpEdit("Attend_LeaveReason", this.lookUpLeaveReason, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefaultPleaseSelect);
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dateSetDate.EditValue = string.Format("{0:yyyy-MM-dd}", FirstDay);
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    this.Text = "新增离职记录";
                }
                else
                {
                    this.Text = "修改离职记录";
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
                this.txtStatus.EditValue = _frm.SelectRowData["StatusNames"].ToString();//用户状态
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
                m_dicItemData["CallDate"] = dateSetDate.EditValue.ToString();//离职提出日期
                m_dicItemData["SetDate"] = dateLastDate.EditValue.ToString();//最后上班日期
                m_dicItemData["LeaveTypeID"] = lookUpLeaveType.EditValue.ToString();//离职类型
                m_dicItemData["ReasonID"] = lookUpLeaveReason.EditValue.ToString();//离职原因
                m_dicItemData["Memo"] = txtMemo.Text.Trim();//离职说明
                m_dicItemData["Flag"] = "0";//
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();


                int result = SysParam.m_daoCommon.SetInsertDataItem(this.TableName, m_dicItemData);
                if (result > 0)
                {
                    XtraMsgBox.Show("新增数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog("离职登记", "新增", txtUserID.Text.Trim() + " :" + dateSetDate.EditValue.ToString());
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
            base.SetModifyProc(ref RtnValue);
            try
            {
                m_dicPrimarName.Clear();
                //m_dicPrimarName["ID"] = txtID.Text.Trim();
                m_dicPrimarName["UserID"] = txtUserID.Text.Trim();
                m_dicItemData = new System.Collections.Specialized.StringDictionary();
                m_dicItemData["UserID"] = txtUserID.Text.Trim();
                m_dicItemData["CallDate"] = dateSetDate.EditValue.ToString();//离职提出日期
                m_dicItemData["SetDate"] = dateLastDate.EditValue.ToString();//最后上班日期
                m_dicItemData["LeaveTypeID"] = lookUpLeaveType.EditValue.ToString();//离职类型
                m_dicItemData["ReasonID"] = lookUpLeaveReason.EditValue.ToString();//离职原因
                m_dicItemData["Memo"] = txtMemo.Text.Trim();//离职说明
                m_dicItemData["OperID"] = Common._personid;
                m_dicItemData["OperDate"] = System.DateTime.Now.ToString();

                int result = SysParam.m_daoCommon.SetModifyDataItem(this.TableName, m_dicItemData, m_dicPrimarName);
                if (result > 0)
                {
                    XtraMsgBox.Show("修改数据成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //日志
                    SysParam.m_daoCommon.WriteLog( "离职登记", "修改", txtUserID.Text.Trim() + " 在" + dateSetDate.EditValue.ToString() + "提出离职");
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

                    //离职提出时间：未输入
                    if (string.IsNullOrEmpty(dateSetDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateSetDate, "请选择离职提出日期!");
                        return;
                    }
                    //最后上班日期：未输入
                    if (string.IsNullOrEmpty(dateLastDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateLastDate, "请选择最后上班日期!");
                        return;
                    }
                    //离职最后上班时间>离职提出时间
                    if (DateTime.Parse(dateLastDate.EditValue.ToString()) <  DateTime.Parse(dateSetDate.EditValue.ToString()))
                    {
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.dateLastDate, "最后上班日期须大于离职提出日期!");
                        return;
                    }
                    string str_sql = "Select  *   from V_Attend_Leave   where 1=1 and myUserID like '%" 
                        + txtUserID.Text.Trim() + "%' ";
                    DataTable m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                     if (m_tblDataList.Rows.Count > 0)
                     {
                         DataValid.ShowErrorInfo(this.ErrorInfo, this.txtUserID, "用户的离职登记信息已经存在，您可以修改用户离职登记信息!");
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
            txtSex.EditValue = this.dr["Sex"].ToString();
            ////状态
            txtStatus.EditValue = this.dr["User_Status"].ToString();
            //向别-班别
            txtMyTeamName.EditValue = this.dr["myTeamName"].ToString();
            //职等
            txtDutyName.EditValue = this.dr["DutyName"].ToString();
            //关位
            txtGuanwei.EditValue = this.dr["GuanweiName"].ToString();
            //离职类型
            lookUpLeaveType.Text = this.dr["LeaveTypeName"].ToString();
            //离职原因
            lookUpLeaveReason.Text = this.dr["LeaveReasonName"].ToString();
            //离职说明
            txtMemo.EditValue = this.dr["Memo"].ToString();
            //提出时间
            dateSetDate.EditValue = this.dr["CallDate"].ToString();
            //最后上班日
            dateLastDate.EditValue = this.dr["SetDate"].ToString();
  
        }


        #endregion

    }
}
