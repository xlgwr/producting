using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using Framework.Abstract;
using Framework.Libs;
using MachineSystem.SysDefine;
using MachineSystem.form.Search;
using MachineSystem.SysCommon;
using MachineSystem.UserControls;

namespace MachineSystem.form.Pad
{
    /********************************************************************************	
    ** 作者： libing   	
    ** 创始时间：2015-07-14	
    ** 修改人：libing	
    ** 修改时间：
    ** 修改内容：
    ** 描述：离职登记
    *********************************************************************************/
    public partial class frmTurnover : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        /// <summary>
        /// 人员数据表
        /// </summary>
        public DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 选中时间
        /// </summary>
        public DateTime m_DataTime;
        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLessFrequently));

        #endregion


        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmTurnover()
        {
            InitializeComponent();
            SetFormValue();
            this.TopMost = true;
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                dtCallDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtSetDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtMemo.Properties.ReadOnly = false;
                txtMemo.Enabled = true;
                //离职类型
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.Attend_LeaveType, this.cboLeaveType, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
                //离职原因
                SysParam.m_daoCommon.SetLoopUpEdit("Attend_LeaveReason", this.comboBoxEditReason, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
            }
            catch (Exception ex)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面初始化失败！" + ex);
                FrmDialog.ShowDialog();
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED  
                this.Opacity = 1;

                //if (this.IsXpOr2003 == true)
                //{
                //    cp.ExStyle |= 0x00080000;  // Turn on WS_EX_LAYERED
                //    this.Opacity = 1;
                //}

                return cp;

            }

        }  //防止闪烁
        private void frmTurnover_Load(object sender, EventArgs e)
        {
            try
            {


                //循环把人员信息放入panel
                panelContent.Controls.Clear();
                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_Top = 0;//上边距
                int lint_TopCount = 0;//当前Top有几行
                int lint_Person = 0;//当前有多少人员 

                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    //人员信息
                    UserPerson m_Person = new UserPerson();
                    m_Person.Name = ("person" + i).ToString();
                    m_Person.TitleName = m_tblDataList.Rows[i]["GuanweiNM"].ToString() + " - " + m_tblDataList.Rows[i]["GuanweiSite"].ToString();
                    if (m_tblDataList.Rows[i]["AttendType"].ToString() == "支援")
                    {
                        m_Person.TitleName = "(支)" + m_tblDataList.Rows[i]["GuanweiNM"].ToString() + " - " + m_tblDataList.Rows[i]["GuanweiSite"].ToString();
                    }
                    if (m_tblDataList.Rows[i]["AttendType"].ToString() == "替关")
                    {
                        m_Person.TitleName = "(替)" + m_tblDataList.Rows[i]["GuanweiNM"].ToString() + " - " + m_tblDataList.Rows[i]["GuanweiSite"].ToString();
                    }
                    m_Person.GuanweiID = m_tblDataList.Rows[i]["GuanweiID"].ToString();
                    m_Person.GuanweiColor = m_tblDataList.Rows[i]["GuanweiColor"].ToString();
                    m_Person.Status = m_tblDataList.Rows[i]["StatusName"].ToString();
                    m_Person.StatusColor = m_tblDataList.Rows[i]["StatusColor"].ToString();
                    m_Person.Time = m_tblDataList.Rows[i]["CardTime"].ToString();
                    m_Person.TimeColor = m_tblDataList.Rows[i]["CardTimeColor"].ToString();
                    m_Person.Remind = m_tblDataList.Rows[i]["warnMemo"].ToString();
                    m_Person.RemindColor = m_tblDataList.Rows[i]["warnColor"].ToString();
                    m_Person.LicenseType = m_tblDataList.Rows[i]["LicenseType"].ToString();
                    m_Person.LicenseColor = m_tblDataList.Rows[i]["LicenseColor"].ToString();
                    m_Person.UserID = m_tblDataList.Rows[i]["UserID"].ToString();
                    m_Person.UserName = m_tblDataList.Rows[i]["UserNM"].ToString();
                    m_Person.UserIdNmColor = m_tblDataList.Rows[i]["UserIdColor"].ToString();
                    m_Person.Tag = "0";
                    //m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                    //m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                    m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, m_tblDataList.Rows[i]["UserID"].ToString());
                    panelContent.Controls.Add(m_Person);
                    lint_Person++;

                    //Top
                    lint_Top = lint_TopCount * (m_Person.Height + 10);

                    _point = new Point(lint_Left + 10, lint_Top);
                    m_Person.Location = _point;
                    lint_Left += m_Person.Width + 10;

                    if (lint_Person % 10 == 0)
                    {//如果画的数量大于十个，则换行
                        lint_Left = 0;
                        lint_TopCount++;
                    }
                }
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面加载失败！" + ex);
                FrmDialog.ShowDialog();
            }
        }

        #endregion

        #region 画面按钮功能处理方法


        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存离职登记
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboLeaveType.EditValue.ToString()=="-1")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择离职类型！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (comboBoxEditReason.EditValue.ToString() == "-1")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择离职原因！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (dtSetDate.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择最后上班日！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (dtCallDate.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择离职提出日！");
                    FrmDialog.ShowDialog();
                    return;
                }
                int result = 0;
                int count = 0;
                m_dicItemData.Clear();
                Common.AdoConnect.Connect.CreateSqlTransaction();

                //DataTable dt_temp = ((DataView)this.gridView1.DataSource).Table.Copy();
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    //已经存在这个人的欠勤信息
                    string str_sql = "select * from V_Attend_Leave as a  where 1=1 ";
                    str_sql += " AND UserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "' ";//用户编号
                    //str_sql += " AND CONVERT(varchar(10),SetDate,120) = '" + dtSetDate.Text.ToString() + "'";//最后上班日期
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        FrmAttendDialog FrmDialog = new FrmAttendDialog(
                            m_tblDataList.Rows[i]["UserID"].ToString() + ":" + m_tblDataList.Rows[i]["UserNM"].ToString()
                            + ",\n已经登记了离职信息！");
                       
                        FrmDialog.ShowDialog();
                        Common.AdoConnect.Connect.TransactionRollback();
                        return;
                    }

                    m_dicItemData.Clear();
                    m_dicItemData["UserID"] = m_tblDataList.Rows[i]["UserID"].ToString();
                    m_dicItemData["CallDate"] = dtCallDate.Text.Trim();//离职提出日
                    m_dicItemData["SetDate"] = dtSetDate.Text.Trim();//最后上班日
                    m_dicItemData["Flag"] = "0";
                    m_dicItemData["Memo"] = txtMemo.Text.Trim();//离职说明
                    m_dicItemData["LeaveTypeID"] = cboLeaveType.EditValue.ToString();//离职类型
                    m_dicItemData["ReasonID"] = comboBoxEditReason.EditValue.ToString();//离职原因
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Leave", m_dicItemData);
                    if (result > 0) count++;
                    //日志
                    SysParam.m_daoCommon.WriteLog("离职登记", "新增", m_tblDataList.Rows[i]["UserNM"].ToString() + ":" + dtCallDate.Text.Trim());
                }
                if (m_tblDataList.Rows.Count == count)
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog(count+"人的离职登记信息，成功提交！");                   
                    FrmDialog.ShowDialog();
                    Common.AdoConnect.Connect.TransactionCommit();
                    this.Close();
                }
                else
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("离职登记提交失败！");
                   
                    FrmDialog.ShowDialog();
                    Common.AdoConnect.Connect.TransactionRollback();
                }

            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("离职登记提交失败！");
                FrmDialog.ShowDialog();
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);

            }
        }
        #endregion

        #region 事件处理方法


        #endregion

        #region 共同方法

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {

        }

        #endregion

        private void dtCallDate_Click(object sender, EventArgs e)
        {
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtSetDate.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd");
            }
        }

        private void dtSetDate_Click(object sender, EventArgs e)
        {
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtCallDate.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd");
            }
        }

    }
}
