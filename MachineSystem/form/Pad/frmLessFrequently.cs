using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using log4net;
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
    ** 描述：欠勤登记
    *********************************************************************************/
    public partial class frmLessFrequently : Framework.Abstract.frmBaseXC
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
        public frmLessFrequently()
        {
            InitializeComponent();
            SetFormValue();
            this.TopMost = true;
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
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                 dtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {

                 FrmAttendDialog FrmDialog = new FrmAttendDialog( "画面初始化失败！"+ex);
                 FrmDialog.ShowDialog();
            }
        }

        private void frmLessFrequently_Load(object sender, EventArgs e)
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

        #endregion

        #region 画面按钮功能处理方法


        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存欠勤登记
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //日期检查：未输入
                if (string.IsNullOrEmpty(dtStartTime.EditValue.ToString()))
                {
                     FrmAttendDialog FrmDialog = new FrmAttendDialog( "请选择欠勤日期!");
                     FrmDialog.ShowDialog();
                    return;
                }


                int result = 0;
                int count = 0;
                Common.AdoConnect.Connect.CreateSqlTransaction();

                //DataTable dt_temp = ((DataView)this.gridView1.DataSource).Table.Copy();
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {

                    //已经存在这个人的欠勤信息
                    string str_sql = "select * from V_Attend_NoAttend_i as a  where 1=1 ";
                    str_sql += " AND myUserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "' ";//用户编号
                    str_sql += " AND '" + DateTime.Parse(dtStartTime.EditValue.ToString()).ToString("yyyy-MM-dd") + "'=  CONVERT(varchar(10),NoAttendDate,120)  ";//欠勤日期
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                        FrmAttendDialog FrmDialog = new FrmAttendDialog(
                             m_tblDataList.Rows[i]["UserID"].ToString() + ":" + m_tblDataList.Rows[i]["UserNM"].ToString()
                            + "\n在【" + dtStartTime.EditValue.ToString() + "】这一天已经登记了欠勤信息！");
                        FrmDialog.ShowDialog();
                        return;
                    }

                    //请假时间不在排班的上班范围内
                    str_sql = "select * from V_Attend_TeamSet_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + m_tblDataList.Rows[i]["myTeamName"].ToString() + "' ";//向别-班别
                    str_sql += " AND '" + DateTime.Parse(dtStartTime.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "' between CONVERT(varchar(10),BgnDate,120) and CONVERT(varchar(10),EndDate,120) ";//开始时间

                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count <= 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                         FrmAttendDialog FrmDialog = new FrmAttendDialog(
                            m_tblDataList.Rows[i]["UserID"].ToString() + ":" + m_tblDataList.Rows[i]["UserNM"].ToString()
                            + "\n在【" + dtStartTime.EditValue.ToString() + "】这一天不上班，\n不需要登记欠勤信息！");
                         FrmDialog.ShowDialog();
                        return;
                    }
                    //已经存在了请假信息
                    str_sql = "select * from V_Attend_Vacation_i as a  where 1=1 ";
                    str_sql += " AND myTeamName='" + m_tblDataList.Rows[i]["myTeamName"].ToString() + "' ";//向别-班别
                    str_sql += " AND myUserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "' ";//用户编号
                    str_sql += " AND '" + DateTime.Parse(dtStartTime.EditValue.ToString()).ToString("yyyy-MM-dd")
                        + "'  between CONVERT(varchar(10),VacationBgnDate,120) and CONVERT(varchar(10),VacationEndDate,120) ";//开始时间
                    if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    {
                        Common.AdoConnect.Connect.TransactionRollback();
                         FrmAttendDialog FrmDialog = new FrmAttendDialog( 
                            m_tblDataList.Rows[i]["UserID"].ToString() + ":" + m_tblDataList.Rows[i]["UserNM"].ToString()
                            + "\n在【" + dtStartTime.EditValue.ToString() + "】有请假记录，\n不能登记欠勤信息！");
                         FrmDialog.ShowDialog();
                        return;
                    }

                    m_dicItemData.Clear();
                    m_dicItemData["UserID"] = m_tblDataList.Rows[i]["UserID"].ToString();
                    m_dicItemData["SetDate"] = dtStartTime.Text.Trim();
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    result = SysParam.m_daoCommon.SetInsertDataItem("Attend_NoAttend", m_dicItemData);
                    if (result > 0) count++;
                    //日志
                    SysParam.m_daoCommon.WriteLog("欠勤登", "新增", m_tblDataList.Rows[i]["UserID"].ToString());
                }
                if (m_tblDataList.Rows.Count == count)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                     FrmAttendDialog FrmDialog = new FrmAttendDialog( "保存数据成功！");
                     FrmDialog.ShowDialog();
                    
                    this.Close();
                }
                else
                {
                    Common.AdoConnect.Connect.TransactionRollback();
                     FrmAttendDialog FrmDialog = new FrmAttendDialog( "保存数据失败！");
                     FrmDialog.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);

                 FrmAttendDialog FrmDialog = new FrmAttendDialog( "保存数据失败！"+ex);
                 FrmDialog.ShowDialog();
            }

        }
        #endregion

        #region 事件处理方法

        private void dtStartTime_Click(object sender, EventArgs e)
        {
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtStartTime.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd");
            }
        }
        #endregion

        private void dtStartTime_Click_2(object sender, EventArgs e)
        {
            dtStartTime.ShowPopup();
        }

        #region 共同方法


        #endregion

       

    }
}
