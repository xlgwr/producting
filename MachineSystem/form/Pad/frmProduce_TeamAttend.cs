using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MachineSystem.UserControls;
using Framework.Libs;
using Framework.Abstract;
using MachineSystem.form.Search;
using log4net;
using System.IO;
using System.Data.SqlClient;
using MachineSystem.SysCommon;
using MachineSystem.form.ParaLicense;
using MachineSystem.form.UserSystem;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MachineSystem.form.Pad
{
    public partial class frmProduce_TeamAttend : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        public Control[] _tmPersonControl;
        public int _iUserNum;
        public int _iUserNumOld;
        public Control[] tmparea;
        bool isFirst { get; set; }
        /// <summary>
        /// 人员数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 关位数据表
        /// </summary>
        DataTable m_tblGuanweiList = new DataTable();

        /// <summary>
        /// 关位信息
        /// </summary>
        UserPersonsList m_PersonList;

        /// <summary>
        /// 人员信息
        /// </summary>
        UserPerson m_Person;

        /// <summary>
        /// 向别
        /// </summary>
        public string parMyteamName;
        /// <summary>
        /// 日期
        /// </summary>
        public string parDdateOperDate;
        /// <summary>
        /// 考勤系统头像目录
        /// </summary>
        string AtPathDir;

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLessFrequently));
        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduce_TeamAttend(string parMyteamName, string parDdateOperDate)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                this.Opacity = 1;

                InitializeComponent();
                this.TopMost = true;
                xtraScrollableControl1.HorizontalScroll.Enabled = false;
                xtraScrollableControl1.HorizontalScroll.Visible = false;
                isRun = false;
                _iUserNum = 0;
                _iUserNumOld = 0;
                m_Person = new UserPerson();
                
                tmparea = new Control[0];

                this.parMyteamName = parMyteamName;
                this.parDdateOperDate = parDdateOperDate;
                this.Activated += new EventHandler(frmProduce_TeamAttend_Activated);
                this.FormClosing += new FormClosingEventHandler(frmProduce_TeamAttend_FormClosing);
                Program._frmProduce_TeamAttend = this;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.TopMost = true;
            }


        }

        private void initPersonCl(int UserPersonNum, Control cl)
        {
            //throw new NotImplementedException();
            Stopwatch tmpWatch = new Stopwatch();
            try
            {
                tmpWatch.Start();

                this.SuspendLayout();

                Program.logFlagStart(log, tmpWatch, "人员考勤");
                this.Cursor = Cursors.WaitCursor;
                //人员

                //this.Text = "人员揭示：初始化控-->人员控件:" + UserPersonNum + "个";
                _tmPersonControl = new Control[UserPersonNum];
                _iUserNum = 0;
                for (int i = 0; i < UserPersonNum; i++)
                {
                    UserPerson tmpd = new UserPerson();
                    tmpd.Visible = false;
                    tmpd.UserID = i.ToString();
                    tmpd.UserName = "UserPerson:" + i.ToString();
                    _tmPersonControl[i] = tmpd;
                }

                cl.Controls.AddRange(_tmPersonControl);

                this.ResumeLayout(false);
                this.PerformLayout();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                Program.logFlagEnd(log, tmpWatch, "人员考勤");

            }

        }

        void frmProduce_TeamAttend_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TopMost = false;
            this.Hide();
            e.Cancel = true;
            //throw new NotImplementedException();
        }

        void frmProduce_TeamAttend_Activated(object sender, EventArgs e)
        {
            SetFormValue();
            this.TopMost = true;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                if (this.parMyteamName != "")
                {
                    lookmyteamName.Text = this.parMyteamName;
                }
                else
                {
                    lookmyteamName.Text = Common._myTeamName;
                }
                if (this.parDdateOperDate != "")
                {
                    dateOperDate1.EditValue = this.parDdateOperDate;
                }
                else
                {
                    dateOperDate1.EditValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                }
                if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面初始化失败！" + ex);
                FrmDialog.ShowDialog();
            }
        }


        #endregion


        #region 画面按钮功能处理方法

        //人员调动按钮点击
        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                if (Program._frmProduce_TeamChange != null)
                {
                    Program._frmProduce_TeamChange.parMyteamName = lookmyteamName.Text.ToString();
                    Program._frmProduce_TeamChange.parDdateOperDate = dateOperDate1.Text.ToString();
                }
                else
                {
                    frmProduce_TeamChange frm = new frmProduce_TeamChange(lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());

                }

                this.TopMost = false;
                this.Hide();

                Program._frmProduce_TeamChange.Show();
                Program._frmProduce_TeamChange.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// 打开人员揭示界面
        /// </summary>
        private void btnUserTotalShow_Click(object sender, EventArgs e)
        {

            if (Program._frmProduce_UserTotalShow == null)
            {
                frmProduce_UserTotalShow frm = new frmProduce_UserTotalShow(lookmyteamName.Text, dateOperDate1.Text);
            }
            else
            {
                Program._frmProduce_UserTotalShow.parMyteamName = lookmyteamName.Text.ToString();
                Program._frmProduce_UserTotalShow.parDdateOperDate = dateOperDate1.Text.ToString();
            }
            this.TopMost = false;
            this.Hide();

            Program._frmProduce_UserTotalShow.Show();
            Program._frmProduce_UserTotalShow.TopMost = true;
            ////自动刷新
            //btnRef();
        }
        #endregion


        #region 事件处理方法

        /// <summary>
        /// 选择向别-班别
        /// </summary>
        private void lookmyteamName_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            frmMyTeamNameSearch frm = new frmMyTeamNameSearch();
            frm.TopMost = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Hide();
                Application.DoEvents();
                lookmyteamName.Text = frm.m_myTeamName;
            }
        }

        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;
            //自动刷新
            btnRef();
            //ThreadPool.QueueUserWorkItem(btnRefleash, "刷新");
        }

        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;
            //自动刷新
            btnRef();
            //ThreadPool.QueueUserWorkItem(btnRefleash, "刷新");
        }

        /// <summary>
        /// 人员对象双击事件,查看人员免许详细
        /// </summary>
        void m_Person_DoubleClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            UserPerson col = sender as UserPerson;
            frmLicense_RecAdd frm = new frmLicense_RecAdd();
            frm.ScanMode = Common.DataModifyMode.upd;
            string str = string.Format(@" select * from V_License_Rec_i where UserID='{0}'", col.UserID);
            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
            if (dt.Rows.Count > 0)
            {
                frm.dr = dt.Rows[0];
                frm.m_isPad = "true";
                frm.ShowDialog();

            }
            else
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("当前人员无免许信息！");
                FrmDialog.ShowDialog();
            }
            frm.m_isPad = "";
        }
        /// <summary>
        /// 请假登记
        /// </summary>
        private void btnForLeave_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            frmForLeave frm = new frmForLeave();
            DataTable dt_temp = new DataTable();

            GetFormList(ref dt_temp);
            if (dt_temp.Rows.Count == 0)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("未选中任何人员信息！");
                FrmDialog.ShowDialog();
                return;
            }

            frm.m_tblDataList = dt_temp;
            frm.m_DataTime = DateTime.Parse(dateOperDate1.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //自动刷新
                btnRef();
            }
        }

        /// <summary>
        /// 欠勤登记
        /// </summary>
        private void btnLessFrequently_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            frmLessFrequently frm = new frmLessFrequently();
            DataTable dt_temp = new DataTable();

            GetFormList(ref dt_temp);
            if (dt_temp.Rows.Count == 0)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("未选中任何人员信息！");
                FrmDialog.ShowDialog();
                return;
            }

            frm.m_tblDataList = dt_temp;
            frm.m_DataTime = DateTime.Parse(dateOperDate1.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //自动刷新
                btnRef();
            }
        }

        /// <summary>
        /// 加班登记
        /// </summary>
        private void btnOvertime_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            frmOvertime frm = new frmOvertime();
            DataTable dt_temp = new DataTable();

            GetFormList(ref dt_temp);
            if (dt_temp.Rows.Count == 0)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("未选中任何人员信息！");
                FrmDialog.ShowDialog();
                return;
            }

            frm.m_tblDataList = dt_temp;
            frm.m_DataTime = DateTime.Parse(dateOperDate1.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //自动刷新
                btnRef();
            }

        }

        /// <summary>
        /// 离职登记
        /// </summary>
        private void btnTurnover_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //this.timer1.Enabled = false;
            frmTurnover frm = new frmTurnover();
            DataTable dt_temp = new DataTable();

            GetFormList(ref dt_temp);
            if (dt_temp.Rows.Count == 0)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("未选中任何人员信息！");
                FrmDialog.ShowDialog();
                Application.DoEvents();
                return;
            }

            frm.m_tblDataList = dt_temp;
            frm.m_DataTime = DateTime.Parse(dateOperDate1.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Hide();
                Application.DoEvents();
                //自动刷新
                btnRef();
            }
        }


        /// <summary>
        /// 排班登记
        /// </summary>
        private void btnScheduling_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            btnScheduling.Enabled = false;
            frmScheduling frm = new frmScheduling(lookmyteamName.Text.Trim());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //自动刷新
                btnRef();
            }
            btnScheduling.Enabled = true;
        }

        /// <summary>
        /// 人员状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            DataTable dt_temp = new DataTable();
            GetFormList(ref dt_temp);
            if (dt_temp.Rows.Count != 1)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("当前操作只能选择一人信息！");
                FrmDialog.ShowDialog();
                return;
            }
            else
            {
                try
                {
                    frmEditProduce_User frm = new frmEditProduce_User();
                    frm.ScanMode = Common.DataModifyMode.del;
                    DataTable dt_data = new DataTable();
                    dt_data.Columns.Add("Userid");
                    dt_data.Columns.Add("UserName");
                    dt_data.Columns.Add("Sex");
                    dt_data.Columns.Add("StatusIDs");
                    dt_data.Columns.Add("DutyName");
                    DataRow row = dt_data.NewRow();

                    row["Userid"] = dt_temp.Rows[0]["Userid"];
                    row["UserName"] = dt_temp.Rows[0]["UserNm"];
                    row["Sex"] = dt_temp.Rows[0]["Sex"];
                    row["StatusIDs"] = dt_temp.Rows[0]["StatusID"];
                    row["DutyName"] = "";

                    frm.dr = row;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        //自动刷新
                        btnRef();
                    }
                }
                catch (Exception ex)
                {

                    XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                }
            }
        }

        void m_Person_AllEventClick(object sender, EventArgs e)
        {
            UserPerson col = sender as UserPerson;
            if (col.BackColor == Color.Red)
            {
                col.BackColor = Color.Transparent;
                col.Tag = 0;
            }
            else
            {
                col.BackColor = Color.Red;
                col.Tag = 1;
            }
        }

        /// <summary>
        /// 刷新页面数据（重新统计）
        /// </summary>
        private void btnRef()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                this.TopMost = true;

                SqlParameter[] paraList = new SqlParameter[7];
                paraList[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
                if (dateOperDate1.EditValue.ToString() != "")
                {
                    DateTime dt = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    paraList[0].Value = dt.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    paraList[0].Value = "";
                }
                String str_sql = string.Format(@"select top 1 JobForID,ProjectID,LineID,TeamID from V_Produce_Para where myTeamName='{0}' Order by id", lookmyteamName.Text);
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                m_dicItemData.Clear();
                if (dt_temp.Rows.Count == 1)
                {
                    m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                    m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                    m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                    m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                }
                else
                {
                    m_dicItemData["JobForID"] = "0";
                    m_dicItemData["ProjectID"] = "0";
                    m_dicItemData["LineID"] = "0";
                    m_dicItemData["TeamID"] = "0";
                }
                paraList[1] = new SqlParameter("@JobForID", SqlDbType.VarChar, 10);
                paraList[1].Value = m_dicItemData["JobForID"].ToString();

                paraList[2] = new SqlParameter("@ProjectID", SqlDbType.VarChar, 10);
                paraList[2].Value = m_dicItemData["ProjectID"].ToString();

                paraList[3] = new SqlParameter("@LineID", SqlDbType.VarChar, 10);
                paraList[3].Value = m_dicItemData["LineID"].ToString();

                paraList[4] = new SqlParameter("@TeamID", SqlDbType.VarChar, 10);
                paraList[4].Value = m_dicItemData["TeamID"].ToString();


                paraList[5] = new SqlParameter("@OperID", SqlDbType.VarChar, 10);
                if (Common._personid.ToString() != "")
                {
                    paraList[5].Value = Common._personid;
                }
                else
                {
                    paraList[5].Value = "0";
                }
                //执行存储过程1
                Common.AdoConnect.Connect.SetExecuteSP("PROC_Attend_Total_Result", Common.Choose.OnlyExecSp, paraList);

                SqlParameter[] paraListCardData = new SqlParameter[1];
                paraListCardData[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
                if (dateOperDate1.EditValue.ToString() != "")
                {
                    DateTime dt = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    paraListCardData[0].Value = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    paraListCardData[0].Value = "";
                }
                //执行存储过程2,打卡时间
                Common.AdoConnect.Connect.SetExecuteSP("PROC_CardData_Attend_Result", Common.Choose.OnlyExecSp, paraListCardData);

                //重新取得页面数据
                GetDspDataList();
                //ThreadPool.QueueUserWorkItem(btnRefleash, "刷新");

            }
            catch (Exception ex)
            {
                log.Error(ex);
                FrmAttendDialog FrmDialog = new FrmAttendDialog("数据刷新失败！" + ex);
                FrmDialog.ShowDialog();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        /// <summary>
        /// 设置时间
        /// </summary>
        private void dateOperDate1_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //this.timer1.Enabled = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            frm.TopMost = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Hide();
                Application.DoEvents();
                dateOperDate1.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");

            }
        }

        /// <summary>
        /// 全选人员对象
        /// </summary>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (chkAll.Checked)
            {//全部选中
                foreach (Control col in panelContent.Controls)
                {
                    if (!col.Visible)
                    {
                        continue;
                    }
                    if (col.GetType().ToString() == "MachineSystem.UserControls.UserPerson")
                    {
                        UserPerson person = col as UserPerson;
                        person.BackColor = Color.Red;
                        person.Tag = 1;
                    }
                }
            }
            else
            {//取消全部选中
                foreach (Control col in panelContent.Controls)
                {
                    if (!col.Visible)
                    {
                        continue;
                    }
                    if (col.GetType().ToString() == "MachineSystem.UserControls.UserPerson")
                    {
                        UserPerson person = col as UserPerson;
                        person.BackColor = Color.Transparent;
                        person.Tag = 0;
                    }
                }

            }


        }

        #endregion

        #region 共同方法

        public void btnRefleash(object o)
        {
            try
            {
                if (isRun)
                {
                    return;
                }
                isRun = true;

                Program._frmMain.Invoke(new Action(delegate()
                {
                    //this.TopMost = false;
                    //this.Text = "人员考勤:" + o.ToString() + ",正在加载中。。。";
                    //this.panelContent.Controls.Clear();
                    foreach (Control item in _tmPersonControl)
                    {
                        item.BackColor = Color.Transparent;
                        item.Tag = 0;
                        item.Visible = false;
                    }
                }));

                Task<string> t = new Task<string>(n => GetDspDataListThread((string)n), "dd");
                t.Start();
                t.Wait();

                var tmpresult = t.Result;
                if (tmpresult.Equals("1"))
                {
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        this.Text = "人员:" + o.ToString() + ",全部加载成功。。。";
                    }));
                }
                else
                {
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        this.Text = tmpresult;
                    }));
                }
            }
            catch (Exception ex)
            {
                Program._frmMain.Invoke(new Action(delegate()
                {
                    Application.DoEvents();
                    MessageBox.Show(ex.Message);
                }));

                //throw;
            }
            finally
            {
                isRun = false;
            }

        }
        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected string GetDspDataListThread(string o)
        {
            try
            {
                Program._frmMain.Invoke(new Action(delegate()
                {
                    //this.TopMost = false;
                }));

                _iUserNum = 0;
                m_tblDataList = new DataTable();

                ////获取人员头像文件夹
                string str_sql = string.Format(@"select * from V_User_TotalShow_Image WHERE (JobForID <> 0) AND (ProjectID <> 0) AND (LineID <> 0) AND (TeamID <> 0) AND (GuanweiID <> 0) ");
                if (lookmyteamName.Text.Trim() != "")
                {
                    str_sql += " and myTeamName='" + lookmyteamName.Text.Trim() + "'";
                }
                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += @" group by  JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite,AttendDate,UserID,UserIdColor,UserNM,UserNmColor,
                                    Sex,JobForNM,ProjectNM,LineNM,TeamNM,myTeamName,OrgName,GuanweiNM,GuanweiColor,TeamSetID,TeamSetNM,AttendType,tiguanGuanweiID,
                                    tiguanGuanweiNM,LicenseType,LicenseColor,AttendMemo,warnMemo,warnColor,CardTime,ConfirmFlag,CardTimeColor,AttendWork,StatusID,
                                    StatusColor,StatusName  ORDER BY GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_width = 0;
                int lint_Top = 0;//上边距
                int lint_TopCount = 0;//当前Top有几行
                int lint_Person = 0;//当前有多少人员 

                for (int i = 0; i < m_tblDataList.Rows.Count; i++)
                {

                    //人员信息
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        Application.DoEvents();

                        var m_Person = (UserPerson)_tmPersonControl[_iUserNum];
                        _iUserNum++;

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
                        try
                        {
                            m_Person.AllEventClick -= new UserPerson.AllEvent(m_Person_AllEventClick);
                            m_Person.DoubleClick -= new UserPerson.DoubleEvent(m_Person_DoubleClick);
                        }
                        catch (Exception ex)
                        {
                            log.Error("AllEventClick Error:" + m_Person.UserID);
                        }
                        m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                        m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                        m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, m_tblDataList.Rows[i]["UserID"].ToString());

                        //Top
                        lint_width = m_Person.Height;
                        lint_Top = lint_TopCount * (lint_width + 10);
                        _point = new Point(lint_Left + 10, lint_Top);

                        //panelContent.Controls.Add(m_Person);
                        m_Person.Location = _point;
                        m_Person.Visible = true;

                        lint_Person++;
                        lint_Left += lint_width + 10;

                        if (lint_Person % 13 == 0)
                        {//如果画的数量大于十个，则换行
                            lint_Left = 0;
                            lint_TopCount++;
                        }
                    }));


                }
                return "1";
            }
            catch (Exception ex)
            {

                Program._frmMain.Invoke(new Action(delegate()
                {
                    FrmAttendDialog FrmDialog = new FrmAttendDialog("数据加载失败！" + ex);
                    FrmDialog.ShowDialog();
                }));

            }
            return "0";
        }


        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
                if (isRun)
                {
                    return;
                }
                isRun = true;

                chkAll.Checked = false;

                //panelContent.Controls.Clear();

                //this.TopMost = false;

                _iUserNum = 0;
                m_tblDataList = new DataTable();

                ////获取人员头像文件夹
                string str_sql = string.Format(@"select * from V_User_TotalShow_Image WHERE (JobForID <> 0) AND (ProjectID <> 0) AND (LineID <> 0) AND (TeamID <> 0) AND (GuanweiID <> 0) ");
                if (lookmyteamName.Text.Trim() != "")
                {
                    str_sql += " and myTeamName='" + lookmyteamName.Text.Trim() + "'";
                }
                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += @" group by  JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite,AttendDate,UserID,UserIdColor,UserNM,UserNmColor,
                                    Sex,JobForNM,ProjectNM,LineNM,TeamNM,myTeamName,OrgName,GuanweiNM,GuanweiColor,TeamSetID,TeamSetNM,AttendType,tiguanGuanweiID,
                                    tiguanGuanweiNM,LicenseType,LicenseColor,AttendMemo,warnMemo,warnColor,CardTime,ConfirmFlag,CardTimeColor,AttendWork,StatusID,
                                    StatusColor,StatusName  ORDER BY GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_width = 0;
                int lint_Top = 0;//上边距
                int lint_TopCount = 0;//当前Top有几行
                int lint_Person = 0;//当前有多少人员 
                this.SuspendLayout();

                _iUserNum = m_tblDataList.Rows.Count;
                var tmpCLcount = panelContent.Controls.Count;

                if (tmpCLcount > 0)
                {
                    if (tmpCLcount < _iUserNum)
                    {
                        var diff = _iUserNum - tmpCLcount;
                        var tmplistcl = new Control[diff];
                        for (int i = 0; i < diff; i++)
                        {
                            m_Person = new UserPerson();
                            m_Person.Name = ("person" + (_iUserNum + i)).ToString();
                            m_Person.Visible = false;
                            tmplistcl[i] = m_Person;
                        }
                        panelContent.Controls.AddRange(tmplistcl);
                    }
                }
                else
                {
                    tmparea = new Control[_iUserNum];
                }
                //panelContent.Width = 15 * (m_Person.Width);
                panelContent.Height = ((_iUserNum / 14) + 3) * m_Person.Height;

                for (int i = 0; i < _iUserNum; i++)
                {

                    //人员信息
                    if (tmpCLcount <= 0)
                    {
                        m_Person = new UserPerson();
                    }
                    else
                    {
                        m_Person = (UserPerson)panelContent.Controls[i];
                    }


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
                    try
                    {
                        m_Person.AllEventClick -= new UserPerson.AllEvent(m_Person_AllEventClick);
                        m_Person.DoubleClick -= new UserPerson.DoubleEvent(m_Person_DoubleClick);
                    }
                    catch (Exception ex)
                    {
                        log.Error("AllEventClick Error:" + m_Person.UserID);
                    }
                    m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                    m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                    m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, m_tblDataList.Rows[i]["UserID"].ToString());
                    if (!m_Person.Visible)
                    {
                        m_Person.Visible = true;
                    }
                    if (tmpCLcount <= 0)
                    {
                        tmparea[i] = m_Person;
                    }
                    //Top
                    //lint_width = m_Person.Height;
                    //lint_Top = lint_TopCount * (lint_width + 10);
                    //_point = new Point(lint_Left + 10, lint_Top);

                    ////panelContent.Controls.Add(m_Person);
                    //m_Person.Location = _point;
                    //m_Person.Visible = true;

                    //lint_Person++;
                    //lint_Left += lint_width + 10;

                    //if (lint_Person % 13 == 0)
                    //{//如果画的数量大于十个，则换行
                    //    lint_Left = 0;
                    //    lint_TopCount++;
                    //}
                }

                if (tmpCLcount <= 0)
                {
                    panelContent.Controls.AddRange(tmparea);
                }
                else
                {
                    if (_iUserNum < tmpCLcount)
                    {
                        for (int i = _iUserNum; i < tmpCLcount; i++)
                        {
                            panelContent.Controls[i].Visible = false;
                        }
                    }

                }

                foreach (Control col in panelContent.Controls)
                {
                    if (!col.Visible)
                    {
                        continue;
                    }
                    if (!col.Tag.Equals(0))
                    {
                        col.BackColor = Color.Transparent;
                        col.Tag = 0;
                    }

                }

                this.ResumeLayout(false);
                this.PerformLayout();

            }
            catch (Exception ex)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("数据加载失败！" + ex);
                FrmDialog.ShowDialog();
            }
            finally
            {
                isRun = false;
            }
        }
        /// <summary>
        /// 获取选中人员信息
        /// </summary>
        public void GetFormList(ref DataTable dt_temp)
        {
            try
            {
                //this.TopMost = false;
                dt_temp = m_tblDataList.Clone();
                foreach (var col in panelContent.Controls)
                {
                    if (col.GetType().ToString() == "MachineSystem.UserControls.UserPerson")
                    {
                        UserPerson person = col as UserPerson;
                        if (person.Tag.ToString() == "1") //选中的人员
                        {
                            DataView view = new DataView(m_tblDataList.Copy());
                            view.RowFilter = "UserID='" + person.UserID + "'";
                            if (view.ToTable().Rows.Count > 0)
                            {
                                dt_temp.Rows.Add(view.ToTable().Rows[0].ItemArray);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("界面初始化失败！" + ex);
                FrmDialog.ShowDialog();
            }
        }


        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProduce_TeamAttend_Load(object sender, EventArgs e)
        {

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//防止窗口跳动
            SetStyle(ControlStyles.DoubleBuffer, true); //防止控件跳动 
            // this.TopMost = true;
        }




        public bool isRun { get; set; }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;
            //自动刷新
            btnRef();
            //
            //ThreadPool.QueueUserWorkItem(btnRefleash, "刷新");
        }

        private void frmProduce_TeamAttend_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void frmProduce_TeamAttend_Leave(object sender, EventArgs e)
        {
            this.TopMost = false;
        }
    }
}
