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
using MachineSystem.UserControls;
using Framework.Libs;
using log4net;
using MachineSystem.form.Search;
using MachineSystem.form.ParaLicense;
using System.Data.SqlClient;
using System.IO;
using MachineSystem.SysCommon;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace MachineSystem.form.Pad
{
    public partial class frmProduce_UserTotalShow : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        bool isEditValue { get; set; }
        public bool isRun { get; set; }
        public objCl _oldUsedObj { get; set; }
        bool isFirst { get; set; }
        /// <summary>
        /// 人员数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 关位(工种)List数组，存放多个m_PersonList关位(工种)
        /// </summary>
        DataTable m_tblGuanweiList = new DataTable();

        /// <summary>
        /// 是否在左边panel中加入控件
        /// </summary>
        bool isLeftInto = false;

        /// <summary>
        /// 关位(工种)信息
        /// </summary>
        UserPersonsList m_PersonList;

        /// <summary>
        /// 人员（关位）信息
        /// </summary>
        UserPerson m_Person;

        /// <summary>
        /// 人员（空位）信息
        /// </summary>
        UserPersonNull m_PersonNull;

        /// <summary>
        /// 考勤系统头像目录
        /// </summary>
        string AtPathDir;

        /// <summary>
        /// 参数：向别
        /// </summary>
        public string parMyteamName;

        /// <summary>
        /// 参数：日期
        /// </summary>
        public string parDdateOperDate;

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmProduce_UserTotalShow));
        /// <summary>
        /// 当前行数,初始值：-1
        /// </summary>
        int row_Cnt = -1;//当前行数

        /// <summary>
        /// 参数：日期
        /// </summary>
        Point _point;//控件坐标

        /// <summary>
        /// 左坐标(x坐标)
        /// </summary>
        int lint_Left = 0;//左坐标

        /// <summary>
        /// 上坐标(y坐标)
        /// </summary>
        int lint_Top = 0;//上坐标

        /// <summary>
        /// 控件之间间隙
        /// </summary>
        int widget_Cnt = 5;

        /// <summary>
        ///半边显示控件个数
        /// </summary>
        int half_Cnt = 7;

        /// <summary>
        /// 当前行左侧控件数量，初始值：1
        /// </summary>
        int wid_Left_Cnt = 1;

        /// <summary>
        /// 当前行右侧控件数量，初始值：1
        /// </summary>
        int wid_Right_Cnt = 1;


        /// <summary>
        /// 标配人数
        /// </summary>
        int bpCnt = 0;//标配人数

        /// <summary>
        ///显示关位顺序
        /// </summary>
        int gwShowSite = 0;//显示关位顺序

        /// <summary>
        /// 控件宽
        /// </summary>
        int mGuanWeiWidth = new UserPersonsList().Width + 18;//关位（工种）控件宽

        /// <summary>
        /// 控件高
        /// </summary>
        int mGuanWeiHeight = new UserPersonsList().Height + 23;//关位（工种）控件高

        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int SendMessage(IntPtr hwnd, int msg, int wParam, int lParam);
        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduce_UserTotalShow(string parMyteamName, string parDdateOperDate)
        {
            isFirst = true;
            Stopwatch s = new Stopwatch();
            try
            {
                Program.logFlagStart(log, s, "****人员揭示总显示时间：");
                this.Cursor = Cursors.WaitCursor;

                InitializeComponent();
                this.TopMost = true;
                isRun = false;
                isEditValue = false;
                _oldUsedObj = new objCl() { u_gwNum = 0, u_UserNullNum = 0, u_UserPersonNum = 0, gwNum = 0, UserNullNum = 0, UserPersonNum = 0 };
                xtraScrollableControl1.HorizontalScroll.Visible = false;
                xtraScrollableControl1.HorizontalScroll.Enabled = false;

                btnRef.Enabled = true;
                btnConfirm.Enabled = true;
                btnProduce_TeamAttend.Enabled = true;
                button1.Enabled = true;

                this.parMyteamName = parMyteamName;
                this.parDdateOperDate = parDdateOperDate;
                this.Activated += new EventHandler(frmProduce_UserTotalShow_Activated);
                this.FormClosing += new FormClosingEventHandler(frmProduce_UserTotalShow_FormClosing);

                initCl();

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                isFirst = false;
                this.TopMost = true;
                Program.logFlagEnd(log, s, "人员揭示总显示时间：");
            }

        }

        private void frmProduce_UserTotalShow_Load(object sender, EventArgs e)
        {

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);//防止窗口跳动
            SetStyle(ControlStyles.DoubleBuffer, true); //防止控件跳动 
        }
        void frmProduce_UserTotalShow_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            btnRef.Enabled = true;
            btnConfirm.Enabled = true;
            btnProduce_TeamAttend.Enabled = true;
            button1.Enabled = true;
            if (!isFirst)
            {
                SetFormValue();
            }
            this.TopMost = true;
        }

        void frmProduce_UserTotalShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TopMost = false;
            this.Hide();

            e.Cancel = true;
            //throw new NotImplementedException();
        }
        public void initCl()
        {
            try
            {
                objCl o = new objCl() { cl = panelContent, gwNum = 30, UserPersonNum = 80, UserNullNum = 70 };
                initPanelContentAddcl(o);

                Program._frmProduce_UserTotalShow = this;

                SetFormValue();

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
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

                btnConfirm.Enabled = false;



            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 画面按钮功能处理方法
        /// <summary>
        /// 刷新页面数据（重新统计）
        /// </summary>
        private void btnRef_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this.TopMost = true;

            if (isEditValue)
            {
                return;
            }

            this.btnRef.Enabled = false;
            if (lookmyteamName.Text.ToString() == "" || dateOperDate1.Text.Trim() == "")
            {
                return;
            }

            chkAll.Checked = false;

            try
            {
                SqlParameter[] paraList = new SqlParameter[7];
                paraList[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 16);
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
                GetProduce_TeamShow();
                GetDspDataList();
                //ThreadPool.QueueUserWorkItem(GetDspDataListThread, "bb");


            }
            catch (Exception ex)
            {
                log.Error(ex);
                XtraMsgBox.Show("数据刷新失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
            finally
            {
                isEditValue = false;
                this.btnRef.Enabled = true;
                btnConfirm.Enabled = true;
                btnProduce_TeamAttend.Enabled = true;
                button1.Enabled = true;
                this.Cursor = Cursors.Default;
            }


        }

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
                Program._frmProduce_TeamChange.TopMost = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }


        }

        //人员考勤按钮点击
        private void btnProduce_TeamAttend_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (Program._frmProduce_TeamAttend != null)
                {
                    Program._frmProduce_TeamAttend.parMyteamName = lookmyteamName.Text.ToString();
                    Program._frmProduce_TeamAttend.parDdateOperDate = dateOperDate1.Text.ToString();
                }
                else
                {
                    frmProduce_TeamAttend frm = new frmProduce_TeamAttend(lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());

                }
                this.TopMost = false;
                this.Hide();

                Program._frmProduce_TeamAttend.Show();
                Program._frmProduce_TeamAttend.TopMost = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }
        /// <summary>
        /// 考勤确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //this.TopMost = false;
            this.btnConfirm.Enabled = false;
            try
            {
                string message = string.Empty;
                if (lookmyteamName.Text.ToString() != "")
                {
                    int result = 0;
                    //查询班别-向别
                    string str = string.Format(@"select * from V_Produce_Para where myteamName='{0}'", lookmyteamName.Text.ToString());
                    DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str);
                    if (dt_temp.Rows.Count == 0) return;
                    int rowCnt = 0;
                    int updateCnt = 0;
                    //选中人员信息
                    DataTable dt_Users = new DataTable();
                    GetFormList(ref dt_Users);
                    for (int i = 0; i < dt_Users.Rows.Count; i++)
                    {
                        //查询是否存在当前人员
                        str = string.Format(@"select * from Attend_Line_Confirm where UserID='{0}' and  AttendDate='{1}' ",
                            dt_Users.Rows[i]["UserID"].ToString(), dateOperDate1.Text.Trim().Substring(0, 10));
                        if (SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str).Rows.Count > 0)//考勤表存在当前人员信息
                        {
                            message += dt_Users.Rows[i]["UserID"].ToString() + "无需再次确认，";
                            continue;
                        }
                        m_dicItemData.Clear();
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        m_dicItemData["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                        m_dicItemData["UserID"] = dt_Users.Rows[i]["UserID"].ToString();
                        m_dicItemData["AttendDate"] = dateOperDate1.Text.Trim().Substring(0, 10);
                        m_dicItemData["ConfirmFlag"] = "1";
                        result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Line_Confirm", m_dicItemData);
                        rowCnt++;
                        if (result > 0)
                        {
                            updateCnt++;
                        }

                    }

                    if (rowCnt == updateCnt)
                    {

                        FrmAttendDialog FrmDialog = new FrmAttendDialog(message + updateCnt + "人考勤确认成功！");
                        FrmDialog.ShowDialog();
                        Application.DoEvents();

                        //自动刷新
                        btnConfirm.Enabled = true;
                        btnProduce_TeamAttend.Enabled = true;
                        button1.Enabled = true;
                        btnRef_Click(null, null);
                    }
                }


            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            finally
            {
                this.btnConfirm.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 全选人员对象
        /// </summary>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            if (chkAll.Checked)
            {//全部选中
                foreach (Control col in panelContent.Controls)
                {
                    if (!col.Visible)
                    {
                        continue;
                    }
                    //只有人员才可以选择
                    if (col.GetType().ToString() == "MachineSystem.UserControls.UserPerson")
                    {
                        UserPerson person = col as UserPerson;
                        if (person.Time != "" && person.TimeColor != "green")
                        {//只有打卡记录的数据才可以全选
                            person.BackColor = Color.Red;
                            person.Tag = 1;
                        }

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


            btnConfirm.Enabled = true;
            btnProduce_TeamAttend.Enabled = true;
            button1.Enabled = true;
        }
        #endregion

        #region 事件处理方法


        //
        /// <summary>
        /// 向别变更
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {
            Application.DoEvents();

            this.TopMost = true;

            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;

            btnRef_Click(null, null);
        }


        //
        /// <summary>
        /// 日期变更
        /// </summary>
        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {
            Application.DoEvents();

            //this.TopMost = false;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;

            btnRef_Click(null, null);
        }

        bool _isClick { get; set; }

        bool _isDBClick { get; set; }
        /// <summary>
        /// 人员对象点击事件
        /// </summary>
        void m_Person_AllEventClick(object sender, EventArgs e)
        {
            if (_isClick)
            {
                return;
            }
            _isClick = true;
            //this.TopMost = false;

            UserPerson col = sender as UserPerson;
            if (col.TimeColor != "green")
            {//没有考勤确认的人员才能选择
                if (col.BackColor == Color.Red)
                {
                    col.BackColor = Color.Transparent;
                    col.Tag = 0;
                }
                else
                {
                    col.BackColor = Color.Red;
                    col.Tag = 1;
                    this.btnConfirm.Enabled = true;
                }
            }
            _isClick = false;

        }

        /// <summary>
        /// 人员对象双击事件
        /// </summary>
        void m_Person_DoubleClick(object sender, EventArgs e)
        {
            if (_isDBClick)
            {
                return;
            }
            _isDBClick = true;

            //this.TopMost = false;
            log.Info("人员对象双击查看免许信息");
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
                XtraMsgBox.Show("当前人员无免许信息！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            _isDBClick = false;
        }


        /// <summary>
        /// 设置时间变更
        /// </summary>
        private void dateOperDate1_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //this.timer1.Enabled = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Hide();

                this.parDdateOperDate = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");
                dateOperDate1.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");
            }
        }

        /// <summary>
        /// 选择向别-班别
        /// </summary>
        private void lookmyteamName_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            log.Info("选择向别-班别开始：" + DateTime.Now);

            frmMyTeamNameSearch frm = new frmMyTeamNameSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frm.Hide();

                this.parMyteamName = frm.m_myTeamName;
                lookmyteamName.Text = frm.m_myTeamName;
            }
        }
        #endregion

        #region 共同方法

        private objCl initPanelContentAddcl(objCl o)
        {
            //throw new NotImplementedException();
            Stopwatch tmpWatch = new Stopwatch();
            try
            {
                tmpWatch.Start();

                this.SuspendLayout();

                Program._objCl = o;
                Program._listCtro = new Control[o.gwNum + o.UserPersonNum + o.UserNullNum];

                //this.Text = "人员揭示：开始初始化控-->关位:" + o.gwNum + "个";

                //************todo something


                //关位
                Program._objCl.u_gwNum = 0;
                for (int i = 0; i < o.gwNum; i++)
                {
                    UserPersonsList tmpd = new UserPersonsList();
                    tmpd.Visible = false;
                    tmpd.GuanweiID = i.ToString();
                    tmpd.RealCount = i;
                    tmpd.StandardCount = i;
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    Program._listCtro[i] = tmpd;
                }

                //人员

                //this.Text = "人员揭示：初始化控-->关位：" + o.gwNum + ",已完成.开始人员控件:" + o.UserPersonNum + "个";

                Program._objCl.u_UserPersonNum = o.gwNum;
                for (int i = o.gwNum; i < (o.gwNum + o.UserPersonNum); i++)
                {
                    UserPerson tmpd = new UserPerson();
                    tmpd.Visible = false;
                    tmpd.UserID = i.ToString();
                    tmpd.UserName = "UserPerson:" + i.ToString();
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    Program._listCtro[i] = tmpd;
                }
                //空位

                //this.Text = "人员揭示：初始化控-->关位：" + o.gwNum + "，人员:" + o.UserPersonNum + ",已完成.开始空位人员控件:" + o.UserNullNum + "个";


                var tmpi = o.gwNum + o.UserPersonNum;
                Program._objCl.u_UserNullNum = tmpi;
                for (int i = tmpi; i < (o.gwNum + o.UserPersonNum + o.UserNullNum); i++)
                {
                    UserPersonNull tmpd = new UserPersonNull();
                    tmpd.Visible = false;
                    tmpd.UserID = i.ToString();
                    tmpd.UserName = "UserPerson:" + i.ToString();
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    Program._listCtro[i] = tmpd;
                }

                //************end todo

                o.cl.Controls.AddRange(Program._listCtro);


                this.ResumeLayout(false);
                this.PerformLayout();

                //this.Text = "人员揭示：开始初始化控件完成,关位：" + o.gwNum + "，人员:" + o.UserPersonNum + ",空位:" + o.UserNullNum + "，Time:" + tmpWatch.Elapsed.ToString();

                tmpWatch.Stop();
                log.Info("人员揭示：开始初始化控件完成,关位：" + o.gwNum + "，人员:" + o.UserPersonNum + ",空位:" + o.UserNullNum + "，Time:" + tmpWatch.Elapsed.ToString());

                return o;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        int LicenseCount = 0;//免许人员数
        int NoLicenseCount = 0;//无免许人员数


        /// <summary>
        /// 获取详细信息一览 thread
        /// </summary>
        protected void GetDspDataListThread(object o)
        {
            if (isRun)
            {
                //Program._frmMain.BeginInvoke(new Action(delegate()
                //{
                //    this.Text = "人员揭示：获取详细信息一览，线程还在运行中，请稍后 再试。。谢谢。";
                //}));
                return;
            }

            isRun = true;
            //time log
            Stopwatch watchsql = new Stopwatch();
            Stopwatch watchControl = new Stopwatch();

            watchsql.Start();
            watchControl.Start();

            NoLicenseCount = 0;//无免许人员数
            try
            {
                Program._frmMain.BeginInvoke(new Action(delegate()
                {
                    //this.TopMost = false;
                }));

                row_Cnt = -1;//布局行号归零
                m_tblDataList = new DataTable();//人员考勤情况数据
                m_tblGuanweiList = new DataTable();//关位信息
                //1、取得关位顺序信息
                string str_sql = string.Empty;
                DateTime startTime = DateTime.Now;

                str_sql = string.Format(@" SELECT a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,
                                                     JobForID,   ProjectID, LineID,  TeamID, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM    (
													select JobForID, ProjectID, LineID, TeamID, GuanweiID, AttendDate,guanweisite,AttendWork,AttendType,AttendMemo  
													from V_Attend_Result_Info
												  ) AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 
                                                    AND ISNULL(a1.AttendMemo,'') <>'支援调出'
													AND a1.AttendType<>'请假' 
													AND a1.AttendType<>'欠勤' 
                                                  GROUP BY a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID, a1.GuanweiID,a1.AttendDate
                                            ) AS h 
                                               ON  a.JobForID = h.JobForID
                                               AND a.ProjectID= h.ProjectID 
                                               AND a.LineID= h.LineID 
                                               AND a.TeamID= h.TeamID 
                                               AND a.GuanweiID= h.GuanweiID
                                               AND a.AttendDate= h.AttendDate
                                   where a.myTeamName='{1}' and a.AttendDate=CONVERT(VARCHAR(10),'{2}',120)
                                    group by  a.myTeamName,a.orgTeamName,a.GuanweiName,a.GuanweiType,a.JobForID, a.ProjectID, a.LineID, a.TeamID, a.GuanweiID,a.AttendDate ,a.SetNum ,h.InCount,a.RowID 
                                    order by a.RowID ",
                                  dateOperDate1.Text.Trim(), lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                m_tblGuanweiList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblGuanweiList.Rows.Count == 0)
                {
                    return;
                }
                //2、人员考勤情况数据取得
                str_sql = string.Format(@"select * from V_User_TotalShow_Image WHERE 
                                                (JobForID <> 0) AND (ProjectID <> 0) 
                                                AND (LineID <> 0) AND (TeamID <> 0) 
                                                AND (GuanweiID <> 0) and ISNULL(AttendMemo,'') <>'支援调出'
                                                AND AttendType<>'请假' AND AttendType<>'欠勤' ");

                str_sql += " and myTeamName='" + lookmyteamName.Text.ToString() + "'";

                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += @"group by  JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite,AttendDate,UserID,UserIdColor,UserNM,UserNmColor,
                                    Sex,JobForNM,ProjectNM,LineNM,TeamNM,myTeamName,OrgName,GuanweiNM,GuanweiColor,TeamSetID,TeamSetNM,AttendType,tiguanGuanweiID,
                                    tiguanGuanweiNM,LicenseType,LicenseColor,AttendMemo,warnMemo,warnColor,CardTime,ConfirmFlag,CardTimeColor,AttendWork,StatusID,
                                    StatusColor,StatusName order by JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                //if (m_tblDataList.Rows.Count==0)
                //{
                //    XtraMsgBox.Show("没有该向别当前日期下的考勤数据，请点击【刷新】按钮统计该向别的考勤数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, null, this.GetType());
                //}


                watchsql.Stop();
                Program._frmMain.Invoke(new Action(delegate()
                {
                    //this.Text += ",获取人员信息: SQL使用时间：" + watchsql.Elapsed.ToString();
                    log.Info(",获取人员信息: SQL使用时间：" + watchsql.Elapsed.ToString());
                    //循环把人员信息放入panel
                    //panelContent.Controls.Clear();
                }));
                isLeftInto = true;//是否考勤区域处理                

                Program._objCl.u_gwNum = 0;
                Program._objCl.u_UserPersonNum = Program._objCl.gwNum;
                Program._objCl.u_UserNullNum = Program._objCl.gwNum + Program._objCl.UserPersonNum;

                //关位
                string strGuwanweiID = string.Empty;
                this.SuspendLayout();
                for (int a = 0; a < m_tblGuanweiList.Rows.Count; a++)
                {
                    strGuwanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();

                    //当前关位下的人员信息,关位下没有人员，不展示关位(工种）信息
                    DataView view = new DataView(m_tblDataList.Copy());
                    view.RowFilter = "GuanweiID='" + strGuwanweiID + "'";

                    DataTable dt_temp = view.ToTable();
                    //if (dt_temp.Rows.Count == 0) continue;

                    //添加关位(工种）
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        Application.DoEvents();
                        btnConfirm.Enabled = true;//考勤确认按钮可用

                        var m_PersonList = (UserPersonsList)Program._listCtro[Program._objCl.u_gwNum]; //new UserPersonsList();
                        Program._objCl.u_gwNum++;

                        m_PersonList.TitleGuanwei = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();//关位名称
                        bpCnt = int.Parse(m_tblGuanweiList.Rows[a]["SetNum"].ToString());

                        m_PersonList.StandardCount = bpCnt;//关位标配人数
                        m_PersonList.RealCount = int.Parse(m_tblGuanweiList.Rows[a]["realityCount"].ToString());//关位实配人数
                        //m_PersonList.AllEventClick += new UserPersonsList.AllEvent(m_PersonList_AllEventClick);

                        this.setImgIndex(true);//设置显示位置
                        //panelContent.Controls.Add(m_PersonList);//展示空间
                        m_PersonList.Location = _point;//设置空间展示坐标
                        m_PersonList.Visible = true;
                    }));

                    gwShowSite = 1;
                    view = new DataView(dt_temp.Copy());
                    view.RowFilter = "GuanweiSite='99'";
                    int intCount = bpCnt + view.ToTable().Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        Boolean perFlg = true;
                        int GwSite = 0;
                        if (dt_temp.Rows.Count > 0)
                        {
                            GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                        }
                        while (GwSite == (i + 1))
                        {
                            Program._frmMain.Invoke(new Action(delegate()
                            {
                                Application.DoEvents();
                                //人员信息
                                var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                                Program._objCl.u_UserPersonNum++; //new UserPerson();

                                perFlg = false;
                                //位置相同
                                m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;

                                if (dt_temp.Rows[0]["AttendType"].ToString() == "支援")
                                {
                                    m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                                }
                                if (dt_temp.Rows[0]["AttendType"].ToString() == "替关")
                                {
                                    m_Person.TitleName = "(替)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                                }

                                m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                                m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                                m_Person.GuanweiSite = GwSite.ToString();
                                m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                                m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                                m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                                m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                                m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                                m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                                m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                                m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                                m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                                m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                                m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                                m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
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
                                m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                                m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);

                                this.setImgIndex(false);//设置显示位置
                                //panelContent.Controls.Add(m_Person);//展示空间
                                m_Person.Location = _point;//设置空间展示坐标
                                m_Person.Visible = true;
                            }));
                            gwShowSite = GwSite + 1;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                            if (dt_temp.Rows.Count > 0)
                            {
                                GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (perFlg && GwSite == 99 && gwShowSite > bpCnt)
                        {

                            Program._frmMain.Invoke(new Action(delegate()
                            {
                                Application.DoEvents();

                                var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                                Program._objCl.u_UserPersonNum++; //new UserPerson();

                                //位置相同

                                m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();

                                if (dt_temp.Rows[0]["AttendType"].ToString() == "支援")
                                {
                                    m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();
                                }
                                if (dt_temp.Rows[0]["AttendType"].ToString() == "替关")
                                {
                                    m_Person.TitleName = "(替)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();
                                }

                                m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                                m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                                m_Person.GuanweiSite = GwSite.ToString();
                                m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                                m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                                m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                                m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                                m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                                m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                                m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                                m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                                m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                                m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                                m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                                m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
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
                                m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());

                                m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);


                                this.setImgIndex(false);//设置显示位置
                                //panelContent.Controls.Add(m_Person);//展示空间
                                m_Person.Location = _point;//设置空间展示坐标
                                m_Person.Visible = true;
                            }));

                            gwShowSite++;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                        }
                        else if (perFlg)
                        {
                            Program._frmMain.Invoke(new Action(delegate()
                             {
                                 Application.DoEvents();
                                 //位置不同，显示空关位
                                 //人员信息
                                 var m_PersonNull = (UserPersonNull)Program._listCtro[Program._objCl.u_UserNullNum];
                                 Program._objCl.u_UserNullNum++;

                                 m_PersonNull.TitleName = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString() + " - " + gwShowSite;
                                 m_PersonNull.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                                 m_PersonNull.GuanweiNM = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();
                                 m_PersonNull.GuanweiSite = gwShowSite.ToString();

                                 m_PersonNull.Tag = "0";
                                 //m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_PersonNull_AllEventClick);
                                 m_PersonNull.ImageUrl = "";


                                 this.setImgIndex(false);//设置显示位置
                                 //panelContent.Controls.Add(m_PersonNull);//展示空间
                                 m_PersonNull.Location = _point;//设置空间展示坐标
                                 m_PersonNull.Visible = true;

                             }));
                            gwShowSite++;
                        }
                    }

                }
                int count = NoLicenseCount - LicenseCount;
                if (count < 0) count = 0;


                watchControl.Stop();
                Program._frmMain.BeginInvoke(new Action(delegate()
                {
                    Application.DoEvents();
                    lblnoLicense.Text = count.ToString();// + "人"; "无免许:" + 
                    //全选的选中取消
                    this.chkAll.Checked = false;
                    //this.Text += ",控件移动：" + watchControl.Elapsed.ToString();
                    log.Info(",控件移动：" + watchControl.Elapsed.ToString());
                }));

                wid_Left_Cnt = 1;
                wid_Right_Cnt = 1;
                isRun = false;
                this.ResumeLayout(false);
            }
            catch (Exception ex)
            {
                isRun = false;
                Program._frmMain.BeginInvoke(new Action(delegate()
                {
                    log.Error(ex);
                    XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                }));

            }
            finally
            {
                Program._frmMain.BeginInvoke(new Action(delegate()
                  {
                      //panelContent.AutoScroll = true;
                      this.Cursor = Cursors.Default;
                  }));
            }
        }


        /// <summary>
        /// 获取详细信息一览 
        /// </summary>
        protected override void GetDspDataList()
        {
            if (isRun)
            {
                //Program._frmMain.BeginInvoke(new Action(delegate()
                //{
                //    this.Text = "人员揭示：获取详细信息一览，线程还在运行中，请稍后 再试。。谢谢。";
                //}));
                return;
            }

            isRun = true;
            //time log
            Stopwatch watchsql = new Stopwatch();
            Stopwatch watchControl = new Stopwatch();

            watchsql.Start();
            watchControl.Start();

            NoLicenseCount = 0;//无免许人员数
            try
            {
                //this.TopMost = false;

                row_Cnt = -1;//布局行号归零
                m_tblDataList = new DataTable();//人员考勤情况数据
                m_tblGuanweiList = new DataTable();//关位信息
                //1、取得关位顺序信息
                string str_sql = string.Empty;
                DateTime startTime = DateTime.Now;

                str_sql = string.Format(@" SELECT a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,
                                                     JobForID,   ProjectID, LineID,  TeamID, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM    (
													select JobForID, ProjectID, LineID, TeamID, GuanweiID, AttendDate,guanweisite,AttendWork,AttendType,AttendMemo  
													from V_Attend_Result_Info
												  ) AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 
                                                    AND ISNULL(a1.AttendMemo,'') <>'支援调出'
													AND a1.AttendType<>'请假' 
													AND a1.AttendType<>'欠勤' 
                                                  GROUP BY a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID, a1.GuanweiID,a1.AttendDate
                                            ) AS h 
                                               ON  a.JobForID = h.JobForID
                                               AND a.ProjectID= h.ProjectID 
                                               AND a.LineID= h.LineID 
                                               AND a.TeamID= h.TeamID 
                                               AND a.GuanweiID= h.GuanweiID
                                               AND a.AttendDate= h.AttendDate
                                   where a.myTeamName='{1}' and a.AttendDate=CONVERT(VARCHAR(10),'{2}',120)
                                    group by  a.myTeamName,a.orgTeamName,a.GuanweiName,a.GuanweiType,a.JobForID, a.ProjectID, a.LineID, a.TeamID, a.GuanweiID,a.AttendDate ,a.SetNum ,h.InCount,a.RowID 
                                    order by a.RowID ",
                                  dateOperDate1.Text.Trim(), lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                m_tblGuanweiList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblGuanweiList.Rows.Count == 0)
                {
                    return;
                }
                //2、人员考勤情况数据取得
                str_sql = string.Format(@"select * from V_User_TotalShow_Image WHERE 
                                                (JobForID <> 0) AND (ProjectID <> 0) 
                                                AND (LineID <> 0) AND (TeamID <> 0) 
                                                AND (GuanweiID <> 0) and ISNULL(AttendMemo,'') <>'支援调出'
                                                AND AttendType<>'请假' AND AttendType<>'欠勤' ");

                str_sql += " and myTeamName='" + lookmyteamName.Text.ToString() + "'";

                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += @"group by  JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite,AttendDate,UserID,UserIdColor,UserNM,UserNmColor,
                                    Sex,JobForNM,ProjectNM,LineNM,TeamNM,myTeamName,OrgName,GuanweiNM,GuanweiColor,TeamSetID,TeamSetNM,AttendType,tiguanGuanweiID,
                                    tiguanGuanweiNM,LicenseType,LicenseColor,AttendMemo,warnMemo,warnColor,CardTime,ConfirmFlag,CardTimeColor,AttendWork,StatusID,
                                    StatusColor,StatusName order by JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                //if (m_tblDataList.Rows.Count==0)
                //{
                //    XtraMsgBox.Show("没有该向别当前日期下的考勤数据，请点击【刷新】按钮统计该向别的考勤数据！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, null, this.GetType());
                //}


                watchsql.Stop();
                //this.Text += ",获取人员信息: SQL使用时间：" + watchsql.Elapsed.ToString();
                log.Info(",获取人员信息: SQL使用时间：" + watchsql.Elapsed.ToString());
                //循环把人员信息放入panel
                //panelContent.Controls.Clear();

                isLeftInto = true;//是否考勤区域处理                

                Program._objCl.u_gwNum = 0;
                Program._objCl.u_UserPersonNum = Program._objCl.gwNum;
                Program._objCl.u_UserNullNum = Program._objCl.gwNum + Program._objCl.UserPersonNum;

                //关位
                string strGuwanweiID = string.Empty;

                this.SuspendLayout();
                var gwcount = m_tblGuanweiList.Rows.Count;
                for (int a = 0; a < gwcount; a++)
                {
                    strGuwanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();

                    //当前关位下的人员信息,关位下没有人员，不展示关位(工种）信息
                    DataView view = new DataView(m_tblDataList.Copy());
                    view.RowFilter = "GuanweiID='" + strGuwanweiID + "'";

                    DataTable dt_temp = view.ToTable();
                    //if (dt_temp.Rows.Count == 0) continue;

                    //添加关位(工种）
                    btnConfirm.Enabled = true;//考勤确认按钮可用

                    var m_PersonList = (UserPersonsList)Program._listCtro[Program._objCl.u_gwNum]; //new UserPersonsList();
                    Program._objCl.u_gwNum++;

                    m_PersonList.TitleGuanwei = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();//关位名称
                    bpCnt = int.Parse(m_tblGuanweiList.Rows[a]["SetNum"].ToString());

                    m_PersonList.StandardCount = bpCnt;//关位标配人数
                    m_PersonList.RealCount = int.Parse(m_tblGuanweiList.Rows[a]["realityCount"].ToString());//关位实配人数
                    //m_PersonList.AllEventClick += new UserPersonsList.AllEvent(m_PersonList_AllEventClick);

                    this.setImgIndex(true);//设置显示位置
                    //panelContent.Controls.Add(m_PersonList);//展示空间
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        m_PersonList.Location = _point;//设置空间展示坐标
                        if (!m_PersonList.Visible)
                        {
                            Program._frmMain.Invoke(new Action(delegate()
                                {
                                    Application.DoEvents();
                                    m_PersonList.Visible = true;
                                }));
                        }
                        //
                    }));
                    gwShowSite = 1;
                    view = new DataView(dt_temp.Copy());
                    view.RowFilter = "GuanweiSite='99'";
                    int intCount = bpCnt + view.ToTable().Rows.Count;
                    for (int i = 0; i < intCount; i++)
                    {
                        Boolean perFlg = true;
                        int GwSite = 0;
                        if (dt_temp.Rows.Count > 0)
                        {
                            GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                        }
                        while (GwSite == (i + 1))
                        {
                            //人员信息
                            var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                            Program._objCl.u_UserPersonNum++; //new UserPerson();

                            perFlg = false;
                            //位置相同
                            m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;

                            if (dt_temp.Rows[0]["AttendType"].ToString() == "支援")
                            {
                                m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                            }
                            if (dt_temp.Rows[0]["AttendType"].ToString() == "替关")
                            {
                                m_Person.TitleName = "(替)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                            }

                            m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                            m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                            m_Person.GuanweiSite = GwSite.ToString();
                            m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                            m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                            m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                            m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                            m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                            m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                            m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                            m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                            m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                            m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                            m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                            m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
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
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);

                            this.setImgIndex(false);//设置显示位置
                            //panelContent.Controls.Add(m_Person);//展示空间
                            Program._frmMain.Invoke(new Action(delegate()
                           {
                               m_Person.Location = _point;//设置空间展示坐标
                               if (!m_Person.Visible)
                               {
                                   m_Person.Visible = true;
                               }
                           }));
                            gwShowSite = GwSite + 1;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                            if (dt_temp.Rows.Count > 0)
                            {
                                GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (perFlg && GwSite == 99 && gwShowSite > bpCnt)
                        {

                            var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                            Program._objCl.u_UserPersonNum++; //new UserPerson();

                            //位置相同

                            m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();

                            if (dt_temp.Rows[0]["AttendType"].ToString() == "支援")
                            {
                                m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();
                            }
                            if (dt_temp.Rows[0]["AttendType"].ToString() == "替关")
                            {
                                m_Person.TitleName = "(替)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();
                            }

                            m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                            m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                            m_Person.GuanweiSite = GwSite.ToString();
                            m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                            m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                            m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                            m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                            m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                            m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                            m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                            m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                            m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                            m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                            m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                            m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
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
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());

                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);


                            this.setImgIndex(false);//设置显示位置
                            //panelContent.Controls.Add(m_Person);//展示空间
                            Program._frmMain.Invoke(new Action(delegate()
                           {
                               Application.DoEvents();
                               m_Person.Location = _point;//设置空间展示坐标
                               if (!m_Person.Visible)
                               {
                                   Program._frmMain.Invoke(new Action(delegate()
                                   {
                                       Application.DoEvents();
                                       m_Person.Visible = true;
                                   }));
                               }
                           }));
                            gwShowSite++;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                        }
                        else if (perFlg)
                        {
                            //位置不同，显示空关位
                            //人员信息
                            var m_PersonNull = (UserPersonNull)Program._listCtro[Program._objCl.u_UserNullNum];
                            Program._objCl.u_UserNullNum++;

                            m_PersonNull.TitleName = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString() + " - " + gwShowSite;
                            m_PersonNull.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                            m_PersonNull.GuanweiNM = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();
                            m_PersonNull.GuanweiSite = gwShowSite.ToString();

                            m_PersonNull.Tag = "0";
                            //m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_PersonNull_AllEventClick);
                            m_PersonNull.ImageUrl = "";


                            this.setImgIndex(false);//设置显示位置
                            //panelContent.Controls.Add(m_PersonNull);//展示空间
                            Program._frmMain.Invoke(new Action(delegate()
                            {
                                Application.DoEvents();
                                m_PersonNull.Location = _point;//设置空间展示坐标
                                if (!m_PersonNull.Visible)
                                {
                                    Program._frmMain.Invoke(new Action(delegate()
                                    {
                                        Application.DoEvents();
                                        m_PersonNull.Visible = true;
                                    }));
                                }
                            }));

                            gwShowSite++;
                        }
                    }

                }
                int count = NoLicenseCount - LicenseCount;
                if (count < 0) count = 0;


                lblnoLicense.Text = count.ToString();// +"人"; "无免许:" + 
                //全选的选中取消
                this.chkAll.Checked = false;


                wid_Left_Cnt = 1;
                wid_Right_Cnt = 1;
                isRun = false;

                #region hide no use control
                //未使用的不显示
                if (_oldUsedObj.gwNum <= 0)
                {
                    _oldUsedObj.gwNum = Program._objCl.gwNum;
                    _oldUsedObj.u_gwNum = Program._objCl.u_gwNum;
                    _oldUsedObj.UserPersonNum = Program._objCl.UserPersonNum;
                    _oldUsedObj.u_UserPersonNum = Program._objCl.u_UserPersonNum;
                    _oldUsedObj.UserNullNum = Program._objCl.UserNullNum;
                    _oldUsedObj.u_UserNullNum = Program._objCl.u_UserNullNum;
                }
                else
                {
                    //关位
                    if (_oldUsedObj.u_gwNum >= Program._objCl.u_gwNum)
                    {
                        for (int i = Program._objCl.u_gwNum; i <= _oldUsedObj.u_gwNum; i++)
                        {
                            Program._listCtro[i].Visible = false;
                        }
                    }
                    else
                    {
                        _oldUsedObj.u_gwNum = Program._objCl.u_gwNum;
                    }
                    //人员
                    if (_oldUsedObj.u_UserPersonNum >= Program._objCl.u_UserPersonNum)
                    {
                        for (int i = Program._objCl.u_UserPersonNum; i <= _oldUsedObj.u_UserPersonNum; i++)
                        {
                            Program._listCtro[i].Visible = false;
                        }
                    }
                    else
                    {
                        _oldUsedObj.u_UserPersonNum = Program._objCl.u_UserPersonNum;
                    }
                    //空位
                    if (_oldUsedObj.u_UserNullNum >= Program._objCl.u_UserNullNum)
                    {
                        for (int i = Program._objCl.u_UserNullNum; i <= _oldUsedObj.u_UserNullNum; i++)
                        {
                            Program._listCtro[i].Visible = false;
                        }
                    }
                    else
                    {
                        _oldUsedObj.u_UserNullNum = Program._objCl.u_UserNullNum;
                    }

                }


                #endregion
                this.ResumeLayout(false);
                this.PerformLayout();
                watchControl.Stop();
                //this.Text += ",移动控件：" + watchControl.Elapsed.ToString();
                log.Info(",移动控件：" + watchControl.Elapsed.ToString());
            }
            catch (Exception ex)
            {
                isRun = false;
                log.Error(ex);
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());


            }
            finally
            {
                //panelContent.AutoScroll = true;
                this.Cursor = Cursors.Default;

            }
        }
        /// <summary>
        /// 获取详细信息一览
        /// </summary>
        protected void GetDspDataListOld()
        {
            this.Cursor = Cursors.WaitCursor;
            NoLicenseCount = 0;//无免许人员数
            try
            {
                //this.TopMost = false;
                row_Cnt = -1;//布局行号归零
                m_tblDataList = new DataTable();//人员考勤情况数据
                m_tblGuanweiList = new DataTable();//关位信息
                //1、取得关位顺序信息
                string str_sql = string.Empty;
                DateTime startTime = DateTime.Now;
                Stopwatch _stopwatch = new Stopwatch();
                _stopwatch.Start();

                str_sql = string.Format(@" SELECT a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,
                                                     JobForID,   ProjectID, LineID,  TeamID, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM    (
													select JobForID, ProjectID, LineID, TeamID, GuanweiID, AttendDate,guanweisite,AttendWork,AttendType,AttendMemo  
													from V_Attend_Result_Info
												  ) AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 
                                                    AND ISNULL(a1.AttendMemo,'') <>'支援调出'
													AND a1.AttendType<>'请假' 
													AND a1.AttendType<>'欠勤' 
                                                  GROUP BY a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID, a1.GuanweiID,a1.AttendDate
                                            ) AS h 
                                               ON  a.JobForID = h.JobForID
                                               AND a.ProjectID= h.ProjectID 
                                               AND a.LineID= h.LineID 
                                               AND a.TeamID= h.TeamID 
                                               AND a.GuanweiID= h.GuanweiID
                                               AND a.AttendDate= h.AttendDate
                                   where a.myTeamName='{1}' and a.AttendDate=CONVERT(VARCHAR(10),'{2}',120)
                                    group by  a.myTeamName,a.orgTeamName,a.GuanweiName,a.GuanweiType,a.JobForID, a.ProjectID, a.LineID, a.TeamID, a.GuanweiID,a.AttendDate ,a.SetNum ,h.InCount,a.RowID 
                                    order by a.RowID ",
                                  dateOperDate1.Text.Trim(), lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                m_tblGuanweiList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //_stopwatch.Stop();
                //var msg = "使用时间:" + _stopwatch.Elapsed.ToString();
                //log.InfoFormat("***取得关位顺序信息：" + msg);

                if (m_tblGuanweiList.Rows.Count == 0)
                {

                    // panelContent.Controls.Clear();
                    return;
                }
                //_stopwatch = new Stopwatch();
                //_stopwatch.Start();
                //2、人员考勤情况数据取得
                str_sql = string.Format(@"select * from V_User_TotalShow_Image WHERE 
                                                (JobForID <> 0) AND (ProjectID <> 0) 
                                                AND (LineID <> 0) AND (TeamID <> 0) 
                                                AND (GuanweiID <> 0) and ISNULL(AttendMemo,'') <>'支援调出'
                                                AND AttendType<>'请假' AND AttendType<>'欠勤' ");

                str_sql += " and myTeamName='" + lookmyteamName.Text.ToString() + "'";

                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += @"group by  JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite,AttendDate,UserID,UserIdColor,UserNM,UserNmColor,
                                    Sex,JobForNM,ProjectNM,LineNM,TeamNM,myTeamName,OrgName,GuanweiNM,GuanweiColor,TeamSetID,TeamSetNM,AttendType,tiguanGuanweiID,
                                    tiguanGuanweiNM,LicenseType,LicenseColor,AttendMemo,warnMemo,warnColor,CardTime,ConfirmFlag,CardTimeColor,AttendWork,StatusID,
                                    StatusColor,StatusName order by JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //_stopwatch.Stop();
                //msg = "使用时间:" + _stopwatch.Elapsed.ToString();
                //log.InfoFormat("***人员考勤情况数据取得：" + msg);

                //循环把人员信息放入panel
                // panelContent.Controls.Clear();
                Program._objCl.u_gwNum = 0;
                Program._objCl.u_UserPersonNum = Program._objCl.gwNum;
                Program._objCl.u_UserNullNum = Program._objCl.gwNum + Program._objCl.UserPersonNum;

                isLeftInto = true;//是否考勤区域处理                

                //_stopwatch = new Stopwatch();
                //_stopwatch.Start();
                //关位
                string strGuwanweiID = string.Empty;



                for (int a = 0; a < m_tblGuanweiList.Rows.Count; a++)
                {
                    strGuwanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                    if (strGuwanweiID == "SSC")
                    {

                    }

                    //Stopwatch _stop = new Stopwatch();
                    //_stop.Start();
                    //当前关位下的人员信息,关位下没有人员，不展示关位(工种）信息
                    DataView view = new DataView(m_tblDataList.Copy());
                    view.RowFilter = "GuanweiID='" + strGuwanweiID + "'";

                    DataTable dt_temp = view.ToTable();

                    //_stop.Stop();
                    //msg = "使用时间:" + _stop.Elapsed.ToString();
                    //log.InfoFormat("***获取当前关位下人员信息：" + msg);
                    //if (dt_temp.Rows.Count == 0) continue;

                    btnConfirm.Enabled = true;//考勤确认按钮可用

                    //添加关位(工种）

                    var m_PersonList = (UserPersonsList)Program._listCtro[Program._objCl.u_gwNum];
                    Program._objCl.u_gwNum++;

                    m_PersonList.TitleGuanwei = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();//关位名称
                    bpCnt = int.Parse(m_tblGuanweiList.Rows[a]["SetNum"].ToString());

                    m_PersonList.StandardCount = bpCnt;//关位标配人数
                    m_PersonList.RealCount = int.Parse(m_tblGuanweiList.Rows[a]["realityCount"].ToString());//关位实配人数
                    //m_PersonList.AllEventClick += new UserPersonsList.AllEvent(m_PersonList_AllEventClick);
                    this.setImgIndex(true);//设置显示位置

                    //m_PersonList.Visible = false;
                    //panelContent.Controls.Add(m_PersonList);//展示空间

                    m_PersonList.Location = _point;//设置空间展示坐标
                    //m_PersonList.Visible = true;

                    gwShowSite = 1;
                    view = new DataView(dt_temp.Copy());
                    view.RowFilter = "GuanweiSite='99'";
                    int intCount = bpCnt + view.ToTable().Rows.Count;
                    Stopwatch _stopsss = new Stopwatch();
                    _stopsss.Start();
                    for (int i = 0; i < intCount; i++)
                    {
                        Boolean perFlg = true;
                        int GwSite = 0;
                        if (dt_temp.Rows.Count > 0)
                        {
                            GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                        }
                        while (GwSite == (i + 1))
                        {
                            //人员信息
                            var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                            Program._objCl.u_UserPersonNum++;
                            //位置相同
                            m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;

                            if ((dt_temp.Rows[0]["AttendType"].ToString() == "支援") || (dt_temp.Rows[0]["AttendType"].ToString() == "替关"))
                            {
                                m_Person.TitleName = "(" + dt_temp.Rows[0]["GuanweiNM"].ToString().Substring(0, 1) + ")" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                                //m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                            }
                            //if (dt_temp.Rows[0]["AttendType"].ToString() == "替关")
                            //{
                            //    m_Person.TitleName = "(替)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                            //}

                            m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                            m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                            m_Person.GuanweiSite = GwSite.ToString();
                            m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                            m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                            m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                            m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                            m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                            m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                            m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                            m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                            m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                            m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                            m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                            m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
                            m_Person.Tag = "0";
                            try
                            {
                                m_Person.AllEventClick -= new UserPerson.AllEvent(m_Person_AllEventClick);
                            }
                            catch (Exception ex)
                            {
                                log.Error("AllEventClick Error:" + m_Person.UserID);
                            }
                            m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                            this.setImgIndex(false);//设置显示位置

                            //Stopwatch _stopm_Person = new Stopwatch();
                            //_stopm_Person.Start();

                            //m_Person.Visible = false;
                            //panelContent.Controls.Add(m_Person);//展示空间
                            m_Person.Location = _point;//设置空间展示坐标
                            //m_Person.Visible = true;

                            //_stopm_Person.Stop();
                            //msg = "使用时间:" + _stopm_Person.Elapsed.ToString();
                            //log.InfoFormat("***加载人员信息到Panel：" + msg);

                            gwShowSite = GwSite + 1;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                            if (dt_temp.Rows.Count > 0)
                            {
                                GwSite = int.Parse(dt_temp.Rows[0]["GuanweiSite"].ToString());//关位位置
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (perFlg && GwSite == 99 && gwShowSite > bpCnt)
                        {
                            var m_Person = (UserPerson)Program._listCtro[Program._objCl.u_UserPersonNum];
                            Program._objCl.u_UserPersonNum++;

                            //位置相同                            
                            m_Person.TitleName = dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + gwShowSite.ToString();
                            if ((dt_temp.Rows[0]["AttendType"].ToString() == "支援") || (dt_temp.Rows[0]["AttendType"].ToString() == "替关"))
                            {
                                m_Person.TitleName = "(" + dt_temp.Rows[0]["GuanweiNM"].ToString().Substring(0, 1) + ")" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                                //m_Person.TitleName = "(支)" + dt_temp.Rows[0]["GuanweiNM"].ToString() + " - " + GwSite;
                            }

                            m_Person.GuanweiID = dt_temp.Rows[0]["GuanweiID"].ToString();
                            m_Person.GuanweiNM = dt_temp.Rows[0]["GuanweiNM"].ToString();
                            m_Person.GuanweiSite = GwSite.ToString();
                            m_Person.GuanweiColor = dt_temp.Rows[0]["GuanweiColor"].ToString();
                            m_Person.Status = dt_temp.Rows[0]["StatusName"].ToString();
                            m_Person.StatusColor = dt_temp.Rows[0]["StatusColor"].ToString();
                            m_Person.Time = dt_temp.Rows[0]["CardTime"].ToString();
                            m_Person.TimeColor = dt_temp.Rows[0]["CardTimeColor"].ToString();
                            m_Person.Remind = dt_temp.Rows[0]["warnMemo"].ToString();
                            m_Person.RemindColor = dt_temp.Rows[0]["warnColor"].ToString();
                            m_Person.LicenseType = dt_temp.Rows[0]["LicenseType"].ToString();
                            m_Person.LicenseColor = dt_temp.Rows[0]["LicenseColor"].ToString();
                            m_Person.UserID = dt_temp.Rows[0]["UserID"].ToString();
                            m_Person.UserName = dt_temp.Rows[0]["UserNM"].ToString();
                            m_Person.UserIdNmColor = dt_temp.Rows[0]["UserIdColor"].ToString();
                            m_Person.Tag = "0";
                            try
                            {
                                m_Person.AllEventClick -= new UserPerson.AllEvent(m_Person_AllEventClick);
                            }
                            catch (Exception ex)
                            {
                                log.Error("AllEventClick Error:" + m_Person.UserID);
                            }
                            m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                            this.setImgIndex(false);//设置显示位置

                            //m_Person.Visible = false;
                            //panelContent.Controls.Add(m_Person);//展示空间
                            m_Person.Location = _point;//设置空间展示坐标
                            //m_Person.Visible = true;

                            gwShowSite++;
                            dt_temp.Rows[0].Delete();
                            NoLicenseCount++;
                        }
                        else if (perFlg)
                        {
                            //位置不同，显示空关位
                            //人员信息
                            var m_PersonNull = (UserPersonNull)Program._listCtro[Program._objCl.u_UserNullNum];
                            Program._objCl.u_UserNullNum++;

                            m_PersonNull.TitleName = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString() + " - " + gwShowSite;
                            m_PersonNull.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                            m_PersonNull.GuanweiNM = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();
                            m_PersonNull.GuanweiSite = gwShowSite.ToString();

                            m_PersonNull.Tag = "0";
                            //m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_PersonNull_AllEventClick);
                            m_PersonNull.ImageUrl = "";
                            this.setImgIndex(false);//设置显示位置

                            //panelContent.Controls.Add(m_PersonNull);//展示空间
                            m_PersonNull.Location = _point;//设置空间展示坐标

                            //m_PersonNull.Visible = true;
                            gwShowSite++;
                        }
                    }

                    //  _stopsss.Stop();
                    //var  msg = strGuwanweiID + "使用时间:" + _stopsss.Elapsed.ToString();
                    //  log.InfoFormat("***加载控件：" + msg);

                }

                //show 
                for (int i = 0; i < Program._objCl.u_gwNum; i++)
                {
                    Program._listCtro[i].Visible = true;
                }
                var tmpi = Program._objCl.gwNum;
                for (int i = tmpi; i < Program._objCl.u_UserPersonNum; i++)
                {
                    Program._listCtro[i].Visible = true;
                }
                var tmpi2 = Program._objCl.gwNum + Program._objCl.UserPersonNum;
                for (int i = tmpi2; i < Program._objCl.u_UserNullNum; i++)
                {
                    Program._listCtro[i].Visible = true;
                }

                int count = NoLicenseCount - LicenseCount;
                if (count < 0) count = 0;
                lblnoLicense.Text = count.ToString();// +"人"; "无免许:" + 

                //全选的选中取消
                this.chkAll.Checked = false;
                btnConfirm.Enabled = true;
                btnProduce_TeamAttend.Enabled = true;
                button1.Enabled = true;
                wid_Left_Cnt = 1;
                wid_Right_Cnt = 1;

                _stopwatch.Stop();
                var msg = "使用时间:" + _stopwatch.Elapsed.ToString();
                //this.Text += "，获取详细信息一览--》" + msg;
                log.InfoFormat("***加载控件：" + msg);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        /// <summary>
        /// 获取揭示汇总--人员揭示(统计数据)
        /// </summary>
        public void GetProduce_TeamShow()
        {
            try
            {
                if (isRun)
                {
                    //this.Text = "人员揭示：获取详细信息一览，线程还在运行中，请稍后 再试。。谢谢。";
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                //panelContent.AutoScroll = false;
                xtraScrollableControl1.VerticalScroll.Value = 0;
                var isCheckCL = Program._listCtro.Where(m => !m.BackColor.Equals(Color.Transparent) || !m.Tag.Equals("0")).ToList();
                foreach (Control item in isCheckCL)
                {
                    if (item.GetType().ToString() == "MachineSystem.UserControls.UserPerson")
                    {
                        item.BackColor = Color.Transparent;
                    }
                    item.Tag = "0";
                    //item.Visible = false;
                }

                Stopwatch tmpW = new Stopwatch();
                tmpW.Start();

                Stopwatch s1 = new Stopwatch();
                Program.logFlagStart(log, s1, "SQL 支援派出 1:");//CONVERT(VARCHAR(10),'{1}',120) 
                string str_sql = string.Format(@"select top 1 zhiyuanIn,zhiyuanOut,jianjie2 from V_Produce_TeamShow_Group_i where 1=1 and myTeamName='{0}' and AttendDate='{1}'",
                    lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim().Substring(0, 10));

                DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s1, "SQL 支援派出 1:");


                if (dt.Rows.Count == 1)
                {
                    //lblhaveLicense.Text = "有免许:" + dt.Rows[0]["haveLicense"].ToString() + "人";
                    //lblnoLicense.Text = "无免许:" + dt.Rows[0]["noLicense"].ToString() + "人";
                    //lblzhijie.Text = "直  接:" + dt.Rows[0]["zhijie"].ToString() + "人";
                    //int jianjie = int.Parse(dt.Rows[0]["jianjie1"].ToString()) + int.Parse(dt.Rows[0]["jianjie2"].ToString());
                    //lbljianjie.Text = "间  接:" + jianjie + "人";
                    //lbltiguan.Text = "已替关:" + dt.Rows[0]["tiguan"].ToString() + "人";
                    lblzhiyuanIn.Text = dt.Rows[0]["zhiyuanIn"].ToString();// +"人"; "支援派出:" + 
                    lblzhiyuanOut.Text = dt.Rows[0]["zhiyuanOut"].ToString();// + "人"; "支援接受:" + 
                }

                Stopwatch s2 = new Stopwatch();
                Program.logFlagStart(log, s2, "SQL 间  接: 2:");
                str_sql = string.Format(@"select count(UserID) from (select distinct a.UserID,a.JobForID,a.ProjectID,a.LineID,a.TeamID,a.myTeamName,a.GuanweiID,a.guanweisite from V_Attend_Result_Info a
                                            inner join  Produce_User b on a.UserID=b.UserID where myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120) and  b.AttendUnit='1.0' and  GuanweiTypeNM like '%间接人员%' ) m",
                                                    lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s2, "SQL 间  接: 2:");

                if (dt_temp.Rows.Count > 0)
                {
                    lbljianjie.Text = dt_temp.Rows[0][0].ToString();// +"人"; "间  接:" +
                }


                Stopwatch s3 = new Stopwatch();
                Program.logFlagStart(log, s3, "SQL 直  接: 3:");
                str_sql = string.Format(@"select count(UserID) from (select distinct a.UserID,a.JobForID,a.ProjectID,a.LineID,a.TeamID,a.myTeamName,a.GuanweiID,a.guanweisite from V_Attend_Result_Info a
                                            inner join  Produce_User b on a.UserID=b.UserID where myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120) and  b.AttendUnit='1.0' and  GuanweiTypeNM like '%直接人员%' ) m",
                                                   lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s3, "SQL 直  接: 3:");

                if (dt_temp.Rows.Count > 0)
                {

                    lblzhijie.Text = dt_temp.Rows[0][0].ToString();// + "人";"直  接:" + 
                }

                str_sql = string.Format(@"select count(AttendType) from V_Attend_Result_Info  where 1=1 and myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120)
                                            and AttendType like '离职%'",
                                                    lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    lblyudinglizhi.Text = dt_temp.Rows[0][0].ToString();//.Count.ToString();// + "人"; "预定离职:" + 
                }


                str_sql = string.Format(@"select top 1 AlredyReplaceJobsCnt from V_Attend_Sum where myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120)",
                                                    lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    lblchongdie.Text = dt_temp.Rows[0]["AlredyReplaceJobsCnt"].ToString();// + "人"; "已替关:" + 
                }


                Stopwatch s6 = new Stopwatch();
                Program.logFlagStart(log, s6, "SQL 重       叠: 6:");
//                str_sql = string.Format(@"select  ISNULL(sum(T.cc),0) as counts from 
//                                                (select A.JobForID,A.ProjectID,A.LineID,A.TeamID,A.GuanweiID,A.GuanweiNM,A.GuanweiSite,sum(1) as cc  from (select distinct * from V_User_TotalShow_Image)  as A
//                                                where  A.myTeamName='{0}' and A.AttendDate=CONVERT(VARCHAR(10),'{1}',120) 
//                                                group by A.JobForID,A.ProjectID,A.LineID,A.TeamID,A.GuanweiID,A.GuanweiNM,A.GuanweiSite having sum(1) > 1) as T",
                                                //lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                str_sql = string.Format(@"select  ISNULL(sum(T.cc),0) as counts from 
                                                (select GuanweiID,GuanweiSite,sum(1) as cc  from V_User_TotalShow_Image 
                                                where  myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120) group by GuanweiID,GuanweiSite having sum(1) > 1) as T ",
                                                lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());

                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s6, "SQL 重       叠: 6:");

                if (dt_temp.Rows.Count > 0)
                {
                    lblchongdie.Text = dt_temp.Rows[0]["counts"].ToString();// + "人"; "重       叠:" +
                }

                str_sql = string.Format(@" select ISNULL(sum(SetNum),0) as SetNumCount from V_Produce_Para_i where  myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120)
                                               and (GuanweiID<>'1' and GuanweiID<>'18' and GuanweiID<>'3' and GuanweiID<>'2') ",
                                            lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    int jianjie = 0;
                    if (dt.Rows.Count > 0)
                    {
                        jianjie = int.Parse(dt.Rows[0]["jianjie2"].ToString());
                    }
                    int sheding = int.Parse(dt_temp.Rows[0]["SetNumCount"].ToString()) - jianjie;
                    lblsheding.Text = sheding.ToString();// + "人";"生产设定:" + 
                }


                Stopwatch s7 = new Stopwatch();
                Program.logFlagStart(log, s7, "SQL 生产不足: 7:");
                str_sql = string.Format(@" select count(a.UserID) from V_Attend_Total_Result as a inner join Attend_Vacation as b on a.UserID=b.UserID
                                                 inner join Attend_NoAttend as c on a.UserID=c.UserID
                                                 inner join Attend_Leave as d on a.UserID=d.UserID where  myTeamName='{0}' and AttendDate=CONVERT(VARCHAR(10),'{1}',120) ",
                                            lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s7, "SQL 生产不足: 7:");


                if (dt_temp.Rows.Count > 0)
                {
                    lblbuzu.Text = dt_temp.Rows[0][0].ToString();//.Count.ToString();// + "人"; "生产不足:" + 
                }


                Stopwatch s8 = new Stopwatch();
                Program.logFlagStart(log, s8, "SQL 有免许: 8:");
                str_sql = string.Format(@"select count(UserID) from (select distinct a.UserID,a.UserNM,a.myTeamname from V_Attend_Total_Result a inner join
                                                dbo.V_License_Rec_i AS c ON a.UserID = c.UserID 
                                                 inner join  Produce_User b on a.UserID=b.UserID 
                                                and c.LicenseTypeName<>'申请中' and  b.AttendUnit='1.0'
                                                and a.myTeamName='{0}' and a.AttendDate=CONVERT(VARCHAR(10),'{1}',120)) m",
                                            lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s8, "SQL 有免许: 8:");


                if (dt_temp.Rows.Count > 0)
                {
                    lblhaveLicense.Text = dt_temp.Rows[0][0].ToString();//.Count.ToString();// +"人"; "有免许:" + 
                    LicenseCount = Int32.Parse(dt_temp.Rows[0][0].ToString());// dt_temp.Rows.Count;
                }


                Stopwatch s9 = new Stopwatch();
                Program.logFlagStart(log, s9, "SQL 在  籍: 9:");
                str_sql = string.Format(@"select count(UserID) from (select distinct a.UserID,a.UserNM,a.myTeamname from V_Attend_Total_Result a 
                                             inner join  Produce_User b on a.UserID=b.UserID 
                                            and  b.AttendUnit='1.0'
                                            and a.myTeamName='{0}' and a.AttendDate=CONVERT(VARCHAR(10),'{1}',120)) m",
                                            lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, s9, "SQL 在  籍: 9:");

                if (dt_temp.Rows.Count > 0)
                {
                    lblzaiji.Text = dt_temp.Rows[0][0].ToString();//.Count.ToString();// + "人";"在  籍:" + 
                }

                tmpW.Stop();
                var msg = "使用时间:" + tmpW.Elapsed.ToString();
                log.Info("人员揭示： 人员统计--》" + msg);
                //this.Text = "人员揭示： 人员统计--》" + msg;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                throw ex;
            }
        }

        /// <summary>
        /// 获取选中人员信息
        /// </summary>
        public void GetFormList(ref DataTable dt_temp)
        {
            try
            {
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
                            dt_temp.Rows.Add(view.ToTable().Rows[0].ItemArray);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("界面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }



        /// <summary>
        /// 用户控件名称取得
        /// </summary>
        /// bGWLsFlag:关位flage：true:关位，false：人员，人员空位
        private void setImgIndex(Boolean bGWLsFlag)
        {
            Stopwatch s = new Stopwatch();
            //工种显示场合
            if (bGWLsFlag)
            {
                //左侧展示关位（工种）
                if (wid_Left_Cnt > 0 && wid_Right_Cnt > 0)
                {
                    //新增一行
                    isLeftInto = true;//左侧显示
                    row_Cnt++;//当前行数+1
                    wid_Left_Cnt = 1;//行左侧控件数量
                    wid_Right_Cnt = 0;//行右侧控件数量

                    //上坐标=当前行数*控件高+控件间隙
                    lint_Top = row_Cnt * mGuanWeiHeight + widget_Cnt;
                    //左坐标=控件间隙
                    lint_Left = widget_Cnt;
                }
                else
                {
                    isLeftInto = false;
                    //X坐标=控件宽*4+控件间隙
                    lint_Left = mGuanWeiWidth * half_Cnt + widget_Cnt;
                    wid_Right_Cnt = 1;//行右侧控件数量

                }

            }
            else //同一工种，关位位置显示场合
            {
                //左侧显示场合,
                if (isLeftInto)
                {

                    //行左侧控件数量<5，左侧展示
                    if (wid_Left_Cnt < half_Cnt)
                    {
                        //左坐标=控件宽*行左侧控件数量+控件间隙
                        lint_Left = mGuanWeiWidth * wid_Left_Cnt + widget_Cnt;
                        wid_Left_Cnt++;
                    }
                    //行左侧控件数量>=5 ，右侧展示
                    else if (wid_Left_Cnt >= half_Cnt)
                    {
                        //左坐标=左侧4个控件宽度 + 控件宽*右侧控件数量+控件间隙
                        lint_Left = mGuanWeiWidth * half_Cnt + mGuanWeiWidth * wid_Right_Cnt + widget_Cnt;
                        isLeftInto = false;
                        wid_Right_Cnt = 1;//行右侧控件数量
                    }

                }
                else//右侧显示场合,
                {
                    //行右侧控件数量<5，右侧展示
                    if (wid_Right_Cnt < half_Cnt)
                    {
                        //左坐标=左侧4个控件宽度 + 控件宽*右侧控件数量+控件间隙
                        lint_Left = mGuanWeiWidth * half_Cnt + mGuanWeiWidth * wid_Right_Cnt + widget_Cnt;
                        wid_Right_Cnt++;
                    }
                    //行右侧控件数量>=4，下一行左侧展示
                    else if (wid_Right_Cnt >= half_Cnt)
                    {
                        //新增一行左侧展示
                        isLeftInto = true;//左侧显示
                        row_Cnt++;//当前行数+1
                        wid_Left_Cnt = 0;//行左侧控件数量
                        wid_Right_Cnt = 0;//行右侧控件数量

                        //上坐标=当前行数*控件高+控件间隙
                        lint_Top = row_Cnt * mGuanWeiHeight + widget_Cnt;
                        //左坐标=控件间隙
                        lint_Left = widget_Cnt;

                        wid_Left_Cnt++;
                    }
                }
            }

            //设置坐标
            _point = new Point(lint_Left + 2, lint_Top);
        }

        #endregion


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            System.Diagnostics.Process.Start("http://10.71.1.242:8082");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            System.Diagnostics.Process.Start("http://192.168.1.246:8082");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            btnRef.Enabled = true;
            btnConfirm.Enabled = true;
            btnProduce_TeamAttend.Enabled = true;
            button1.Enabled = true;
        }

        private void btnProduce_TeamAttend_MouseEnter(object sender, EventArgs e)
        {
            btnRef.Enabled = true;
            btnConfirm.Enabled = true;
            btnProduce_TeamAttend.Enabled = true;
            button1.Enabled = true;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            btnRef.Enabled = true;
            btnConfirm.Enabled = true;
            btnProduce_TeamAttend.Enabled = true;
            button1.Enabled = true;
        }

        private void frmProduce_UserTotalShow_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
        }

        private void frmProduce_UserTotalShow_Leave(object sender, EventArgs e)
        {
            this.TopMost = false;
        }
    }

    public class objCl
    {
        public Control cl { get; set; }
        public int gwNum { get; set; }
        public int UserPersonNum { get; set; }
        public int UserNullNum { get; set; }

        public int u_gwNum { get; set; }
        public int u_UserPersonNum { get; set; }
        public int u_UserNullNum { get; set; }

    }
}
