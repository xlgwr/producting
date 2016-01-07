using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MachineSystem.TabPage;
using Framework.Abstract;
using Framework.Libs;
using System.Collections.Specialized;
using DevExpress.Utils.About;
using DevExpress.XtraBars;
using System.Threading;
using System.Diagnostics;
using MachineSystem.SysDefine;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
using MachineSystem.form.Pad;
using log4net;

namespace MachineSystem.form.Master
{
    public partial class frmMenu : Framework.Abstract.frmBaseXC
    {
        #region 变量定义


        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmMenu));
        #endregion

        #region 属性设置

        #endregion

        #region 画面初始化

        public frmMenu()
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "主菜单 InitializeComponent：");
            InitializeComponent();
            Program.logFlagEnd(log, s, "主菜单 InitializeComponent：");
            this.Activated += new EventHandler(frmMenu_Activated);


            //============================/
            //========画面控件绑定=========/
            //============================/


            //============================/
            //========画面对象初始化=======/
            //============================/

        }

        void frmMenu_Activated(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            this.Refresh();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "主菜单 Load：");
            try
            {
                this.Text += " " + EnumDefine.VersionNo;
                if (CommParg.mfrmMenu == null)
                    CommParg.mfrmMenu = this;

                SetFormOpen();//显示窗体
                GetGetUserImageURL();

                Program.logFlagEnd(log, s, "主菜单 Load：");

                Application.DoEvents();

                if (Common._authornm != null)
                {
                    if (Common._authornm != "")
                    {
                        if (Common._authornm.IndexOf("班长", 0, Common._authornm.Length) >= 0)
                        {
                            //班长登录场合
                            frmProduce_UserTotalShow frm = new frmProduce_UserTotalShow(Common._myTeamName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));

                            frm.TopMost = true;
                            frm.Show();//打开平板人员揭示
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }

        }

        private void SetFormOpen()
        {
            ArrayList _al = new ArrayList();
            string _frmname;
            try
            {
                Common._rwconfig._Path = Application.StartupPath + @"\" + Common._userInfo;
                _al = Common._rwconfig.ReadKeys(SysParam.StartupModule, Common._rwconfig._Path);
                for (int i = 0; i < _al.Count; i++)
                {
                    _frmname = _al[i].ToString();
                    ShowForm(_frmname);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion

        #region 按钮单击事件

        /// <summary>
        /// 皮肤选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void itemStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.Skins.SkinManager.Default.Skins[(int)e.Item.Tag].SkinName);
            Common._sysrun.FormSkin = (Common.FormSkin)((int)e.Item.Tag);

        }


        /// <summary>
        /// 工具菜单单击事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTool_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (e.Item.Tag != null)
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;



                //退出按钮
                if (e.Link.Item.Tag.ToString() == "Exit")
                {
                    //common._its_accessdb = null;
                    if (XtraMsgBox.Show("[" + this.Text + "]?", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;

                    this.Close();
                    return;
                }

                //安装按钮处理
                if (e.Link.Item.Tag.ToString() == "frmSetup")
                {
                    frmSetting frmSetup = new frmSetting();
                    frmSetup.ShowDialog();
                    frmSetup = null;
                    return;
                }


                //关于

                if (e.Link.Item.Tag.ToString() == "frmAbout")
                {
                    return;
                }

                //帮助文档
                if (e.Link.Item.Tag.ToString().IndexOf("help") >= 0)
                {
                    try
                    {

                        System.Diagnostics.Process process = new System.Diagnostics.Process();
                        process.StartInfo.FileName = Common.GetSolutionPath(Application.StartupPath) + e.Link.Item.Tag.ToString() + ".pdf";
                        process.StartInfo.Verb = "Open";
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        process.Start();
                        return;
                    }
                    catch (Exception ex)
                    {
                        XtraMsgBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (!CheckExistsForm(e.Item.Tag.ToString().ToString()))
                {
                    ShowForm(e.Link.Item.Tag.ToString());
                }

                Cursor.Current = currentCursor;
            }

        }

        /// <summary>
        /// 从系统中注销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barLogOnSys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Common._authorid = null;
                Common._authornm = null;

                Common._personid = null;
                Common._personname = null;
                Common._personpswd = null;

                Common._myTeamName = "";
                Common._orgTeamName = "";
                Common._JobForId = "";
                Common._JobForName = "";
                Common._LineId = "";
                Common._LineName = "";
                Common._ProjectId = "";
                Common._ProjectName = "";
                Common._TeamId = "";
                Common._TeamName = "";


                this.Dispose();

                frmMenu frmmenu = new frmMenu();
                frmLogin frmLog = new frmLogin(ref frmmenu);
                frmLog.ShowDialog();

                if (frmLog.DialogResult == DialogResult.OK)
                {
                    frmLog.Dispose();
                    frmLog = null;

                    frmmenu.ShowDialog();
                    GC.Collect();

                    Application.Exit();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }

        }

        private void ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (e.Item.Tag != null)
                {
                    Cursor currentCursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    //人员揭示
                    if (e.Item.Tag.ToString().ToLower() == "usertotalshow")
                    {
                        if (Program._frmProduce_UserTotalShow != null)
                        {
                            Program._frmProduce_UserTotalShow.TopMost = true;
                            Program._frmProduce_UserTotalShow.Show();
                        }
                        else
                        {
                            frmProduce_UserTotalShow frm = new frmProduce_UserTotalShow("", "");
                            frm.Show();
                        }

                        return;
                    }
                    else
                    {
                        if (Program._frmProduce_UserTotalShow != null)
                        {
                            Program._frmProduce_UserTotalShow.TopMost = false;
                        }
                    }
                    if (Program._frmProduce_TeamAttend != null)
                    {
                        Program._frmProduce_TeamAttend.TopMost = false;
                    }
                    if (Program._frmProduce_TeamChange != null)
                    {
                        Program._frmProduce_TeamChange.TopMost = false;
                    }
                    //
                    if (e.Link.Item.Tag.ToString() == "Exit")
                    {
                        //common._its_accessdb = null;
                        this.Close();
                        return;
                    }
                    //关于
                    if (e.Item.Tag.ToString().ToLower() == "about")
                    {
                        MachineSystem.TabPage.frmAbout frm = new MachineSystem.TabPage.frmAbout();
                        frm.ShowDialog();
                        return;
                    }


                    if (!CheckExistsForm(e.Item.Tag.ToString().ToString()))
                    {
                        ShowForm(e.Link.Item.Tag.ToString());
                    }

                    Cursor.Current = currentCursor;
                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        #region 处理方法

        /// <summary>
        /// 设置画面控件数据
        /// </summary>

        public override void SetFormValue()
        {
            base.SetFormValue();

            int i = 0;

            //系统状态信息设置
            barSAuthor.Caption = Common._authornm;
            barSUser.Caption = Common._personname;
            barSFactName.Caption = Common._ProjectName;
            barSUser.Caption = Common._personname;

            if (!Common._personid.Equals(Common._Administrator))
            {
                //SetMenuItemValid();

                //读取用户权限信息
                GetAuthorData();
                this.SetVisibility();
            }


            foreach (DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins)
            {

                DevExpress.XtraBars.BarButtonItem itemStyle = new DevExpress.XtraBars.BarButtonItem();
                itemStyle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(itemStyle_ItemClick);
                itemStyle.Caption = skin.SkinName;
                itemStyle.Tag = i;
                btnStyle.AddItem(itemStyle);
                i++;
            }
        }
        #region 读取权限信息

        /// <summary>
        /// 读取用户权限信息
        /// </summary>
        private void GetAuthorData()
        {
            int index = 0;

            DataTable dtRoleInfo;
            StringDictionary dicItemData;
            StringDictionary dicConds;
            StringDictionary dicLikeConds;

            string w_FrmId;

            try
            {

                Common._dicRoleInfo = new StringDictionary();

                //===================================/
                //==========获取权限信息=============/
                //===================================/
                Common._dicRoleInfo = new StringDictionary();


                string str_sql = string.Format(@"select a.*, c.StatusID from Oper_Role_Permissions a
                                                    inner join Oper_User_Role b on a.RoleID= b.RoleID
                                                    inner join Oper_Info c on b.UserID=c.OperID
                                                     where a.RoleID='{0}'", Common._authorid);
                dtRoleInfo = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                if (dtRoleInfo != null && dtRoleInfo.Rows.Count > 0)
                {
                    for (index = 0; index < dtRoleInfo.Rows.Count; index++)
                    {
                        w_FrmId = dtRoleInfo.Rows[index]["pFromName"].ToString();
                        Common._dicRoleInfo[w_FrmId] = dtRoleInfo.Rows[index]["StatusID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //LOG006=获取权限数据失败，请检查！

                XtraMsgBox.Show("获取权限数据失败，请检查！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        public void SetVisibility()
        {
            //判断子菜单是否显示
            bool IsVisible = false;
            try
            {
                //主菜单显示处理
                if (Common._Administrator.Equals(Common._personid))
                {
                    this.barSubItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem23.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem24.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    this.barSubItem28.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem29.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.barSubItem27.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barSubItem33.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    ////设置画面使用权限信息
                    foreach (DevExpress.XtraBars.LinkPersistInfo item in this.bar1.LinksPersistInfo)
                    {
                        //帮助全部显示
                        if (item.Item.Name.ToString() == "barSubItem50" || item.Item.Name.ToString() == "barButtonItem266") continue;

                        showSub(item.Item, out  IsVisible);
                        item.Visible = IsVisible;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void showSub(DevExpress.XtraBars.BarItem ctl, out  bool IsVisible)
        {
            IsVisible = false;
            try
            {
                if (ctl.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem")
                {
                    string strForm = ctl.Tag.ToString() + ".cs";
                    if (Common._dicRoleInfo.ContainsKey(strForm))
                    {
                        string str = Common._dicRoleInfo[strForm];
                        if (Common._dicRoleInfo[strForm] == "1")
                        {
                            ctl.Visibility = BarItemVisibility.Always;
                            IsVisible = true;
                        }
                        else
                        {
                            ctl.Visibility = BarItemVisibility.Never;
                        }
                    }
                    else
                    {

                        ctl.Visibility = BarItemVisibility.Never;
                    }
                }
                if (ctl.GetType().ToString() == "DevExpress.XtraBars.BarSubItem")
                {
                    DevExpress.XtraBars.BarSubItem ct = (DevExpress.XtraBars.BarSubItem)ctl;
                    foreach (DevExpress.XtraBars.LinkPersistInfo item in ct.LinksPersistInfo)
                    {
                        bool TempVisible = false;
                        showSub(item.Item, out TempVisible);
                        item.Visible = TempVisible;
                        //用或运算将值传给上成BarSubItem
                        IsVisible = IsVisible || TempVisible;
                    }
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public bool CheckExistsForm(string frmName)
        {
            foreach (System.Windows.Forms.Form Childfrm in this.MdiChildren)
            {
                if (frmName == Childfrm.Name)
                {
                    if (Childfrm.WindowState != FormWindowState.Maximized)
                    {
                        Childfrm.WindowState = FormWindowState.Maximized;
                    }

                    //设置为选择画面
                    Childfrm.Activate();

                    return true;
                }


            }
            return false;
        }

        public bool CheckExistsForm(string frmName, ref System.Windows.Forms.Form frm)
        {
            foreach (System.Windows.Forms.Form Childfrm in this.MdiChildren)
            {
                if (frmName == Childfrm.Name)
                {
                    if (Childfrm.WindowState == FormWindowState.Minimized)
                    {
                        Childfrm.WindowState = FormWindowState.Maximized;
                    }
                    //设置为选择画面
                    Childfrm.Activate();

                    frm = Childfrm;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取人员头像文件夹路径
        /// </summary>
        public void GetGetUserImageURL()
        {
            try
            {
                string _sql = string.Format("select * from P_System_Para ");
                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);

                DataView view = new DataView(dt_temp.Copy());
                view.RowFilter = "pIndex='1'";
                Common.EmPathDir = view.ToTable().Rows[0]["pName"].ToString();//人事系统头像共享文件夹-办公

                view = new DataView(dt_temp.Copy());
                view.RowFilter = "pIndex='3'";
                Common.EmHttpIpWork = view.ToTable().Rows[0]["pName"].ToString();//考勤系统ip（车间）

                view = new DataView(dt_temp.Copy());
                view.RowFilter = "pIndex='4'";
                Common.EmHttpIpRoom = view.ToTable().Rows[0]["pName"].ToString();//考勤系统ip（办公室）

                view = new DataView(dt_temp.Copy());
                view.RowFilter = "pIndex='2'";
                Common.AtPathDir = view.ToTable().Rows[0]["pName"].ToString();//考勤系统头像目录


                view = new DataView(dt_temp.Copy());
                view.RowFilter = "pIndex='5'";
                Common.EmPathDir2 = view.ToTable().Rows[0]["pName"].ToString();//人事系统头像共享文件夹-车间
            }
            catch (Exception e)
            {
                XtraMsgBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowForm(string frmName)
        {
            System.Windows.Forms.Form frmNew = null;


            try
            {
                Type Dlg = Type.GetType("MachineSystem.TabPage." + frmName);

                frmNew = (Form)Dlg.InvokeMember(null,
                   System.Reflection.BindingFlags.CreateInstance,
                   null, null, null);

                frmNew.MdiParent = this;
                frmNew.Name = frmName;

                frmNew.Show();

            }
            catch (Exception e)
            {
                XtraMsgBox.Show(e.Message, e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowForm(string frmName, ref System.Windows.Forms.Form frm)
        {
            System.Windows.Forms.Form frmNew = null;


            try
            {
                Type Dlg = Type.GetType("MachineSystem.TabPage." + frmName);

                frmNew = (Form)Dlg.InvokeMember(null,
                   System.Reflection.BindingFlags.CreateInstance,
                   null, null, null);

                frmNew.MdiParent = this;
                frmNew.Name = frmName;
                frm = frmNew;
                frmNew.Show();


            }
            catch (Exception e)
            {
                XtraMsgBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void barButtonItem175_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = this.MdiChildren.Length - 1; i >= 0; i--)
            {
                this.MdiChildren[i].Close();
            }
        }

    }

}
