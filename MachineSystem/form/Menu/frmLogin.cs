using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Framework.Libs;
using Framework.DataAccess;
using MachineSystem.SysDefine;
using Framework.Abstract;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Net;
using System.Runtime.InteropServices;
using MachineSystem.TabPage;
using MachineSystem.form.Pad;
using log4net;

namespace MachineSystem.form.Master
{
    public partial class frmLogin : Framework.Abstract.frmBaseXC
    {

        #region 变量定义
        private string m_strHistoryDBName = "RFIDDelivHistoryDB";
        private string m_strDeliveryDBName;

        private string m_strFactoryNo = string.Empty;
        private frmMenu m_frmMainMenu;

        public string m_IP = string.Empty;
        public string m_MAC = string.Empty;
        public string m_PCname = string.Empty;


        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lparam);

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmLogin));
        #endregion

        #region 画面初始化

        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="frmmneu"></param>
        public frmLogin(ref frmMenu frmMainMenu)
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "1.Login InitializeComponent：");

            InitializeComponent();

            //============================/
            //========画面控件绑定=========/
            //============================/


            //============================/
            //========画面对象初始化=======/
            //============================/
            m_frmMainMenu = frmMainMenu;

            this.m_dicConds["UserID"] = "true";
            this.m_dicConds["UserName"] = "true";

            this.m_strDeliveryDBName = Common._sysrun.DataBaseName;

            //设定验证条件
            SetValidCondition();

            Program.logFlagEnd(log, s, "1.Login InitializeComponent：");

        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "1.Login Load：");

            //窗体初始化处理
            SetFormValue();


            this.lblVersion.Text = "Developer: ISEC Co.,Ltd. Shenzhen Branch Ver " + EnumDefine.VersionNo;

            this.lblVersion.Text = "Copyright ©2003-2015 深圳市艾赛克科技有限公司   版权所有 ";

            //string strWireIP = string.Empty, strWireLessIP = string.Empty, strWireMAC = string.Empty, strWireLessMAC = string.Empty;
            //GetShowAdapterInfo(ref strWireLessIP, ref strWireIP, ref strWireMAC, ref strWireLessMAC);
            ////有线+无线
            //m_IP = strWireIP + strWireLessIP;
            //m_MAC = strWireMAC + strWireLessMAC;
            Program.logFlagEnd(log, s, "1.Login Load：");
        }

        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            try
            {
                base.SetFormValue();

                //是否有记住用户名和密码
                Common._rwconfig._Path = Application.StartupPath + @"\" + Common._userInfo;
                string str_userNo = Common._rwconfig.ReadTextFile("LOGIN", "UserID");
                string str_pwd = Common._rwconfig.ReadTextFile("LOGIN", "Pwd");
                if (str_userNo != "" && str_pwd != "")
                {
                    txtUser.Text = str_userNo;
                    txtPaswd.Text = Encrypt.Common.UserDecryptPassWord(str_pwd);
                    checkBox1.Checked = true;
                }

                //if (Common._rwconfig.ReadTextFile("IP", "erpIp") == "")
                //{
                //    Common._rwconfig.WriteTextFile("IP", "erpIp", "192.168.0.252");
                //}

                //系统数据初始化处理
                Common.AdoConnect = new dbaFactory();
                Common.AdoConnect.Connect.SetParameter();

                ////画面控件窗体初始化
                Common.SetContorlFocus(this.txtUser);
                try
                {
                    //AutoUpdate.AppUpdater ft = new AutoUpdate.AppUpdater();
                    //if (ft.CheckForUpdate() > 0)
                    //{
                    //    string filename = "AutoUpdate_App.exe";
                    //    //更新文件与程序文件所在一个目录

                    //    System.Diagnostics.Process Proc;
                    //    Proc = new Process();
                    //    Proc.StartInfo.FileName = filename;
                    //    Proc.Start();

                    //    Application.ExitThread();
                    //}
                }
                catch (Exception ex)
                {
                    XtraMsgBox.Show("登录初始化失败，请检查！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());

                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("登录初始化失败，请检查！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 按钮单击事件

        private void timerForm_Tick(object sender, EventArgs e)
        {
            //iCnt += 1;
            //Application.DoEvents();
            //this.pBarLoad.Text = iCnt.ToString();
            //if (iCnt > 100) timerForm.Stop();
        }

        /// <summary>
        /// 登录按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                //窗体初始化处理
                //人员登录场合

                if (!this.GetUserLogin())
                {
                    return;
                }

                m_frmMainMenu.SetFormValue();//打开菜单
                Program._frmMain = m_frmMainMenu;
                this.Hide();
                m_frmMainMenu.ShowDialog();
                this.DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 取消按钮处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void chkHistory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!chkHistory.Checked)
                {
                    Common._sysrun.DataBaseName = this.m_strDeliveryDBName;
                }
                else
                {
                    Common._sysrun.DataBaseName = this.m_strHistoryDBName;
                }

                //系统数据初始化处理
                Common.AdoConnect = new dbaFactory();
                Common.AdoConnect.Connect.SetParameter();

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 处理方法


        #region 登录处理设置

        /// <summary>
        /// 用户登录处理
        /// </summary>
        /// <returns></returns>
        public bool GetUserLogin()
        {
            Stopwatch s=new Stopwatch();
            Program.logFlagStart(log, s, "用户登录处理：总时间");
            bool RtnValue = false;
            DataTable dtUser = null;
            StringDictionary m_dic = new StringDictionary();
            DataTable dt_temp = new DataTable();

            try
            {


                Stopwatch sq = new Stopwatch();
                Program.logFlagStart(log, sq, "用户登录 验证输入.");
                if (!this.validData.Validate()) return false;

                //获取画面控件数据
                GetGrpDataItem();
                Program.logFlagEnd(log, sq, "用户登录 验证输入.");


                Stopwatch sqlw = new Stopwatch();
                Program.logFlagStart(log, sqlw, "用户登录处理SQL.");

                string str_sql = string.Empty;
                if (txtUser.Text.Trim().Equals(Common._Administrator))
                {
                    str_sql = string.Format(@"select a.*,a.OperID as UserID from Oper_Info a where OperID='admin'  and Pwd='{0}'", txtPaswd.Text.Trim());
                }
                else
                {
                    str_sql = string.Format(@"select a.*,b.UserName,b.JobForID,b.JobForName,b.ProjectID,b.ProjectName,
                                                        b.LineID,b.LineName,b.TeamID,b.TeamName,b.myTeamName,b.orgTeamName,
                                                        c.StatusID, c.TypeID,c.Pwd 
                                                from Oper_User_Role a 
                                                        inner join  V_Produce_User  b  on a.UserID=b.UserID
                                                        inner join Oper_Info c         on a.UserID=c.OperID 
                                                where 
                                                        a.UserID='{0}' and c.Pwd='{1}'",
                                            txtUser.Text.Trim(), txtPaswd.Text.Trim());
                }

                dtUser = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Program.logFlagEnd(log, sqlw, "用户登录处理SQL.");


                Stopwatch sa = new Stopwatch();
                Program.logFlagStart(log, sa, "用户登录 其它.");

                if (this.m_dicItemData["UserID"].Equals(Common._Administrator))
                {
                    Common._personid = Common._Administrator;
                }
                else if (dtUser == null || dtUser.Rows.Count <= 0)
                {
                    XtraMsgBox.Show("用户编号或密码输入错误，请检查！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else
                {
                    if (dtUser.Rows[0]["StatusID"].ToString() != "1")
                    {
                        XtraMsgBox.Show("该用户已被禁用，不能登录系统！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    if (!this.m_dicItemData["UserID"].Equals(Common._Administrator))
                    {
                        Common._authorid = dtUser.Rows[0]["RoleID"].ToString();
                        Common._authornm = dtUser.Rows[0]["ReMark"].ToString();
                        Common._myTeamName = dtUser.Rows[0]["myTeamName"].ToString();
                        Common._orgTeamName = dtUser.Rows[0]["orgTeamName"].ToString();
                        Common._JobForId = dtUser.Rows[0]["JobForId"].ToString();
                        Common._JobForName = dtUser.Rows[0]["JobForName"].ToString();
                        Common._LineId = dtUser.Rows[0]["LineId"].ToString();
                        Common._LineName = dtUser.Rows[0]["LineName"].ToString();
                        Common._ProjectId = dtUser.Rows[0]["ProjectId"].ToString();
                        Common._ProjectName = dtUser.Rows[0]["ProjectName"].ToString();
                        Common._TeamId = dtUser.Rows[0]["TeamId"].ToString();
                        Common._TeamName = dtUser.Rows[0]["TeamName"].ToString();
                    }
                    Common._isHistory = this.chkHistory.Checked;
                    Common._personid = dtUser.Rows[0]["UserID"].ToString();
                    Common._personname = dtUser.Rows[0]["UserName"].ToString();
                    Common._personpswd = dtUser.Rows[0]["Pwd"].ToString();
                    //Common._m_M_Parameter = SysParam.m_daoCommon.GetTableInfo(MasterTable.M_Parameter.TableName, m_dic, m_dic, m_dic, "");

                    //保存登录账号和密码
                    if (checkBox1.Checked)
                    {
                        Common._rwconfig._Path = Application.StartupPath + @"\" + Common._userInfo;
                        Common._rwconfig.WriteTextFile("LOGIN", "UserID", m_dicItemData["UserID"].ToString());
                        Common._rwconfig.WriteTextFile("LOGIN", "Pwd", m_dicItemData["Pwd"].ToString());
                    }
                    else
                    {
                        Common._rwconfig._Path = Application.StartupPath + @"\" + Common._userInfo;
                        Common._rwconfig.WriteTextFile("LOGIN", "UserID", "");
                        Common._rwconfig.WriteTextFile("LOGIN", "Pwd", "");
                    }
                }
                Program.logFlagEnd(log, sa, "用户登录 其它.");
                RtnValue = true;

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("登录失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                log.Error(ex);
                return false;

            }
            Program.logFlagEnd(log, s, "用户登录处理:总时间");
            return RtnValue;
        }



        /// <summary>
        /// 设定验证条件
        /// </summary>
        protected override void SetValidCondition()
        {
            DataValid.SetControlValidBlank(this.validData, this.txtUser, "用户编号不能为空，请输入！");
            DataValid.SetControlValidBlank(this.validData, this.txtPaswd, "用户口令不能为空，请输入！");
        }

        /// <summary>
        /// 获取画面控件数据
        /// </summary>
        protected override void GetGrpDataItem()
        {
            try
            {
                this.m_dicItemData["UserID"] = this.txtUser.Text.Trim();

                //数据加密处理
                this.m_dicItemData["Pwd"] = Encrypt.Common.UserEncryptPassWord(this.txtPaswd.Text);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.O || e.KeyChar == (char)Keys.Enter)
                {
                    btnConfirm_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// 获取系统Ip地址
        /// </summary>
        /// <param name="wireLessIp"></param>
        /// <param name="wireIp"></param>
        public void GetShowAdapterInfo(ref string wireLessIp, ref string wireIp, ref string wireMAC, ref string wireLessMAC)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            int index = 0;
            IPInterfaceProperties ip;

            foreach (NetworkInterface adapter in adapters)
            {
                index++;
                IPInterfaceProperties IPInterfaceProperties = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = IPInterfaceProperties.UnicastAddresses;
                foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                {
                    switch (adapter.NetworkInterfaceType)
                    {
                        case NetworkInterfaceType.Wireless80211:

                            if (UnicastIPAddressInformation.Address.IsIPv6LinkLocal == false)
                            {
                                wireLessIp = UnicastIPAddressInformation.Address.ToString();
                                wireLessMAC = adapter.GetPhysicalAddress().ToString();
                            }

                            break;
                        case NetworkInterfaceType.Ethernet:
                            if (UnicastIPAddressInformation.Address.IsIPv6LinkLocal == false)
                            {
                                wireIp = UnicastIPAddressInformation.Address.ToString();
                                wireMAC += adapter.GetPhysicalAddress().ToString();
                            }

                            break;
                    }
                }
            }
        }

        #endregion

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnCancel.Image = MachineSystem.Properties.Resources.X2;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnCancel.Image = MachineSystem.Properties.Resources.X;
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

    }
}
