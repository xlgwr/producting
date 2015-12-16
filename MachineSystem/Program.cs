using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using Framework.Libs;
using Framework.FileOperate;
using MachineSystem.TabPage;
using MachineSystem.form.Master;
using MachineSystem.SysDefine;
using System.Net;
using System.Xml;
using Framework.Abstract;
using System.Collections.Specialized;
using MachineSystem.form.Pad;
using Framework.DataAccess;
using System.Drawing;
using log4net;

namespace MachineSystem
{
    static class Program
    {
        #region add by xlg
        public static Dictionary<string, Image> _dicCheckImage = new Dictionary<string, Image>();
        public static frmMenu _frmMain = null;
        public static frmProduce_UserTotalShow _frmProduce_UserTotalShow = null;
        public static frmProduce_TeamAttend _frmProduce_TeamAttend = null;
        public static frmProduce_TeamChange _frmProduce_TeamChange = null;
        public static Control[] _listCtro;
        public static objCl _objCl;
        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));
        #endregion
        /// <summary>
        /// 应用程序的主入口点。

        /// </summary>
        [STAThread]
        static void Main()
        {
            Stopwatch s = new Stopwatch();
            logFlagStart(log, s, "Main:主程序。");

            int processCount = 0;
            Process[] pa = Process.GetProcesses();//获取当前进程数组。

            foreach (Process PTest in pa)
            {
                if (PTest.ProcessName == Process.GetCurrentProcess().ProcessName)
                {
                    processCount += 1;
                }
            }

            if (processCount > 1)
            {

                //如果程序已经运行，则给出提示。并退出本进程。

                DialogResult dr;
                dr = DevExpress.XtraEditors.XtraMessageBox.Show(Process.GetCurrentProcess().ProcessName + "程序已经在运行！", "退出程序", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; //Exit;

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Encrypt.Common.IV_64 = "RIFIDApp";
            Encrypt.Common.KEY_64 = "RIFIDApp";

            //读取系统配置信息(初始化运行 frmSetting 需注释)
            Common.GetIniData();

            //皮肤设定
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");

            DevExpress.Skins.SkinManager.EnableMdiFormSkins();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.Skins.FormSkins).Assembly);
           
            
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();

            Application.EnableVisualStyles();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.Skins.SkinManager.Default.Skins[Convert.ToInt32(Common._sysrun.FormSkin)].SkinName);
            Common._sysrun.EditColumnBackColor = System.Drawing.Color.Orange;

            EnumDefine.VersionNos = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            EnumDefine.StartupPath = Application.StartupPath;

            //==================================================/
            //================获取共通多语言信息=================/
            //==================================================/

            //如果不需要使用皮肤
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetFlatStyle();

            //初始化参数
            //1.如果要运行frmSetting，则只需要以下二句，注意要将Common类的subGetIniData方法中的写上即可
            //2.将frmSetting窗体中的frmSetting_Load()这一句加上 this._sysrun = new SysRun();;
            //Common._rwconfig = new RWConfig(Common.GetSolutionPath(Application.StartupPath) + @"Language\chinese.ini");
            //Application.Run(new frmSetting());


            /*

            try
            {

                EnumDefine.VersionNos = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                //=============版本更新确认======================
                //string mainAppExe = Application.StartupPath + "\\AutoUpdate_App.exe";
                string localXmlFile = Application.StartupPath + "\\UpdateList.xml";

                //从本地读取更新配置文件信息
                XmlFiles updaterXmlFiles = new XmlFiles(localXmlFile);

                string tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                string UpdaterUrl = updaterXmlFiles.GetNodeValue("//Url") + "UpdateList.xml";

                //返回下载更新文件的临时目录
                DownAutoUpdateFile(tempUpdatePath, UpdaterUrl);

                string serverXmlFile = tempUpdatePath + "\\UpdateList.xml";

                if (System.IO.File.Exists(serverXmlFile))
                {

                    updaterXmlFiles = new XmlFiles(serverXmlFile);

                    XmlNodeList newNodeList = updaterXmlFiles.GetNodeList("AutoUpdater/Application/Version");

                    ////删除以前下载的临时文件
                    if (System.IO.Directory.Exists(serverXmlFile))
                    {
                        System.IO.Directory.Delete(serverXmlFile, true);
                    }

                    updaterXmlFiles = null;

                    if (newNodeList != null && newNodeList.Count > 0)
                    {
                        string newVer = newNodeList.Item(0).Value;

                        if (!newVer.Equals(EnumDefine.VersionNos))
                        {

                            if (XtraMsgBox.Show("检测到该软件有更新版本，确定要更新吗？", Common._SysName,
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {

                                Application.Exit();

                                XtraMsgBox.Show("请点击“成型机生产管理系统”获取最新版本！", Common._SysName,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return;
                            }
                        }
                    }

                }

            }
            catch (Exception)
            {

            }

            //*/
            logFlagEnd(log, s, "Main:主程序。");
            try
            {
                logFlagStart(log, s, "Main:主程序-->初始化 总时间。");
                SysParam.m_daoCommon = new daoCommon();


                frmMenu frmmenu = new frmMenu();

                frmLogin frmLog = new frmLogin(ref frmmenu);

                logFlagEnd(log, s, "Main:主程序-->初始化 总时间。");


                frmLog.ShowDialog();
                if (frmLog.DialogResult == DialogResult.OK)
                {
                    frmLog.Dispose();
                    frmLog = null;

                    //frmmenu.ShowDialog();
                    GC.Collect();

                    //if (UHFReader.UHFReader.GetInstance().isComOpen)
                    //{
                    //    UHFReader.UHFReader.GetInstance().SetClosePort();
                    //}

                    Application.Exit();
                }
                else
                {
                    frmLog.Dispose();
                    frmLog = null;
                    GC.Collect();

                    //if (UHFReader.UHFReader.GetInstance().isComOpen)
                    //{
                    //    UHFReader.UHFReader.GetInstance().SetClosePort ();
                    //}

                    Application.Exit();
                }


            }
            catch (Exception)
            {


            }
        }

        /// <summary>
        /// 返回下载更新文件的临时目录
        /// </summary>
        /// <returns></returns>
        public static void DownAutoUpdateFile(string downpath, string UpdaterUrl)
        {
            if (!System.IO.Directory.Exists(downpath))
                System.IO.Directory.CreateDirectory(downpath);
            string serverXmlFile = downpath + @"/UpdateList.xml";

            try
            {

                WebRequest req = WebRequest.Create(UpdaterUrl);
                WebResponse res = req.GetResponse();
                if (res.ContentLength > 0)
                {
                    WebClient wClient = new WebClient();
                    wClient.DownloadFile(UpdaterUrl, serverXmlFile);
                }
            }
            catch (Exception)
            {

                ////删除以前下载的临时文件
                if (System.IO.Directory.Exists(serverXmlFile))
                {
                    System.IO.Directory.Delete(serverXmlFile, true);
                }

                throw;
            }

            //return tempPath;
        }

        static void timer_Tick(object sender, EventArgs e)
        {
            //if (m_frmmenu == null)
            //{

            //    Form f = new Form();
            //    f.IsMdiContainer = true;
            //    m_frmmenu = new frmMenu();
            //    f.Visible = false;

            //    frmNavigation frmNavi = new frmNavigation();
            //    frmNavi.MdiParent = m_frmmenu;

            //    frmNavi.Show();
            //    m_frmmenu.Size = new System.Drawing.Size(0, 0);
            //    m_frmmenu.ShowInTaskbar = false;
            //    m_frmmenu.FormBorderStyle = FormBorderStyle.None;
            //    m_frmmenu.SendToBack();
            //    //frmmenu.Show();
            //    //m_frmmenu.Dispose();
            //}

            (sender as Timer).Stop();

        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
            // here you can log the exception ...
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            // here you can log the exception ...
        }
        public static void logFlagStart(ILog log, Stopwatch start, string msg)
        {
            start.Start();
            log.Info("14开始运行：" + msg);
        }
        public static void logFlagEnd(ILog log, Stopwatch end, string msg)
        {
            end.Stop();
            log.Info("14结束运行：" + msg + ",使用时间：" + end.Elapsed.ToString());
            end.Reset();
        }
    }
}