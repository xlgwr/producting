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
using System.Data.SqlClient;
using MachineSystem.form.Search;
using System.IO;
using MachineSystem.form.ParaLicense;
using System.Collections.Specialized;
using MachineSystem.SysCommon;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
/********************************************************************************
** 作者： libing
** 创始时间：2015-6-8

** 修改人：xuensheng
** 修改时间：2015-8-4
** 修改内容：代码规范化

** 描述：
**    主要用于平板端的人员调动操作
 *     人员调入：未定义 ->  考勤
 *     人员调出：考勤 ->  未定义
 *     支援调入：支援 ->  考勤
 *     支援调出：考勤 ->  支援
 *     替关调整：替关者 ->  关位
 *     关位调整：关位 ->  关位
*********************************************************************************/
namespace MachineSystem.form.Pad
{
    public partial class frmProduce_TeamChange : Framework.Abstract.frmBaseXC
    {
        #region 变量定义
        public static Control[] _lstContr2;
        public static objCl _objcl2;

        bool isEditValue { get; set; }
        bool isRun { get; set; }
        /// <summary>
        /// 人员数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// Line别数据表
        /// </summary>
        DataTable m_tblLineList = new DataTable();

        /// <summary>
        ///未定义数据表
        /// </summary>
        DataTable m_tblProduceList = new DataTable();

        /// <summary>
        /// 关位信息数据表
        /// </summary>
        DataTable m_tblGuanweiList = new DataTable();

        /// <summary>
        /// 支援人员数据表
        /// </summary>
        DataTable m_tblSupportList = new DataTable();

        /// <summary>
        /// 根据向别-班别查出JobForID，ProjectID，LineID，TeamID
        /// </summary>
        DataTable m_tblAllList = new DataTable();

        /// <summary>
        /// 是否在左边panel中加入控件
        /// </summary>
        bool isLeftInto = false;

        /// <summary>
        /// 界面初始
        /// </summary>
        bool isLoad = false;

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
        ///From人员信息
        /// </summary>
        UserPerson moveFromPerson;


        /// <summary>
        ///To人员信息
        /// </summary>
        UserPerson moveToPerson;
        /// <summary>
        ///To人员空位信息
        /// </summary>
        UserPersonNull moveToPersonNull;
        /// <summary>
        ///To工种信息
        /// </summary>
        UserPersonsList moveToPersonList;
        /// <summary>
        ///moveToPersonFlg:
        ///1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
        /// </summary>
        string moveToPersonFlg;

        /// <summary>
        /// 参数：向别
        /// </summary>
        public string parMyteamName;

        /// <summary>
        /// 参数：日期
        /// </summary>
        public string parDdateOperDate;

        /// <summary>
        ///未定义人员颜色
        /// </summary>
        string YellowColor = "yellow";//
        string GreenColor = "green";//

        /// <summary>
        /////From选择类型;
        /// 1：【考勤】工种；  2：【考勤】（标配）人员；  
        /// 3：【考勤】（标配以外）人员； 4：【考勤】（空位）人员
        /// 5：【未定义】人员； 6：【未定义】（空位）人员
        /// 7：【支援】人员； 8：【支援】（空位）人员
        /// </summary>
        int strFromSelFlg = 0;

        /// <summary>
        /////To选择类型;
        /// 1：【考勤】工种；  2：【考勤】（标配）人员；  
        /// 3：【考勤】（标配以外）人员； 4：【考勤】（空位）人员
        /// 5：【未定义】人员； 6：【未定义】（空位）人员
        /// 7：【支援】人员； 8：【支援】（空位）人员
        /// </summary>
        int strToSelFlg = 0;

        ////1：【考勤】工种；  2：【考勤】（标配）人员；  3：【考勤】（标配以外）人员； 4：【考勤】（空位）人员
        ///// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
        int selectFlg = 0;

        ////1：替关者；2：非替关者；3：支援者
        int userTypeFlg = 0;

        /// <summary>
        ///【考勤】工种可选flg
        /// </summary>
        Boolean bPlistFlg = false;

        /// <summary>
        ///【考勤】关位可选flg
        /// </summary>
        Boolean bPerFlg = true;

        /// <summary>
        ///【考勤】空位可选flg
        /// </summary>
        Boolean bPerNullFlg = false;

        /// <summary>
        ///【未定义】关位可选flg
        /// </summary>
        Boolean bProFlg = true;//

        /// <summary>
        ///【未定义】空位可选flg
        /// </summary>
        Boolean bProNullFlg = false;//

        /// <summary>
        ///【支援】关位可选flg
        /// </summary>
        Boolean bSupFlg = true;//

        /// <summary>
        /////【支援】空位可选flg
        /// </summary>
        Boolean bSupNullFlg = false;//可选flg

        /// <summary>
        ///人员调动类型;
        /// 1 人员调入  2 人员调出  3 关位调整
        /// 4 支援调出  5 支援调入  6 替关调动
        /// </summary>
        string pflag = "";//人员调动类型

        // 日志	
        private static readonly ILog log = LogManager.GetLogger(typeof(frmProduce_TeamChange));
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
        int widget_Cnt = 5;//控件之间间隙

        /// <summary>
        ///半边显示控件个数
        /// </summary>
        int half_Cnt = 6;
        /// <summary>
        /// 当前行数,初始值：-1
        /// </summary>
        int row_Cnt = -1;//当前行数

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
        int mGuanWeiWidth = new UserPersonsList().Width + 15;//关位（工种）控件宽

        /// <summary>
        /// 控件高
        /// </summary>
        int mGuanWeiHeight = new UserPersonsList().Height + 15;//关位（工种）控件高

        #endregion

        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmProduce_TeamChange(string parMyteamName, string parDdateOperDate)
        {
            Stopwatch s = new Stopwatch();
            try
            {
                Program.logFlagStart(log, s, "****人员调换总显示时间：");
                this.Cursor = Cursors.WaitCursor;
                InitializeComponent();
                this.TopMost = true;
                isRun = false;
                isEditValue = false;

                initCl();

                this.parMyteamName = parMyteamName;
                this.parDdateOperDate = parDdateOperDate;
                this.Activated += new EventHandler(frmProduce_TeamChange_Activated);
                this.FormClosing += new FormClosingEventHandler(frmProduce_TeamChange_FormClosing);

                SetFormValue();
                Program._frmProduce_TeamChange = this;

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                Program.logFlagEnd(log, s, "人员调换总显示时间："); 
                this.TopMost = true;
            }

        }

        public void initCl()
        {
            try
            {
                _objcl2 = new objCl() { cl = panelContent, gwNum = 30, UserPersonNum = 80, UserNullNum = 70 };
                initPanelContentAddcl(_objcl2);

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        private objCl initPanelContentAddcl(objCl o)
        {
            //throw new NotImplementedException();
            Stopwatch tmpWatch = new Stopwatch();
            try
            {
                tmpWatch.Start();

                this.SuspendLayout();

                _objcl2 = (objCl)o;

                _lstContr2 = new Control[o.gwNum + o.UserPersonNum + o.UserNullNum];

                //this.Text = "人员揭示：开始初始化控-->关位:" + o.gwNum + "个";

                //************todo something


                //关位
                _objcl2.u_gwNum = 0;
                for (int i = 0; i < o.gwNum; i++)
                {
                    UserPersonsList tmpd = new UserPersonsList();
                    tmpd.Visible = false;
                    tmpd.GuanweiID = i.ToString();
                    tmpd.RealCount = i;
                    tmpd.StandardCount = i;
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    _lstContr2[i] = tmpd;
                }

                //人员

                //this.Text = "人员揭示：初始化控-->关位：" + o.gwNum + ",已完成.开始人员控件:" + o.UserPersonNum + "个";

                _objcl2.u_UserPersonNum = o.gwNum;
                for (int i = o.gwNum; i < (o.gwNum + o.UserPersonNum); i++)
                {
                    UserPerson tmpd = new UserPerson();
                    tmpd.Visible = false;
                    tmpd.UserID = i.ToString();
                    tmpd.UserName = "UserPerson:" + i.ToString();
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    _lstContr2[i] = tmpd;
                }
                //空位

                //this.Text = "人员揭示：初始化控-->关位：" + o.gwNum + "，人员:" + o.UserPersonNum + ",已完成.开始空位人员控件:" + o.UserNullNum + "个";


                var tmpi = o.gwNum + o.UserPersonNum;
                _objcl2.u_UserNullNum = tmpi;
                for (int i = tmpi; i < (o.gwNum + o.UserPersonNum + o.UserNullNum); i++)
                {
                    UserPersonNull tmpd = new UserPersonNull();
                    tmpd.Visible = false;
                    tmpd.UserID = i.ToString();
                    tmpd.UserName = "UserPerson:" + i.ToString();
                    //tmpd.Left = (i % 14) * tmpd.Width;
                    //tmpd.Top = (i % 10) * tmpd.Height;
                    _lstContr2[i] = tmpd;
                }

                //************end todo

                o.cl.Controls.AddRange(_lstContr2);

                this.ResumeLayout(false);
                this.PerformLayout();
                tmpWatch.Stop();

                var msg = "人员调换：初始化控件,关位：" + o.gwNum + "，人员:" + o.UserPersonNum + ",空位:" + o.UserNullNum + "，Time:" + tmpWatch.Elapsed.ToString();
                Program.logFlagStart(log, tmpWatch, msg);


                return o;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        void frmProduce_TeamChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TopMost = false;
            this.Hide();
            e.Cancel = true;
            //throw new NotImplementedException();
        }

        void frmProduce_TeamChange_Activated(object sender, EventArgs e)
        {
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

                dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 08:00";
                dtpEndDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00";

                isLoad = false;
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("画面初始化失败！" + ex);
                FrmDialog.ShowDialog();
            }
        }


        #endregion

        #region 事件处理方法

        /// <summary>
        /// 查询条件：选择向别-班别
        /// </summary>
        private void lookmyteamName_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            frmMyTeamNameSearch frm = new frmMyTeamNameSearch();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lookmyteamName.Text = frm.m_myTeamName;
                if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;
                //重新取得页面数据
                //ThreadPool.QueueUserWorkItem(btnRefleash, "查询条件：选择向别-班别");

            }
            this.TopMost = true;
        }

        /// <summary>
        /// 查询条件：设置考勤日期（查询条件）
        /// </summary>
        private void dateOperDate1_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //this.timer1.Enabled = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dateOperDate1.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");

            }
            this.TopMost = true;
        }


        /// <summary>
        /// 设置调动开始日期
        /// </summary>
        private void dtpStartDate_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtpStartDate.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");
            }
            this.TopMost = true;
        }

        /// <summary>
        /// 设置调动结束日期
        /// </summary>
        private void dtpEndDate_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            FrmSetDateTime frm = new FrmSetDateTime();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                dtpEndDate.Text = DateTime.Parse(frm.m_DateTime).ToString("yyyy-MM-dd HH:mm");
            }
            this.TopMost = true;
        }


        /// <summary>
        /// 查询条件：向别-班别 变更
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (isLoad) return;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;

            //重新取得页面数据
            //刷新
            btnRef_Click(null, null);

            //ThreadPool.QueueUserWorkItem(btnRefleash, "查询条件：向别-班别 变更");
            this.TopMost = true;
        }

        /// <summary>
        /// 查询条件：
        /// </summary>
        private void dateOperDate1_EditValueChanged(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (isLoad) return;
            if (lookmyteamName.Text.Trim() == "" || dateOperDate1.Text.Trim() == "") return;

            //重新取得页面数据
            //刷新
            btnRef_Click(null, null);

            //ThreadPool.QueueUserWorkItem(btnRefleash, "查询条件：考勤日期变更");
            this.TopMost = true;
        }

        /// <summary>
        /// 人员对象双击事件,查看人员免许详细
        /// </summary>
        void m_Person_DoubleClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            UserPerson col = sender as UserPerson;
            frmLicense_RecAdd frm = new frmLicense_RecAdd();
            frm.TopMost = true;
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
            this.TopMost = true;
        }

        /// <summary>
        /// 点击关位（工种）信息
        /// </summary>
        void m_PersonList_AllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (bPlistFlg)//1：【考勤】工种可选择
            {

                //1：【考勤】工种；  2：【考勤】（标配）人员；  4：【考勤】（空位）人员
                // 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 1;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;

        }

        /// <summary>
        /// 点击【考勤】人员信息
        /// </summary>
        void m_Person_AllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (bPerFlg)//可选场合
            {

                //1：【考勤】工种；  2：【考勤】（标配）人员；  4：【考勤】（空位）人员
                /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 2;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;
        }

        /// <summary>
        /// 考勤人员空位对象点击事件
        /// </summary>
        void m_PersonNull_AllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //选择From的场合，不可用
            if (bPerNullFlg)
            {
                //1：【考勤】工种；  2：【考勤】（标配）人员；  4：【考勤】（空位）人员
                /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 4;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;
        }


        /// <summary>
        /// 点击未定义人员
        /// </summary>
        void m_Person_ProductAllEventClick(object sender, EventArgs e)
        {
            try
            {
                //this.TopMost = false;
                //选择From的场合，不可用
                if (bProFlg)
                {
                    UserPerson col = sender as UserPerson;
                    //1：【考勤】工种；  2：【考勤】（标配）人员； 4：【考勤】（空位）人员
                    /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                    selectFlg = 5;

                    //选择判定控件状态,并设置调动类型，调动From，To数据 
                    checkSelectPersons(selectFlg, sender);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                this.TopMost = true;
            }

        }

        /// <summary>
        /// 未定义人员空位对象点击事件
        /// </summary>
        void m_ProjectNull_AllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //选择From的场合，不可用
            if (bProNullFlg)
            {
                //1：【考勤】工种；  2：【考勤】（标配）人员；  3：【考勤】（标配以外）人员； 4：【考勤】（空位）人员
                /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 6;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;
        }
        /// <summary>
        /// 点击支援人员
        /// </summary>
        void m_Person_SupportAllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //选择From的场合，不可用
            if (bSupFlg)
            {
                //1：【考勤】工种；  2：【考勤】（标配）人员；  4：【考勤】（空位）人员
                /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 7;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;
        }

        /// <summary>
        /// 支援人员空位对象点击事件
        /// </summary>
        void m_SupperNull_AllEventClick(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //选择From的场合，不可用
            if (bSupNullFlg)
            {
                UserPersonNull col = sender as UserPersonNull;
                //1：【考勤】工种；  2：【考勤】（标配）人员；   4：【考勤】（空位）人员
                /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员
                selectFlg = 8;

                //选择判定控件状态,并设置调动类型，调动From，To数据 
                checkSelectPersons(selectFlg, sender);
            }
            this.TopMost = true;
        }

        /// <summary>
        /// 搜索未定义人员数据
        /// </summary>
        private void btnUserIDSearch2_Click(object sender, EventArgs e)
        {
            //this.timer1.Enabled = false;
            GetDspProductList();
        }

        /// <summary>
        /// 搜索支援人员
        /// </summary>
        private void btnUserIDSearch3_Click(object sender, EventArgs e)
        {
            //this.timer1.Enabled = false;
            GetSupportList();
        }

        /// <summary>
        /// 刷新页面数据
        /// </summary>
        private void btnRef_Click(object sender, EventArgs e)
        {

            if (isEditValue)
            {
                return;
            }
            isEditValue = true;

            this.btnRef.Enabled = false;
            try
            {

                SetExecuteAttendResult();

            }
            catch (Exception ex)
            {
                log.Error(ex);
                FrmAttendDialog FrmDialog = new FrmAttendDialog("数据刷新失败！" + ex);
                FrmDialog.ShowDialog();
            }
            finally
            {
                isEditValue = false;
                this.btnRef.Enabled = true;
            }
        }

        /// <summary>
        /// 执行存储过程（调用存储过程重新统计）
        /// </summary>
        public void SetExecuteAttendResult()
        {
            this.Cursor = Cursors.WaitCursor;

            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "执行存储过程。");

            if (lookmyteamName.Text.ToString() == "" || dateOperDate1.Text.Trim() == "")
            {
                return;
            }
            //根据向别-班别查出JobForID，ProjectID，LineID，TeamID
            m_tblAllList = new DataTable();
            string str_sql = string.Format(@"select distinct JobForID,ProjectID,LineID,TeamID from V_Produce_para where myTeamName='{0}' ", lookmyteamName.Text.Trim());
            m_tblAllList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
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

                m_dicItemData.Clear();
                if (m_tblAllList.Rows.Count == 1)
                {
                    m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                    m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                    m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                    m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
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

                //GridCommon.SetTeamChangeAttendResult(paraList[0].Value.ToString(), m_dicItemData["JobForID"], m_dicItemData["ProjectID"], m_dicItemData["LineID"], m_dicItemData["TeamID"], userid, pFlag);

                SqlParameter[] paraListCardData = new SqlParameter[1];
                paraListCardData[0] = new SqlParameter("@SetDate", SqlDbType.VarChar, 10);
                if (dateOperDate1.EditValue.ToString() != "")
                {
                    DateTime dt = DateTime.Parse(dateOperDate1.EditValue.ToString());
                    paraListCardData[0].Value = dt.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    paraListCardData[0].Value = "";
                }
                //执行存储过程2,打卡时间
                Common.AdoConnect.Connect.SetExecuteSP("PROC_CardData_Attend_Result", Common.Choose.OnlyExecSp, paraListCardData);


                Program.logFlagEnd(log, s, "执行存储过程。");

                ////重新取得页面数据           
                GetDspDataList();//考勤数据
                GetDspProductList();//未定义人员数据
                GetSupportList();//支援人员数据

                //ThreadPool.QueueUserWorkItem(btnRefleash, "执行存储过程");


            }
            catch (Exception ex)
            {
                log.Error(ex);
                FrmAttendDialog FrmDialog = new FrmAttendDialog("存储过程执行失败！" + ex);
                FrmDialog.ShowDialog();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                isEditValue = false;
            }

        }


        /// <summary>
        /// 人员调动：保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            if (txtFrom.Text == "" || txtTo.Text == "" || m_tblAllList.Rows.Count == 0)
            {
                return;
            }

            if (DateTime.Parse(dtpStartDate.Text.Trim()) >= DateTime.Parse(dtpEndDate.Text.Trim())
                && (pflag == "4" || pflag == "5" || pflag == "6"))// 4 支援调出  5 支援调入  6 替关调动
            {
                FrmAttendDialog FrmDialog = new FrmAttendDialog("【结束时间】必须大于【开始时间】！");
                FrmDialog.ShowDialog();
                return;
            }
            m_tblAllList = new DataTable();
            string str_sql = string.Format(@"select distinct JobForID,ProjectID,LineID,TeamID from V_Produce_para where myTeamName='{0}' ", lookmyteamName.Text.Trim());
            m_tblAllList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
            try
            {
                Common.AdoConnect.Connect.CreateSqlTransaction();
                int result = 0;
                //人员调动类型; pflag   1 人员调入  2 人员调出  3 关位调整  4 支援调出  5 支援调入  6 替关调动
                str_sql = string.Empty;
                string strGuanweiID = string.Empty;
                string strGuanweiSite = string.Empty;

                //From人员信息:moveFromPerson
                switch (pflag)
                {
                    case "1"://1 人员调入

                        //修改用户line信息                      
                        m_dicPrimarName.Clear();
                        m_dicPrimarName["UserID"] = moveFromPerson.UserID.ToString();

                        m_dicItemData.Clear();
                        m_dicItemData["userID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["AttendUnit"] = "1";
                        if (moveToPersonFlg == "1") //1:moveToPersonList
                        {//To工种信息 moveToPersonList
                            strGuanweiID = moveToPersonList.GuanweiID.ToString();
                            strGuanweiSite = "99";//关位位置
                        }
                        else if (moveToPersonFlg == "2")//2:moveToPerson
                        {//To人员信息 moveToPerson;
                            strGuanweiID = moveToPerson.GuanweiID.ToString();
                            strGuanweiSite = moveToPerson.GuanweiSite;//关位位置
                        }
                        else if (moveToPersonFlg == "3")//3:moveToPersonNull
                        {//To人员空位信息 moveToPersonNull
                            strGuanweiID = moveToPersonNull.GuanweiID.ToString();
                            strGuanweiSite = moveToPersonNull.GuanweiSite;//关位位置
                        }
                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置

                        ////查询当前关位下是否有两个人员，如果是这不能继续添加
                        //str_sql = string.Format(@"select AttendDate from Attend_Total_Result where JobForID='{0}' and ProjectID='{1}' and LineID='{2}' and TeamID='{3}' and GuanWeiID='{4}' and GuanweiSite='{5}' and AttendDate='{6}'",
                        //    m_dicItemData["JobForID"], m_dicItemData["ProjectID"], m_dicItemData["LineID"], m_dicItemData["TeamID"], strGuanweiID, strGuanweiSite, dateOperDate1.Text.Trim());
                        //DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                        //if (dt.Rows.Count >= 2)
                        //{
                        //    FrmAttendDialog FrmDialog = new FrmAttendDialog("当前关位最多只能添加两个人！");
                        //    FrmDialog.ShowDialog();
                        //    return;
                        //}

                        //更新考勤单位是1的数据到Produce_User表中
                        result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);

                        m_dicItemData.Clear();
                        m_dicItemData["userID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();

                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置
                        //增加调动信息
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = "4000-01-01";
                        m_dicItemData["PFlag"] = pFlag.pflag1.GetHashCode().ToString();
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);


                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();

                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("人员调入", txtFrom.Text, txtTo.Text,
                                dtpStartDate.EditValue.ToString(), "");
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-人员调入", "新增", moveFromPerson.UserName);

                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;
                    case "2":// 2 人员调出 
                        //From人员信息:moveFromPerson --moveToPersonNull
                        //1、清除人员信息表中的line信息

                        if (moveFromPerson.TitleName.Contains("支"))
                        {
                            XtraMsgBox.Show("当前支援人员不能支援调出!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        m_dicPrimarName.Clear();
                        m_dicPrimarName["UserID"] = moveFromPerson.UserID.ToString();

                        m_dicItemData.Clear();
                        m_dicItemData["userID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = "0";
                        m_dicItemData["ProjectID"] = "0";
                        m_dicItemData["LineID"] = "0";
                        m_dicItemData["TeamID"] = "0";
                        m_dicItemData["GuanweiID"] = "0";
                        m_dicItemData["GuanweiSite"] = "0";
                        m_dicItemData["AttendUnit"] = "0";
                        m_dicItemData["AttendUnit"] = "1";
                        //更新考勤单位是1的数据到Produce_User表中
                        result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);

                        //更新人员调入，关位调整数据的结束日期
                        str_sql = string.Format(@"update Attend_Move set EndDate='{0}' ,OperID='{1}', OperDate='{2}' where 1=1 
                                AND UserID='{3}'  
                                AND EndDate='4000-01-01'",
                              dtpStartDate.EditValue.ToString(),
                               Common._personid, DateTime.Now.ToString(),
                              moveFromPerson.UserID.ToString());
                        result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());

                        //增加人员调出信息
                        m_dicPrimarName.Clear();
                        m_dicItemData.Clear();
                        m_dicItemData["UserID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["GuanweiID"] = moveFromPerson.GuanweiID.ToString();
                        m_dicItemData["GuanweiSite"] = moveFromPerson.GuanweiSite;//关位位置
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = "1900-01-01";
                        m_dicItemData["PFlag"] = pFlag.pflag2.GetHashCode().ToString();
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();

                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("人员调出", txtFrom.Text, txtTo.Text,
                                dtpStartDate.EditValue.ToString(), "");
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-人员调出", "新增", moveFromPerson.UserName);

                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;

                    case "3"://3 关位调整
                        //From人员信息:moveFromPerson
                        m_dicPrimarName.Clear();
                        m_dicPrimarName["UserID"] = moveFromPerson.UserID.ToString();

                        m_dicItemData.Clear();
                        m_dicItemData["userID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["AttendUnit"] = "1";
                        if (moveToPersonFlg == "1") //1:moveToPersonList
                        {//To工种信息 moveToPersonList
                            strGuanweiID = moveToPersonList.GuanweiID.ToString();
                            strGuanweiSite = "99";//关位位置
                        }
                        else if (moveToPersonFlg == "2")//2:moveToPerson
                        {//To人员信息 moveToPerson;
                            strGuanweiID = moveToPerson.GuanweiID.ToString();
                            strGuanweiSite = moveToPerson.GuanweiSite;//关位位置
                        }
                        else if (moveToPersonFlg == "3")//3:moveToPersonNull
                        {//To人员空位信息 moveToPersonNull
                            strGuanweiID = moveToPersonNull.GuanweiID.ToString();
                            strGuanweiSite = moveToPersonNull.GuanweiSite;//关位位置
                        }
                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置
                        //更新考勤单位是1的数据到Produce_User表中
                        result = SysParam.m_daoCommon.SetModifyDataItem("Produce_User", m_dicItemData, m_dicPrimarName);

                        //更新人员调入，关位调整数据
                        //更新人员调入，关位调整数据的结束日期
                        str_sql = string.Format(@"update Attend_Move set EndDate='{0}', OperID='{1}', OperDate='{2}' where 1=1 
                                AND UserID='{3}'  
                                AND EndDate='4000-01-01'",
                              dtpStartDate.EditValue.ToString(),
                               Common._personid, DateTime.Now.ToString(),
                              moveFromPerson.UserID.ToString());
                        result = SysParam.m_daoCommon.SetModifyDataItemBySql(str_sql, new StringDictionary(), new StringDictionary(), new StringDictionary());


                        //增加调动信息
                        m_dicItemData.Clear();
                        m_dicItemData["UserID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = "4000-01-01";
                        m_dicItemData["PFlag"] = pFlag.pflag3.GetHashCode().ToString();
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();
                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("关位调整", txtFrom.Text, txtTo.Text,
                               dtpStartDate.EditValue.ToString(), "");
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-关位调整", "新增", moveFromPerson.UserName);
                            //DialogResult = DialogResult.OK;
                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;

                    case "4"://4 支援调出 

                        if (moveFromPerson.TitleName.Contains("支"))
                        {
                            XtraMsgBox.Show("当前支援人员不能支援调出!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //增加调动信息:保存调出前的向别，关位信息
                        m_dicItemData.Clear();
                        m_dicItemData["UserID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = dtpEndDate.EditValue.ToString();
                        m_dicItemData["PFlag"] = pFlag.pflag4.GetHashCode().ToString();

                        m_dicItemData["GuanweiID"] = moveFromPerson.GuanweiID.ToString();
                        m_dicItemData["GuanweiSite"] = moveFromPerson.GuanweiSite;//关位位置
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();
                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("支援调出", txtFrom.Text, txtTo.Text,
                               dtpStartDate.EditValue.ToString(), dtpEndDate.EditValue.ToString());
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-支援调出", "新增", moveFromPerson.UserName);

                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;
                    case "5"://5 支援调入

                        //增加调动信息：保存调入后的向别，关位信息
                        if (moveFromPerson.ProjectID == m_tblAllList.Rows[0]["ProjectID"].ToString() && moveFromPerson.JobForID == m_tblAllList.Rows[0]["JobForID"].ToString()
                            && moveFromPerson.LineID == m_tblAllList.Rows[0]["LineID"].ToString())
                        {
                            XtraMsgBox.Show("当前支援人员不能支援调入!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        m_dicItemData.Clear();
                        m_dicItemData["UserID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = dtpEndDate.EditValue.ToString();
                        m_dicItemData["PFlag"] = pFlag.pflag5.GetHashCode().ToString();
                        if (moveToPersonFlg == "1") //1:moveToPersonList
                        {//To工种信息 moveToPersonList
                            strGuanweiID = moveToPersonList.GuanweiID.ToString();
                            strGuanweiSite = "99";//关位位置
                        }
                        else if (moveToPersonFlg == "2")//2:moveToPerson
                        {//To人员信息 moveToPerson;
                            strGuanweiID = moveToPerson.GuanweiID.ToString();
                            strGuanweiSite = moveToPerson.GuanweiSite;//关位位置
                        }
                        else if (moveToPersonFlg == "3")//3:moveToPersonNull
                        {//To人员空位信息 moveToPersonNull
                            strGuanweiID = moveToPersonNull.GuanweiID.ToString();
                            strGuanweiSite = moveToPersonNull.GuanweiSite;//关位位置
                        }
                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();
                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("支援调入", txtFrom.Text, txtTo.Text,
                               dtpStartDate.EditValue.ToString(), dtpEndDate.EditValue.ToString());
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-支援调入", "新增", moveFromPerson.UserName);

                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;
                    case "6"://6 替关调动
                        //增加调动信息：保存需要替关的向别，关位信息
                        m_dicItemData.Clear();
                        m_dicItemData["UserID"] = moveFromPerson.UserID.ToString();
                        m_dicItemData["JobForID"] = m_tblAllList.Rows[0]["JobForID"].ToString();
                        m_dicItemData["ProjectID"] = m_tblAllList.Rows[0]["ProjectID"].ToString();
                        m_dicItemData["LineID"] = m_tblAllList.Rows[0]["LineID"].ToString();
                        m_dicItemData["TeamID"] = m_tblAllList.Rows[0]["TeamID"].ToString();
                        m_dicItemData["StrDate"] = dtpStartDate.EditValue.ToString();
                        m_dicItemData["EndDate"] = dtpEndDate.EditValue.ToString();
                        m_dicItemData["PFlag"] = pFlag.pflag6.GetHashCode().ToString();
                        if (moveToPersonFlg == "1") //1:moveToPersonList
                        {//To工种信息 moveToPersonList
                            strGuanweiID = moveToPersonList.GuanweiID.ToString();
                            strGuanweiSite = "99";//关位位置
                        }
                        else if (moveToPersonFlg == "2")//2:moveToPerson
                        {//To人员信息 moveToPerson;
                            strGuanweiID = moveToPerson.GuanweiID.ToString();
                            strGuanweiSite = moveToPerson.GuanweiSite;//关位位置
                        }
                        else if (moveToPersonFlg == "3")//3:moveToPersonNull
                        {//To人员空位信息 moveToPersonNull
                            strGuanweiID = moveToPersonNull.GuanweiID.ToString();
                            strGuanweiSite = moveToPersonNull.GuanweiSite;//关位位置
                        }
                        m_dicItemData["GuanweiID"] = strGuanweiID;
                        m_dicItemData["GuanweiSite"] = strGuanweiSite;//关位位置
                        m_dicItemData["OperID"] = Common._personid;
                        m_dicItemData["OperDate"] = DateTime.Now.ToString();
                        result = SysParam.m_daoCommon.SetInsertDataItem(TableNames.Attend_Move, m_dicItemData);
                        if (result > 0)
                        {
                            Common.AdoConnect.Connect.TransactionCommit();
                            FrmMoveDialog frmMoveDialog = new FrmMoveDialog("替关调动", txtFrom.Text, txtTo.Text,
                               dtpStartDate.EditValue.ToString(), dtpEndDate.EditValue.ToString());
                            frmMoveDialog.ShowDialog();
                            //日志
                            SysParam.m_daoCommon.WriteLog("平板-人员调动-替关调动", "新增", moveFromPerson.UserName);
                            //自动刷新
                            btnRef_Click(null, null);
                        }
                        break;

                    default: break;
                }



            }
            catch (Exception ex)
            {
                Common.AdoConnect.Connect.TransactionRollback();
                log.Error(ex);
            }
            finally
            {
                this.TopMost = true;
            }
        }

        /// <summary>
        /// 人员调动：清空
        /// </summary>
        private void btnCanel_Click(object sender, EventArgs e)
        {
            txtFrom.Text = "";
            txtTo.Text = "";
            strFromSelFlg = 0;
            strToSelFlg = 0;
            bPlistFlg = false;
            bPerFlg = true;
            bPerNullFlg = false;
            bProFlg = true;//
            bProNullFlg = false;//
            bSupFlg = true;//
            bSupNullFlg = false;//可选flg
            txtFrom.Text = "";
            txtTo.Text = "";
            if (m_tblDataList.Rows.Count > 0)
            {
                string TeamSetNM = m_tblDataList.Rows[0]["TeamSetNM"].ToString();
                //设置调动开始时间、结束时间
                if (TeamSetNM == "白班")
                {
                    dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 08:00";
                    dtpEndDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00";
                }
                else
                {
                    dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 22:00";
                    dtpEndDate.EditValue = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 07:00";
                }

            }

            //清除所有【考勤】选择
            foreach (var cols in panelContent.Controls)
            {
                if (cols.GetType().Name == "UserPersonsList")
                {
                    UserPersonsList m_PersonList = (UserPersonsList)cols;
                    if (m_PersonList.BackColor == Color.Red)
                    {
                        m_PersonList.BackColor = Color.Transparent;
                        m_PersonList.Tag = 0;
                    }
                }
                if (cols.GetType().Name == "UserPerson")
                {
                    UserPerson m_Person = (UserPerson)cols;
                    if (m_Person.BackColor == Color.Red)
                    {
                        m_Person.BackColor = Color.Transparent;
                        m_Person.Tag = 0;
                    }
                }
                if (cols.GetType().Name == "UserPersonNull")
                {
                    UserPersonNull m_PersonNull = (UserPersonNull)cols;
                    if (m_PersonNull.BackColor == Color.Red)
                    {
                        m_PersonNull.BackColor = Color.Transparent;
                        m_PersonNull.Tag = 0;
                    }
                }
            }
            //清除所有【未定义】选择
            foreach (var cols in panelProduct.Controls)
            {
                if (cols.GetType().Name == "UserPerson")
                {
                    UserPerson m_Person = (UserPerson)cols;
                    if (m_Person.BackColor == Color.Red)
                    {
                        m_Person.BackColor = Color.Transparent;
                        m_Person.Tag = 0;
                    }
                }
                if (cols.GetType().Name == "UserPersonNull")
                {
                    UserPersonNull m_PersonNull = (UserPersonNull)cols;
                    if (m_PersonNull.BackColor == Color.Red)
                    {
                        m_PersonNull.BackColor = Color.Transparent;
                        m_PersonNull.Tag = 0;
                    }
                }
            }
            //清除所有【支援】选择
            foreach (var cols in panelSupport.Controls)
            {
                if (cols.GetType().Name == "UserPerson")
                {
                    UserPerson m_Person = (UserPerson)cols;
                    if (m_Person.BackColor == Color.Red)
                    {
                        m_Person.BackColor = Color.Transparent;
                        m_Person.Tag = 0;
                    }
                }
                if (cols.GetType().Name == "UserPersonNull")
                {
                    UserPersonNull m_PersonNull = (UserPersonNull)cols;
                    if (m_PersonNull.BackColor == Color.Red)
                    {
                        m_PersonNull.BackColor = Color.Transparent;
                        m_PersonNull.Tag = 0;
                    }
                }
            }
        }

        /// <summary>
        /// 打开人员考勤界面
        /// </summary>
        private void btnProduce_TeamAttend_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //this.TopMost = false;
                //this.timer1.Enabled = false;
                this.DialogResult = DialogResult.OK;
                this.Close();

                if (Program._frmProduce_TeamAttend != null)
                {
                    Program._frmProduce_TeamAttend.parMyteamName = lookmyteamName.Text.ToString();
                    Program._frmProduce_TeamAttend.parDdateOperDate = dateOperDate1.Text.ToString();
                }
                else
                {
                    frmProduce_TeamAttend frm = new frmProduce_TeamAttend(lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                }
                Program._frmProduce_TeamAttend.TopMost = true;
                Program._frmProduce_TeamAttend.Show();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            ////自动刷新
            //btnRef_Click(null,null)
        }

        /// <summary>
        /// 打开人员揭示界面
        /// </summary>
        private void btnUserTotalShow_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            //this.timer1.Enabled = false;
            this.DialogResult = DialogResult.OK;
            this.Close();

            if (Program._frmProduce_UserTotalShow == null)
            {
                frmProduce_UserTotalShow frm = new frmProduce_UserTotalShow(lookmyteamName.Text, dateOperDate1.Text);
            }
            Program._frmProduce_UserTotalShow.TopMost = true;
            Program._frmProduce_UserTotalShow.Show();
            ////自动刷新
            // //btnRef_Click(null,null)

        }
        #endregion

        #region 共同方法

        public void btnRefleash(object o)
        {
            try
            {
                Program._frmMain.Invoke(new Action(delegate()
                {
                    //this.Text = "人员调换:" + o.ToString() + ",正在加载中。。。";
                    //panelContent.Controls.Clear();
                }));

                Task<string> t = new Task<string>(n => GetDspDataListThread((string)n), "dd");
                t.Start();
                t.Wait();

                var tmpresult = t.Result;
                if (tmpresult.Equals("1"))
                {
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        //this.Text = "人员调换:" + o.ToString() + ",获取考勤人员信息加载成功，开始未定义人员,支援人员数据。。。";
                    }));
                    GetDspProductList();//未定义人员数据
                    GetSupportList();//支援人员数据
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        //this.Text = "人员调换:" + o.ToString() + ",全部加载成功。。。";
                    }));
                }
                else
                {
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        //this.Text = tmpresult;
                    }));
                }
            }
            catch (Exception ex)
            {
                Program._frmMain.Invoke(new Action(delegate()
                {
                    MessageBox.Show(ex.Message);
                }));
                //throw;
            }

        }
        /// <summary>
        /// 获取考勤人员信息一览
        /// </summary>
        protected string GetDspDataListThread(string o)
        {
            try
            {
                if (isRun)
                {
                    return "0";
                }
                isRun = true;
                Program._frmMain.Invoke(new Action(delegate()
                {
                    Application.DoEvents();
                    //panelContent.Enabled = false;
                    panelContent.AutoScroll = false;
                    btnRef.Enabled = false;
                    lookmyteamName.Enabled = false;
                    dateOperDate1.Enabled = false;
                    //this.TopMost = false;
                }));
                row_Cnt = -1;//布局行号归零
                m_tblDataList = new DataTable();//人员考勤情况数据
                m_tblGuanweiList = new DataTable();//关位信息
                //1、取得关位顺序信息
                string str_sql = string.Empty;
                str_sql = string.Format(@" SELECT a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,
                                                     JobForID,   ProjectID, LineID,  TeamID, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM   (
													select JobForID, ProjectID, LineID, TeamID, GuanweiID, AttendDate,guanweisite,AttendWork,AttendType,AttendMemo  
													from V_Attend_Result_Info
												  ) AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 AND ISNULL(a1.AttendMemo,'') <>'支援调出'
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
                                        group by RowID,a.AttendDate,a.JobForID,a.ProjectID,a.LineID,a.TeamID,a.myTeamName,a.orgTeamName,a.GuanweiID,
                                       a.GuanweiName,a.GuanweiType,a.SetNum,h.InCount order by RowID ",
                                  dateOperDate1.Text.Trim(), lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                m_tblGuanweiList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblGuanweiList.Rows.Count == 0)
                {
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        //panelContent.Controls.Clear();

                        foreach (Control item in _lstContr2)
                        {
                            item.Tag = "0";
                            item.Visible = false;
                        }
                    }));
                    return "0";
                }
                //2、人员考勤情况数据取得
                str_sql = string.Format(@"select distinct  * from V_User_TotalShow_Image WHERE 
                                                (JobForID <> 0) AND (ProjectID <> 0) 
                                                AND (LineID <> 0) AND (TeamID <> 0) 
                                                AND (GuanweiID <> 0) and ISNULL(AttendMemo,'') <>'支援调出' ");

                str_sql += " and myTeamName='" + lookmyteamName.Text.ToString() + "'";

                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += " order by JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    string TeamSetNM = m_tblDataList.Rows[0]["TeamSetNM"].ToString();
                    //设置调动开始时间、结束时间
                    if (TeamSetNM == "白班")
                    {
                        dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 08:00";
                        dtpEndDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00";
                    }
                    else
                    {
                        dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 22:00";
                        dtpEndDate.EditValue = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 07:00";
                    }

                }

                Program._frmMain.Invoke(new Action(delegate()
                {
                    //循环把人员信息放入panel
                    //panelContent.Controls.Clear();
                    foreach (Control item in _lstContr2)
                    {
                        item.Tag = "0";
                        item.Visible = false;
                    }
                }));
                isLeftInto = true;//是否考勤区域处理


                _objcl2.u_gwNum = 0;
                _objcl2.u_UserPersonNum = _objcl2.gwNum;
                _objcl2.u_UserNullNum = _objcl2.gwNum + _objcl2.UserPersonNum;

                //关位
                string strGuwanweiID = string.Empty;

                var guanweicount = m_tblGuanweiList.Rows.Count;
                for (int a = 0; a < guanweicount; a++)
                {
                    strGuwanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();

                    //当前关位下的人员信息,关位下没有人员，不展示关位(工种）信息
                    DataView view = new DataView(m_tblDataList.Copy());
                    view.RowFilter = "GuanweiID='" + strGuwanweiID + "'";

                    DataTable dt_temp = view.ToTable();

                    //添加关位(工种）
                    Program._frmMain.Invoke(new Action(delegate()
                    {
                        Application.DoEvents();
                        var m_PersonList = (UserPersonsList)_lstContr2[_objcl2.u_gwNum]; //new UserPersonsList();
                        _objcl2.u_gwNum++;

                        m_PersonList.TitleGuanwei = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();//关位名称
                        m_PersonList.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                        bpCnt = int.Parse(m_tblGuanweiList.Rows[a]["SetNum"].ToString());

                        m_PersonList.StandardCount = bpCnt;//关位标配人数
                        m_PersonList.RealCount = int.Parse(m_tblGuanweiList.Rows[a]["realityCount"].ToString());//关位实配人数
                        try
                        {
                            m_PersonList.AllEventClick -= new UserPersonsList.AllEvent(m_PersonList_AllEventClick);
                        }
                        catch (Exception ex)
                        {
                            log.Error("AllEventClick Error:" + m_Person.UserID);
                        }

                        m_PersonList.AllEventClick += new UserPersonsList.AllEvent(m_PersonList_AllEventClick);


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
                                var m_Person = (UserPerson)_lstContr2[_objcl2.u_UserPersonNum];
                                _objcl2.u_UserPersonNum++; //new UserPerson();

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
                                //m_Person.Visible = false;
                                //panelContent.Controls.Add(m_Person);//展示空间
                                m_Person.Location = _point;//设置空间展示坐标
                                m_Person.Visible = true;

                            }));

                            gwShowSite = GwSite + 1;
                            dt_temp.Rows[0].Delete();
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
                                var m_Person = (UserPerson)_lstContr2[_objcl2.u_UserPersonNum];
                                _objcl2.u_UserPersonNum++; //new UserPerson();

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
                                //m_Person.Visible = false;
                                //panelContent.Controls.Add(m_Person);//展示空间
                                m_Person.Location = _point;//设置空间展示坐标
                                m_Person.Visible = true;
                            }));

                            gwShowSite++;
                            dt_temp.Rows[0].Delete();
                        }
                        else if (perFlg)
                        {
                            Program._frmMain.Invoke(new Action(delegate()
                            {
                                Application.DoEvents();
                                //位置不同，显示空关位
                                //人员信息  
                                var m_PersonNull = (UserPersonNull)_lstContr2[_objcl2.u_UserNullNum];
                                _objcl2.u_UserNullNum++;

                                m_PersonNull.TitleName = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString() + " - " + gwShowSite;
                                m_PersonNull.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                                m_PersonNull.GuanweiNM = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();
                                m_PersonNull.GuanweiSite = gwShowSite.ToString();

                                m_PersonNull.Tag = "0";
                                m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_PersonNull_AllEventClick);
                                m_PersonNull.ImageUrl = "";


                                btnRef.Enabled = true;
                                lookmyteamName.Enabled = true;
                                dateOperDate1.Enabled = true;

                                this.setImgIndex(false);//设置显示位置
                                // m_PersonNull.Visible = false;
                                // panelContent.Controls.Add(m_PersonNull);//展示空间
                                m_PersonNull.Location = _point;//设置空间展示坐标
                                m_PersonNull.Visible = true;

                            }));
                            gwShowSite++;

                        }

                    }
                }
                wid_Left_Cnt = 1;
                wid_Right_Cnt = 1;
                return "1";
            }
            catch (Exception ex)
            {
                Program._frmMain.Invoke(new Action(delegate()
                {
                    Application.DoEvents();
                    //panelContent.Enabled = true;
                    panelContent.AutoScroll = true;
                    btnRef.Enabled = true;
                    lookmyteamName.Enabled = true;
                    dateOperDate1.Enabled = true;

                    FrmAttendDialog FrmDialog = new FrmAttendDialog("考勤数据加载失败！" + ex);
                    FrmDialog.ShowDialog();
                }));

                return ex.Message;
            }
            finally
            {
                isRun = false;
                Program._frmMain.Invoke(new Action(delegate()
                {
                    Application.DoEvents();
                    //panelContent.Enabled = true;
                    panelContent.AutoScroll = true;
                }));

            }

        }



        /// <summary>
        /// 获取系统中未定义人员
        /// </summary>
        public void GetDspProductList()
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "获取系统中未定义人员");
            try
            {

                this.SuspendLayout();

                //this.TopMost = false;

                panelProduct.Controls.Clear();
                panelProduct.AutoScroll = false;

                string str_sql = "";
                if (m_tblAllList.Rows.Count == 0)
                {
                    str_sql = string.Format(@"select distinct JobForID,ProjectID,LineID,TeamID from V_Produce_Para where myTeamName='{0}' order by JobForID,ProjectID,LineID,TeamID", lookmyteamName.Text.Trim());
                    m_tblAllList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (m_tblAllList.Rows.Count == 0)
                    {

                        panelProduct.Controls.Clear();

                        return;
                    }
                }
                str_sql = string.Format(@"select distinct TOP 20 
	                                        UserID,UserName  
                                        from V_Produce_User
	                                        where JobForID ='0' and ProjectID ='0'
                                            and LineID ='0' and TeamID='0' and GuanweiID='0'
                                            and User_Status='在职' 
                                            and UserID not in( 
		                                        select UserID
		                                        from Attend_Move
		                                        where
			                                        PFlag=2--人员调出
			                                        group by UserID 
			                                        having max(StrDate)>convert(VARCHAR(16),'{0}',120) 
                                            )", dateOperDate1.Text.Trim());

                if (txtProductUserID.Text.Trim() != "")
                {
                    str_sql += " and UserID like'%" + txtProductUserID.Text.Trim() + "%'";
                }
                str_sql += "  order by UserID";
                m_tblProduceList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_Top = 0;//上边距
                int lint_TopCount = 0;//当前Top有几行

                for (int i = 0; i < m_tblProduceList.Rows.Count; i++)
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        lint_TopCount++;
                        lint_Left = 0;
                    }
                    //人员信息
                    m_Person = new UserPerson();
                    m_Person.Name = ("person_" + (i + 1)).ToString();
                    m_Person.TitleName = "未定义人员";

                    m_Person.UserID = m_tblProduceList.Rows[i]["UserID"].ToString();
                    m_Person.UserName = m_tblProduceList.Rows[i]["UserName"].ToString();
                    m_Person.GuanweiColor = YellowColor;
                    m_Person.UserIdNmColor = GreenColor;
                    m_Person.Tag = "0";//选中为1，未选中为0
                    m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_ProductAllEventClick);
                    m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, m_tblProduceList.Rows[i]["UserID"].ToString());

                    panelProduct.Controls.Add(m_Person);
                    lint_Top = lint_TopCount * m_Person.Height;
                    _point = new Point(lint_Left + 10, lint_Top + 5);
                    m_Person.Location = _point;

                    lint_Left += m_Person.Width + 5;
                }

                if (m_tblProduceList.Rows.Count % 2 == 0 && m_tblProduceList.Rows.Count != 0)
                {
                    lint_TopCount++;
                    lint_Left = 0;
                }

                //未定义-人员信息空位

                m_PersonNull = new UserPersonNull();
                m_PersonNull.Name = ("person_" + m_tblProduceList.Rows.Count + 1).ToString(); ;
                m_PersonNull.TitleName = "未定义人员";
                m_PersonNull.GuanweiColor = YellowColor;
                m_PersonNull.UserIdNmColor = GreenColor;
                m_PersonNull.Tag = "0";
                m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_ProjectNull_AllEventClick);
                m_PersonNull.ImageUrl = "";


                panelProduct.Controls.Add(m_PersonNull);
                lint_Top = lint_TopCount * m_PersonNull.Height;
                _point = new Point(lint_Left + 10, lint_Top + 5);
                m_PersonNull.Location = _point;


                btnCanel_Click(null, null);
                panelProduct.AutoScroll = true;
                this.TopMost = true;
                this.ResumeLayout(false);
                this.PerformLayout();
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("未定义数据检索失败！" + ex);
                FrmDialog.ShowDialog();
                log.Error(ex);
                panelProduct.AutoScroll = true;

            }
            finally
            {
                Program.logFlagEnd(log, s, "获取系统中未定义人员");
            }
        }


        /// <summary>
        /// 获取考勤人员信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "获取考勤人员信息一览");
            try
            {
                //this.TopMost = false;
                row_Cnt = -1;//布局行号归零
                m_tblDataList = new DataTable();//人员考勤情况数据
                m_tblGuanweiList = new DataTable();//关位信息
                //1、取得关位顺序信息
                string str_sql = string.Empty;
                str_sql = string.Format(@" SELECT a.*,  IsNull(h.InCount,0) as realityCount
                                            FROM (select CONVERT(VARCHAR(10),'{0}',120) as AttendDate,
                                                     JobForID,   ProjectID, LineID,  TeamID, myTeamName, orgTeamName,
                                                     GuanweiID, GuanweiName,  GuanweiType,  RowID, SetNum 
                                                  FROM V_Produce_Para
                                                ) AS a 
                                         LEFT  JOIN (SELECT 
                                                       a1.JobForID, a1.ProjectID, a1.LineID, a1.TeamID,  a1.GuanweiID,   a1.AttendDate, 
                                                       COUNT(*) AS InCount
                                                  FROM   (
													select JobForID, ProjectID, LineID, TeamID, GuanweiID, AttendDate,guanweisite,AttendWork,AttendType,AttendMemo  
													from V_Attend_Result_Info
												  ) AS a1 --考勤汇总报表
                                                  WHERE      a1.AttendWork > 0 AND ISNULL(a1.AttendMemo,'') <>'支援调出'
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
                                        group by RowID,a.AttendDate,a.JobForID,a.ProjectID,a.LineID,a.TeamID,a.myTeamName,a.orgTeamName,a.GuanweiID,
                                       a.GuanweiName,a.GuanweiType,a.SetNum,h.InCount order by RowID ",
                                  dateOperDate1.Text.Trim(), lookmyteamName.Text.ToString(), dateOperDate1.Text.Trim());
                m_tblGuanweiList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblGuanweiList.Rows.Count == 0)
                {
                    panelContent.Controls.Clear();
                    return;
                }
                //2、人员考勤情况数据取得
                str_sql = string.Format(@"select distinct  * from V_User_TotalShow_Image WHERE 
                                                (JobForID <> 0) AND (ProjectID <> 0) 
                                                AND (LineID <> 0) AND (TeamID <> 0) 
                                                AND (GuanweiID <> 0) and ISNULL(AttendMemo,'') <>'支援调出' ");

                str_sql += " and myTeamName='" + lookmyteamName.Text.ToString() + "'";

                str_sql += " and AttendDate=CONVERT(VARCHAR(10),'" + dateOperDate1.Text.Trim() + "',120) ";
                str_sql += " order by JobForID,ProjectID,LineID,TeamID,GuanweiID,GuanweiSite ";

                m_tblDataList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (m_tblDataList.Rows.Count > 0)
                {
                    string TeamSetNM = m_tblDataList.Rows[0]["TeamSetNM"].ToString();
                    //设置调动开始时间、结束时间
                    if (TeamSetNM == "白班")
                    {
                        dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 08:00";
                        dtpEndDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00";
                    }
                    else
                    {
                        dtpStartDate.EditValue = DateTime.Now.ToString("yyyy-MM-dd") + " 22:00";
                        dtpEndDate.EditValue = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 07:00";
                    }

                }
                //循环把人员信息放入panel
                panelContent.Controls.Clear();
                isLeftInto = true;//是否考勤区域处理


                //关位
                string strGuwanweiID = string.Empty;

                var gwcount = m_tblGuanweiList.Rows.Count;
                for (int a = 0; a < gwcount; a++)
                {
                    strGuwanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();

                    //当前关位下的人员信息,关位下没有人员，不展示关位(工种）信息
                    DataView view = new DataView(m_tblDataList.Copy());
                    view.RowFilter = "GuanweiID='" + strGuwanweiID + "'";

                    DataTable dt_temp = view.ToTable();

                    //添加关位(工种）
                    m_PersonList = new UserPersonsList();
                    m_PersonList.TitleGuanwei = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();//关位名称
                    m_PersonList.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                    bpCnt = int.Parse(m_tblGuanweiList.Rows[a]["SetNum"].ToString());

                    m_PersonList.StandardCount = bpCnt;//关位标配人数
                    m_PersonList.RealCount = int.Parse(m_tblGuanweiList.Rows[a]["realityCount"].ToString());//关位实配人数
                    m_PersonList.AllEventClick += new UserPersonsList.AllEvent(m_PersonList_AllEventClick);
                    this.setImgIndex(true);//设置显示位置
                    panelContent.Controls.Add(m_PersonList);//展示空间
                    m_PersonList.Location = _point;//设置空间展示坐标

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
                            m_Person = new UserPerson();
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
                            m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                            this.setImgIndex(false);//设置显示位置
                            panelContent.Controls.Add(m_Person);//展示空间
                            m_Person.Location = _point;//设置空间展示坐标
                            gwShowSite = GwSite + 1;
                            dt_temp.Rows[0].Delete();
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
                            m_Person = new UserPerson();

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
                            m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_AllEventClick);
                            m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, dt_temp.Rows[0]["UserID"].ToString());
                            m_Person.DoubleClick += new UserPerson.DoubleEvent(m_Person_DoubleClick);
                            this.setImgIndex(false);//设置显示位置
                            panelContent.Controls.Add(m_Person);//展示空间
                            m_Person.Location = _point;//设置空间展示坐标
                            gwShowSite++;
                            dt_temp.Rows[0].Delete();
                        }
                        else if (perFlg)
                        {
                            //位置不同，显示空关位
                            //人员信息
                            m_PersonNull = new UserPersonNull();
                            m_PersonNull.TitleName = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString() + " - " + gwShowSite;
                            m_PersonNull.GuanweiID = m_tblGuanweiList.Rows[a]["GuanweiID"].ToString();
                            m_PersonNull.GuanweiNM = m_tblGuanweiList.Rows[a]["GuanweiName"].ToString();
                            m_PersonNull.GuanweiSite = gwShowSite.ToString();

                            m_PersonNull.Tag = "0";
                            m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_PersonNull_AllEventClick);
                            m_PersonNull.ImageUrl = "";
                            this.setImgIndex(false);//设置显示位置
                            panelContent.Controls.Add(m_PersonNull);//展示空间
                            m_PersonNull.Location = _point;//设置空间展示坐标
                            gwShowSite++;

                        }

                    }
                }
                wid_Left_Cnt = 1;
                wid_Right_Cnt = 1;
            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("考勤数据加载失败！" + ex);
                FrmDialog.ShowDialog();
            }
            finally
            {

                Program.logFlagEnd(log, s, "获取考勤人员信息一览");
                this.TopMost = true;
            }
        }
        /// <summary>
        /// 获取支援的人员
        /// </summary>
        public void GetSupportList()
        {
            Stopwatch s = new Stopwatch();
            Program.logFlagStart(log, s, "获取支援的人员");
            try
            {

                //this.TopMost = false;
                panelSupport.Controls.Clear(); //JobForID，ProjectID，LineID，TeamID
                panelSupport.AutoScroll = false;

                string str_sql = string.Empty;
                if (m_tblAllList.Rows.Count == 0)
                {
                    str_sql = string.Format(@"select distinct JobForID,ProjectID,LineID,TeamID from V_Produce_Para where myTeamName='{0}' order by JobForID,ProjectID,LineID,TeamID", lookmyteamName.Text.Trim());
                    m_tblAllList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                    if (m_tblAllList.Rows.Count == 0)
                    {

                        panelSupport.Controls.Clear();

                        return;
                    }
                }

                str_sql = string.Format(@"select distinct  *  from V_Attend_Support  where 1=1 and AttendMemo LIKE '%支援调出%'  ");

                if (txtSupportID.Text.Trim() != "")
                {
                    str_sql += " and UserID like'%" + txtSupportID.Text.Trim() + "%'";
                }
                str_sql += " and AttendDate=convert(varChar(10),'" + dateOperDate1.EditValue.ToString() + "',120)";
                //str_sql += " order by AttendMemo ,t1.UserID ";//2015-11-23

                m_tblSupportList = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                //循环把人员信息放入panel
                Point _point;//控件坐标
                int lint_Left = 0;//左边距
                int lint_Top = 0;//上边距
                int lint_TopCount = 0;//当前Top有几行
                //if (m_tblDataList.Rows.Count == 0) return;//2015-11-23

                for (int i = 0; i < m_tblSupportList.Rows.Count; i++)
                {
                    if (i % 2 == 0 && i != 0)
                    {
                        lint_TopCount++;
                        lint_Left = 0;
                    }
                    //人员信息
                    m_Person = new UserPerson();
                    m_Person.Name = ("person" + (i + 1)).ToString();
                    Color cl = Color.FromName(m_tblSupportList.Rows[i]["warnColor"].ToString());
                    m_Person.TitleName = "(支)-" + m_tblSupportList.Rows[i]["GuanweiNM"].ToString();
                    m_Person.lblTitle.BackColor = cl;
                    if (m_tblSupportList.Rows[i]["AttendMemo"].ToString() == "支援调入")
                    {
                        //已经调入：绿色，不可选择
                        m_Person.GuanweiColor = GreenColor;
                    }
                    else
                    {
                        //已经调出：黄色，不可选择
                        m_Person.GuanweiColor = YellowColor;
                    }

                    m_Person.GuanweiID = m_tblSupportList.Rows[i]["GuanweiID"].ToString();
                    m_Person.GuanweiNM = m_tblSupportList.Rows[i]["GuanweiNM"].ToString();
                    m_Person.Status = m_tblSupportList.Rows[i]["StatusName"].ToString();
                    m_Person.StatusColor = m_tblSupportList.Rows[i]["StatusColor"].ToString();
                    m_Person.Time = m_tblSupportList.Rows[i]["CardTime"].ToString();
                    m_Person.TimeColor = m_tblSupportList.Rows[i]["CardTimeColor"].ToString();
                    m_Person.Remind = m_tblSupportList.Rows[i]["AttendMemo"].ToString().Substring(2, 2);//支援调入，支援调出
                    m_Person.JobForID = m_tblSupportList.Rows[i]["JobForID"].ToString();
                    m_Person.ProjectID = m_tblSupportList.Rows[i]["ProjectID"].ToString();
                    m_Person.LineID = m_tblSupportList.Rows[i]["LineID"].ToString();

                    m_Person.RemindColor = m_tblSupportList.Rows[i]["warnColor"].ToString();
                    m_Person.LicenseType = m_tblSupportList.Rows[i]["LicenseType"].ToString();
                    m_Person.LicenseColor = m_tblSupportList.Rows[i]["LicenseColor"].ToString();

                    m_Person.UserID = m_tblSupportList.Rows[i]["UserID"].ToString();
                    m_Person.UserName = m_tblSupportList.Rows[i]["UserNM"].ToString();

                    m_Person.Tag = "0";


                    m_Person.UserIdNmColor = GreenColor;
                    m_Person.Tag = "0";//选中为1，未选中为0
                    m_Person.AllEventClick += new UserPerson.AllEvent(m_Person_SupportAllEventClick);
                    m_Person.ImageUrl = GridCommon.GetUserImage(SysParam.m_daoCommon, m_tblSupportList.Rows[i]["UserID"].ToString());

                    panelSupport.Controls.Add(m_Person);
                    lint_Top = lint_TopCount * m_Person.Height;
                    _point = new Point(lint_Left + 10, lint_Top + 10);
                    lint_Left = m_Person.Width;
                    m_Person.Location = _point;
                    m_Person.Visible = true;

                }

                if (m_tblSupportList.Rows.Count % 2 == 0 && m_tblSupportList.Rows.Count != 0)
                {
                    lint_TopCount++;
                    lint_Left = 0;
                }
                //人员信息
                m_PersonNull = new UserPersonNull();
                m_PersonNull.Name = ("person" + m_tblSupportList.Rows.Count + 1).ToString();
                m_PersonNull.TitleName = "支援调出";
                m_PersonNull.Tag = "0";//选中为1，未选中为0
                m_PersonNull.GuanweiColor = YellowColor;
                m_PersonNull.UserIdNmColor = GreenColor;
                m_PersonNull.AllEventClick += new UserPersonNull.AllEvent(m_SupperNull_AllEventClick);
                m_PersonNull.ImageUrl = "";

                panelSupport.Controls.Add(m_PersonNull);
                lint_Top = lint_TopCount * m_PersonNull.Height;
                _point = new Point(lint_Left + 10, lint_Top + 5);
                m_PersonNull.Location = _point;


            }
            catch (Exception ex)
            {

                FrmAttendDialog FrmDialog = new FrmAttendDialog("支援数据检索失败！" + ex);
                FrmDialog.ShowDialog();
                log.Error(ex);

            }
            finally
            {

                panelSupport.AutoScroll = true;

                Program.logFlagEnd(log, s, "获取支援的人员");
                this.TopMost = true;

            }
        }


        /// <summary>
        /// 用户关位，布局
        /// </summary>
        /// bGWLsFlag:关位flg，true：显示工种，false：显示关位
        private void setImgIndex(Boolean bGWLsFlag)
        {
            //this.TopMost = false;
            //工种显示场合
            if (bGWLsFlag)
            {
                //左侧展示关位（工种）
                if (wid_Right_Cnt > 0 && wid_Right_Cnt > 0)
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

        /// <summary>
        /// 选择判定,并设置调动状态，调动From，To数据 
        /// </summary>
        /// <param name="selectFlg">1：【考勤】工种；  2：【考勤】（标配）人员；  3：【考勤】（标配以外）人员； 4：【考勤】（空位）人员
        /// 5：【未定义】人员； 6：【未定义】（空位）人员；  7：【支援】人员； 8：【支援】（空位）人员</param>
        /// <param name="ObjSender">工种,人员，空位</param>
        /// <summary>
        private void checkSelectPersons(int selectFlg, object ObjSender)
        {
            //this.TopMost = false;
            if (strFromSelFlg == 0) //from没有选择的情况
            {
                //From选择类型保存
                strFromSelFlg = selectFlg;
                switch (strFromSelFlg)
                {//根据from选择类型，设定To的那些可选

                    case 2://From: 2：【考勤】（标配）人员
                        bPlistFlg = true;//【考勤】工种可选flg
                        bPerFlg = true;//【考勤】关位可选flg
                        bPerNullFlg = true;//【考勤】空位可选flg
                        bProFlg = false;//【未定义】关位可选flg
                        bProNullFlg = true;//【未定义】空位可选flg
                        bSupFlg = false;//【支援】关位可选flg
                        bSupNullFlg = true;//【支援】空位可选flg
                        break;
                    case 5://From: 5：【未定义】人员
                        bPlistFlg = true;//【考勤】工种可选flg
                        bPerFlg = true;//【考勤】关位可选flg
                        bPerNullFlg = true;//【考勤】空位可选flg
                        bProFlg = false;//【未定义】关位可选flg
                        bProNullFlg = false;//【未定义】空位可选flg
                        bSupFlg = false;//【支援】关位可选flg
                        bSupNullFlg = false;//【支援】空位可选flg
                        break;

                    case 7://From: 7：【支援】人员
                        bPlistFlg = true;//【考勤】工种可选flg
                        bPerFlg = true;//【考勤】标配关位可选flg
                        bPerNullFlg = true;//【考勤】空位可选flg
                        bProFlg = false;//【未定义】关位可选flg
                        bProNullFlg = false;//【未定义】空位可选flg
                        bSupFlg = false;//【支援】关位可选flg
                        bSupNullFlg = false;//【支援】空位可选flg
                        break;

                    default: break;
                }
                m_Person = ObjSender as UserPerson;
                //组长不可以调出
                if (m_Person.GuanweiNM == "组长")
                {
                    return;
                }
                m_Person.BackColor = Color.Red;
                m_Person.Tag = 1;
                txtFrom.Text = m_Person.TitleName + ":" + m_Person.UserID + "," + m_Person.UserName;
                moveFromPerson = m_Person;
                txtTo.Text = "";

            }
            //from已经选择的情况，根据From，对To处理
            else
            {
                strToSelFlg = selectFlg;
                if (strFromSelFlg == 2)//From:【考勤】标配人员
                {
                    switch (strToSelFlg)
                    {//根据from选择类型，设定To的那些可选
                        case 1://From:【考勤】标配人员-->  To选择是： 1：【考勤】工种
                            m_PersonList = ObjSender as UserPersonsList;
                            //组长、班长、副班长、替关者不可以调入
                            if (m_PersonList.TitleGuanwei == "组长" || m_PersonList.TitleGuanwei == "班长"
                                || m_PersonList.TitleGuanwei == "副班长" || m_PersonList.TitleGuanwei == "替关者")
                            {
                                return;
                            }

                            m_PersonList.BackColor = Color.Red;
                            m_PersonList.Tag = 1;
                            moveToPersonList = m_PersonList;
                            txtTo.Text = moveToPersonList.TitleGuanwei;
                            moveToPersonFlg = "1";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            if (moveFromPerson.TitleName.Substring(1, 1) == "支")
                            {
                                //支援者
                                pflag = "5";//人员调动类型:支援调入
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else if (moveFromPerson.TitleName.Substring(1, 1) == "替"
                                || moveFromPerson.TitleName.Substring(0, 3) == "替关者")
                            {
                                //替关者
                                pflag = "6";//人员调动类型:替关调动
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else//非替关者
                            {
                                pflag = "3";//人员调动类型:关位调整
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = false;//结束日期

                            }
                            break;
                        case 2://From:【考勤】标配人员-->  To选择是：2：【考勤】标配人员
                            UserPerson m_Person = ObjSender as UserPerson;
                            if (m_Person.BackColor == Color.Red
                                || m_Person.GuanweiNM == "组长" || m_Person.GuanweiNM == "班长"
                                || m_Person.GuanweiNM == "副班长" || m_Person.GuanweiNM == "替关者")
                            {//选择自己的场合,组长\副班长\班长\替关者 选择的场合，不可选择
                                return;
                            }

                            m_Person.BackColor = Color.Red;
                            m_Person.Tag = 1;
                            moveToPerson = m_Person;
                            txtTo.Text = m_Person.TitleName + ":" + m_Person.UserID + "," + m_Person.UserName;
                            moveToPersonFlg = "2";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            if (moveFromPerson.TitleName.Substring(1, 1) == "替"
                                || moveFromPerson.TitleName.Substring(0, 3) == "替关者")
                            {
                                pflag = "6";//人员调动类型:替关调动
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else if (moveFromPerson.TitleName.Substring(1, 1) == "支")
                            {
                                pflag = "5";//人员调动类型:支援调入
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else//非替关者
                            {
                                pflag = "3";//人员调动类型:关位调整
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = false;//结束日期
                            }

                            break;
                        case 4://From:【考勤】标配人员-->  To选择是：4：【考勤】（空位）人员

                            //设置调入数据
                            m_PersonNull = ObjSender as UserPersonNull;
                            //组长、班长、副班长、替关者不可以调入(===============================================================2015-12-04 都可以调入)
                            //if (m_PersonNull.TitleName.Substring(0,2) == "组长" || m_PersonNull.TitleName.Substring(0,2) == "班长"
                            //    || m_PersonNull.TitleName.Substring(0,2) == "副班" || m_PersonNull.TitleName.Substring(0,2) == "替关")
                            //{
                            //    return;
                            //}
                            m_PersonNull.BackColor = Color.Red;
                            m_PersonNull.Tag = 1;
                            moveToPersonNull = m_PersonNull;
                            txtTo.Text = moveToPersonNull.TitleName;
                            moveToPersonFlg = "3";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            if (moveFromPerson.TitleName.Substring(1, 1) == "替"
                               || moveFromPerson.TitleName.Substring(0, 3) == "替关者" || moveFromPerson.TitleName.Substring(0, 2) == "班长")
                            {
                                pflag = "6";//人员调动类型:替关调动
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else if (moveFromPerson.TitleName.Substring(1, 1) == "支")
                            {
                                pflag = "5";//人员调动类型:支援调入
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = true;//结束日期
                            }
                            else//非替关者
                            {
                                pflag = "3";//人员调动类型:关位调整
                                dtpStartDate.Enabled = true;//开始日期
                                dtpEndDate.Enabled = false;//结束日期
                            }


                            break;

                        case 6://From:【考勤】标配人员-->  To选择是：6：【未定义】（空位）人员
                            m_PersonNull = ObjSender as UserPersonNull;
                            //设置调入数据
                            if (moveFromPerson.TitleName.Substring(0, 2) == "班长" && !Common._personid.Equals(Common._Administrator))
                            {
                                return;
                            }
                            m_PersonNull.BackColor = Color.Red;
                            m_PersonNull.Tag = 1;
                            moveToPersonNull = m_PersonNull;
                            txtTo.Text = moveToPersonNull.TitleName;
                            moveToPersonFlg = "3";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "2";//人员调动类型:人员调出
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = false;//结束日期

                            break;
                        case 8://From:【考勤】标配人员-->  To选择是：8：【支援】（空位）人员

                            //设置调入数据
                            m_PersonNull = ObjSender as UserPersonNull;
                            m_PersonNull.BackColor = Color.Red;
                            m_PersonNull.Tag = 1;
                            moveToPersonNull = m_PersonNull;
                            txtTo.Text = moveToPersonNull.TitleName;
                            moveToPersonFlg = "3";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "4";//人员调动类型:支援调出
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = true;//结束日期

                            break;
                        default: break;
                    }
                }

                //From:【未定义】关位
                else if (strFromSelFlg == 5)
                {
                    switch (strToSelFlg)
                    {//根据from选择类型，设定To的那些可选
                        case 1://From:【未定义】关位--> TO选择是：1：【考勤】工种
                            m_PersonList = ObjSender as UserPersonsList;
                            //组长、班长、副班长、替关者不可以调入
                            if (m_PersonList.TitleGuanwei == "组长" || m_PersonList.TitleGuanwei == "班长"
                                || m_PersonList.TitleGuanwei == "副班长" || m_PersonList.TitleGuanwei == "替关者")
                            {
                                return;
                            }
                            m_PersonList.BackColor = Color.Red;
                            m_PersonList.Tag = 1;
                            moveToPersonList = m_PersonList;
                            txtTo.Text = moveToPersonList.TitleGuanwei;
                            moveToPersonFlg = "1";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "1";//人员调动类型:人员调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = false;//结束日期

                            break;
                        case 2://From:【未定义】关位--> TO选择是：2：【考勤】（标配）人员
                            m_Person = ObjSender as UserPerson;

                            if (m_Person.BackColor == Color.Red
                                || m_Person.GuanweiNM.Substring(0, 2) == "组长" || m_Person.GuanweiNM.Substring(0, 2) == "班长"
                                || m_Person.GuanweiNM.Substring(0, 2) == "副班" || m_Person.GuanweiNM.Substring(0, 2) == "替关")
                            {//选择自己的场合,组长\副班长\班长\替关者 选择的场合，不可选择
                                return;
                            }
                            m_Person.BackColor = Color.Red;
                            m_Person.Tag = 1;
                            moveToPerson = m_Person;
                            txtTo.Text = m_Person.TitleName + ":" + m_Person.UserID + "," + m_Person.UserName;
                            moveToPersonFlg = "2";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "1";//人员调动类型:人员调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = false;//结束日期


                            break;
                        case 4://From:【未定义】关位--> TO选择是：4：【考勤】（空位）人员

                            //设置调入数据
                            m_PersonNull = ObjSender as UserPersonNull;
                            if (m_PersonNull.TitleName.Substring(0, 2) == "组长" || m_PersonNull.TitleName.Substring(0, 2) == "班长"
                               || m_PersonNull.TitleName.Substring(0, 2) == "副班" || m_PersonNull.TitleName.Substring(0, 2) == "替关")
                            {
                                return;
                            }
                            m_PersonNull.BackColor = Color.Red;
                            m_PersonNull.Tag = 1;
                            moveToPersonNull = m_PersonNull;
                            txtTo.Text = moveToPersonNull.TitleName;
                            moveToPersonFlg = "3";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "1";//人员调动类型:人员调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = false;//结束日期

                            break;
                        default: break;
                    }
                }

                //From:【支援】关位
                else if (strFromSelFlg == 7)
                {
                    //开始时间，结束时间取得
                    string str_sql = string.Format(@"select distinct StrDate,EndDate  from V_Attend_Move_i  
                                                    where 1=1 and '{0}' between StrDate and EndDate
                                                    and MoveStatus='支援调出' and UserID='{1}'   ",
                                                    dateOperDate1.EditValue.ToString(), moveFromPerson.UserID);
                    DataTable m_tbl = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

                    switch (strToSelFlg)
                    {//根据from选择类型，设定To的那些可选
                        case 1://From:【支援】关位--> TO选择是：1：【考勤】工种
                            m_PersonList = ObjSender as UserPersonsList;
                            //组长、班长、副班长、替关者不可以调入
                            if (m_PersonList.TitleGuanwei == "组长" || m_PersonList.TitleGuanwei == "班长"
                                || m_PersonList.TitleGuanwei == "副班长" || m_PersonList.TitleGuanwei == "替关者")
                            {
                                return;
                            }
                            m_PersonList.BackColor = Color.Red;
                            m_PersonList.Tag = 1;
                            moveToPersonList = m_PersonList;
                            txtTo.Text = moveToPersonList.TitleGuanwei;
                            moveToPersonFlg = "1";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "5";//人员调动类型:支援调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = true;//结束日期
                            if (m_tbl.Rows.Count > 0)
                            {
                                dtpStartDate.EditValue = m_tbl.Rows[0]["StrDate"].ToString();//开始日期
                                dtpEndDate.EditValue = m_tbl.Rows[0]["EndDate"].ToString();//结束日期
                            }
                            break;
                        case 2://From:【支援】关位--> TO选择是：2：【考勤】（标配）人员
                            m_Person = ObjSender as UserPerson;
                            if (m_Person.BackColor == Color.Red
                               || m_Person.GuanweiNM == "组长" || m_Person.GuanweiNM == "班长"
                               || m_Person.GuanweiNM == "副班长" || m_Person.GuanweiNM == "替关者")
                            {//选择自己的场合,组长\副班长\班长\替关者 选择的场合，不可选择
                                return;
                            }
                            m_Person.BackColor = Color.Red;
                            m_Person.Tag = 1;
                            moveToPerson = m_Person;
                            txtTo.Text = m_Person.TitleName + ":" + m_Person.UserID + "," + m_Person.UserName;
                            moveToPersonFlg = "2";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull
                            pflag = "5";//人员调动类型:支援调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = true;//结束日期
                            if (m_tbl.Rows.Count > 0)
                            {
                                dtpStartDate.EditValue = m_tbl.Rows[0]["StrDate"].ToString();//开始日期
                                dtpEndDate.EditValue = m_tbl.Rows[0]["EndDate"].ToString();//结束日期
                            }
                            break;
                        case 4://From:【支援】关位--> TO选择是：4：【考勤】（空位）人员

                            //设置调入数据
                            m_PersonNull = ObjSender as UserPersonNull;
                            if (m_PersonNull.TitleName.Substring(0, 2) == "组长" || m_PersonNull.TitleName.Substring(0, 2) == "班长"
                               || m_PersonNull.TitleName.Substring(0, 2) == "副班" || m_PersonNull.TitleName.Substring(0, 2) == "替关")
                            {
                                return;
                            }
                            m_PersonNull.BackColor = Color.Red;
                            m_PersonNull.Tag = 1;
                            moveToPersonNull = m_PersonNull;
                            txtTo.Text = moveToPersonNull.TitleName;
                            moveToPersonFlg = "3";//1:moveToPersonList; 2:moveToPerson;3:moveToPersonNull

                            pflag = "5";//人员调动类型:支援调入
                            dtpStartDate.Enabled = true;//开始日期
                            dtpEndDate.Enabled = true;//结束日期
                            if (m_tbl.Rows.Count > 0)
                            {
                                dtpStartDate.EditValue = m_tbl.Rows[0]["StrDate"].ToString();//开始日期
                                dtpEndDate.EditValue = m_tbl.Rows[0]["EndDate"].ToString();//结束日期
                            }
                            break;

                        default: break;
                    }

                }
                bPlistFlg = false;//关位list
                bPerFlg = false;
                bPerNullFlg = false;
                bProFlg = false;//未定义
                bProNullFlg = false;//未定义
                bSupFlg = false;//支援
                bSupNullFlg = false;//支援
            }
            //this.TopMost = false;

        }


        #endregion

        private void txtProductUserID_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }




    }
}
