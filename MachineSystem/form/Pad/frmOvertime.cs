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
using DevExpress.XtraEditors;
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
    ** 描述：加班登记
    *********************************************************************************/
    public partial class frmOvertime : Framework.Abstract.frmBaseXC
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


        /// <summary>
        /// 申请事由
        /// </summary>
        public DataTable m_tblOTApply = new DataTable();
        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmOvertime()
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
                //申请事由
                string str = string.Format(@"select * from P_OTApply ");
                m_tblOTApply = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                lookUpEditOTApply.Properties.DataSource = m_tblOTApply;
                lookUpEditOTApply.Properties.ValueMember = "ID";
                lookUpEditOTApply.Properties.DisplayMember = "OTApply";
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_OTType, lookOTKind, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);

                dtStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");// HH:mm
                dtEndTime2.Text = DateTime.Now.ToString("HH:mm");//yyyy-MM-dd
                dtEndTime.Text = DateTime.Now.ToString("HH:mm");//yyyy-MM-dd
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面初始化失败！");
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
        private void frmOvertime_Load(object sender, EventArgs e)
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

                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面加载失败！");
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
        /// 保存加班登记
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (lookOTKind.EditValue.ToString() == "-1")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择加班类型！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (lookUpEditOTApply.EditValue.ToString() == "-1")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择申请事由！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (dtStartTime.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择加班开始日期！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (dtEndTime.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择开始时间！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (dtEndTime2.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择结束时间！");
                    FrmDialog.ShowDialog();
                    return;
                }
                if (txtNum.Text.Trim() == "")
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择加班时数！");
                    FrmDialog.ShowDialog();
                    return;
                }
                //if (DateTime.Parse(dtEndTime.Text.Trim()) >= DateTime.Parse(dtEndTime2.Text.Trim()))
                //{
                //    FrmAttendDialog FrmDialog = new FrmAttendDialog("开始时间不能大于等于结束时间！");
                //    FrmDialog.ShowDialog();
                //    return;
                //}
                //int s = Convert.ToDateTime(dtEndTime.Text).Hour;
                //int n = Convert.ToDateTime(dtStartTime.Text).Hour;
                // double time = Convert.ToDateTime(dtEndTime.Text).Hour - Convert.ToDateTime(dtStartTime.Text).Hour;
                //if (double.Parse(txtNum.Text.Trim()) > time)
                //{
                //    FrmAttendDialog FrmDialog = new FrmAttendDialog("加班时数大于输入时间段!");
                //    FrmDialog.ShowDialog();
                //    return;
                //}

                int result = 0;
                int count = 0;
                m_dicItemData.Clear();
                Common.AdoConnect.Connect.CreateSqlTransaction();

                //DataTable dt_temp = ((DataView)this.gridView1.DataSource).Table.Copy();
                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {
                    #region remove  by xlg, 2015-12-04
                    ////已经存在这个人的加班信息
                    //string str_sql = "select a.myUserID  from V_Attend_OT_Set as a  where 1=1 ";
                    //str_sql += " AND myUserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "' ";//用户编号


                    // 赵杰 2015-12-02 删除开始
                    // 判断时间包含逻辑错误
                    //str_sql += " AND (OTStrDateSel between '" + dtStartTime.Text.ToString()
                    //    + "' and  '" + dtEndTime.Text.ToString();//加班日期
                    //str_sql += "' or OTEndTimeSel between '" + dtStartTime.Text.ToString()
                    //   + "' and  '" + dtEndTime.Text.ToString()+ "')";//加班日期
                    // 赵杰 2015-12-02 删除结束

                    //// 赵杰 2015-12-02 新增开始
                    //str_sql += " AND ((OTStrDateSel <= '" + dtStartTime.Text.ToString() + "' and OTEndTimeSel >= '" + dtStartTime.Text.ToString() + "') or "
                    //    + "(OTStrDateSel <= '" + dtEndTime.Text.ToString() + "' and OTEndTimeSel >= '" + dtEndTime.Text.ToString() + "'))";
                    //// 赵杰 2015-12-02 新增结束


                    //str_sql += " AND OtDate='" + dtStartTime.Text.ToString() + "' ";//加班日期

                    //if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    //{
                    //    Common.AdoConnect.Connect.TransactionRollback();
                    //    FrmAttendDialog FrmDialog = new FrmAttendDialog(m_tblDataList.Rows[i]["UserNM"].ToString() + "\n已经存在" +
                    //       dtStartTime.Text.ToString() + "的 加班信息,\n不需要登记加班信息！");
                    //    FrmDialog.ShowDialog();
                    //    return;
                    //}
                    ////已经存在这个人的欠勤信息
                    //str_sql = "select * from V_Attend_NoAttend_i as a  where 1=1 ";
                    //str_sql += " AND myUserID='" + m_tblDataList.Rows[i]["UserID"].ToString() + "' ";//用户编号
                    //str_sql += " AND CONVERT(varchar(10),NoAttendDate,120) between '" + dtStartTime.Text.ToString()
                    //    + "' and  '" + dtEndTime.Text.Trim() + "'";//欠勤日期
                    //if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    //{
                    //    Common.AdoConnect.Connect.TransactionRollback();
                    //    FrmAttendDialog FrmDialog = new FrmAttendDialog(m_tblDataList.Rows[i]["UserNM"].ToString() + "\n已经存在" +
                    //        dtStartTime.Text.ToString() + "至" + dtEndTime.Text.Trim() + "的时间内有欠勤信息,\n不能登记加班信息！");
                    //    FrmDialog.ShowDialog();
                    //    return;
                    //}

                    ////是否存在这个人的请假信息 2015-11-23
                    //str_sql = string.Format(@"select * from Attend_Vacation where UserID='{0}'  AND ((BgnDate between '{1}' and  '{2}')  or(EndDate between '{3}' and  '{4}')) ",
                    //                         m_tblDataList.Rows[i]["UserID"].ToString(), dtStartTime.Text.ToString(), dtEndTime.Text.Trim(), dtStartTime.Text.ToString(), dtEndTime.Text.Trim());
                    //if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql).Rows.Count > 0)
                    //{
                    //    Common.AdoConnect.Connect.TransactionRollback();
                    //    FrmAttendDialog FrmDialog = new FrmAttendDialog(m_tblDataList.Rows[i]["UserNM"].ToString() + "\n已经存在" +
                    //        dtStartTime.Text.ToString() + "至" + dtEndTime.Text.Trim() + "的时间内有请假信息,\n不能登记加班信息！");
                    //    FrmDialog.ShowDialog();
                    //    return;
                    //}
                    #endregion


                    m_dicItemData.Clear();
                    m_dicItemData["UserID"] = m_tblDataList.Rows[i]["UserID"].ToString();
                    m_dicItemData["OtDate"] = dtStartTime.Text.Trim();
                    m_dicItemData["StartTime"] = dtEndTime.Text.Trim();
                    m_dicItemData["EndTime"] = dtEndTime2.Text.Trim();
                    m_dicItemData["OTNum"] = txtNum.Text.Trim();
                    m_dicItemData["OTApplyID"] = lookUpEditOTApply.EditValue.ToString();
                    m_dicItemData["OTType"] = lookOTKind.EditValue.ToString();
                    m_dicItemData["OperID"] = Common._personid;
                    m_dicItemData["OperDate"] = DateTime.Now.ToString();

                    result = SysParam.m_daoCommon.SetInsertDataItem("Attend_OT_Set", m_dicItemData);

                    if (result > 0) count++;
                    //日志
                    SysParam.m_daoCommon.WriteLog("加班登记", "新增", m_tblDataList.Rows[i]["UserID"].ToString());
                }
                if (m_tblDataList.Rows.Count == count)
                {
                    Common.AdoConnect.Connect.TransactionCommit();
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("保存数据成功！");
                    FrmDialog.ShowDialog();
                    this.Close();
                }
                else
                {
                    Common.AdoConnect.Connect.TransactionRollback();
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("保存数据失败！");
                    FrmDialog.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);

                FrmAttendDialog FrmDialog = new FrmAttendDialog("保存数据失败！" + ex);
                FrmDialog.ShowDialog();
            }

        }
        #endregion

        #region 事件处理方法
        private void dtStartTime_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtStartTime.Text = frm.m_DateTime;// DateTime.Parse().ToString("yyyy-MM-dd");
            }
        }

        private void dtEndTime_Click(object sender, EventArgs e)
        {
            FrmSetDateTime2 frm = new FrmSetDateTime2();
            if (frm.ShowDialog() == DialogResult.OK)
            {               
                dtEndTime.Text = frm.m_DateTime;//DateTime.Parse().ToString("HH:mm");
            }
        }

        private void txtNum_Click(object sender, EventArgs e)
        {
            frmOvertimeHours frm = new frmOvertimeHours();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtNum.Text = frm.m_Hours;
                //double time = Convert.ToDateTime(dtEndTime.Text).Hour - Convert.ToDateTime(dtStartTime.Text).Hour;
                //if (double.Parse(frm.m_Hours) > time)
                //{
                //    FrmAttendDialog FrmDialog = new FrmAttendDialog("加班时数大于输入时间段!");
                //    FrmDialog.ShowDialog();
                //    txtNum.Text = "0";
                //    return;
                //}
            }
        }

        private void lookUpEditOTApply_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit look = (LookUpEdit)sender;
            DataView view = new DataView(m_tblOTApply);
            view.RowFilter = "ID='" + look.EditValue.ToString() + "'";
            DataTable dt = view.ToTable();
            if (dt.Rows.Count > 0)
            {
                lookUpEditOTType.Text = dt.Rows[0]["OTKind"].ToString();
            }
        }

        //结束时间变更
        private void dtEndTime_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtEndTime.Text) || string.IsNullOrEmpty(dtEndTime2.Text))
            {
                return;
            }
            //DateTime dtBegin = DateTime.Parse(dtEndTime.EditValue.ToString());
            //DateTime dtEnd = DateTime.Parse(dtEndTime2.EditValue.ToString());

            //if (DateTime.Compare(dtEnd, dtBegin) < 0)
            //{
            //    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择开始时间大于结束时间!");
            //    FrmDialog.ShowDialog();
            //    return;
            //}
            //TimeSpan ts = dtBegin.Subtract(dtEnd).Duration();
            //计算时间差//加班时数
            //txtNum.Text = decimal.Round(decimal.Parse(ts.TotalHours.ToString()), 2).ToString();//取得2位小数


        }

        #endregion

        private void dtEndTime2_Click(object sender, EventArgs e)
        {
            FrmSetDateTime2 frm = new FrmSetDateTime2();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtEndTime2.Text = frm.m_DateTime;// DateTime.Parse().ToString("HH:mm");
            }
        }

        private void dtEndTime2_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtEndTime.Text) || string.IsNullOrEmpty(dtEndTime2.Text))
            {
                return;
            }
            //DateTime dtBegin = DateTime.Parse(dtEndTime.EditValue.ToString());
            //DateTime dtEnd = DateTime.Parse(dtEndTime2.EditValue.ToString());

            //if (DateTime.Compare(dtEnd, dtBegin) < 0)
            //{
            //    FrmAttendDialog FrmDialog = new FrmAttendDialog("请选择开始时间大于结束时间!");
            //    FrmDialog.ShowDialog();
            //    return;
            //}
        }

        private void txtNum_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtStartTime_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void dtStartTime_Click_1(object sender, EventArgs e)
        {
            dtStartTime.ShowPopup();
        }


        #region 共同方法

        #endregion




    }
}
